using System;
using System.Collections.Generic;
using System.Text;

namespace SPPTracer
{
    interface IOutput
    {
        void output(string xmlRes, string jsonRes);
    }
}
