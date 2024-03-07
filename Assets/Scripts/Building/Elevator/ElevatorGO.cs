using System;
using System.Collections;
using Elevator.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Elevator
{
    public class ElevatorGO : MonoBehaviour, IObserver
    {
        private Elevator _elevator;

        [Inject]
        public void Construct(IElevator elevator)
        {
            Debug.Log($"!!!!!!!!!!!!!!!");
            _elevator = elevator as Elevator;
            _elevator?.Attach(this);
        }

        private void Start()
        {
        }

        private void Update()
        {
           
        }

        

        

        public void UpdateElevatorStatus(IObserverble observerble)
        {
            
        }
    }
}