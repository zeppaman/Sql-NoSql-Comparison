using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkFramework
{
    public  class Benchmark
    {
        public BenchmarkExecution SqlImplementation { get; set; }
        public BenchmarkExecution NoSqlImplementation { get; set; }
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
            long sqlTime = 0;
            long noSqlTime = 0;

            for (int i = 1; i <= this.Retry; i++)
            {
                //execution order is swithcet do prevent test influences
                if (i % 2 == 0)
                {
                    sqlTime+= SqlImplementation.RunExecution(input);
                    noSqlTime += NoSqlImplementation.RunExecution(input);
                }
                else
                {                    
                    noSqlTime += NoSqlImplementation.RunExecution(input);
                    sqlTime += SqlImplementation.RunExecution(input);
                }
            }

            ResultHolder.Results.Add(new ResultHolder.Result() {
                NoSqlExecution=noSqlTime/this.Retry,
                SqlExecution = sqlTime / this.Retry,
                Testname=this.TestDescription
            });
        }
    }
}
