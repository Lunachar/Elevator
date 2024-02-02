
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using Unity.VisualScripting;
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
        public void Construct(Building building, Boot boot,  int currentFloor, int totalFloors)
        {
            _building = building;
            _boot = boot;
            CurrentFloor = currentFloor;
            _totalFloors = totalFloors;
            //_floor = floor;
        }

        public Person()
        {
           var personFirstNameGenerator = RandomizerFactory.GetRandomizer(new FieldOptionsFirstName());
           Name = personFirstNameGenerator.Generate();

           var personLastNameGenerator = RandomizerFactory.GetRandomizer(new FieldOptionsLastName());
           LastName = personLastNameGenerator.Generate();

           var age = new RandomizerNumber<int>(new FieldOptionsInteger()
           {
               Min = 0,
               Max = 120
           });
           Age = age.Generate().GetValueOrDefault();


           //CurrentFloor = GetCurrentFloor();
           
           var targetFloor = new RandomizerNumber<int>(new FieldOptionsInteger()
           {
               Min = 1,
               Max = _boot.GetNumberOfFloors() /*????*/
           });
           TargetFloor = targetFloor.Generate().GetValueOrDefault();
           while (TargetFloor == CurrentFloor)
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
