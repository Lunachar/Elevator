using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class PersonGO : MonoBehaviour
    {
        public TMP_Text _text;
        private Building _building;

        [Inject]
        private Person _person;
        
        private void Start()
        {
            //_person = _building._floorList.GetFloors().;
            _text.text = SetPersonName();
            Debug.Log($"||TEXT: {_text.text}");
        }

        private string SetPersonName()
        {
            return _person.GetPersonName();
        }
    }
}