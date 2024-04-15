using UnityEngine;

namespace Elevator.Interfaces.EventBus
{
    public enum EVENT_TYPE // All events are defined here
    {
        GAME_INIT,
        GAME_END,
        ELEVATOR_IS_MOVING,
        FLOOR_CHANGED
    }
    public interface IListener
    {
        void OnEvent(EVENT_TYPE EventType,
            Component Sender, object Param = null);
    }
}