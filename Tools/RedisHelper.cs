using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis.Generic;

namespace Tools
{
    public class RedisHelper:IDisposable
    {
        public static RedisClient redisClient = null;
        static RedisHelper()
        {
            if (redisClient == null)
            {
                CreateClient("127.0.0.1", 6379);
            }
        }

        public static void CreateClient(string hostIP, int port)
        {
            if (redisClient == null)
            {
                redisClient = new RedisClient(hostIP, port);
            }

        }
        
        public static T Get<T>(string key)
        {
            return redisClient.Get<T>(key);
        }
        public static bool Set<T>(string key,T value)
        {
           return redisClient.Set<T>(key, value);
        }
        public static byte[] getValueByte(string key)
        {
            byte[] value = redisClient.Get(key);
            return value;
        }
        public static string getValueString(string key)
        {
            string value = redisClient.GetValue(key);
            return value;
        }
        /// <summary>
        /// 获得某个hash型key下所有字段
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public static List<string> GetHashFields(string hashId)
        {
            List<string> hashFileds = redisClient.GetHashKeys(hashId);
            return hashFileds;
        }
        /// <summary>
        /// 获得某个hash型key下所有值
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public static List<string> GetHashValues(string hashId)
        {
            List<string> hashFileds = redisClient.GetHashValues(hashId);
            return hashFileds;
        }
         /// <summary>
         /// 获得hash型key某个字段的值
         /// </summary>
         /// <param name="key"></param>
         /// <param name="field"></param>
         public static string GetHashField(string key, string field)
         {
             string value = redisClient.GetValueFromHash(key, field);
             return value;
         }
         /// <summary>
         /// 设置hash型key某个字段的值
         /// </summary>
         /// <param name="key"></param>
         /// <param name="field"></param>
         /// <param name="value"></param>
         public static void SetHashField(string key, string field, string value)
         {
             redisClient.SetEntryInHash(key, field, value);
         }
         /// <summary>
         ///使某个字段增加
         /// </summary>
         /// <param name="key"></param>
         /// <param name="field"></param>
         /// <returns></returns>
         public static void SetHashIncr(string key, string field, long incre)
         {
             redisClient.IncrementValueInHash(key, field, incre);
 
         }
         /// <summary>
         /// 向list类型数据添加成员，向列表底部(右侧)添加
         /// </summary>
         /// <param name="Item"></param>
         /// <param name="list"></param>
         public static void AddItemToListRight(string list, string item)
         {
             redisClient.AddItemToList(list, item);
         }
         /// <summary>
         /// 向list类型数据添加成员，向列表顶部(左侧)添加
         /// </summary>
         /// <param name="list"></param>
         /// <param name="item"></param>
         public static void AddItemToListLeft(string list, string item)
         {
             redisClient.LPush(list, Encoding.Default.GetBytes(item));
         }
         /// <summary>
         /// 从list类型数据读取所有成员
         /// </summary>
         public static List<string> GetAllItems(string list)
         {
             List<string> listMembers = redisClient.GetAllItemsFromList(list);
             return listMembers;
         }
         /// <summary>
         /// 从list类型数据指定索引处获取数据，支持正索引和负索引
         /// </summary>
         /// <param name="list"></param>
         /// <returns></returns>
         public static string GetItemFromList(string list, int index)
         {
             string item = redisClient.GetItemFromList(list, index);
             return item;
         }
         /// <summary>
         /// 向列表底部（右侧）批量添加数据
         /// </summary>
         /// <param name="list"></param>
         /// <param name="values"></param>
         public static void GetRangeToList(string list, List<string> values)
         {
             redisClient.AddRangeToList(list, values);
         }
         /// <summary>
         /// 向集合中添加数据
         /// </summary>
         /// <param name="item"></param>
         /// <param name="set"></param>
         public static void GetItemToSet(string set, string item)
         {
             redisClient.AddItemToSet(set,item);
         }
         /// <summary>
         /// 获得集合中所有数据
         /// </summary>
         /// <param name="set"></param>
         /// <returns></returns>
         public static HashSet<string> GetAllItemsFromSet(string set)
         {
             HashSet<string> items = redisClient.GetAllItemsFromSet(set);
             return items;
         }
         /// <summary>
         /// 获取fromSet集合和其他集合不同的数据
         /// </summary>
         /// <param name="fromSet"></param>
         /// <param name="toSet"></param>
         /// <returns></returns>
         public static HashSet<string> GetSetDiff(string fromSet, params string[] toSet)
         {
             HashSet<string> diff = redisClient.GetDifferencesFromSet(fromSet, toSet);
             return diff;
         }
         /// <summary>
         /// 获得所有集合的并集
         /// </summary>
         /// <param name="set"></param>
         /// <returns></returns>
         public static HashSet<string> GetSetUnion(params string[] set)
         {
             HashSet<string> union = redisClient.GetUnionFromSets(set);
             return union;
         }
         /// <summary>
         /// 获得所有集合的交集
         /// </summary>
         /// <param name="set"></param>
         /// <returns></returns>
         public static HashSet<string> GetSetInter(params string[] set)
         {
             HashSet<string> inter = redisClient.GetIntersectFromSets(set);
             return inter;
         }
         /// <summary>
         /// 向有序集合中添加元素
         /// </summary>
         /// <param name="set"></param>
         /// <param name="value"></param>
         /// <param name="score"></param>
         public static void AddItemToSortedSet(string set, string value, long score)
         {
             redisClient.AddItemToSortedSet(set, value, score);
         }
         /// <summary>
         /// 获得某个值在有序集合中的排名，按分数的降序排列
         /// </summary>
         /// <param name="set"></param>
         /// <param name="value"></param>
         /// <returns></returns>
         public static int GetItemIndexInSortedSetDesc(string set, string value)
         {
             int index = redisClient.GetItemIndexInSortedSetDesc(set, value);
             return index;
         }
         /// <summary>
         /// 获得某个值在有序集合中的排名，按分数的升序排列
         /// </summary>
         /// <param name="set"></param>
         /// <param name="value"></param>
         /// <returns></returns>
         public static int GetItemIndexInSortedSet(string set, string value)
         {
             int index = redisClient.GetItemIndexInSortedSet(set, value);
             return index;
         }
         /// <summary>
         /// 获得有序集合中某个值得分数
         /// </summary>
         /// <param name="set"></param>
         /// <param name="value"></param>
         /// <returns></returns>
         public static double GetItemScoreInSortedSet(string set, string value)
         {
             double score = redisClient.GetItemScoreInSortedSet(set, value);
             return score;
         }
         /// <summary>
         /// 获得有序集合中，某个排名范围的所有值
         /// </summary>
         /// <param name="set"></param>
         /// <param name="beginRank"></param>
         /// <param name="endRank"></param>
         /// <returns></returns>
         public static List<string> GetRangeFromSortedSet(string set, int beginRank, int endRank)
         {
             List<string> valueList = redisClient.GetRangeFromSortedSet(set, beginRank, endRank);
             return valueList;
         }
         /// <summary>
         /// 获得有序集合中，某个分数范围内的所有值，升序
         /// </summary>
         /// <param name="set"></param>
         /// <param name="beginScore"></param>
         /// <param name="endScore"></param>
         /// <returns></returns>
         public static List<string> GetRangeFromSortedSet(string set, double beginScore, double endScore)
         {
             List<string> valueList = redisClient.GetRangeFromSortedSetByHighestScore(set, beginScore, endScore);
             return valueList;
         }
         /// <summary>
         /// 获得有序集合中，某个分数范围内的所有值，降序
         /// </summary>
         /// <param name="set"></param>
         /// <param name="beginScore"></param>
         /// <param name="endScore"></param>
         /// <returns></returns>
         public static List<string> GetRangeFromSortedSetDesc(string set, double beginScore, double endScore)
         {
             List<string> vlaueList = redisClient.GetRangeFromSortedSetByLowestScore(set, beginScore, endScore);
             return vlaueList;
         }
        /// <summary>
         /// 获得强类型IRedisTypedClient
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
         public static IRedisTypedClient<T> GetTypedClient<T>()
         {
             return redisClient.As<T>();
         }
         public void Dispose()
         {
             redisClient.Dispose();
         }
    }
}
