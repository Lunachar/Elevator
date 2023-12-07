using System.Collections.Generic;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace Elevator
{
    public class Building
    {
        public List<Floor> Floors { get; set; }
        public Elevator Elevator { get; set; }

        

        public Building(int numberOfFloors, int elevatorCapacity)
        {
            Floors = GenerateFloors(numberOfFloors);
            Elevator = new Elevator(elevatorCapacity);
        }

        private List<Floor> GenerateFloors(int numberOfFloors)
        {
            List<Floor> floors = new List<Floor>();
            for (int i = 1; i <= numberOfFloors; i++)
            {
               //int munberOfPeople = new RandomIntegerGenerator().GenerateValue(0, 15);
               //floors.Add(new Floor(i, munberOfPeople));
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