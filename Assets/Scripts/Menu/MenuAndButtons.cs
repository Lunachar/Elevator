using System;
using System.Collections;
using System.Collections.Generic;
using Elevator;
using Elevator.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Elevator
{
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
            Debug.LogWarning($"INITIALIZE!!!");
            _diContainer = diContainer;
            _personGo = personGo;
            //_check = _personGo.isGoing;
            Debug.LogWarning($"{_personGo.isGoing}");
        }
        void Start()
        {
            _check = GameObject.FindObjectOfType<PersonGO>().isGoing;
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
        
        public void MoveToTargetFloor(int floorNumber)
        {
            if (_elevator != null)
            {
                if (!_check)
                {
                    if (!_elevator.Moving && _elevator.CurrentFloor != floorNumber)
                    {
                        StartCoroutine(_elevator.ElevatorMove(floorNumber, _stage, ElevatorMovementCurve));
                    }
                }
                else
                {
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