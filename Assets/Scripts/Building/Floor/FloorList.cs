using System.Collections.Generic;

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
           
            _floors = GenerateFloors(numberOfFloors, floorFactory);;
        }
        public List<Floor> GenerateFloors(int numberOfFloors, FloorFactory floorFactory)
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