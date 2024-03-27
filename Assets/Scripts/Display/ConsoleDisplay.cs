using System;
using Elevator.Interfaces;
using UnityEngine;
using Zenject;

namespace Elevator.Display
{
    /// <summary>
    /// Responsible for displaying floor details in the console.
    /// </summary>
    public class ConsoleDisplay
    {
        private Building _building;

        public ConsoleDisplay(Building building)
        {
            _building = building;
        }
        
        public void ShowFloorDetailsInConsole()
        {
            if (_building == null) return;
            foreach (var floor in _building._floorList.GetFloors())
            {
                Debug.Log($"Floor {floor.Number}, peoples {floor.GetPersonsListOnFloor().Count}: ");

                foreach (var person in floor.GetPersonsListOnFloor())
                {
                    Debug.Log(person.ToString());
                }
            }
        }
    }
}