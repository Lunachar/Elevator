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

        /// <summary>
        /// Generates people for a specific floor.
        /// </summary>
        /// <param name="maxPeoplePerFloor">The maximum number of people per floor.</param>
        /// <param name="floorNumber">The number of the floor for which people are generated.</param>
        /// <param name="totalFloors">The total number of floors in the building.</param>
        /// <returns>A list of generated people.</returns>
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
                var person = _container.Instantiate<Person>();
                person.Initialize(floorNumber);
                generatedPeople.Add(person);
                _elevator.Attach(person);
            }
            return generatedPeople;
        }

        /// <summary>
        /// Gets the total number of floors in the building.
        /// </summary>
        /// <returns>The total number of floors.</returns>
        public int GetTotalFloors()
        {
            return _totalFloors;
        }

        /// <summary>
        /// Gets the current floor number.
        /// </summary>
        /// <returns>The current floor number.</returns>
        public int GetCurrentFloor()
        {
            return _floorNumber;
        }
    }
}
