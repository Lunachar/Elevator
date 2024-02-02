using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class PersonGO : MonoBehaviour
    {
        public TMP_Text _text;
        private Person _person;
        private Building _building;

        [Inject]
        public void Construct(Building building)
        {
            _building = building;
        }
        private void Start()
        {
            //_person = _building._floorList.GetFloors().;
            _text.text = SetPersonName();
        }

        private string SetPersonName()
        {
            return _person.GetPersonName();
        }
    }
}