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
        public Button buttonUp;                         // The button that moves the elevator up.
        public Button buttonDown;                       // The button that moves the elevator down.
        public AnimationCurve ElevatorMovementCurve;    // The curve for the elevator movement.
        public AnimationCurve ElevatorBoundCurve;       // The curve for the elevator boundary.

        private Boot _boot;                             // Reference to Boot.
        private Elevator _elevator;                     // Reference to Elevator.
        private GameObject _stage;                      // Reference to Stage (all floors in one parent game object).
        //private bool _check;                          // Whether or not the elevator is moving.
        
        private DiContainer _diContainer;               // Reference to the container.
        private UnityDisplay _unityDisplay;             // Reference to the UnityDisplay.

        private PersonGO _personGo;                     // Reference to the PersonGO.
        private List<PersonGO> _people;                 // Reference to the List of PersonGO.
        
        
        private void Start()
        {
            // Find references and set up button listeners
            _boot = FindObjectOfType<Boot>().GetComponent<Boot>();
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
        /// <param name="elevator">The elevator.</param> 
        public void MoveToTargetFloor(int floorNumber)
        {
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
                        Debug.Log($"USUAL CURVE");
                    }

                    if (_elevator == null)
                    {
                        Debug.Log("Elevator is null");
                    }

                    if (_boot == null)
                    {
                        Debug.Log("Boot is null");
                    }
                    if (!_elevator.Moving && (floorNumber > _boot.NumberOfFloors() || floorNumber < 1))
                    {
                        StartCoroutine(_elevator.ElevatorMove(floorNumber, _stage, ElevatorBoundCurve));
                        Debug.Log($"BOUNCE CURVE");
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
