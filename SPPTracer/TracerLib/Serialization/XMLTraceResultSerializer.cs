using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


namespace TracerLib
{
    public class XMLTraceResultSerializer : ITraceResultSerializer
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Threade));
        private static readonly XmlSerializer Serializer2 = new XmlSerializer(typeof(TraceResult));

        public string Serialize(List<Threade> res)
        {

            StringWriter stringWriter = new StringWriter();

            for (int i = 0; i < res.Count; i++)
            {
                Serializer.Serialize(stringWriter, res[i]);
                //SerealizeElement(results[i].TraceResult);
            }
            return stringWriter.ToString();

        }


        private void SerealizeElement(List<TraceResult> res) 
        {

            for (int x = 0; x < res.Count; x++)
            {
                StringWriter stringWriter = new StringWriter();
                Serializer2.Serialize(stringWriter, res[x]);
                
            }
        }
    }
}
