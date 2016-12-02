using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkFramework;
using NHibernate.Linq;
namespace SNS.Benchmark.Runner.Sql
{
    /// <summary>
    /// Perform anlytics query (export, report, kpi)
    /// </summary>
    public class SqlAnalytics : BenchmarkExecution
    {
        private static Dictionary<string, string> sqlToExecute = new Dictionary<string, string>();

         static SqlAnalytics()
        {
            sqlToExecute.Add("export", @"SELECT * FROM [Category] 
                                            INNER JOIN[Master] on[Master].CategoryId =[Category].CategoryId
                                            INNER JOIN[Detail] on[Master].MasterId =[Detail].MasterId ");

            sqlToExecute.Add("report", @"SELECT [Category].Name, SUM(Detail.FieldToSum) 
                                            FROM [Category] 
                                            INNER JOIN [Master] on [Master].CategoryId=[Category].CategoryId
                                            INNER JOIN [Detail] on [Master].MasterId=[Detail].MasterId
                                            GROUP BY [Category].Name");
            sqlToExecute.Add("kpi", @"SELECT  SUM(Detail.FieldToSum) 
                                            FROM [Category] 
                                            INNER JOIN [Master] on [Master].CategoryId=[Category].CategoryId
                                            INNER JOIN [Detail] on [Master].MasterId=[Detail].MasterId");

        }


        /// <summary>
        /// execute query by name
        /// </summary>
        /// <param name="input"></param>
        public override void Execute(object input)
        {
            string queryName = input.ToString();
            using (var s = NHibernateContext.Current.SessionFactory.OpenSession())
            {
                var result = s.CreateSQLQuery(sqlToExecute[queryName]).List();               

            }
        }
    }
}
