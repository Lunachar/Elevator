using System.Collections.Generic;
using RandomDataGenerator;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using Zenject;


namespace Elevator
{
    public class Floor
    {
        public int Number { get; }
        public int TotalFloors { get; }
        
        private List<Person> _personList;
        [Inject] private DB_Setup _dbSetup;

        public Floor(int floorNumber, int totalFloors, int maxPeoplePerFloor)
        {
            Number = floorNumber;
            TotalFloors = totalFloors;
            _personList = GeneratePeople(maxPeoplePerFloor);
            SavePeopleToDB();
        }

        private List<Person> GeneratePeople(int maxPeoplePerFloor)
        {
            var generatedPeople = new List<Person>();
            //var numberOfPeople = maxPeoplePerFloor;
            var numberOfPeople = new RandomizerNumber<int>(new FieldOptionsInteger()
            {
                Min = 0,
                Max = maxPeoplePerFloor
            }).Generate().GetValueOrDefault();
            for (int i = 0; i < numberOfPeople; i++)
            {
                generatedPeople.Add(new Person(Number, TotalFloors));
            }
            return generatedPeople;
        }

        private void SavePeopleToDB()
        {
            foreach (var person in _personList)
            {
                _dbSetup.InsertPersonData(person);
            }
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