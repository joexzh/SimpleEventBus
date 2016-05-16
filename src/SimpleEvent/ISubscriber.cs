using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleEvent
{
    public interface ISubscriber<TEvent> where TEvent : IEvent
    {
        void HandleEvent(TEvent e);

        Type EventType { get; }
    }
}
