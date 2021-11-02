using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace TracerLib
{
    public class JsonTraceResultSerializer : ITraceResultSerializer
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public string Serialize(List<Threade> traceResult)
        {
            //ThreadResult st1 = new ThreadResult();
            //st1.SortingStruct(traceResult);
            return JsonSerializer.Serialize(traceResult, options);
            //return JsonSerializer.Serialize(traceResult, options);
        }
    }
}
