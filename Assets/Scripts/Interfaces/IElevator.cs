namespace Elevator.Interfaces
{
    public interface IElevator
    {
        int Capacity { get; set; }
        void CallTo(int floorNumber);
        void MoveTo(int floorNumber);
        //
        // void SetCapacity(int capacity);
    }
}