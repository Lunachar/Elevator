using System.Collections.Generic;
using Elevator.Interfaces;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class Building
    {
        private IElevator _elevator { get; set; }
        private int _maxPeoplePerFloor { get; set; }
        //internal List<Floor> _floors { get; private set; }

        public FloorList _floorList;
        //private Boot _boot;

        public Building(IElevator elevator/*, Boot boot*/, FloorList floorList)
        {
            Debug.Log("2: in Building construct");
            
            _elevator = elevator;
            //_maxPeoplePerFloor = boot.GetMaxPeoplePerFloor();
            _floorList = floorList;
            
            Debug.Log("3: in Building construct");
        }

        // private List<Floor> GenerateFloors(int numberOfFloors, FloorFactory floorFactory)
        // {
        //     List<Floor> floors = new List<Floor>();
        //     Debug.Log("here2");
        //     for (int i = 1; i <= numberOfFloors; i++)
        //     {
        //         floors.Add(floorFactory.Create(i, numberOfFloors, _maxPeoplePerFloor));
        //     }
        //
        //     return floors;
        // }

        public void CallElevator(int floorNumber)
        {
            
        }

        public void MoveElevator(int floorNumber)
        {
            
        }
       

    }
    
}