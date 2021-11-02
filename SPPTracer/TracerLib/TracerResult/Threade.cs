using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Text.Json.Serialization;

namespace TracerLib
{
    [Serializable]
    [XmlType("Threade")]
    public class Threade
    {

        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public int id { get; set; }

        [XmlAttribute("time")]
        [JsonPropertyName("time")]
        public int time { get; set; }

        [XmlElement("methods")]
        [JsonPropertyName("methods")]
        public List<TraceResult> TraceResult { get; set; }
    }
}
