using System;
using System.Collections;
using System.Collections.Generic;
using Elevator.Interfaces;
using Elevator.Interfaces.EventBus;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Elevator
{
    public class ElevatorGO : MonoBehaviour, IObserver
    {
        private Elevator _elevator;

        [SerializeField]private GameManager _gameManager;
        
        public bool Moving
        {
            get { return _elevator.IsMoving; }
            set
            {
                if (_elevator != null)
                {
                    _elevator.IsMoving = value;

                    EventManager.Instance.PostNotification(EVENT_TYPE.ELEVATOR_IS_MOVING, this, value);
                }
            }
        }
        
        //private Boot _boot;                                  // Reference to Boot
        private const float ElevatorMovementCoefficient = 1f; // Elevator movement coefficient

        private List<PersonGO> _passengersInsideElevator = new List<PersonGO>();
        private List<PersonGO> _unloadPassengersNow = new List<PersonGO>();

        [Inject]
        public void Construct(IElevator elevator, GameManager gameManager)
        {
            // Debug.Log($"!!!!!!!!!!!!!!!");
            _elevator = elevator as Elevator;
            _elevator?.Attach(this);
            _gameManager = gameManager;
        }
        
        public void AddPassengerToElevator(PersonGO passenger)
        {
            _passengersInsideElevator.Add(passenger);
        }

        private void PassengersToUnloadNow()
        {
            _unloadPassengersNow.Clear();
            for (var index = 0; index < _passengersInsideElevator.Count; index++)
            {
                var passenger = _passengersInsideElevator[index];
                if (passenger != null && passenger.personTargetFloor == _elevator.CurrentFloor)
                {
                    _unloadPassengersNow.Add(passenger);
                    Debug.Log($"Passenger {passenger.personTargetFloor} has arrived");
                }
            }
        }

        private void UnloadPassengers()
        {
            for (var person = 0; person < _unloadPassengersNow.Count; person++)
            {
                var passenger = _unloadPassengersNow[person];
                if (passenger)
                {
                    ExitElevator();
                    Debug.Log($"Passenger {passenger.personTargetFloor} has been unloaded");
                    _passengersInsideElevator.Remove(passenger);
                    _gameManager = FindObjectOfType<GameManager>();
                    
                    if (_gameManager == null)
                    {
                        Debug.Log("Game Manager is Null");
                        //break;
                    }
                    _gameManager?.PassengersCounter();
                }
            }

            _unloadPassengersNow.Clear();
        }

        
        private void ExitElevator()
        {
            for (var index = 0; index < _unloadPassengersNow.Count; index++)
            {
                var person = _unloadPassengersNow[index];
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
        /// <param name="elevator">elevator link</param>
        public IEnumerator ElevatorMove(int floorNumber, GameObject stage, AnimationCurve animCurve, IElevator elevator)
        {
            _elevator = elevator as Elevator;
            if (_elevator == null)
            {
                Debug.LogError("_elevator is null");
                yield break;
            }

            // Set moving flag to true
            Moving = true;
            // Get initial position of the stage
            Vector3 initialPosition = stage.transform.position;
            Vector3 targetPosition;
            int startingFloorNumber = _elevator.CurrentFloor; // Save the starting floor number
            Debug.Log($"current floor {_elevator.CurrentFloor}");

            // Calculate target position based on the target floor
            
            if (_elevator.CurrentFloor < floorNumber && floorNumber <= MainMenu.NumberOfFloors)
            {
                var floorDifference = floorNumber - _elevator.CurrentFloor;   // Difference between the target floor and the current floor
                targetPosition = initialPosition + new Vector3(0f, -6f * floorDifference, 0f);
                _elevator.CurrentFloor += floorDifference;
            }
            else if (_elevator.CurrentFloor > floorNumber && floorNumber >= 1)
            {
                var floorDifference = _elevator.CurrentFloor - floorNumber;
                targetPosition = initialPosition + new Vector3(0f, 6f * floorDifference, 0f);
                _elevator.CurrentFloor -= floorDifference;
            }
            else if ((_elevator.CurrentFloor == 1 && floorNumber < 1) || (_elevator.CurrentFloor == MainMenu.NumberOfFloors && floorNumber > MainMenu.NumberOfFloors))
            {
                targetPosition = initialPosition + new Vector3(0f, 0f, 0f);
            }
            
            else
            {
                // Elevator cannot move to the specified floor
                targetPosition = initialPosition;
                Debug.Log($"{_elevator.CurrentFloor}== {floorNumber}");
                Debug.Log($"HA HA no way there");
            }

            // Initialize variables for Lerping
            float elapsedTime = 0f;
            float waitTime = 0.05f;
            float floorDistance = Mathf.Abs(startingFloorNumber - floorNumber); // Distance between the target floor and the current floor 
            //Debug.Log($"FloorDistance {startingFloorNumber} - {floorNumber} = {floorDistance}");
            float moveDuration = floorDistance * ElevatorMovementCoefficient;
            //Debug.Log($"MoveDuration {moveDuration} coef {ElevatorMovementCoefficient}");

            // Move the elevator smoothly using Lerp
            while (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;
                float easeValue = animCurve.Evaluate(t);
                stage.transform.position = Vector3.LerpUnclamped(initialPosition, targetPosition, easeValue);
                elapsedTime += Time.deltaTime;
                _gameManager?.MoveCounter();
                yield return null;
            }

            // Set the stage position to the target position
            stage.transform.position = targetPosition;
            
            PassengersToUnloadNow();
            UnloadPassengers();
            
            // Set moving flag to false
            Moving = false;

            // Notify observers about elevator status change
            _elevator.Notify();
        } 

        // private void Awake()
        // {
        //     _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        //     if (!_gameManager)
        //     {
        //         Debug.LogError("GM is Null");
        //     }
        // }

        private void Update()
        {
           
        }

        

        

        public void UpdateElevatorStatus(IObserverble observerble)
        {
            
        }
    }
}