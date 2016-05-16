using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleEvent
{
    class DefaultSubscriber<TEvent> : ISubscriber<TEvent> where TEvent : IEvent
    {
        private Action<TEvent> _action;

        public DefaultSubscriber(Action<TEvent> action)
        {
            _action = action;
        }

        public Type EventType
        {
            get
            {
                return typeof(TEvent);
            }
        }

        public void HandleEvent(TEvent e)
        {
            _action(e);
        }
    }
}
