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

        public BenchmarkSuite AddRunnable(object input, string testname,  params BenchmarkExecution[] implementations)
        {
            Benchmark b = new Benchmark(this.ResultHolder, testname);
            b.Implementations = implementations;

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

            string title = "Testname;";
            for (int i = 0; i < this.Benchmarks.Count; i++)
            {
                for (int k = 0; k < Benchmarks[i].BenchmarkToRun.Implementations.Length; k++)
                {
                    title += Benchmarks[i].BenchmarkToRun.Implementations[k].GetType().Name + ";";
                }
                break;
            }
            Console.WriteLine(title);
            Console.WriteLine("************************************");
            foreach (var res in this.ResultHolder.Results)
            {
                
                string line = res.Testname+";";

                for (int i = 0; i < res.Execution.Length; i++)
                {
                    line += res.Execution[i] + ";";
                }

                Console.WriteLine(line);

            }

            Console.WriteLine("====================================");
        }
    }
}
