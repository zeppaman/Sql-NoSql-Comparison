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
    /// <summary>
    /// Add a set of item use bulk insert
    /// </summary>
    public class SqlAddItem : BenchmarkExecution
    {
        private const int bulkSize = 100000;
        /// <summary>
        /// Insert data in bulk transaction. 
        /// When input is integer many items are randomly generated, when a list of category is given input is saved directly.
        /// </summary>
        /// <param name="input"></param>
        public override void Execute(object input)
        {
            List<Category> itemsToAdd = new List<Category>();

            // this to allow this test to be called from outside with data or used for generate data
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
                    if (i % bulkSize == 0)
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
