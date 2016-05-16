using SimpleEvent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleEvent
{
    public class EventBus
    {
        private static ThreadLocal<IList> _subscribers = new ThreadLocal<IList>(() => new List<object>());
        private static ThreadLocal<object> _safeLock = new ThreadLocal<object>(() => new object());
        private object _lockObj { get { return _safeLock.Value; } }

        public static EventBus Instance { get { return Singleton.GetInstance(); } }

        public void Publish<TEvent>(TEvent e) where TEvent : IEvent
        {
            lock (_lockObj)
            {
                foreach (ISubscriber<TEvent> subscriber in _subscribers.Value)
                {
                    if (subscriber.EventType == typeof(TEvent))
                    {
                        subscriber.HandleEvent(e);
                    }
                }
            }
        }

        public void Subscribe<TEvent>(ISubscriber<TEvent> subscriber) where TEvent : IEvent
        {
            lock (_lockObj)
            {
                _subscribers.Value.Add(subscriber);
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent
        {
            lock (_lockObj)
            {
                var defaultSbuscriber = new DefaultSubscriber<TEvent>(action);
                _subscribers.Value.Add(defaultSbuscriber);
            }
        }

        public void Reset()
        {
            lock (_lockObj)
            {
                _subscribers.Value.Clear();
            }
        }

        class Singleton
        {
            private static EventBus _eventBus = new EventBus();
            public static EventBus GetInstance()
            {
                return _eventBus;
            }
        }
    }
}
