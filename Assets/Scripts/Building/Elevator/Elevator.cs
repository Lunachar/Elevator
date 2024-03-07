using System;
using System.Collections;
using System.Collections.Generic;
using Elevator;
using Elevator.Interfaces;
using UnityEngine;

namespace Elevator
{
    public class Elevator : IElevator, IObserverble
    {
        private List<IObserver> _observers { get; set; }
        private float moveDuration = 0.1f;
        
        
        public Boot _boot;
        public int Capacity { get; set; }
        private int _filling;

        public int Filling
        {
            get => _filling;
            set => _filling = Mathf.Clamp(value, 0, Capacity);
        }
        public int CurrentFloor { get; set; }
        public bool Moving;
        
        

        public Elevator(Boot boot, ElevatorGO elevatorGo)
        {
            Capacity = boot.ElevatorCapacity;
            CurrentFloor = 1;
            _observers = new List<IObserver>();
            _boot = boot;
        }


        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.UpdateElevatorStatus(this);
            }
        }
        
        
        
        public IEnumerator ElevatorMove(int floorNumber, GameObject stage, AnimationCurve animCurve)
        {
            Moving = true;
            Vector3 initialPosition = stage.transform.position;
            Vector3 targetPosition;
            Debug.Log($"{CurrentFloor}== {floorNumber}");
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
                targetPosition = initialPosition;
                Debug.Log($"{CurrentFloor}== {floorNumber}");
                Debug.Log($"HA HA no way there");
            }
            

            float elapsedTime = 0f;
            float waitTime = 0.05f;

            while (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;
                float easeValue = animCurve.Evaluate(t);
                stage.transform.position = Vector3.LerpUnclamped(initialPosition, targetPosition, easeValue);
                elapsedTime += Time.deltaTime;
                yield return new WaitForSeconds(waitTime);
            }

            stage.transform.position = targetPosition;
            Moving = false;

            Notify();
        }

        
    }
}