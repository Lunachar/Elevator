
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using Unity.VisualScripting;


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
           while (TargetFloor == CurrentFloor)
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
