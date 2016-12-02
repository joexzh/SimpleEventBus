using System;

namespace SimpleEvent
{
    class DefaultSubscriber<TEvent> : ISubscriber<TEvent> where TEvent : IEvent
    {
        private readonly Action<TEvent> _action;

        public DefaultSubscriber(Action<TEvent> action)
        {
            _action = action;
        }

        public Type EventType => typeof(TEvent);

        public void HandleEvent(TEvent e)
        {
            _action(e);
        }
    }
}
