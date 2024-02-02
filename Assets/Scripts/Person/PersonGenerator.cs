using System.Collections.Generic;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace Elevator
{
    public class PersonGenerator
    {
        public List<Person> GeneratePeople(int maxPeoplePerFloor, int floorNumber, int totalFloors)
        {
            var generatedPeople = new List<Person>();
            var numberOfPeople = new RandomizerNumber<int>(new FieldOptionsInteger()
            {
                Min = 0,
                Max = maxPeoplePerFloor
            }).Generate().GetValueOrDefault();
            
            for (int i = 0; i < numberOfPeople; i++)
            {
                var person = new Person();
                person.Construct(null, null, floorNumber, totalFloors);
                generatedPeople.Add(person);
            }
            return generatedPeople;
        }
    }
}