using System;
using System.Collections.Generic;
using Elevator.Display;
using Elevator.Interfaces;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class Boot : MonoBehaviour
    {
        [Inject] private ConsoleDisplay _consoleDisplay;
        private Building _building;
        [Inject] private DB_Setup _dbSetup;
        [Inject] private IElevator _elevator;
        
        public int NumberOfFloors;
        public int ElevatorCapacity;
        public int MaxPeoplePerFloor;

        [Inject]
        public void Construct()
        {
            _building = new Building(NumberOfFloors, _elevator, ElevatorCapacity,  MaxPeoplePerFloor);
            _consoleDisplay.SetBuilding(_building);
        }

        private void Start()
        {
            if (_building != null && _consoleDisplay != null)
            {
                ShowFloorDetails();
            }
            else
            {
                Debug.Log("Building or ConsoleDisplay is not initialized.");
            }
        }

        private void ShowFloorDetails()
        {
            _consoleDisplay.ShowFloorDetails();
        }
    }
}