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
        public AnimationCurve personMovementCurve;      // AnimationCurve for person movement
        public float moveDuration = 1f;                 // How long it takes to move
        public TMP_Text text;                           // Text to display person name
        public int personCurrentFloor;                  // Current floor of person
        public int personTargetFloor;                   // Target floor of person
        public bool isGoing;                            // Whether person is going

        private Person _person;                         // Person script
        private Elevator _elevator;                     // Elevator script
        private Transform _transformOfElevator;         // Elevator transform
        private bool _isInElevator;                     // Whether person is in elevator
        private FloorGO _floorGO;                       // Floor Game Object

        [Inject]
        public void Construct(IElevator elevator, Person person)
        {
            _elevator = elevator as Elevator;
            _person = person;
        }


        private void Start()
        {
            _elevator.Attach(this);
            _transformOfElevator = GameObject.Find("Elevator(Clone)").transform;
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
                float t = elapsedTime / moveDuration;
                var easeValue = personMovementCurve.Evaluate(t);
                gameObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, easeValue);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            gameObject.transform.position = targetPosition;
            _isInElevator = true;

            Debug.Log($"Person in Elevator");
            _elevator.AddPassengerToElevator(this);
            isGoing = false;
        }

        public IEnumerator MoveToFloor()
        {
            isGoing = true;
            float elapsedTime = 0f;
            Vector3 initialPosition = gameObject.transform.position;
            int currentFloor = _elevator.CurrentFloor;
            GameObject floorGO = GameObject.Find("Floor " + currentFloor);
            Debug.Log($"++++{floorGO.name}");
            _floorGO = floorGO.GetComponent<FloorGO>();
            

            Vector3 targetPosition = _floorGO.ExitPoint.position;
                
            while (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;
                var easeValue = personMovementCurve.Evaluate(t);
                gameObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, easeValue);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            gameObject.transform.position = targetPosition;
            _isInElevator = true;

            Debug.Log($"Person on the floor");
            isGoing = false;
        }

        public void DissapearAfterDelay()
        {
            Destroy(gameObject, 1f);
        }

        public void ExitElevator()
        {
            StartCoroutine(MoveToFloor());
            DissapearAfterDelay();
        }
        public void SetCurrentFloor(int cfloor)
        {
            personCurrentFloor = cfloor;
        }

        public void SetTargetFloor(int tfloor)
        {
            personTargetFloor = tfloor;
        }

        public void SetFloor(FloorGO floor)
        {
            _floorGO = floor;
        }

        public void UpdateElevatorStatus(IObserverble observerble)
        {
            if (personCurrentFloor == _elevator.CurrentFloor && !_isInElevator)
            {
                StartCoroutine(MoveToElevator());
                gameObject.transform.SetParent(_transformOfElevator, true);
                
            }
        }
    }
}