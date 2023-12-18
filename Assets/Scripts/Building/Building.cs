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
        internal List<Floor> _floors { get; set; }
        private IElevator _elevator { get; set; }
        private int _maxPeoplePerFloor { get; set; }

        public Building(int numberOfFloors, IElevator elevator, int elevatorCapacity, int maxPeoplePerFloor)
        {
            Debug.Log("2: in Building construst");
            _maxPeoplePerFloor = maxPeoplePerFloor;
            _floors = GenerateFloors(numberOfFloors);
            _elevator = elevator;
            _elevator.SetCapacity(elevatorCapacity);
        }

        private List<Floor> GenerateFloors(int numberOfFloors)
        {
            List<Floor> floors = new List<Floor>();
            for (int i = 1; i <= numberOfFloors; i++)
            {
               floors.Add(new Floor(i, numberOfFloors, _maxPeoplePerFloor));
            }

            return floors;
        }

        public void CallElevator(int floorNumber)
        {
            
        }

        public void MoveElevator(int floorNumber)
        {
            
        }
       

    }
    
}