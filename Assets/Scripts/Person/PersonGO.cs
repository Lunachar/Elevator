using System;
using System.Collections;
using Elevator.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Elevator
{
    public class PersonGO : MonoBehaviour, IObserver
    {
        public AnimationCurve personMovementCurve;
        public float moveDuration = 1f;
        public TMP_Text text;
        public int personCurrentFloor;
        public bool isGoing;

        private Person _person;
        private Elevator _elevator;

        [Inject]
        public void Construct(IElevator elevator, Person person)
        {
            _elevator = elevator as Elevator;
            _person = person;
        }


        private void Start()
        {
            _elevator.Attach(this);
            Debug.Log($"PCF:::::{_person.CurrentFloor}");
            Debug.Log($"ECF:::::{_elevator.CurrentFloor}");
        }

        public IEnumerator MoveToElevator()
        {
            isGoing = true;
            float elapsedTime = 0f;
            Vector3 initialPosition = gameObject.transform.position;
            Vector3 targetPosition = initialPosition + new Vector3(-5f, 0f, 0f);
            while (elapsedTime < moveDuration)
            {
                Debug.Log($"in move_________________");
                float t = elapsedTime / moveDuration;
                var easeValue = personMovementCurve.Evaluate(t);
                gameObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, easeValue);
                elapsedTime += Time.deltaTime;
                yield return new WaitForSeconds(0.1f);
            }

            Debug.Log($"Person in Elevator");
            isGoing = false;
            _person.Completed = true;
        }

        public void SetCurrentFloor(int cfloor)
        {
            personCurrentFloor = cfloor;
        }

        public bool GetIsGoing()
        {
            return isGoing;
        }

        public void UpdateElevatorStatus(IObserverble observerble)
        {
            if (personCurrentFloor == _elevator.CurrentFloor)
            {
                StartCoroutine(MoveToElevator());
            }
        }
    }
}