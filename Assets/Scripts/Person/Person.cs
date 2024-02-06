
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;


namespace Elevator
{
    public class Person
    {
        internal string Name { get; set; }
        internal string LastName { get; set; }
        internal int Age { get; set; }
        internal int CurrentFloor { get; set; }
        internal int TargetFloor { get; set; }
        internal bool Completed { get; set; }
        
        private Building _building;
        private Boot _boot;
        private int _totalFloors;

        [Inject]
            private PersonGenerator _personGenerator;

        
        public void Initialize()
        {
            //_totalFloors = _boot.GetNumberOfFloors();
           var personFirstNameGenerator = RandomizerFactory.GetRandomizer(new FieldOptionsFirstName());
           Name = personFirstNameGenerator.Generate();
           Debug.Log($"|||NAME: {Name}");

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

        public string GetPersonName()
        {
            return $"{Name} {LastName}";
        }
        
        public override string ToString()
        {
            return $"{Name} {LastName}, {Age}. Now on {CurrentFloor} floor, destination: {TargetFloor} floor.";
        }
    }
}
