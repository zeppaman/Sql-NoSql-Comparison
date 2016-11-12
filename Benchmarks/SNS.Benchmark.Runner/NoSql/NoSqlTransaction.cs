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
    public class NoSqlTransaction : BenchmarkExecution
    {
        private static IMongoCollection<Category>  list =MongoContext.Current.DataBase.GetCollection<Category>("Category");
        private static IMongoCollection<Detail> detailsList = MongoContext.Current.DataBase.GetCollection<Detail>("Detail");
        private static IMongoCollection<Master> masterList = MongoContext.Current.DataBase.GetCollection<Master>("Master");
        public override void Execute(object input)
        {
            int transactions = (int)input;
          

            for (int i = 0; i < transactions; i++)
            {              

                Master m = DataGenerator.NextMaster();
               
                masterList.InsertOne(m);
                List<Detail> details = DataGenerator.Details(m.MasterId, 20);
                foreach (var v in details)
                {
                    detailsList.InsertOne(v);
                }
            }

          
        }
    }
}
