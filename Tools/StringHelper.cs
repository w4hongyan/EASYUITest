using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class StringHelper
    {
        public static string StringFilter(string inputstr)
        {
            Dictionary<string, string> filterDic = new Dictionary<string, string>();
            filterDic.Add("\"", "");
            filterDic.Add("'", "");
            filterDic.Add("“", "");
            filterDic.Add("”", "");
            filterDic.Add("‘", "");
            filterDic.Add("’", "");
            return StringFilter(inputstr, filterDic);
        }

        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringFilter(string inputstr, Dictionary<string, string> filterDic)
        {
            string outstr = inputstr;
            foreach (var item in filterDic)
            {
                outstr = outstr.Replace(item.Key, item.Value);
            }
            return outstr;
        }
    }
}
