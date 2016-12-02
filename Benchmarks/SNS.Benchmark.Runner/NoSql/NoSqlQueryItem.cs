using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkFramework;
using MongoDB.Driver;
using SNS.Benchmark.Runner.Entities;
using SNS.Benchmark.Runner.Sql.Entities;

namespace SNS.Benchmark.Runner.NoSql
{
    /// <summary>
    /// Test query performance
    /// </summary>
    public class NoSqlQueryItem : BenchmarkExecution
    {
        private static IMongoCollection<Category>  list =MongoContext.Current.DataBase.GetCollection<Category>("Category");
        /// <summary>
        /// Execute the test
        /// </summary>
        /// <param name="input">input is a integer array with number of test execution for each type
        /// type 1: query by id found
        /// type 2: query by id NOT found
        /// type 3: query paged with filters and sort
        /// </param>
        public override void Execute(object input)
        {
            int[] queryRatio = (int[])input;
            Guid IdUnableToFind = Guid.NewGuid();

            var fistItem = list.AsQueryable<Category>().Take(1).FirstOrDefault();
            for (int i = 0; i < queryRatio[0]; i++)
            {
                var itemNotFound = list.AsQueryable<Category>().Where(x => x.CategoryId.Equals(IdUnableToFind));
            }

            for (int i = 0; i < queryRatio[1]; i++)
            {
                var itemFound = list.AsQueryable<Category>().Where(x => x.CategoryId.Equals(fistItem.CategoryId));
            }

            for (int i = 0; i < queryRatio[2]; i++)
            {
                var page = list.AsQueryable<Category>().Where(x => !x.Name.Contains("TEXT UNFINDABLE")).Skip(100).Take(100).OrderBy(x => x.Name).OrderByDescending(x => x.CategoryId);
                

            }
        }
    }
}
