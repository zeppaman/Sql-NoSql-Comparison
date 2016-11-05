﻿using System;
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
    public class NoSqlAddItem : BenchmarkExecution
    {
        private static IMongoCollection<Category>  list =MongoContext.Current.DataBase.GetCollection<Category>("Category");
        public override void Execute(object input)
        {
            //IMongoCollection<Category> list=MongoContext.Current.DataBase.GetCollection<Category>("Category");
            int itemsToAddCount = (int)input;
            List<Category> itemsToAdd = new List<Category>();
            for (int i = 0; i < itemsToAddCount; i++)
            {
                itemsToAdd.Add(DataGenerator.NextCategory());
            }

            list.InsertMany(itemsToAdd, new InsertManyOptions() { IsOrdered=true });
        }
    }
}