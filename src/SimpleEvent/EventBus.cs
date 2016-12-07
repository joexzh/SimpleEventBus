using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace SimpleEvent
{
    public class EventBus : IDisposable
    {
        private static readonly ThreadLocal<IList<object>> Subscribers = new ThreadLocal<IList<object>>(() => new List<object>());
        private static readonly IList<object> _globalSubscribers = new List<object>();

        private static readonly ThreadLocal<object> LocalLock = new ThreadLocal<object>(() => new object());
        private static readonly object GlobalLock = new object();
        private static object LockObj => LocalLock.Value;

        public static EventBus Instance => Singleton.GetInstance();

        private EventBus()
        {
        }

        public void Publish<TEvent>(TEvent e) where TEvent : IEvent
        {
            lock (LockObj)
            {
                foreach (ISubscriber<TEvent> subscriber in GetSubscribers())
                {
                    if (subscriber.EventType == typeof(TEvent))
                    {
                        subscriber.HandleEvent(e);
                    }
                }
            }
        }

        /// <summary>
        /// Subscribe to local thread
        /// </summary>
        /// <param name="subscriber"></param>
        public void SubscribeLocal<TEvent>(ISubscriber<TEvent> subscriber) where TEvent : IEvent
        {
            lock (LockObj)
            {
                Subscribers.Value.Add(subscriber);
            }
        }

        /// <summary>
        /// Subscribe to local thread
        /// </summary>
        /// <param name="action"></param>
        public void SubscribeLocal<TEvent>(Action<TEvent> action) where TEvent : IEvent
        {
            lock (LockObj)
            {
                var defaultSbuscriber = new DefaultSubscriber<TEvent>(action);
                Subscribers.Value.Add(defaultSbuscriber);
            }
        }

        /// <summary>
        /// You should use it only when your app started.
        /// </summary>
        /// <param name="subscriber"></param>
        public void SubscribeGlobal<TEvent>(ISubscriber<TEvent> subscriber) where TEvent : IEvent
        {
            lock (GlobalLock)
            {
                _globalSubscribers.Add(subscriber);
            }
        }

        public void SubscribeGlobal<TEvent>(Action<TEvent> action) where TEvent : IEvent
        {
            lock (LockObj)
            {
                var defaultSbuscriber = new DefaultSubscriber<TEvent>(action);
                _globalSubscribers.Add(defaultSbuscriber);
            }
        }

        public void Reset()
        {
            lock (LockObj)
            {
                Subscribers.Value.Clear();
            }
        }

        private IList<object> GetSubscribers()
        {
            var s = new List<object>();
            s.AddRange(_globalSubscribers);
            s.AddRange(Subscribers.Value);
            return s;
        }

        public void Dispose()
        {
            Reset();
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
