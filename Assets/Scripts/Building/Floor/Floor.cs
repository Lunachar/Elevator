using System.Collections.Generic;
using RandomDataGenerator;



namespace Elevator
{
    public class Floor
    {
        public int Number { get; set; }
        public List<Person> People { get; set; }

        public Floor(int floorNumber, int numberOfPeople)
        {
            Number = floorNumber;
            People = GeneratePeople(numberOfPeople);
        }
        
        private List<Person> GeneratePeople(int numberOfPeople)
        {
            var people = new List<Person>();
            for (int i = 0; i < numberOfPeople; i++)
            {
                //people.Add(new Person(Number));
            }

            return people;
        }

    }
}