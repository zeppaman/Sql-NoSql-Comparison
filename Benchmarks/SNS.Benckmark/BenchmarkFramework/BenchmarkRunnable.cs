using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkFramework
{
    public class BenchmarkRunnable
    {
        public object Input { get; set; }
        public Benchmark BenchmarkToRun {get;set;}
    }
}
