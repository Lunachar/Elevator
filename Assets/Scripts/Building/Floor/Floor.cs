using System.Collections.Generic;
using RandomDataGenerator;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;


namespace Elevator
{
    public class Floor
    {
        public int Number { get; set; }
        public int FloorsAmount { get; set; }
        //public People People { get; set; }
        private List<Person> _personList;

        public Floor(int floorNumber, int floorsAmount, int maxPeoplePerFloor)
        {
            Number = floorNumber;
            FloorsAmount = floorsAmount;
            _personList = GeneratePeople(maxPeoplePerFloor);
            /*_personList = People.GetPersonsList();*/
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
                generatedPeople.Add(new Person(Number, FloorsAmount));
            }
            return generatedPeople;
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