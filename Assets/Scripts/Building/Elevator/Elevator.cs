using System.Collections;
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
        // List of observers subscribed to elevator events
        private List<IObserver> _observers { get; set; }
        // Duration of elevator movement between floors
        private float moveDuration = 0.1f;
        
        // Reference to the Boot
        public Boot _boot;
        
        // Maximum capacity of the elevator
        public int Capacity { get; set; }
        
        // Current number of people inside the elevator
        private int _filling;

        // Property to get or set the current filling of the elevator
        public int Filling
        {
            get => _filling;
            set => _filling = Mathf.Clamp(value, 0, Capacity);
        }
        
        // Current floor of the elevator
        public int CurrentFloor { get; set; }
        // Flag indicating if the elevator is currently moving
        public bool Moving;

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

            // Calculate target position based on the target floor
            if (CurrentFloor < floorNumber && floorNumber <= _boot.GetNumberOfFloors())
            {
                var floorDifference = floorNumber - CurrentFloor;
                targetPosition = initialPosition + new Vector3(0f, -6f * floorDifference, 0f);
                CurrentFloor += floorDifference;
            }
            else if (CurrentFloor > floorNumber && floorNumber >= 1)
            {
                var floorDifference = CurrentFloor - floorNumber;
                targetPosition = initialPosition + new Vector3(0f, 6f * floorDifference, 0f);
                CurrentFloor -= floorDifference;
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

            // Move the elevator smoothly using Lerp
            while (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;
                float easeValue = animCurve.Evaluate(t);
                stage.transform.position = Vector3.LerpUnclamped(initialPosition, targetPosition, easeValue);
                elapsedTime += Time.deltaTime;
                yield return new WaitForSeconds(waitTime);
            }

            // Set the stage position to the target position
            stage.transform.position = targetPosition;
            // Set moving flag to false
            Moving = false;

            // Notify observers about elevator status change
            Notify();
        }       
    }
}
