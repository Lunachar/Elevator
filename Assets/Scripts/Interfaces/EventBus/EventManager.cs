using System.Collections.Generic;
using UnityEngine;

namespace Elevator.Interfaces.EventBus
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance
        {
            get { return instance; }
            set { }
        }

        private static EventManager instance = null;

        private Dictionary<EVENT_TYPE, List<IListener>> listeners = new Dictionary<EVENT_TYPE, List<IListener>>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        // Add listener in list
        public void AddListener(EVENT_TYPE EventType, IListener Listener)
        {
            List<IListener> ListenList = null;
            if (listeners.TryGetValue(EventType, out ListenList))
            {
                // List is already exist, add new element
                ListenList.Add(Listener);
                return;
            }

            // else, create new list as a dictionary key
            ListenList = new List<IListener>();
            ListenList.Add(Listener);
            listeners.Add(EventType, ListenList);
        }

        // Send event to all listeners
        public void PostNotification(
            EVENT_TYPE eventType,
            Component sender,
            object param = null)
        {
            List<IListener> ListenList = null;

            // If there is no listeners - exit
            if (!listeners.TryGetValue(eventType, out ListenList))
            {
                return;
            }

            // There are some listeners. Send them notification
            for (int i = 0; i < ListenList.Count; i++)
            {
                if (!ListenList[i].Equals(null))
                {
                    ListenList[i].OnEvent(eventType, sender, param);
                }
            }
        }


        // Remove event from dictionary, including all listeners
        public void RemoveEvent(EVENT_TYPE eventType)
        {
            // Remove event from dictionary
            listeners.Remove(eventType);
        }

        // Remove redundant listeners from dictionary
        public void RemoveRedundancies()
        {
            // Create new dictionary
            Dictionary<EVENT_TYPE, List<IListener>> TmpListeners = new Dictionary<EVENT_TYPE, List<IListener>>();

            // Loop through dictionary
            foreach (KeyValuePair<EVENT_TYPE, List<IListener>> Item in listeners)
            {
                // Loop through listeners and remove null values
                for (int i = Item.Value.Count - 1; i >= 0; i--)
                {
                    if (Item.Value[i].Equals(null))
                    {
                        Item.Value.RemoveAt(i);
                    }
                }

                // If there are items left, add to tmp dictionary
                if (Item.Value.Count > 0)
                {
                    TmpListeners.Add(Item.Key, Item.Value);
                }

                // Replace old dictionary with new one
                listeners = TmpListeners;
            }

            void OnLevelWasLoaded()
            {
                RemoveRedundancies();
            }
        }
    }
}