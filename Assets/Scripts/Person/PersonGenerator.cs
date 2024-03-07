using System.Collections.Generic;
using System.ComponentModel;
using Elevator.Interfaces;
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
        private Elevator _elevator;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
            _elevator = container.Resolve<IElevator>() as Elevator;
        }
        public List<Person> GeneratePeople(int maxPeoplePerFloor, int floorNumber, int totalFloors)
        {
            _totalFloors = totalFloors;
            Debug.Log($"|||) PersonGenerator max people: {maxPeoplePerFloor} floorNumber: {floorNumber} totalFloors: {totalFloors}");
            var generatedPeople = new List<Person>();
            var numberOfPeople = new RandomizerNumber<int>(new FieldOptionsInteger()
            {
                Min = 0,
                Max = maxPeoplePerFloor + 1
            }).Generate().GetValueOrDefault();
            
            for (int i = 0; i < numberOfPeople; i++)
            {
                _floorNumber = floorNumber;
                //var person = new Person();
                var person = _container.Instantiate<Person>();
                person.Initialize(floorNumber);
                //person.Construct(floorNumber, totalFloors);
                generatedPeople.Add(person);
                _elevator.Attach(person);
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