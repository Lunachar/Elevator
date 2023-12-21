using System;
using Elevator.Interfaces;
using UnityEngine;
using Zenject;

namespace Elevator.Display
{
    public class ConsoleDisplay /*: MonoBehaviour, IObserver*/
    {
        private Building _building;

        public void SetBuilding(Building building)
        {
            _building = building;
        }

        public void ShowFloorDetailsInConsole()
        {
            if (_building != null)
            {
                foreach (var floor in _building._floors)
                {
                    Debug.Log($"Floor {floor.Number}, peoples {floor.GetPersonsListOnFloor().Count}: ");

                    foreach (var person in floor.GetPersonsListOnFloor())
                    {
                        Debug.Log(person.ToString());
                    }
                }
            }
        }
        private void Start()
        {

        }

        public void Update()
        {

        }
    }
}