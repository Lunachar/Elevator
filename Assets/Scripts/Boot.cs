using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elevator
{
    public class Boot : MonoBehaviour
    {
        public Building Building;
        public int NumberOfFloors;
        public int ElevatorCapacity;
        public int MaxPeoplePerFloor;

        private void Start()
        {
            Building = new Building(NumberOfFloors, ElevatorCapacity, MaxPeoplePerFloor);
            ShowFloorDetails();
        }

        private void ShowFloorDetails()
        {
            foreach (var floor in Building.Floors)
            {
                Debug.Log($"Floor {floor.Number}, peoples {floor.GetPersonsList().Count}: ");

                foreach (var person in floor.GetPersonsList())
                {
                    Debug.Log(person.ToString());
                }
            }
        }
    }
}