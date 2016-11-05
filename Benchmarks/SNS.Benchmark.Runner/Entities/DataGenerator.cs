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
    }
}
