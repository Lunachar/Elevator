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
        //private DB_Setup _dbSetup;

        public Floor(int floorNumber, int totalFloors, int maxPeoplePerFloor, PersonGenerator personGenerator, DatabaseManager databaseManager)
        {
            Number = floorNumber;
            TotalFloors = totalFloors;
            _personGenerator = personGenerator;
            _personList = _personGenerator.GeneratePeople(maxPeoplePerFloor, floorNumber, totalFloors);
            //_dbSetup = dbSetup;
            databaseManager?.SavePeopleToDB(_personList);
        }

        // private List<Person> GeneratePeople(int maxPeoplePerFloor)
        // {
        //     return _personGenerator.GeneratePeople(maxPeoplePerFloor, Number, TotalFloors);
        // }


        public void SetPersonsList(List<Person> personsList)
        {
            _personList = personsList;
        }
        public List<Person> GetPersonsList()
        {
            return _personList;
        }

        // public override string ToString()
        // {
        //     return $"Floor number: {Number}, number of people: {_personList.Count}";
        // }
    }
}