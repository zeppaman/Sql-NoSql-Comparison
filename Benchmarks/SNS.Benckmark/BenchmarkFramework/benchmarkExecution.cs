using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkFramework
{
    public abstract class BenchmarkExecution
    {
        public abstract void Execute(object input);


        public long RunExecution(object input)
        {
            Stopwatch stp = new Stopwatch();
            stp.Start();
            Execute(input);
            stp.Stop();

            return stp.ElapsedMilliseconds;
        }
    }
}
