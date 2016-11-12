using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNS.Benchmark.Runner.Sql.Entities;

namespace SNS.Benchmark.Runner.Entities
{
    public  static class DataGenerator
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
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

        public static  Category NextCategory()
        {
            Category c = new Category();
            c.CategoryId = Guid.NewGuid();
            c.Name = NextString(30);
            return c;
        }

        public  static List<Detail> Details(Guid masterId, int v)
        {
            List<Detail> result = new List<Detail>();
            for (int i = 0; i < v; i++)
            {
                result.Add(NextDetail(masterId));
            }
            return result;
        }

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

        public static Master NextMaster()
        {
            Master d = new Master();
            d.MasterId = Guid.NewGuid();
            d.MasterField0 = NextString(10);
            d.MasterField1 = NextString(10);
            d.MasterField2 = NextString(10);
            d.MasterField3 = NextString(10);

            d.FieldToSum = random.Next(100);
            return d;
        }
    }
}
