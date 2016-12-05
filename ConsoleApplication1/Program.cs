using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleEvent;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            EventBus.Instance.SubscribeGlobal(new SampleSubscriber());
            SimpleEvent.EventBus.Instance.Publish(new SampleEvent());
            Console.WriteLine();
            Console.ReadLine();

            EventBus.Instance.SubscribeLocal(new SampleSubscriber());
            EventBus.Instance.SubscribeLocal(new SampleSubscriber());
            SimpleEvent.EventBus.Instance.Publish(new SampleEvent());

            Console.WriteLine();
            Console.ReadLine();

            Task.Run(() =>
            {
                EventBus.Instance.SubscribeLocal(new SampleSubscriber());
                SimpleEvent.EventBus.Instance.Publish(new SampleEvent());
                EventBus.Instance.Reset();
            });
            EventBus.Instance.Reset();
            Console.ReadLine();
        }
    }
}
