using System.Collections.Generic;
using Elevator.Managers;
using RandomDataGenerator;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using Zenject;


namespace Elevator
{
    public class Floor
    {
        private readonly PersonGenerator _personGenerator;
        public int Number { get; }
        public int TotalFloors { get; }
        
        private List<Person> _personList;

        public Floor(int floorNumber, int totalFloors, int maxPeoplePerFloor, PersonGenerator personGenerator, DatabaseManager databaseManager)
        {
            Number = floorNumber;
            TotalFloors = totalFloors;
            _personGenerator = personGenerator;
            _personList = _personGenerator.GeneratePeople(maxPeoplePerFloor, floorNumber, totalFloors);
        }


        public void SetPersonsListOnFloor(List<Person> personsList)
        {
            _personList = personsList;
        }
        public List<Person> GetPersonsListOnFloor()
        {
            return _personList;
        }

        // public override string ToString()
        // {
        //     return $"Floor number: {Number}, number of people: {_personList.Count}";
        // }
    }
}