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
using SNS.Benchmark.Runner.Sql.Entities;
using SNS.Benchmark.Runner.Entities;

namespace SNS.Benchmark.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            InitTest();

            int searchRepeatCount = 2;
            int reportRepeatCount = 2;


            for (int i = 0; i < searchRepeatCount; i++)
            {
                //Add 1.1111.111 rows
                RunInsertBulk();
                Console.WriteLine("Test table Size: {0} M", (i+1)*2); // each test are executed twice
                //Perform search over data base
                RunSearch();
            }



            // 51 110  master rows, 20 deatails per master rows=> 1M
            for (int i = 0; i < reportRepeatCount; i++)
            {
                RunAddDataInTransaction();
                Console.WriteLine("Test table Size: {0} M", (i + 1) * 2); // each test are executed twice

                RunReports(i<3);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Run report test
        /// </summary>
        private static void RunReports(bool testExport)
        {
            BenchmarkSuite bms4 = new BenchmarkSuite();

            bms4.AddRunnable("kpi", "KPI", new NoSqlAnalytics(), new SqlAnalytics());
            bms4.AddRunnable("kpi", "Report", new NoSqlAnalytics(), new SqlAnalytics());
            if (testExport)
            {
                bms4.AddRunnable("export", "Export", new NoSqlAnalytics(), new SqlAnalytics());
            }

            bms4.ExecuteAll();

            bms4.WriteResultToConsole();


           
        }



        /// <summary>
        /// add data in transaction
        /// </summary>
        private static void RunAddDataInTransaction()
        {
            //Transaction bm
            BenchmarkSuite bms3 = new BenchmarkSuite();

            bms3.AddRunnable(10, "Trans-10", new NoSqlTransaction(), new SqlTransaction());
            bms3.AddRunnable(100, "Trans-100", new NoSqlTransaction(), new SqlTransaction());
            bms3.AddRunnable(1000, "Trans-1000", new NoSqlTransaction(), new SqlTransaction());
            bms3.AddRunnable(10000, "Trans-10000", new NoSqlTransaction(), new SqlTransaction());
            bms3.AddRunnable(40000, "Trans-30000", new NoSqlTransaction(), new SqlTransaction());

            bms3.ExecuteAll();

            bms3.WriteResultToConsole();
        }


        /// <summary>
        /// Perform many search
        /// </summary>
        private static void RunSearch()
        {
            //Search bm
            BenchmarkSuite bms2 = new BenchmarkSuite();

            bms2.AddRunnable(new int[] { 2, 30, 10 }, "Search-2-30-10", new NoSqlQueryItem(), new SqlQueryItem());
            bms2.AddRunnable(new int[] { 2, 10, 10 }, "Search-2-10-10", new NoSqlQueryItem(), new SqlQueryItem());
            bms2.AddRunnable(new int[] { 2, 1, 10 }, "Search-2-1-10", new NoSqlQueryItem(), new SqlQueryItem());

            bms2.ExecuteAll();

            bms2.WriteResultToConsole();
        }

        /// <summary>
        /// Insert many rows in batch statement to db
        /// </summary>
        private static void RunInsertBulk()
        {
            //Bulk insert bm
            BenchmarkSuite bms = new BenchmarkSuite();

            bms.AddRunnable(1, "BulkInsert-1", new NoSqlAddItem(), new SqlAddItem());
            bms.AddRunnable(10, "BulkInsert-10", new NoSqlAddItem(), new SqlAddItem());
            bms.AddRunnable(100, "BulkInsert-100", new NoSqlAddItem(), new SqlAddItem());
            bms.AddRunnable(1000, "BulkInsert-1000", new NoSqlAddItem(), new SqlAddItem());
            bms.AddRunnable(10000, "BulkInsert-10000", new NoSqlAddItem(), new SqlAddItem());
            bms.AddRunnable(100000, "BulkInsert-100000", new NoSqlAddItem(), new SqlAddItem());
            bms.AddRunnable(1000000, "BulkInsert-1000000", new NoSqlAddItem(), new SqlAddItem());

            bms.ExecuteAll();

            bms.WriteResultToConsole();
        }

        private static void InitTest()
        {
            NHibernateContext.ApplySchemaChanges();

            MongoContext.Current.CreateCollectionIfNotExists("Category");
            MongoContext.Current.CreateCollectionIfNotExists("Master");
            MongoContext.Current.CreateCollectionIfNotExists("Detail");

            (new NoSqlAddItem()).Execute(1);
            (new SqlAddItem()).Execute(1);

            //insert reference categories
            List<Category> itemsToAdd = new List<Category>();

            Guid extToken = Guid.NewGuid();
            for (int i = 0; i < 10; i++)
            {
                Category c = new Category();
                c.Name = "Cat Sample " + i + " " + extToken.ToString().Substring(0,5);
                c.CategoryId = Guid.NewGuid();
                itemsToAdd.Add(c);
            }

            DataGenerator.BenchmarkCategories = itemsToAdd;

             (new NoSqlAddItem()).Execute(itemsToAdd);
             (new SqlAddItem()).Execute(itemsToAdd);

        }
    }
}



//BenchmarkSuite bms = new BenchmarkSuite();

//bms.AddRunnable(null, "TestFramework", new NopExecution(100), new NopExecution(12));
//            bms.AddRunnable(null, "TestFramework1", new NopExecution(1100), new NopExecution(112));

//            bms.ExecuteAll();

//            bms.WriteResultToConsole();