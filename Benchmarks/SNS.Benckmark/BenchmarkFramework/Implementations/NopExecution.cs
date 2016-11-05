using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BenchmarkFramework.Implementations
{
    public class NopExecution : BenchmarkExecution
    {
        public int Delay{get;set;}
        public NopExecution(int delay)
        {
            this.Delay = delay;
        }
        public override void Execute(object input)
        {
            Thread.Sleep(this.Delay);
          
        }
    }
}
