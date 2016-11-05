using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkFramework
{
    public class BenchmarkSuite
    {
        public List<BenchmarkRunnable> Benchmarks = new List<BenchmarkRunnable>();
        public ResultHolder ResultHolder = new ResultHolder();

        public BenchmarkSuite()
        {
        }

        public BenchmarkSuite AddRunnable(object input, string testname, BenchmarkExecution noSqlImplementation, BenchmarkExecution sqlImplementation)
        {
            Benchmark b = new Benchmark(this.ResultHolder, testname);
            b.NoSqlImplementation = noSqlImplementation;
            b.SqlImplementation = sqlImplementation;

            return AddRunnable(b, input);

        }

        /// <summary>
        /// Attention: benchmark may live outside ResultHolder
        /// </summary>
        /// <param name="benchmark"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public BenchmarkSuite AddRunnable(Benchmark benchmark, object input)
        {


            BenchmarkRunnable br = new BenchmarkRunnable();
            br.BenchmarkToRun = benchmark;
            br.Input = input;

            this.Benchmarks.Add(br);
            return this;
        }


        public void ExecuteAll()
        {
            foreach (BenchmarkRunnable item in Benchmarks)
            {
                item.BenchmarkToRun.Compare(item.Input);
            }
        }

        public void WriteResultToConsole()
        {
            Console.WriteLine("====================================");
            Console.WriteLine("************************************");
            Console.WriteLine("Testname;NoSQL;SQL;");
            Console.WriteLine("************************************");
            foreach (var res in this.ResultHolder.Results)
            {

                Console.WriteLine("{0},{1},{2}", res.Testname, res.NoSqlExecution, res.SqlExecution);

            }

            Console.WriteLine("====================================");
        }
    }
}
