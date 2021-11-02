using Microsoft.VisualStudio.TestTools.UnitTesting;
using TracerLib;
using System.Threading;
using System.Collections.Generic;

namespace TracerTest
{
    [TestClass]
    public class TracerTest
    {
        private ITracer tracer;
        private Foo foo;
        private Bar bar;

        [TestInitialize]
        public void Initialize()
        {
            tracer = new Tracer();
            foo = new Foo(tracer);
            bar = new Bar(tracer);
        }

        [TestMethod]
        public void OneThreadTest()
        {
            foo.MyMethod();
            List<List<TraceResult>> traceResult = tracer.GetTraceResult();
            Assert.AreEqual(traceResult.Count, 1);

        }

        [TestMethod]
        public void TwoThreadsTest()
        {
            Thread t1 = new Thread(foo.MyMethod);
            t1.Start();
            t1.Join();
            foo.MyMethod();

            List<List<TraceResult>> traceResult = tracer.GetTraceResult();
            Assert.AreEqual(traceResult.Count, 2);

        }

        [TestMethod]
        public void ThreeThreadsTest()
        {
            Thread t1 = new Thread(foo.MyMethod);
            Thread t2 = new Thread(foo.MyMethod);
            t1.Start();
            t1.Join();
            t2.Start();
            t2.Join();
            foo.MyMethod();

            List<List<TraceResult>> traceResult = tracer.GetTraceResult();
            Assert.AreEqual(traceResult.Count, 3);

        }

        [TestMethod]
        public void NumberMemberTest()
        {

            tracer.StartTrace();
            foo.MyMethod();
            bar.InnerMethod();
            foo.MyMethod();
            bar.InnerMethod();
            tracer.StopTrace();

            List<List<TraceResult>> traceResult = tracer.GetTraceResult();
            ThreadResult st1 = new ThreadResult();
            List<Threade> ResFinaly = new List<Threade>();

            ResFinaly = st1.SortingStruct(traceResult);
            Assert.AreEqual(ResFinaly[0].TraceResult[0].methods.Count, 4);

        }

    }


    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        public ITracer Tracer => _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            _bar.InnerMethod();
            _tracer.StopTrace();
        }

    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(30);
            _tracer.StopTrace();
        }
    }
}
