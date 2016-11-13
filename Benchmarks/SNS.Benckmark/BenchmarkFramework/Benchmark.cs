using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkFramework
{
    public  class Benchmark
    {
        public BenchmarkExecution[] Implementations { get; set; }
        public int Retry { get; set; }

       
        public string TestDescription { get; set; }

        public ResultHolder ResultHolder { get; private set; }

        public Benchmark(ResultHolder holder, string TestDescription)
        {
            this.ResultHolder = holder;
            this.Retry = 2;
            this.TestDescription = TestDescription;
        }

        public void Compare(object input)
        {

            if (BenchMarkContext.Current.EnableFileLog)
            {
                string[] names = new string[Implementations.Length];
                for (int i = 0; i < Implementations.Length; i++)
                {
                    names[i] = Implementations[i].GetType().Name;
                }
                BenchMarkContext.Current.LogInfo(
                    this.TestDescription,names );
            }

            long[] times = new long[Implementations.Length];
            for (int i = 0; i < times.Length; i++)
            {
                times[i] = 0;
            }

            for (int i = 0; i < this.Retry; i++)
            {

                times [i]+= Implementations[i].RunExecution(input);
                
            }

            long[] avgTimes = new long[Implementations.Length];
            for (int i = 0; i < avgTimes.Length; i++)
            {
                avgTimes[i] = times[i]/(this.Retry);
            }

            if (BenchMarkContext.Current.EnableFileLog)
            {

                BenchMarkContext.Current.LogInfo(
                    this.TestDescription, avgTimes);
            }

            ResultHolder.Results.Add(new ResultHolder.Result() {
                Execution= avgTimes,
                Testname=this.TestDescription
            });
        }
    }
}
