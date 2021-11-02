using System;
using System.Collections.Generic;
using System.Text;

namespace SPPTracer
{
    class OutputConsole : IOutput
    {
        public void output(string xmlRes, string jsonRes)
        {
            Console.WriteLine(jsonRes);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(xmlRes);
        }
    }
}
