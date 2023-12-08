using System.Collections.Generic;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace Elevator
{
    public class Building
    {
        internal List<Floor> Floors { get; set; }
        private Elevator Elevator { get; set; }
        private int _maxPeoplePerFloor { get; set; }

        public Building(int numberOfFloors, int elevatorCapacity, int maxPeoplePerFloor)
        {
            _maxPeoplePerFloor = maxPeoplePerFloor;
            Floors = GenerateFloors(numberOfFloors);
            Elevator = new Elevator(elevatorCapacity);
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