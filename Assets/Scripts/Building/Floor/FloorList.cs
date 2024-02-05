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
            var numberOfFloors = _boot.GetNumberOfFloors();
            Debug.Log($"||FLOORLIST {numberOfFloors}");
           
            _floors = GenerateFloors(numberOfFloors, floorFactory);;
        }

        private List<Floor> GenerateFloors(int numberOfFloors, FloorFactory floorFactory)
        {
            var maxPeoplePerFloor = _boot.GetMaxPeoplePerFloor();
            List<Floor> floors = new List<Floor>();
            for (int i = 1; i <= numberOfFloors; i++)
            {
                floors.Add(floorFactory.Create(i,numberOfFloors, _boot.GetMaxPeoplePerFloor()));
            }

            return floors;
        }

        public List<Floor> GetFloors()
        {
            return _floors;
        }
    }
}