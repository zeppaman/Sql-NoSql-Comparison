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
    public class SqlTransaction : BenchmarkExecution
    {
        public override void Execute(object input)
        {
            int transactions = (int)input;

            using (IStatelessSession s = NHibernateContext.Current.SessionFactory.OpenStatelessSession())
            {
                Guid IdUnableToFind = Guid.NewGuid();



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
