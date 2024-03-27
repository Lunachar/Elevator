using System;
using System.Collections.Generic;
using Elevator.Display;
using Elevator.Interfaces;
using Elevator.Managers;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class Boot : MonoBehaviour, IStartData
    {
        // Components
        private ConsoleDisplay _consoleDisplay; // For displaying information in the console
        private UnityDisplay _unityDisplay; // For visualizing the elevator system in Unity
        private Building _building; // Represents the building structure
        private GameObject _stage; // Represents the main stage object
        
        private IElevator _elevator; // The elevator instance
        private FloorFactory _floorFactory; // Factory for creating floors
        private FloorList _floorList; // List of floors in the building
        private PersonGO _personGo; // Represents the person game object
        
        private DatabaseManager _databaseManager; // Manages the database
        
        // The only data input in the game
        public int NumberOfFloors; // Total number of floors in the building
        public int ElevatorCapacity; // Maximum capacity of the elevator
        public int MaxPeoplePerFloor; // Maximum number of people per floor
        
        [Inject]
        public void Construct(Building building, ConsoleDisplay consoleDisplay, UnityDisplay unityDisplay, IElevator elevator, FloorFactory floorFactory, DatabaseManager databaseManager, FloorList floorList, PersonGO personGo)
        {
            // Dependency injection to initialize components
            _consoleDisplay = consoleDisplay;
            _unityDisplay = unityDisplay;
            _building = building;
            _elevator = elevator;
            _floorFactory = floorFactory;
            _databaseManager = databaseManager;
            _floorList = floorList;
            _personGo = personGo;
        }

        private void Start()
        {
            // Initialization tasks when the script instance is being loaded
            if (_building != null && _consoleDisplay != null)
            {
                // Show floor details in the console
                ShowFloorDetails();
                
                // Rotate the database
                RotateDatabase();
                
                // Save persons to the database
                SavePersonsToDB();
                
                // Find the main stage object
                _stage = GameObject.Find("STAGE");
            }
            else
            {
                Debug.Log("Building or ConsoleDisplay is not initialized.");
            }
        }

        private void ShowFloorDetails()
        {
            // Helper method to display floor details in the console
            _consoleDisplay.ShowFloorDetailsInConsole();
        }
        
        private void SavePersonsToDB()
        {
            // Helper method to save persons to the database
            foreach (var floor in _building._floorList.GetFloors())
            {
                _databaseManager.SavePeopleToDB(floor.GetPersonsListOnFloor());
            }
        }

        private void RotateDatabase()
        {
            // Helper method to rotate the database
            _databaseManager.RotateDatabase();
        }

        // Accessor methods
        public int GetMaxPeoplePerFloor()
        {
            return MaxPeoplePerFloor;
        }

        public int GetNumberOfFloors()
        {
            return NumberOfFloors;
        }

        public int GetElevatorCapacity()
        {
            return ElevatorCapacity;
        }

        public IElevator GetElevator()
        {
            return _elevator;
        }

        public GameObject GetStage()
        {
            return _stage;
        }

        public PersonGO GetPersonGO()
        {
            return _personGo;
        }
    }
}
