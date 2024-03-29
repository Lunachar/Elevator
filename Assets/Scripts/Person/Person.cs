using System;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class Person : IObserver
    {
        internal string Name { get; set; }
        internal string LastName { get; set; }
        internal int Age { get; set; }
        internal int CurrentFloor { get; set; }
        internal int TargetFloor { get; set; }
        internal bool Completed { get; set; }
        
        [Inject]
        private PersonGenerator _personGenerator;

        /// <summary>
        /// Initializes the person with random data and sets the current floor.
        /// </summary>
        /// <param name="currentFloor">The current floor of the person.</param>
        public void Initialize(int currentFloor)
        {
            CurrentFloor = currentFloor;
            var personFirstNameGenerator = RandomizerFactory.GetRandomizer(new FieldOptionsFirstName());
            Name = personFirstNameGenerator.Generate();
            Debug.Log($"||||) FloorNumver: {CurrentFloor}");

            var personLastNameGenerator = RandomizerFactory.GetRandomizer(new FieldOptionsLastName());
            LastName = personLastNameGenerator.Generate();

            var age = new RandomizerNumber<int>(new FieldOptionsInteger()
            {
                Min = 0,
                Max = 120
            });
            Age = age.Generate().GetValueOrDefault();
          
            CurrentFloor = _personGenerator.GetCurrentFloor();
            var targetFloor = new RandomizerNumber<int>(new FieldOptionsInteger()
            {
                Min = 1,
                Max = _personGenerator.GetTotalFloors() 
            });
            TargetFloor = targetFloor.Generate().GetValueOrDefault();
            while (TargetFloor == _personGenerator.GetCurrentFloor())
            {
                TargetFloor = targetFloor.Generate().GetValueOrDefault();
            }
        }

        /// <summary>
        /// Gets the full name of the person.
        /// </summary>
        /// <returns>The full name of the person.</returns>
        public string GetPersonName()
        {
            return $"{Name} {LastName}";
        }
        
        /// <summary>
        /// Returns a string representation of the person, including name, age, current floor, and target floor.
        /// </summary>
        /// <returns>A string representation of the person.</returns>
        public override string ToString()
        {
            return $"{Name} {LastName}, {Age}. Now on {CurrentFloor} floor, destination: {TargetFloor} floor.";
        }

        /// <summary>
        /// Gets the current floor of the person.
        /// </summary>
        /// <returns>The current floor of the person.</returns>
        public int GetCurrentFloor()
        {
            return CurrentFloor;
        }
        
        public int GetTargetFloor(){ return TargetFloor;}

        /// <summary>
        /// Updates the current floor of the person.
        /// </summary>
        public void UpdateCurrentFloor()
        {
            CurrentFloor = _personGenerator.GetCurrentFloor();
        }

        /// <summary>
        /// Updates the elevator status.
        /// </summary>
        /// <param name="observerble">The observerble parameter.</param>
        public void UpdateElevatorStatus(IObserverble observerble)
        {
            // Implementation pending
        }
    }
}
