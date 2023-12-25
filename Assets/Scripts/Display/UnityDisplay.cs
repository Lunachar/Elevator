using System;
using Elevator.Interfaces;
using UnityEngine;
using Zenject;

namespace Elevator.Display
{
    public class UnityDisplay /*: MonoBehaviour, IObserver*/
    {
        public GameObject BuildingPrefab;
        public GameObject ElevatorPrefab;
        public GameObject FloorPrefab;
        public GameObject PersonPrefab;

        [Inject] private Building _building;
        [Inject] private Floor _floor;

        internal void Start()
        {
            GameObject building = Instantiate(BuildingPrefab, new Vector3(0, 0,0), Quaternion.identity);
            GameObject elevator = Instantiate(ElevatorPrefab, building.transform);
            GameObject firstFloor = Instantiate(FloorPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            firstFloor.transform.SetParent(building.transform);

                int numberOfFloors = _building._floors.Count;
                float floorHeight = 5.0f;
                for (int i = 1; i < numberOfFloors; i++)
                {
                    GameObject floor = Instantiate(FloorPrefab, new Vector3(0f, i * floorHeight, 0f), Quaternion.identity);
                    floor.transform.SetParent(building.transform);
                }
            

            // int numberOfPeople = 3;
            // for (int i = 0; i <= numberOfPeople; i++)
            // {
            //     GameObject person = Instantiate(PersonPrefab, new Vector3(0f, i * floorHeight, 0f), Quaternion.identity);
            //     person.transform.SetParent(building.transform);
            // }
        }
        public void SetBuilding(Building building)
        {
            _building = building;
        }

        // public void Update(Elevator elevator)
        // {
        //     
        // }
    }
}