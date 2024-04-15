using Elevator.Interfaces;

namespace Elevator
{
    public interface IObserverble
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
}