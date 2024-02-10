using System;
using Elevator.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
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
            //_numberOfFloors = _building._floorList.GetFloors().Count;

            //Debug.Log($"Count: {_numberOfFloors} \n Total Floors: {_numberOfFloors}");
            
            VisualizeBuilding();
        }

        private void VisualizeBuilding()
        {
            var emptyObject = Instantiate(_emptyObject);
            emptyObject.name = "STAGE";
            
            int floorHeight = 0;
            var floors = _building._floorList.GetFloors();
            for (var i = 0; i < floors.Count; i++)
            {
                var floor = floors[i];
                var floorInstance = _container.InstantiatePrefabForComponent<FloorGO>(_floorGO,
                    new Vector3(0f, (floor.Number - 1 + floorHeight), 0f),
                    Quaternion.identity,
                    emptyObject.transform);
                floorInstance.name = $"Floor {i + 1}";

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

                floorHeight += 5;
            }

            /*var elevatorInstance = */_container.InstantiatePrefabForComponent<ElevatorGO>(_elevatorGo);
        }



        
        

        public void Update()
        {
            //throw new NotImplementedException();
        }
    }
}