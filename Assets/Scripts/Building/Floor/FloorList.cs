using System.Collections.Generic;
using UnityEngine;

namespace Elevator
{
    public class FloorList
    {
        private List<Floor> _floors;
        private Boot _boot;

        public FloorList(Boot boot, FloorFactory floorFactory)
        {
            _boot = boot;
            var numberOfFloors = _boot.NumberOfFloors();
            Debug.Log($"||FLOORLIST {numberOfFloors}");
           
            _floors = GenerateFloors(numberOfFloors, floorFactory);;
        }

        private List<Floor> GenerateFloors(int numberOfFloors, FloorFactory floorFactory)
        {
            List<Floor> floors = new List<Floor>();
            for (int i = 1; i <= numberOfFloors; i++)
            {
                floors.Add(floorFactory.Create(i,numberOfFloors, _boot.MaxPeoplePerFloor()));
                Debug.LogWarning($"I) Floor Number {i}");
            }

            return floors;
        }

        public List<Floor> GetFloors()
        {
            return _floors;
        }
    }
}