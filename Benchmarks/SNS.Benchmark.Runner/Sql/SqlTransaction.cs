using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkFramework;
using NHibernate;
using NHibernate.Linq;
using SNS.Benchmark.Runner.Entities;
using SNS.Benchmark.Runner.NoSql;
using SNS.Benchmark.Runner.Sql.Entities;

namespace SNS.Benchmark.Runner.Sql
{
    /// <summary>
    /// this test save data in tranction
    /// </summary>
    public class SqlTransaction : BenchmarkExecution
    {
        /// <summary>
        /// Execute the test
        /// </summary>
        /// <param name="input">number of transaction to execute</param>
        public override void Execute(object input)
        {
            int transactions = (int)input;

            using (IStatelessSession s = NHibernateContext.Current.SessionFactory.OpenStatelessSession())
            {
                

                for (int i = 0; i < transactions; i++)
                {
                    var t = s.BeginTransaction();

                    Master m = DataGenerator.NextMaster();

                    List<Detail> details = DataGenerator.Details(m.MasterId, 20);

                    s.Insert(m);
                    foreach (Detail d in details)
                    {
                        s.Insert(d);
                    }

                    t.Commit();
                    

                }
            }

        }
    }
}
