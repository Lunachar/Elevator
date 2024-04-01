using System;
using System.Collections.Generic;
using Elevator.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Elevator.Display
{
    public class UnityDisplay : MonoBehaviour, IObserver
    {
        // Reference to the building
        private Building _building;

        // Dependency injection container
        private DiContainer _container;

        // Prefabs for floor, elevator, person, and empty object
        private FloorGO _floorGO;
        private ElevatorGO _elevatorGo;
        private PersonGO _personGo;
        private EmptyObject _emptyObject;

        // References to the menu prefab
        public GameObject UpAndDownButtonsMenu;
        public GameObject FloorButtonsMenu;


        // Constructor for dependency injection
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

        // Start is called before the first frame update
        private void Start()
        {
            // Visualize the building and control panel if building reference is not null
            if (_building != null)
            {
                VisualizeBuilding();
                VisualizeControlPanel();
            }
            else
            {
                Debug.LogError("Building reference is null in UnityDisplay.");
            }
        }

        // Visualize the building structure
        private void VisualizeBuilding()
        {
            // Instantiate an empty object to serve as the stage
            var stage = Instantiate(_emptyObject);
            stage.name = "STAGE";

            // Initialize variables for floor height and list of floors
            int floorHeight = 0;
            List<Floor> floors = _building._floorList.GetFloors();

            // Iterate through each floor in the building
            foreach (Floor floor in floors)
            {
                // Instantiate the floor prefab and set its position and text
                FloorGO floorInstance = _container.InstantiatePrefabForComponent<FloorGO>(_floorGO,
                    new Vector3(0f, floorHeight, 0f),
                    Quaternion.identity,
                    stage.transform);
                floorInstance.name = $"Floor {floor.Number}";
floorInstance.GetComponentInChildren<FloorGO>().floorNumber = floor.Number;
                floorInstance.GetComponentInChildren<TMP_Text>().text = $"Floor {floor.Number}";

                // Iterate through each person on the floor
                List<Person> personsOnFloor = floor.GetPersonsListOnFloor();
                int personOffset = 0;
                foreach (Person person in personsOnFloor)
                {
                    // Instantiate the person prefab and set its position and text
                    PersonGO personInstance = _container.InstantiatePrefabForComponent<PersonGO>(_personGo,
                        new Vector3((1f * personOffset) - 2, -2.73f + floorHeight, 0f),
                        Quaternion.identity,
                        floorInstance.transform);
                    
                    personInstance.SetCurrentFloor(person.GetCurrentFloor());
                    personInstance.SetTargetFloor(person.GetTargetFloor());
                    
                    //personInstance.SetFloor(floorInstance);
                    
                    personInstance.text.text = person.GetPersonName();
                    personOffset += 2; // Increase offset for next person
                }

                floorHeight += 6; // Increase floor height for next floor
            }

            // Instantiate the elevator prefab
            _container.InstantiatePrefabForComponent<ElevatorGO>(_elevatorGo);
        }

        // Visualize the control panel
        private void VisualizeControlPanel()
        {
            // Instantiate the menu prefab
            Instantiate(UpAndDownButtonsMenu);
            Instantiate(FloorButtonsMenu);
        }

        // Method to handle elevator status updates
        public void UpdateElevatorStatus(IObserverble observerble)
        {
            Debug.LogWarning("UpdateElevatorStatus method is not implemented.");
        }
    }
}
