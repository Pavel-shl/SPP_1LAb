using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;

namespace TracerLib
{
    public class Tracer : ITracer
    {
 
        List<List<TraceResult>> AllThredsMethods = new List<List<TraceResult>>();
        TraceResult Result = new TraceResult();
        private Stack<Stopwatch> stopWatch = new Stack<Stopwatch>();
        private static object Lock = new object();
        public Stack<TraceResult> poradoki = new Stack<TraceResult>();
        public Stack<TraceResult> saver = new Stack<TraceResult>();


        public List<List<TraceResult>> GetTraceResult()
        {
            CopyStack();
            AllThredsMethods[0][0].poradok = new Stack<TraceResult>();
            AllThredsMethods[0][0].poradok = poradoki;
            return AllThredsMethods;
        }

        private void CopyStack()
        {
            if (poradoki.Count == 0)
            {
                poradoki = saver;
            }
            else
                saver = new Stack<TraceResult>(poradoki);
        }

        public void StartTrace()
        {
            lock(Lock)
            { 
                Stopwatch sw = new Stopwatch();
                sw.Start();
                stopWatch.Push(sw);

                if (CheckIsHave())
                {
                    TraceResult Result = new TraceResult();

                    StackTrace stackTrace = new StackTrace(true);
                    var frame = stackTrace.GetFrame(1);
                    Result.NameClass = frame.GetMethod().DeclaringType.Name;
                    Result.NameMethods = frame.GetMethod().Name;
                    Result.thread = Thread.CurrentThread.ManagedThreadId;
                    if (Result.NameMethods != "Main")
                    {
                        MethodBase methodBase = stackTrace.GetFrame(2).GetMethod();
                        Result.CallClass = methodBase.Name;

                    }
                    else
                    {
                        Result.CallClass = null;
                    }
                    int i = FindThread();
                    AllThredsMethods[i].Add(Result);
                
                }
                else
                {
                    List<TraceResult> threade = new List<TraceResult>();
                    TraceResult Result = new TraceResult();

                    StackTrace stackTrace = new StackTrace(true);
                    var frame = stackTrace.GetFrame(1);
                    Result.NameClass = frame.GetMethod().DeclaringType.Name;
                    Result.NameMethods = frame.GetMethod().Name;
                    Result.thread = Thread.CurrentThread.ManagedThreadId;
                    if (Result.NameMethods != "Main")
                    {
                        MethodBase methodBase = stackTrace.GetFrame(2).GetMethod();
                        Result.CallClass = methodBase.Name;

                    }
                    else
                    {
                        Result.CallClass = null;
                    }
                    threade.Add(Result);
                    int i = FindThread();
                    AllThredsMethods.Add(threade);
                }
            }
        }

        public void StopTrace()
        {
            lock (Lock)
            {
                TimeSpan ts = stopWatch.Pop().Elapsed;
                long tick = ts.Ticks;
                int ms = (int)ts.TotalMilliseconds;

                List<TraceResult> temp = new List<TraceResult>();
                temp = AllThredsMethods[FindThread()];

                TraceResult find = new TraceResult();
                StackTrace stackTrace = new StackTrace(true);
                var frame = stackTrace.GetFrame(1);
                find.NameClass = frame.GetMethod().DeclaringType.Name;
                find.NameMethods = frame.GetMethod().Name;
                find.thread = Thread.CurrentThread.ManagedThreadId;
                if (find.NameMethods != "Main")
                {
                    MethodBase methodBase = stackTrace.GetFrame(2).GetMethod();
                    find.CallClass = methodBase.Name;

                }
                else
                {
                    find.CallClass = null;
                }

                poradoki.Push(find);
                FindLast(temp, find, ms);
            }
        }

        private void FindLast(List<TraceResult> temp, TraceResult find, int ms)
        {
            for (int i = temp.Count -1; i >= 0; i--)
            {
                if (temp[i].NameClass == find.NameClass &&
                    temp[i].NameMethods == find.NameMethods &&
                    temp[i].thread == find.thread)
                {
                    find.time = ms;
                    temp[i] = find;
                }
            }
        }

        private int FindThread()
        {
            for (int i = 0; i < AllThredsMethods.Count; i++)
            {
                int temp = AllThredsMethods[i][0].thread;
                if (temp == Thread.CurrentThread.ManagedThreadId)
                {
                    return i;
                }
            }
            return 0;
        }

        private bool CheckIsHave()
        {
            for (int i = 0; i < AllThredsMethods.Count; i++)
            {
                if (AllThredsMethods[i][0].NameClass != null)
                {
                    if (Thread.CurrentThread.ManagedThreadId == AllThredsMethods[i][0].thread)
                    {
                        return true;
                    }
                } 
            }


            return false;
        }
    }
}
