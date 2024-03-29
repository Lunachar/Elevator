using Elevator.Managers;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class FloorFactory 
    {
        private readonly PersonGenerator _personGenerator;
        private readonly DB_Setup _dbSetup;
        private readonly DatabaseManager _databaseManager;

        public FloorFactory(PersonGenerator personGenerator, DB_Setup dbSetup)
        {
            Debug.Log($"FloorFactory Constructor");
            _personGenerator = personGenerator;
            _dbSetup = dbSetup;
        }

        public Floor Create(int floorNumber, int totalFloors, int maxPeoplePerFloor)
        {
            var peopleList = _personGenerator.GeneratePeople(maxPeoplePerFloor, floorNumber, totalFloors);
            Debug.Log($"||) FLOORFACTORY floor number: {floorNumber}");
            var floor = new Floor(floorNumber, totalFloors, maxPeoplePerFloor, _personGenerator, peopleList/*, _databaseManager*/);
            //floor.SetPersonsListOnFloor(peopleList);
            return floor;
        }
    }
}