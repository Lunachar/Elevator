using System;
using System.Collections;
using System.Collections.Generic;
using Elevator.Display;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Elevator
{
    /// <summary>
    /// Manages the UI buttons for controlling the elevator.
    /// </summary>
    public class MenuButtonsUpAndDown : MonoBehaviour
    {
        public Button buttonUp;
        public Button buttonDown;
        public AnimationCurve ElevatorMovementCurve;
        public AnimationCurve ElevatorBoundCurve;

        private Boot _boot;
        private Elevator _elevator;
        private GameObject _stage;
        private bool _check;
        
        private DiContainer _diContainer;
        private UnityDisplay _unityDisplay;

        private PersonGO _personGo;
        private List<PersonGO> _people;
        
        [Inject]
        public void Initialize(DiContainer diContainer, PersonGO personGo, UnityDisplay unityDisplay)
        {
            // Inject dependencies
            _diContainer = diContainer;
            _personGo = personGo;
            // Check if PersonGO is going
            _personGo = personGo;
            //Debug.LogWarning($"{_personGo.isGoing}");
            _unityDisplay = unityDisplay;
        }

        private void Start()
        {
            // Find references and set up button listeners
            _elevator = FindObjectOfType<Boot>().GetElevator() as Elevator;
            _stage = GameObject.Find("STAGE");
            
            Debug.LogError($"BOOL: {AnyPersonOnFloorMoving(_elevator.CurrentFloor)}");
            
            buttonUp.onClick.AddListener(MoveElevatorUp);
            buttonDown.onClick.AddListener(MoveElevatorDown);

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveElevatorUp();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveElevatorDown();
            }
        }

        private void MoveElevatorUp()
        {
            MoveToTargetFloor(_elevator.CurrentFloor + 1);
        }

        private void MoveElevatorDown()
        {
            MoveToTargetFloor(_elevator.CurrentFloor - 1);
        }

        private bool AnyPersonOnFloorMoving(int floorNumber)
        {
            PersonGO[] allPersons = GameObject.FindObjectsOfType<PersonGO>();
            foreach (PersonGO person in allPersons)
            {
                if (person.personCurrentFloor == floorNumber && person.isGoing)
                {
                    return true; // At least one PersonGO is moving
                }
            }
            return false; // No PersonGO is moving
        }
        private bool AnyPersonFromElevatorMoving()
        {
            _people = _elevator.GetPassengersInsideElevator();
            
            foreach (PersonGO person in _people)
            {
                if (person.isGoing)
                {
                    return true; // At least one PersonGO is moving
                }
            }
            return false; // No PersonGO is moving
        }
        
        /// <summary>
        /// Moves the elevator to the target floor.
        /// </summary>
        /// <param name="floorNumber">The target floor number.</param>
        public void MoveToTargetFloor(int floorNumber)
        {
            _elevator = FindObjectOfType<Boot>().GetElevator() as Elevator;
            // Check if the elevator is initialized
            if (_elevator != null)
            {
                Debug.LogError($"BOOL: {AnyPersonOnFloorMoving(_elevator.CurrentFloor)}");
                // Check if the person is not moving
                if (!AnyPersonOnFloorMoving(_elevator.CurrentFloor) && !AnyPersonFromElevatorMoving())
                {
                    // Check if the elevator is not moving and the target floor is different
                    if (!_elevator.Moving && _elevator.CurrentFloor != floorNumber)
                    {
                        StartCoroutine(_elevator.ElevatorMove(floorNumber, _stage, ElevatorMovementCurve));
                    }

                    if (!_elevator.Moving && (floorNumber > _boot.GetNumberOfFloors() || floorNumber < 1))
                    {
                        StartCoroutine(_elevator.ElevatorMove(floorNumber, _stage, ElevatorBoundCurve));
                    }
                }
                else
                {
                    // Log a message if the person is currently moving
                    Debug.Log($"Person is moving");
                }
            }
            else
            {
                Debug.Log("Elevator is null in MoveToTargetFloor!");
            }
        }
    }
}
