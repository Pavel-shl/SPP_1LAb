using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib
{
    public interface ITraceResultSerializer
    {
        string Serialize(List<Threade> traceResult);
    }
}
