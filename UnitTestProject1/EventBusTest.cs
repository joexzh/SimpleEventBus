using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleEvent;

namespace UnitTestProject1
{
    [TestClass]
    public class EventBusTest
    {


        public EventBusTest()
        {
            

        }

        [TestMethod]
        public void TestMethod1()
        {
            var subscriber = new SampleSubscriber();
            var publisher = new SimpleEvent.EventBus();
            publisher.Subscribe(subscriber);
            publisher.Publish(new SampleEvent());
        }
    }
}
