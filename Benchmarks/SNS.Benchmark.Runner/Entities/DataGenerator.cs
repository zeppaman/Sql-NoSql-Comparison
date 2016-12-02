using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNS.Benchmark.Runner.Sql.Entities;

namespace SNS.Benchmark.Runner.Entities
{
    /// <summary>
    /// this class generates items to store in db
    /// </summary>
    public  static class DataGenerator
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden


        public static List<Category> BenchmarkCategories { get; internal set; }

        /// <summary>
        /// return a random string of a given size
        /// </summary>
        /// <param name="size">size of the string to return</param>
        /// <returns></returns>
        private static string NextString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Give a random category
        /// </summary>
        /// <returns></returns>
        public static  Category NextCategory()
        {
            Category c = new Category();
            c.CategoryId = Guid.NewGuid();
            c.Name = NextString(30);
            return c;
        }

        /// <summary>
        /// Return a list of details entity, child of a given masterid
        /// </summary>
        /// <param name="masterId"> the master id</param>
        /// <param name="v"> number of items to return</param>
        /// <returns></returns>
        public  static List<Detail> Details(Guid masterId, int v)
        {
            List<Detail> result = new List<Detail>();
            for (int i = 0; i < v; i++)
            {
                result.Add(NextDetail(masterId));
            }
            return result;
        }

        /// <summary>
        /// return a  random detail item 
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public static Detail NextDetail(Guid masterId)
        {
            Detail d = new Detail();
            d.DetailId = Guid.NewGuid();
            d.DetailField0 = NextString(10);
            d.DetailField1 = NextString(10);
            d.DetailField2 = NextString(10);
            d.DetailField3 = NextString(10);

            d.FieldToSum = random.Next(100);
            d.MasterId = masterId;
            return d;

        }


        /// <summary>
        /// Return a master entity to return
        /// </summary>
        /// <returns></returns>
        public static Master NextMaster()
        {
            Master d = new Master();
            d.MasterId = Guid.NewGuid();
            d.MasterField0 = NextString(10);
            d.MasterField1 = NextString(10);
            d.MasterField2 = NextString(10);
            d.MasterField3 = NextString(10);

            d.CategoryId = GetRandomCategory().CategoryId;

            d.FieldToSum = random.Next(100);
            return d;
        }

        /// <summary>
        /// Returns a random category
        /// </summary>
        /// <returns></returns>
        private static Category GetRandomCategory()
        {
            int i = random.Next(0, BenchmarkCategories.Count);
            return DataGenerator.BenchmarkCategories[i];
        }
    }
}
