using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DllTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<string[]> list=new List<string[]>();
            //list.Add(new string[]{"name","姓名"});
            //DataTable dt = GetTable();
            //NPOIHelper.CreateXls(@"D:\text.xls", "你好", dt,list);
            TestGetMemCache();
            //DataTable table= NPOIHelper.ToDataTable(@"D:\import.xls", "0", 0);
            //LogHelper.WriteLog(typeof(Program), "测试日志");
            Console.WriteLine("ok");

            Console.ReadKey();
        }
        public static void TestGetMemCache()
        {

            for (var i = 0; i < 100; i++)
               Console.WriteLine(MemCacheHelper.GetMemCache("Hello" + i));
        }
        public static void TestMemCache()
        {

            for (var i = 0; i < 100; i++)
                MemCacheHelper.SetMemCache("Hello"+i, "World"+i);
        }
        public static DataTable GetTable()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectingString))
                {
                    string sql = "Select top 1000 * from Uuser";
                    return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
