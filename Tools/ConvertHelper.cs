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
        /// <summary>
        /// 获取表的所有列名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static List<string> GetTableColumns(string tableName)
        {
            List<string> colNames=new List<string>();
            string sql = "Select Name FROM SysColumns Where id=Object_Id('" + tableName + "') ORDER BY colid asc";
            DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectingString, CommandType.Text, sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    colNames.Add(dt.Rows[i]["Name"].ToString());
                }
            }
            return colNames;
        }
        /// <summary>
        /// 将DataTable转换为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T GetEntity<T>(DataTable table) where T : new()
        {
            T entity = default(T);
            foreach (DataRow row in table.Rows)
            {
                entity = new T();
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (row.Table.Columns.Contains(item.Name))
                    {
                        if (DBNull.Value != row[item.Name])
                        {
                            item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                        }

                    }
                }
            }
            return entity;
        }
        /// <summary>
        /// 将DataTable转换为List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> GetEntities<T>(DataTable table) where T : new()
        {
            IList<T> entities = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T entity = new T();
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (row.Table.Columns.Contains(item.Name))
                    {
                        if (DBNull.Value != row[item.Name])
                        {
                            item.SetValue(entity, ChangeType(row[item.Name], item.PropertyType), null);
                        }
                    }
                }
                entities.Add(entity);
            }
            return entities;
        }

        public static object ChangeType(object obj, Type conversionType)
        {
            return ChangeType(obj, conversionType, null);
        }

        public static object ChangeType(object obj, Type conversionType, IFormatProvider provider)
        {


            Type nullableType = Nullable.GetUnderlyingType(conversionType);
            if (nullableType != null)
            {
                if (obj == null)
                {
                    return null;
                }
                return Convert.ChangeType(obj, nullableType, provider);
            }

            if (typeof(System.Enum).IsAssignableFrom(conversionType))
            {
                return Enum.Parse(conversionType, obj.ToString());
            }
            return Convert.ChangeType(obj, conversionType, provider);

        }

    }
}
