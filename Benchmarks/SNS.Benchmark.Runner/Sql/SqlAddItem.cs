using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkFramework;
using NHibernate;
using SNS.Benchmark.Runner.Entities;
using SNS.Benchmark.Runner.NoSql;

namespace SNS.Benchmark.Runner.Sql
{
    public class SqlAddItem : BenchmarkExecution
    {
        public override void Execute(object input)
        {
            int addCount = (int)input;
            
            IStatelessSession s=NHibernateContext.Current.SessionFactory.OpenStatelessSession();
            var v = s.BeginTransaction();
            
            for (int i = 0; i < addCount; i++)
            {
                s.Insert(DataGenerator.NextCategory());
                if(i%100000==0)
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
