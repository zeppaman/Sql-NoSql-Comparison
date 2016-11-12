using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkFramework;
using MongoDB.Driver;
using SNS.Benchmark.Runner.Sql.Entities;

namespace SNS.Benchmark.Runner.NoSql
{
    public class NoSqlAnalytics : BenchmarkExecution
    {
        private static IMongoCollection<Category> categories = MongoContext.Current.DataBase.GetCollection<Category>("Category");
        private static IMongoCollection<Master> master = MongoContext.Current.DataBase.GetCollection<Master>("Master");
        private static IMongoCollection<Detail> detail = MongoContext.Current.DataBase.GetCollection<Detail>("Detail");

        public override void Execute(object input)
        {
            string queryName = input.ToString();

            switch (queryName)
            {
                case "report": ComputeReport(); break;
                case "kpi": ComputeKPI(); break;
                case "export": ComputeExport(); break;
            }
        }

        private void ComputeExport()
        {
            List<object> result = new List<object>();
            var ids = master.AsQueryable().Where(x=>x.CategoryId.CompareTo(new Guid())!=0)
            .Select(x => x.CategoryId).Distinct().ToList();

            var builder = Builders<Category>.Filter;
            var filter = builder.In<Guid>(x=>x.CategoryId,ids);
            var cats=categories
                .Find(filter)
                .ToList();

            foreach (var cat in cats)
            {
                var maters = master.AsQueryable<Master>().Where(x => x.CategoryId == cat.CategoryId).ToList();
                foreach (var mas in maters)
                {
                    var deta = detail.AsQueryable<Detail>().Where(x => x.MasterId == mas.MasterId).ToList();


                    var join = deta.Select(x => new { Detail = x, Master = mas, Category = cat }).ToArray();
                    result.Add(join);
                }
            }
        }

        private void ComputeKPI()
        {
            var computeSum = detail
                .AsQueryable<Detail>().Sum(x => x.FieldToSum);
        }

        private void ComputeReport()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            var ids = master.AsQueryable().Select(x => x.CategoryId).Distinct();
            var cats = categories
                .AsQueryable<Category>()
                .Where(x => ids.Contains(x.CategoryId))
                .ToList();

            foreach (var cat in cats)
            {
                var masterIds = master
                    .AsQueryable<Master>()
                    .Where(x => x.CategoryId == cat
                    .CategoryId)
                    .Select(x => x.MasterId)
                    .Distinct()
                    .ToList();


                var sum = detail.AsQueryable().Where(x => masterIds.Contains(x.MasterId)).Sum(x => x.FieldToSum);

                result.Add(cat.Name, sum);
            }
        }
    }
}
