using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkFramework
{
    public class ResultHolder
    {
        public  class Result
        {
            public string Testname { get; set; }
            public long[] Execution { get; set; }
        }

        public List<Result> Results { get; set; }

        public ResultHolder()
        {
            Results = new List<Result>();
        }
    }
}
