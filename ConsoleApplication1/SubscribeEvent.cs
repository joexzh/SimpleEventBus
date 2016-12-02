using SimpleEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class SubscribeEvent
    {
        static SubscribeEvent()
        {
            EventBus.Instance.Subscribe(new SampleSubscriber());
        }
    }
}
