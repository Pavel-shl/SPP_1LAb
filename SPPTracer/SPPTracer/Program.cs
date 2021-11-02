using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using TracerLib;


namespace SPPTracer
{

    class Program
    {
        public static ITracer tracer = new Tracer();
        
        static void Main(string[] args)
        {           
            /* Foo foo = new Foo(tracer);
             Thread t1 = new Thread(foo.MyMethod);
             Thread t2 = new Thread(MyMethod1);
             t1.Start();
             t2.Start();
             t1.Join();
             t2.Join();
             Thread myThread = new Thread(new ThreadStart(MyMethod2));
             myThread.Start(); // запускаем поток
             myThread.Join();
             tracer.StartTrace();
             t2 = new Thread(foo.MyMethod);
             t2.Start();
             t2.Join();
             MyMethod1();
             MyMethod1();
             t2 = new Thread(MyMethod2);
             t2.Start();
             t2.Join();
             tracer.StopTrace();*/


            Thread t2 = new Thread(MyMethod1);
            t2.Start();
            t2.Join();
            tracer.StartTrace();
            MyMethod1();
            MyMethod3();
            t2 = new Thread(MyMethod2);
            t2.Start();
            t2.Join();
            tracer.StopTrace();

            var traceResultSerializer = new JsonTraceResultSerializer();
            var traceResultSerializer2 = new XMLTraceResultSerializer();

            List<List<TraceResult>> tempe = new List<List<TraceResult>>(tracer.GetTraceResult());
            List<Threade> res = new List<Threade>(), copy = new List<Threade>();
            ThreadResult st1 = new ThreadResult();

            res = st1.SortingStruct(tempe);
            copy = new List<Threade>(res);
            string jsonRes = traceResultSerializer.Serialize(res);

            string xmlRes = traceResultSerializer2.Serialize(copy);

            OutputConsole con = new OutputConsole();
            OutputFile fil = new OutputFile();

            con.output(xmlRes, jsonRes);
            fil.output(xmlRes, jsonRes);


        }

        public static void MyMethod1()
        {
            tracer.StartTrace();
            MyMethod2();
            System.Threading.Thread.Sleep(500);
            tracer.StopTrace();
          
        }

        public static void MyMethod2()
        {
            tracer.StartTrace();
            System.Threading.Thread.Sleep(250);
            tracer.StopTrace();
        }

        public static void MyMethod3()
        {
            tracer.StartTrace();
            var t2 = new Thread(MyMethod2);
            t2.Start();
            t2.Join();
            MyMethod2();
            MyMethod4();
            System.Threading.Thread.Sleep(250);
            tracer.StopTrace();
        }


        public static void MyMethod4()
        {
            tracer.StartTrace();
            MyMethod2();
            System.Threading.Thread.Sleep(250);
            tracer.StopTrace();
        }


    }

    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            Thread thread = new Thread(_bar.InnerMethod);
            thread.Start();
            thread.Join();
            Thread.Sleep(10);
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
            Thread.Sleep(20);
            _tracer.StopTrace();
        }
    }


}
