using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkFramework
{
    public class BenchMarkContext
    {
        #region properties
        public bool EnableFileLog = true;
        public string LogFilePath = "";
        public string LogLayout = "${date};${message}";

        #endregion

        #region methods
        public void LogInfo(string testname, params string[] testNames)
        {
            testname += ";";
            for (int i = 0; i < testNames.Length; i++)
            {
                testname += string.Format("{0};", testNames[i]);
            }
            LogInfo(testname);

        }
        public void LogInfo(string testname, params long[] testResult)
        {
            testname += ";";
            for (int i = 0; i < testResult.Length; i++)
            {
                testname += string.Format("{0};", testResult[i]);
            }
            LogInfo(testname);

        }
        public void LogInfo(string text)
        {
            string line = LogLayout
                .Replace("${date}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("${message}", text);
            File.AppendAllLines(LogFilePath, new string[] { line });
        }
        #endregion


        #region singleton
        private static BenchMarkContext current { get; set; }

        public static BenchMarkContext Current {
            get { return current ?? (current = NewConxtext()); }
        }

        private static BenchMarkContext NewConxtext()
        {
            BenchMarkContext bc = new BenchMarkContext();
            bc.LogFilePath = ".\\BenchMarkLog." + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            return bc;
        }

        #endregion

    }
}
