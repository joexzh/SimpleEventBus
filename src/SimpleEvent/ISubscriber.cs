using System;

namespace SimpleEvent
{
    public interface ISubscriber<in TEvent> where TEvent : IEvent
    {
        void HandleEvent(TEvent e);

        Type EventType { get; }
    }
}
