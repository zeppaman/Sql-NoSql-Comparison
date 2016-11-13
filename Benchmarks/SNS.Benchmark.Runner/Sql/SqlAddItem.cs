using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkFramework;
using NHibernate;
using SNS.Benchmark.Runner.Entities;
using SNS.Benchmark.Runner.NoSql;
using SNS.Benchmark.Runner.Sql.Entities;

namespace SNS.Benchmark.Runner.Sql
{
    public class SqlAddItem : BenchmarkExecution
    {
        public override void Execute(object input)
        {
            List<Category> itemsToAdd = new List<Category>();

            if (input is List<Category>)
            {
                itemsToAdd = (List<Category>)input;
            }
            else
            {
                int addCount = (int)input;
                for (int i = 0; i < addCount; i++)
                {
                    itemsToAdd.Add(DataGenerator.NextCategory());
                }
            }

            using (IStatelessSession s = NHibernateContext.Current.SessionFactory.OpenStatelessSession())
            {
                var v = s.BeginTransaction();

                for (int i = 0; i < itemsToAdd.Count; i++)
                {
                    s.Insert(itemsToAdd[i]);
                    if (i % 100000 == 0)
                    {

                        v.Commit();
                        v = s.BeginTransaction();

                    }
                }
                v.Commit();
                s.Close();
            }
        }
    }
}
