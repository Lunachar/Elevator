using System;
using Elevator.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Zenject;

namespace Elevator.Display
{
    public class UnityDisplay : MonoBehaviour, IObserver
    {
         private Building _building;

         private DiContainer _container;
         
         private FloorGO _floorGO;
         private ElevatorGO _elevatorGo;
         private PersonGO _personGo;
         private EmptyObject _emptyObject;
         
         private int _numberOfFloors;

         public GameObject button;
         public GameObject Menu;

         

         [Inject]
         public void Construct(Building building, DiContainer container, FloorGO floorGo, ElevatorGO elevatorGo, PersonGO personGo, EmptyObject emptyObject)
         {
             _building = building;

             _container = container;
             
             _floorGO = floorGo;
             _elevatorGo = elevatorGo;
             _personGo = personGo;
             _emptyObject = emptyObject;
         }
        
        

        public void Start()
        {   
            VisualizeBuilding();
            VisualizeControlPanel();
        }

        private void VisualizeBuilding()
        {
            var emptyObject = Instantiate(_emptyObject);
            emptyObject.name = "STAGE";
            
            var floorHeight = 0;
            var floors = _building._floorList.GetFloors();
            for (var i = 0; i < floors.Count; i++)
            {
                var floor = floors[i];
                var floorInstance = _container.InstantiatePrefabForComponent<FloorGO>(_floorGO,
                    new Vector3(0f, (floorHeight), 0f),
                    Quaternion.identity,
                    emptyObject.transform);
                floorInstance.name = $"Floor {floor.Number}";
                _floorGO._text.text = $"Floor {floor.Number}";

                var personsOnFloor = floor.GetPersonsListOnFloor();
                int personOffset = 0;
                foreach (var person in personsOnFloor)
                {
                    var personInstance = _container.InstantiatePrefabForComponent<PersonGO>(_personGo,
                        new Vector3((1f * personOffset) - 2, (floor.Number + floorHeight - 3.5f), 0f),
                        Quaternion.identity,
                        floorInstance.transform);
                    personInstance._text.text = person.GetPersonName();
                    personOffset += 2;
                }

                floorHeight += 6;
            }

            /*var elevatorInstance = */_container.InstantiatePrefabForComponent<ElevatorGO>(_elevatorGo);
        }

        private void VisualizeControlPanel()
        {
            var buttonUp = Instantiate(button, new Vector3(5.5f, 2f), Quaternion.identity);
            
            var buttonDown = Instantiate(button, new Vector3(7.5f, 2f), Quaternion.identity);
            var bButton = Instantiate(Menu);
            //bButton.GetComponent<Button>().clicked().Add;
            //BButton.OnCl


        }

        
        public void Update()
        {
            //throw new NotImplementedException();
        }
    }
}