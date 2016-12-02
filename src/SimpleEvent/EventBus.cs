using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace SimpleEvent
{
    public class EventBus
    {
        private static readonly ThreadLocal<IList> Subscribers = new ThreadLocal<IList>(() => new List<object>());
        private static readonly ThreadLocal<object> SafeLock = new ThreadLocal<object>(() => new object());
        private static object LockObj => SafeLock.Value;

        private static IList _globalSubscribers = new List<object>();

        public static EventBus Instance => Singleton.GetInstance();

        private EventBus()
        {
            //todo add globle subscribers to local

        }

        public void Publish<TEvent>(TEvent e) where TEvent : IEvent
        {
            lock (LockObj)
            {
                foreach (ISubscriber<TEvent> subscriber in Subscribers.Value)
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
            lock (LockObj)
            {
                Subscribers.Value.Add(subscriber);
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent
        {
            lock (LockObj)
            {
                var defaultSbuscriber = new DefaultSubscriber<TEvent>(action);
                Subscribers.Value.Add(defaultSbuscriber);
            }
        }

        public void Reset()
        {
            lock (LockObj)
            {
                Subscribers.Value.Clear();
            }
        }

        class Singleton
        {
            private static readonly EventBus EventBus = new EventBus();
            public static EventBus GetInstance()
            {
                return EventBus;
            }
        }
    }
}
