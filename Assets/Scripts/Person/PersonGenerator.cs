using System.Collections.Generic;
using System.ComponentModel;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class PersonGenerator
    {
        private int _floorNumber;
        private int _totalFloors;
        private DiContainer _container;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }
        public List<Person> GeneratePeople(int maxPeoplePerFloor, int floorNumber, int totalFloors)
        {
            _totalFloors = totalFloors;
            Debug.Log($"||PersonGenerator max people: {maxPeoplePerFloor} floorNumber: {floorNumber} totalFloors: {totalFloors}");
            var generatedPeople = new List<Person>();
            var numberOfPeople = new RandomizerNumber<int>(new FieldOptionsInteger()
            {
                Min = 0,
                Max = maxPeoplePerFloor
            }).Generate().GetValueOrDefault();
            
            for (int i = 0; i < numberOfPeople; i++)
            {
                _floorNumber = floorNumber;
                //var person = new Person();
                var person = _container.Instantiate<Person>();
                person.Initialize();
                //person.Construct(floorNumber, totalFloors);
                generatedPeople.Add(person);
            }
            return generatedPeople;
        }

        public int GetTotalFloors()
        {
            return _totalFloors;
        }

        public int GetCurrentFloor()
        {
            return _floorNumber;
        }
    }
}