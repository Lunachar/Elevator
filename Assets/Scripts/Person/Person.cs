
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using Unity.VisualScripting;


namespace Elevator
{
    public class Person
    {
        private string Name { get; set; }
        private string LastName { get; set; }
        private int Age { get; set; }
        internal int CurrentFloor { get; set; }
        internal int TargetFloor { get; set; }

        public Person(int currentFloor, int floorsAmount)
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


           CurrentFloor = currentFloor;
           
           var targetFloor = new RandomizerNumber<int>(new FieldOptionsInteger()
           {
               Min = 1,
               Max = floorsAmount
           });
           TargetFloor = targetFloor.Generate().GetValueOrDefault();
           if (TargetFloor == CurrentFloor)
           {
               TargetFloor = targetFloor.Generate().GetValueOrDefault();
           }
        }

        public override string ToString()
        {
            return $"{Name} {LastName}, {Age}. Now on {CurrentFloor} floor, destination: {TargetFloor} floor.";
        }
    }
}
