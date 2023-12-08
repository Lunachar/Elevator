// using System;
// using System.Collections.Generic;
// using RandomDataGenerator.FieldOptions;
// using RandomDataGenerator.Randomizers;
//
// namespace Elevator
// {
//     public class People
//     {
//         public List<Person> Persons;
//
//         private int _currentFloor;
//         public int CurrentFloor
//         {
//             get => _currentFloor;
//             set
//             {
//                 if (value >= 1 && value <= 15)
//                 {
//                     _currentFloor = value;
//                 }
//                 else
//                 {
//                     _currentFloor = value < 1 ? 1 : 15;
//                 }
//             }
//         }
//         private int _targetFloor;
//
//         public int TargetFloor
//         {
//             get => _targetFloor;
//             set
//             {
//                 if (value >= 1 && value <= 15)
//                 {
//                     _targetFloor = value;
//                 }
//                 else
//                 {
//                     _targetFloor = value < 1 ? 1 : 15;
//                 }
//             }
//         }
//
//         public People(int currentFloor, int floorAmount)
//         {
//             CurrentFloor = currentFloor;
//
//             // var targetFloor = new RandomizerNumber<int>(new FieldOptionsInteger()
//             // {
//             //     Min = 1,
//             //     Max = floorAmount
//             // });
//             // TargetFloor = targetFloor.Generate().GetValueOrDefault();
//
//
//
//             Persons = new List<Person>();
//             
//             for (int i = 0; i < numberOfPeople; i++)
//             {
//                 Persons.Add(new Person(currentFloor, floorAmount));
//             }
//         }
//
//         public List<Person> GetPersonsList()
//         {
//             return Persons;
//         }
//         
//     }
// }