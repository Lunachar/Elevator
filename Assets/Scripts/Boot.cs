using System;
using System.Collections.Generic;
using Elevator.Display;
using Elevator.Interfaces;
using Elevator.Managers;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class Boot : MonoBehaviour
    {
        // The only data input in the game
        [SerializeField]private int _numberOfFloors; // Total number of floors in the building
        [SerializeField]private int _elevatorCapacity; // Maximum capacity of the elevator
        [SerializeField]private int _maxPeoplePerFloor; // Maximum number of people per floor

        public int PNumberOfFloors
        {
            get { return _numberOfFloors; }
            set {
                if (value < 2 || value > 10) { throw new Exception("Invalid number of floors. Must be between 2 and 10."); }
                _numberOfFloors = value;
            }
        }

        public int PElevatorCapacity
        {
            get { return _elevatorCapacity; }
            set
            {
                if (value < 1 || value > 10) { throw new Exception("Invalid elevator capacity. Must be between 1 and 10."); }
                _elevatorCapacity = value;
            }
        }

        public int PMaxPeoplePerFloor
        {
            get { return _maxPeoplePerFloor; }
            set
            {
                if (value < 1 || value > 10) { throw new Exception("Invalid maximum people per floor. Must be between 1 and 10."); }
                _maxPeoplePerFloor = value;
            }
        }

        // Components
        private ConsoleDisplay _consoleDisplay; // For displaying information in the console
        private UnityDisplay _unityDisplay; // For visualizing the elevator system in Unity
        private Building _building; // Represents the building structure
        private GameObject _stage; // Represents the main stage object
        private MainMenu _mainMenu; // Represents the main menu
        
        private IElevator _elevator; // The elevator instance
        private FloorFactory _floorFactory; // Factory for creating floors
        private FloorList _floorList; // List of floors in the building
        private PersonGO _personGo; // Represents the person game object
        
        private DatabaseManager _databaseManager; // Manages the database
        
        [Inject]
        public void Construct(Building building, ConsoleDisplay consoleDisplay, UnityDisplay unityDisplay, IElevator elevator, FloorFactory floorFactory, DatabaseManager databaseManager, FloorList floorList, PersonGO personGo, MainMenu mainMenu)
        {
            // Dependency injection to initialize components
            _consoleDisplay = consoleDisplay;
            _unityDisplay = unityDisplay;
            _mainMenu = mainMenu;
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
        public int MaxPeoplePerFloor()
        {
            PMaxPeoplePerFloor = MainMenu.MaxPeoplePerFloor;
            return PMaxPeoplePerFloor;
        }
        
        public int NumberOfFloors()
        {
            PNumberOfFloors = MainMenu.NumberOfFloors;
            return PNumberOfFloors;
        }
        
        public int ElevatorCapacity()
        {
            PElevatorCapacity = MainMenu.ElevatorCapacity;
            return PElevatorCapacity;
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
