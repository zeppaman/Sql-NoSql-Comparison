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
    public class SqlQueryItem : BenchmarkExecution
    {
        public override void Execute(object input)
        {
            int[] queryRatio = (int[])input;
            
            ISession s=NHibernateContext.Current.SessionFactory.OpenSession();
            Guid IdUnableToFind = Guid.NewGuid();

            
                var fistItem = s.Query<Category>().Take(1).FirstOrDefault();
            

            for (int i = 0; i < queryRatio[0]; i++)
            {
                var itemNotFound = s.Query<Category>().Where(x => x.CategoryId.Equals(IdUnableToFind));
            }

            for (int i = 0; i < queryRatio[1]; i++)
            {
                var itemFound = s.Query<Category>().Where(x => x.CategoryId.Equals(fistItem.CategoryId));
            }

            for (int i = 0; i < queryRatio[2]; i++)
            {
                var page = s.Query<Category>().Where(x => !x.Name.Contains("TEXT UNFINDABLE")).Skip(100).Take(100).OrderBy(x => x.Name).OrderByDescending(x => x.CategoryId);
            }

        }
    }
}
