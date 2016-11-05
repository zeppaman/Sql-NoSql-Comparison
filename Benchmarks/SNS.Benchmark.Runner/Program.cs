using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkFramework;
using BenchmarkFramework.Implementations;
using SNS.Benchmark.Runner.NoSql;
using MongoDB;
using
     MongoDB.Driver;
using MongoDB.Bson;
using SNS.Benchmark.Runner.Sql;

namespace SNS.Benchmark.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            InitTest();

            //Bulk insert bm
            BenchmarkSuite bms = new BenchmarkSuite();
            
            bms.AddRunnable(10, "BulkInsert-1", new NoSqlAddItem(), new SqlAddItem());
            bms.AddRunnable(10, "BulkInsert-10", new NoSqlAddItem(), new SqlAddItem());
            bms.AddRunnable(100, "BulkInsert-100", new NoSqlAddItem(), new SqlAddItem());
            bms.AddRunnable(1000, "BulkInsert-1000", new NoSqlAddItem(), new SqlAddItem());
            bms.AddRunnable(10000, "BulkInsert-10000", new NoSqlAddItem(), new SqlAddItem());
            bms.AddRunnable(100000, "BulkInsert-100000", new NoSqlAddItem(), new SqlAddItem());
            bms.AddRunnable(1000000, "BulkInsert-1000000", new NoSqlAddItem(), new SqlAddItem());

            bms.ExecuteAll();

            bms.WriteResultToConsole();




            //Search bm
            BenchmarkSuite bms2 = new BenchmarkSuite();

            bms2.AddRunnable(new int[] {2,30,10 }, "Search-2-30-10", new NoSqlQueryItem(), new SqlQueryItem());
            bms2.AddRunnable(new int[] {2, 10, 10 }, "Search-2-10-10", new NoSqlQueryItem(), new SqlQueryItem());
            bms2.AddRunnable(new int[] { 2, 1, 10 }, "Search-2-1-10", new NoSqlQueryItem(), new SqlQueryItem());

            bms2.ExecuteAll();

            bms2.WriteResultToConsole();



            Console.ReadLine();


        }

        private static void InitTest()
        {
            NHibernateContext.ApplySchemaChanges();

            MongoContext.Current.CreateCollectionIfNotExists("Category");
            MongoContext.Current.CreateCollectionIfNotExists("Master");
            MongoContext.Current.CreateCollectionIfNotExists("Detail");

            (new NoSqlAddItem()).Execute(1);
            (new SqlAddItem()).Execute(1);

        }
    }
}



//BenchmarkSuite bms = new BenchmarkSuite();

//bms.AddRunnable(null, "TestFramework", new NopExecution(100), new NopExecution(12));
//            bms.AddRunnable(null, "TestFramework1", new NopExecution(1100), new NopExecution(112));

//            bms.ExecuteAll();

//            bms.WriteResultToConsole();