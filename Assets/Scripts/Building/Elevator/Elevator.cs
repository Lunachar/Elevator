using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elevator.Interfaces;
using Elevator.Interfaces.EventBus;

namespace Elevator
{
    /// <summary>
    /// Represents the elevator in the building.
    /// </summary>
    public class Elevator : IElevator, IObserverble
    {
        private int _currentFloor;                          // Current floor of the elevator
        public int CurrentFloor
        {
            get { return _currentFloor; }
            set { _currentFloor = value; }
        }

        public bool IsMoving;                             // Indicates whether the elevator is moving
        
        
        public int Capacity { get; set; }                   // Maximum capacity of the elevator
        
        private List<IObserver> _observers { get; set; }    // List of observers subscribed to elevator events
        // private float moveDuration = 0.2f;                  // Duration of elevator movement between floors
        
        
        private int _filling;                               // Current number of people inside the elevator

        public int Filling                                  // Property to get or set the current filling of the elevator
        {
            get => _filling;
            set => _filling = Mathf.Clamp(value, 0, Capacity);
        }

        public ElevatorGO ElevatorGo;                       // Reference to the ElevatorGO prefab
        

        
        
        /// <summary>
        /// Constructor for the Elevator class.
        /// </summary>
        /// <param name="boot">Reference to the Boot scriptable object.</param>
        /// <param name="elevatorGo">Reference to the ElevatorGO prefab.</param>
        public Elevator(Boot boot, ElevatorGO elevatorGo)
        {
            // Initialize elevator properties
            Capacity = boot.ElevatorCapacity();
            CurrentFloor = 1;
            _observers = new List<IObserver>();
            ElevatorGo = elevatorGo;
            //_boot = boot;
        }

        /// <summary>
        /// Attaches an observer to the elevator.
        /// </summary>
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        /// <summary>
        /// Detaches an observer from the elevator.
        /// </summary>
        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        /// <summary>
        /// Notifies all attached observers about elevator status changes.
        /// </summary>
        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.UpdateElevatorStatus(this);
            }
        }

              
    }
}
