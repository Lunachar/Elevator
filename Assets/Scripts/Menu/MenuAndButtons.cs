using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Elevator
{
    /// <summary>
    /// Manages the UI buttons for controlling the elevator.
    /// </summary>
    public class MenuAndButtons : MonoBehaviour
    {
        public Button buttonUp;
        public Button buttonDown;
        public AnimationCurve ElevatorMovementCurve;

        private Boot _boot;
        private Elevator _elevator;
        private GameObject _stage;
        private bool _check;
        
        private DiContainer _diContainer;

        private PersonGO _personGo;
        
        [Inject]
        public void Initialize(DiContainer diContainer, PersonGO personGo)
        {
            // Inject dependencies
            _diContainer = diContainer;
            _personGo = personGo;
            // Check if PersonGO is going
            _personGo = personGo;
            Debug.LogWarning($"{_personGo.isGoing}");
        }

        private void Start()
        {
            // Find references and set up button listeners
            _elevator = FindObjectOfType<Boot>().GetElevator() as Elevator;
            _stage = GameObject.Find("STAGE");
            if (_elevator != null)
            {
                Debug.Log($"IS ELEVATOR: {_elevator.Capacity}");
            }

            buttonUp.onClick.AddListener(MoveElevatorUp);
            buttonDown.onClick.AddListener(MoveElevatorDown);
        }

        private void MoveElevatorUp()
        {
            MoveToTargetFloor(_elevator.CurrentFloor + 1);
        }

        private void MoveElevatorDown()
        {
            MoveToTargetFloor(_elevator.CurrentFloor - 1);
        }
        
        /// <summary>
        /// Moves the elevator to the target floor.
        /// </summary>
        /// <param name="floorNumber">The target floor number.</param>
        public void MoveToTargetFloor(int floorNumber)
        {
            // Check if the elevator is initialized
            if (_elevator != null)
            {
                // Check if the person is not moving
                if (!_personGo.isGoing)
                {
                    // Check if the elevator is not moving and the target floor is different
                    if (!_elevator.Moving && _elevator.CurrentFloor != floorNumber)
                    {
                        StartCoroutine(_elevator.ElevatorMove(floorNumber, _stage, ElevatorMovementCurve));
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
