using System.Collections.Generic;
using Elevator.Interfaces;

namespace Elevator
{
    public class Elevator
    {
        private int Capacity { get; }
        public int CurrentFloor { get; set; }
        public List<IObserver> Observers { get; set; }

        public Elevator(int capacity)
        {
            Capacity = capacity;
            CurrentFloor = 1;
            Observers = new List<IObserver>();
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
    }
}