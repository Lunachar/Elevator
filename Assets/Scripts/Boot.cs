using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elevator
{
    public class Boot : MonoBehaviour
    {
        public Building Building;

        private void Start()
        {
            Person person1 = new Person(1, 15);
            Person person2 = new Person(2, 15);
            
            Debug.Log(person1);
            Debug.Log(person2);
        }
    }
}