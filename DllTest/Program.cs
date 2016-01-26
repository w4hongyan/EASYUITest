using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
            //TestGetMemCache();
            //DataTable table= NPOIHelper.ToDataTable(@"D:\import.xls", "0", 0);
            //LogHelper.WriteLog(typeof(Program), "测试日志");
            //List<string> list=RedisHelper.GetAllItems("QueueListId");
            //foreach (var item in list)
            //{
            //  Console.WriteLine(item);  
            //}
            //Console.WriteLine(RedisHelper.GetAllItems("QueueListId"));
            TestRedisObject();
            Console.WriteLine("ok");

            Console.ReadKey();
        }
        public static void TestRedisObject()
        {

            using (var cars = RedisHelper.GetTypedClient<Car>())
            {
                if (cars.GetAll().Count > 0)
                    cars.DeleteAll();

                var dansFord = new Car
                {
                    Id = cars.GetNextSequence(),
                    Title = "Dan's Ford",
                    Make = new Make { Name = "Ford" },
                    Model = new Model { Name = "Fiesta" }
                };
                var beccisFord = new Car
                {
                    Id = cars.GetNextSequence(),
                    Title = "Becci's Ford",
                    Make = new Make { Name = "Ford" },
                    Model = new Model { Name = "Focus" }
                };
                var vauxhallAstra = new Car
                {
                    Id = cars.GetNextSequence(),
                    Title = "Dans Vauxhall Astra",
                    Make = new Make { Name = "Vauxhall" },
                    Model = new Model { Name = "Asta" }
                };
                var vauxhallNova = new Car
                {
                    Id = cars.GetNextSequence(),
                    Title = "Dans Vauxhall Nova",
                    Make = new Make { Name = "Vauxhall" },
                    Model = new Model { Name = "Nova" }
                };

                var carsToStore = new List<Car> { dansFord, beccisFord, vauxhallAstra, vauxhallNova };
                cars.StoreAll(carsToStore);

                Console.WriteLine("Redis Has-> " + cars.GetAll().Count + " cars");

                cars.ExpireAt(vauxhallAstra.Id, DateTime.Now.AddSeconds(5)); //Expire Vauxhall Astra in 5 seconds

                Thread.Sleep(6000); //Wait 6 seconds to prove we can expire our old Astra

                Console.WriteLine("Redis Has-> " + cars.GetAll().Count + " cars");

                //Get Cars out of Redis
                var carsFromRedis = cars.GetAll().Where(car => car.Make.Name == "Ford");

                foreach (var car in carsFromRedis)
                {
                    Console.WriteLine("Redis Has a ->" + car.Title);
                }

            }
            Console.ReadLine(); 
        }
        public static void TestRedisSet()
        {
            for (int i = 0; i < 10000; i++)
            {
                RedisHelper.Set<string>("key" + i, i.ToString());
            }
        }

        public static List<string> GetCity(string key)
        {
            return RedisHelper.Get<List<string>>(key);
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
    public class Car
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Make Make { get; set; }
        public Model Model { get; set; }
    }

    public class Make
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Model
    {
        public int Id { get; set; }
        public Make Make { get; set; }
        public string Name { get; set; }
    }
}
