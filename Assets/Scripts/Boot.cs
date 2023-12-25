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
        [Inject] private ConsoleDisplay _consoleDisplay;
        [Inject] private UnityDisplay _unityDisplay;
        private Building _building;
        //[Inject] private DB_Setup _dbSetup;
        [Inject] private IElevator _elevator;
        [Inject] private FloorFactory _floorFactory;
        [Inject] private DatabaseManager _databaseManager;
        
        public int NumberOfFloors;
        public int ElevatorCapacity;
        public int MaxPeoplePerFloor;

        [Inject]
        public void Construct()
        {
            _building = new Building(NumberOfFloors, _elevator, ElevatorCapacity,  MaxPeoplePerFloor, _floorFactory);
            _consoleDisplay.SetBuilding(_building);
            _unityDisplay.SetBuilding(_building);
            
        }

        private void Start()
        {
            if (_building != null && _consoleDisplay != null)
            {
                ShowFloorDetails();
                RotateDatabase();
                SavePersonsToDB();
                _unityDisplay.Start();
            }
            else
            {
                Debug.Log("Building or ConsoleDisplay is not initialized.");
            }
        }

        private void ShowFloorDetails()
        {
            _consoleDisplay.ShowFloorDetailsInConsole();
        }

        private void SavePersonsToDB()
        {
            foreach (var floor in _building._floors)
            {
                _databaseManager.SavePeopleToDB(floor.GetPersonsListOnFloor());
            }
        }

        private void RotateDatabase()
        {
            _databaseManager.RotateDatabase();
        }
    }
}