﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elevator.Interfaces;

namespace Elevator
{
    /// <summary>
    /// Represents the elevator in the building.
    /// </summary>
    public class Elevator : IElevator, IObserverble
    {
        public int CurrentFloor { get; set; }               // Current floor of the elevator
        public bool Moving;                                 // Indicates whether the elevator is moving
        public int Capacity { get; set; }                   // Maximum capacity of the elevator
        
        private List<IObserver> _observers { get; set; }    // List of observers subscribed to elevator events
        // private float moveDuration = 0.2f;                  // Duration of elevator movement between floors
        private Boot _boot;                                  // Reference to the Boot
        private const float ElevatorMovementCoefficient = 1f; // Elevator movement coefficient

        private List<PersonGO> _passengersInsideElevator = new List<PersonGO>();
        private List<PersonGO> _unloadPassengersNow = new List<PersonGO>();
        
        private int _filling;                               // Current number of people inside the elevator

        public int Filling                                  // Property to get or set the current filling of the elevator
        {
            get => _filling;
            set => _filling = Mathf.Clamp(value, 0, Capacity);
        }
        

        
        
        /// <summary>
        /// Constructor for the Elevator class.
        /// </summary>
        /// <param name="boot">Reference to the Boot scriptable object.</param>
        /// <param name="elevatorGo">Reference to the ElevatorGO prefab.</param>
        public Elevator(Boot boot, ElevatorGO elevatorGo)
        {
            // Initialize elevator properties
            Capacity = boot.ElevatorCapacity;
            CurrentFloor = 1;
            _observers = new List<IObserver>();
            _boot = boot;
        }

        /// <summary>
        /// Attaches an observer to the elevator.
        /// </summary>
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        /// <summary>
        /// Detaches an observer from the elevator.
        /// </summary>
        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        /// <summary>
        /// Notifies all attached observers about elevator status changes.
        /// </summary>
        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.UpdateElevatorStatus(this);
            }
        }

        public void AddPassengerToElevator(PersonGO passenger)
        {
            _passengersInsideElevator.Add(passenger);
        }

        private void PassengersToUnloadNow()
        {
            _unloadPassengersNow.Clear();
            foreach (var passenger in _passengersInsideElevator)
            {
                if (passenger.personTargetFloor == CurrentFloor)
                {
                    _unloadPassengersNow.Add(passenger);
                    Debug.Log($"Passenger {passenger.personTargetFloor} has arrived");
                }
            }
        }

        private void UnloadPassengers()
        {
            foreach (var passenger in _unloadPassengersNow)
            {
                ExitElevator();
                _passengersInsideElevator.Remove(passenger);
                Debug.Log($"Passenger {passenger.personTargetFloor} has been unloaded");
            }
        }

        private void ExitElevator()
        {
            foreach (PersonGO person in _unloadPassengersNow)
            {
                person.ExitElevator();
            }
        }

        public List<PersonGO> GetPassengersInsideElevator()
        {
            return _passengersInsideElevator;
        }

        /// <summary>
        /// Initiates the movement of the elevator to a target floor.
        /// </summary>
        /// <param name="floorNumber">The target floor number.</param>
        /// <param name="stage">The GameObject representing the stage where the elevator is located.</param>
        /// <param name="animCurve">The animation curve for smooth movement.</param>
        public IEnumerator ElevatorMove(int floorNumber, GameObject stage, AnimationCurve animCurve)
        {
            // Set moving flag to true
            Moving = true;
            // Get initial position of the stage
            Vector3 initialPosition = stage.transform.position;
            Vector3 targetPosition;
            int startingFloorNumber = CurrentFloor; // Save the starting floor number

            // Calculate target position based on the target floor
            if (CurrentFloor < floorNumber && floorNumber <= _boot.NumberOfFloors)
            {
                var floorDifference = floorNumber - CurrentFloor;   // Difference between the target floor and the current floor
                targetPosition = initialPosition + new Vector3(0f, -6f * floorDifference, 0f);
                CurrentFloor += floorDifference;
            }
            else if (CurrentFloor > floorNumber && floorNumber >= 1)
            {
                var floorDifference = CurrentFloor - floorNumber;
                targetPosition = initialPosition + new Vector3(0f, 6f * floorDifference, 0f);
                CurrentFloor -= floorDifference;
            }
            else if ((CurrentFloor == 1 && floorNumber < 1) || (CurrentFloor == _boot.NumberOfFloors && floorNumber > _boot.NumberOfFloors))
            {
                targetPosition = initialPosition + new Vector3(0f, 0.2f, 0f);
            }
            
            else
            {
                // Elevator cannot move to the specified floor
                targetPosition = initialPosition;
                Debug.Log($"{CurrentFloor}== {floorNumber}");
                Debug.Log($"HA HA no way there");
            }

            // Initialize variables for Lerping
            float elapsedTime = 0f;
            float waitTime = 0.05f;
            float floorDistance = Mathf.Abs(startingFloorNumber - floorNumber); // Distance between the target floor and the current floor 
            Debug.Log($"FloorDistance {startingFloorNumber} - {floorNumber} = {floorDistance}");
            float moveDuration = floorDistance * ElevatorMovementCoefficient;
            Debug.Log($"MoveDuration {moveDuration} coef {ElevatorMovementCoefficient}");

            // Move the elevator smoothly using Lerp
            while (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;
                float easeValue = animCurve.Evaluate(t);
                stage.transform.position = Vector3.LerpUnclamped(initialPosition, targetPosition, easeValue);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Set the stage position to the target position
            stage.transform.position = targetPosition;
            
            PassengersToUnloadNow();
            UnloadPassengers();
            
            // Set moving flag to false
            Moving = false;

            // Notify observers about elevator status change
            Notify();
        }       
    }
}
