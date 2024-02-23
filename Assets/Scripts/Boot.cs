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
        private ConsoleDisplay _consoleDisplay;
        private UnityDisplay _unityDisplay;
        private Building _building;
        
        private IElevator _elevator;
        private FloorFactory _floorFactory;
        private FloorList _floorList;
        
        private DatabaseManager _databaseManager;
        
        #region The only data input in the game

        public int NumberOfFloors;
        public int ElevatorCapacity;
        public int MaxPeoplePerFloor;
        
        #endregion


        [Inject]
        public void Construct(Building building, ConsoleDisplay consoleDisplay, UnityDisplay unityDisplay, IElevator elevator, FloorFactory floorFactory, DatabaseManager databaseManager, FloorList floorList)
        {
            Debug.Log("1: in Boot");
            _consoleDisplay = consoleDisplay;
            Debug.Log("2: in Boot");
            _unityDisplay = unityDisplay;
            Debug.Log("3: in Boot");
            _building = building;
            _elevator = elevator;
            _floorFactory = floorFactory;
            _databaseManager = databaseManager;
            _floorList = floorList;
        }
        private void Start()
        {
            Debug.Log($"constr 1");
            
            if (_building != null && _consoleDisplay != null)
            {
                Debug.Log($"1 {_building.ToString()}");
                Debug.Log($"2 {_building._floorList.ToString()}");
                Debug.Log($"there ARE : {_building._floorList.GetFloors().Count}");
                ShowFloorDetails();
                RotateDatabase();
                SavePersonsToDB();
                //_unityDisplay.VisualizeBuilding(BuildingPrefab, ElevatorPrefab, FloorPrefab, PersonPrefab);
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
        
        // public List<Floor> GenerateFloors()/*(int numberOfFloors, FloorFactory floorFactory)*/
        // {
        //     List<Floor> floors = new List<Floor>();
        //     Debug.Log("here2");
        //     for (int i = 1; i <= NumberOfFloors; i++)
        //     {
        //         floors.Add(_floorFactory.Create(i, NumberOfFloors, MaxPeoplePerFloor));
        //     }
        //
        //     return floors;
        // }

        private void SavePersonsToDB()
        {
            foreach (var floor in _building._floorList.GetFloors())
            {
                _databaseManager.SavePeopleToDB(floor.GetPersonsListOnFloor());
            }
        }

        private void RotateDatabase()
        {
            _databaseManager.RotateDatabase();
        }

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
    }
}