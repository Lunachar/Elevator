﻿using System.Collections.Generic;
using Elevator.Interfaces;

namespace Elevator
{
    public class Elevator : IElevator
    {
        public int Capacity { get; set; }
        public int CurrentFloor { get; set; }
        public List<IObserver> Observers { get; set; }

        // public Elevator(int capacity)
        // {
        //     Capacity = capacity;
        //     CurrentFloor = 1;
        //     Observers = new List<IObserver>();
        // }

        public void SetCapacity(int capacity)
        {
            Capacity = capacity;
        }

        public void Attach(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in Observers)
            {
                observer.Update(this);
            }
        }

        public void CallTo(int floorNumber)
        {
            throw new System.NotImplementedException();
        }

        public void MoveTo(int floorNumber)
        {
            throw new System.NotImplementedException();
        }
    }
}