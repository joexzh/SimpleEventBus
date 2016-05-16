using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnitTestProject1
{
    class SampleEvent : SimpleEvent.IEvent
    {
        public string SayHello { get; } = "Hello world";
    }

    class SampleSubscriber : SimpleEvent.ISubscriber<SampleEvent>
    {
        public Type EventType
        {
            get
            {
                return typeof(SampleEvent);
            }
        }

        public void HandleEvent(SampleEvent e)
        {
            System.Diagnostics.Debug.WriteLine(e.SayHello);
        }
    }
}
