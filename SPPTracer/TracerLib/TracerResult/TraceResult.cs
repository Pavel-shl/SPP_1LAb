using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace TracerLib
{
    [Serializable]
    [XmlType("TraceResult")]
    public class TraceResult
    {
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        public string NameMethods { get; set; }

        [JsonPropertyName("class")]
        [XmlAttribute("class")]
        public string NameClass { get; set; }

        [XmlAttribute("time")]
        [JsonPropertyName("time")]
        public int time { get; set; }

        [XmlElement("methods")]
        [JsonPropertyName("methods")]
        public List<TraceResult> methods { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public int thread { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public string CallClass { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public Stack<TraceResult> poradok { get; set; }
    }
}
