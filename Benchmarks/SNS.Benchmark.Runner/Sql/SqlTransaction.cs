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
            
            ISession s=NHibernateContext.Current.SessionFactory.OpenSession();
            Guid IdUnableToFind = Guid.NewGuid();

            var fistItem = s.Query<Category>().Take(1).FirstOrDefault();

            for (int i = 0; i < transactions; i++)
            {
                var t=s.BeginTransaction();

                Master m = DataGenerator.NextMaster();
                m.CategoryId = fistItem.CategoryId;
                List<Detail> details = DataGenerator.Details(m.MasterId, 20);

                s.SaveOrUpdate(m);
                foreach (Detail d in details)
                {
                    s.SaveOrUpdate(d);
                }

                t.Commit();

            }

        }
    }
}
