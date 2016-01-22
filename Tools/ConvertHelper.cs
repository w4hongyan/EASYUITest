using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    /// <summary>
    /// 类型转换帮助类
    /// </summary>
    public class ConvertHelper
    {
        /// <summary>
        /// 获取DataTable的所有列名
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<string> GetDataTableColumns(DataTable dt)
        {
            List<string> colNames = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                colNames.Add(dc.ColumnName);
            }
            return colNames;
        }
    }
}
