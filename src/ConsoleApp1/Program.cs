using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var subscriber = new SampleSubscriber();
            var publisher = new SimpleEvent.EventBus();
            publisher.Subscribe(subscriber);
            publisher.Publish(new SampleEvent());

            Console.ReadLine();
        }
    }
}
