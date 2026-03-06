using System;
using System.Collections.Generic;
using UnityEngine;

namespace CopGameDev.RPG.Core.Events
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly SortedDictionary<EventPriority, HashSet<Action<T>>> handlers =
            new SortedDictionary<EventPriority, HashSet<Action<T>>>(Comparer<EventPriority>.Create((a, b) => b.CompareTo(a)));

        static EventBus()
        {
            handlers.Add(EventPriority.Lowest, new HashSet<Action<T>>());
            handlers.Add(EventPriority.Low, new HashSet<Action<T>>());
            handlers.Add(EventPriority.Normal, new HashSet<Action<T>>());
            handlers.Add(EventPriority.High, new HashSet<Action<T>>());
            handlers.Add(EventPriority.Highest, new HashSet<Action<T>>());
            handlers.Add(EventPriority.Monitor, new HashSet<Action<T>>());
        }

        public static void Register(Action<T> handler, EventPriority priority = EventPriority.Normal)
        {
            if (handlers[priority].Contains(handler))
                return;

            handlers[priority].Add(handler);
        }

        public static void Deregister(Action<T> handler)
        {
            foreach (var key in handlers.Keys)
            {
                handlers[key].Remove(handler);
            }
        }

        public static void Raise(T @event)
        {
            var snapshot = new Dictionary<EventPriority, HashSet<Action<T>>>();
            foreach (var kvp in handlers)
            {
                snapshot[kvp.Key] = new HashSet<Action<T>>(kvp.Value);
            }

            foreach (var kvp in snapshot)
            {
                var priority = kvp.Key;
                foreach (var handler in kvp.Value)
                {
                    if (!handlers[priority].Contains(handler))
                        continue;

                    handler?.Invoke(@event);
                }
            }
        }

        private static void Clear()
        {
            Debug.Log($"Clearing {typeof(T).Name} handlers");
            foreach (var handlerSet in handlers.Values)
            {
                handlerSet.Clear();
            }
        }
    }

    public enum EventPriority
    {
        Lowest = 0,
        Low = 1,
        Normal = 2,
        High = 3,
        Highest = 4,
        Monitor = 5
    }
}
