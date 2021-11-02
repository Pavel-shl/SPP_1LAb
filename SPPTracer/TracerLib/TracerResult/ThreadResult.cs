using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace TracerLib
{
    
    [XmlType("Threade")]
    public class ThreadResult
    {
        public List<Threade> thread = new List<Threade>();
        Threade temp = new Threade();
        public List<Threade> SortingStruct(List<List<TraceResult>> structu)
        {

            
            for (int i = 0; i < structu.Count; i++)
            {
                //create thread
                Threade temp = new Threade();
                List<TraceResult> newList = new List<TraceResult>();  
                temp.id = structu[i][0].thread;
                temp.time = structu[i][0].time;
                temp.TraceResult = new List<TraceResult>();
                thread.Add(temp);
                newList = FormingListPosl(structu, temp.id);
                for (int j = 0; j < newList.Count; j++)
                {
                    List<TraceResult> copy = new List<TraceResult>(thread[i].TraceResult);
                    //ctreate list methods
                    if (j>0 && FindPrevius(j, newList, copy))
                    {
                        RecourseAdd(j, newList, copy);
                    }
                    else
                    {
                        thread[i].TraceResult.Add(newList[j]);
                        //temp = thread[i];
                    }
                }
            }

            return thread;
        }

        private void RecourseAdd( int j, List<TraceResult> structu, List<TraceResult> finder)
        {
            for (int x = 0; x < finder.Count; x++)
            {
                if (finder[x].methods != null)
                {
                    List<TraceResult> copy = new List<TraceResult>();
                    copy = finder[x].methods;
                    RecourseAdd(j, structu, copy);
                }

                if (structu[j].CallClass == finder[x].NameMethods)
                {
                    if (finder[x].methods == null)
                    {
                        finder[x].methods = new List<TraceResult>();
                        finder[x].methods.Add(structu[j]);
                    }
                    else
                        finder[x].methods.Add(structu[j]);

                }
            }
        }

        private bool FindPrevius(int j, List<TraceResult> structu, List<TraceResult> finder)
        {
            for (int x = 0; x < finder.Count; x++)
            {
                if (finder[x].methods != null)
                {
                    List<TraceResult> copy = new List<TraceResult>();
                    copy = finder[x].methods;
                    if (true == FindPrevius(j, structu, copy))
                        return true;
                }

                if (structu[j].CallClass == finder[x].NameMethods)
                {
                    return true;
                }

            }

            return false;
        }


        private List<TraceResult> FormingListPosl(List<List<TraceResult>> structu, int id)
        {
            List<TraceResult> tempe = new List<TraceResult>();
            TraceResult temp = new TraceResult();
            Stack<TraceResult> temple1 = new Stack<TraceResult>();
            Stack<TraceResult> mirow = new Stack<TraceResult>();

            temple1 = structu[0][0].poradok;
            for (; temple1.Count != 0;)
            {
                temp = temple1.Pop();
                if (temp.thread == id)
                {
                    tempe.Add(temp);
                }
                else
                    mirow.Push(temp);
            }

            for (; mirow.Count!=0 ; )
            {
                temple1.Push(mirow.Pop());
            }

            return tempe;
        }


    } 

}
