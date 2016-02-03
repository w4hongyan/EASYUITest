using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tools;

namespace BLL
{
    public class BaseController : ActionHandler, IHttpHandler
    {
        public string sql = "";//默认查询语句
        public string orderStr = "";//默认排序字段
        int page = 0;
        int rows = 0;
        string action = "";//action
        Dictionary<string, string> queryDic = new Dictionary<string, string>();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NameValueCollection collection = context.Request.QueryString;

            foreach (string key in collection.AllKeys)
            {
                queryDic.Add(key, collection[key]);
            }
            if (queryDic.ContainsKey("page") && queryDic.ContainsKey("rows"))
            {
                page = Convert.ToInt32(queryDic["page"]);//删除分页参数，留下查询参数
                queryDic.Remove("page");
                rows = Convert.ToInt32(queryDic["rows"]);
                queryDic.Remove("rows");
            }
            if (queryDic.ContainsKey("action"))//如果不是默认操作
            {
                action = queryDic["action"];
                queryDic.Remove("action");
            }
            ActionEventArgs arg = new ActionEventArgs(context, action);
            Action += DefaultExport;
            Action += DefaultSearch;
            OnAction(arg);
        }
        public void DefaultSearch(object sender, ActionEventArgs e)
        {
            if (e.action == "")
            {
                e.context.Response.Write(GetEasyUIJson(queryDic));
            }
        }
        public void DefaultExport(object sender, ActionEventArgs e)
        {
            if (e.action == "doexport")
            {
                string columnStr = e.context.Request.QueryString["columnStr"];
                queryDic.Remove("columnStr");
                List<string[]> renameList = new List<string[]>();
                string[] columnArray = columnStr.Split(',');
                foreach (string column in columnArray)
                {
                    renameList.Add(column.Split('@'));
                }
                ExportToExcel(e.context.Response, queryDic, "导出模板", "测试", renameList);
            }
        }
        /// <summary>
        /// 拼接查询字符串
        /// </summary>
        /// <param name="sql">原始sql</param>
        /// <param name="queryDic"></param>
        /// <returns></returns>
        public string GetSqlWhere(string sql, Dictionary<string, string> queryDic)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM (");
            sb.Append(sql);
            sb.Append(")_T");
            if (queryDic.Count > 0)
            {
                sb.Append(" WHERE ");
                foreach (string key in queryDic.Keys)
                {
                    string[] queryArray = GetQueryArray(key);
                    if (queryArray.Length > 1)
                    {
                        sb.Append(queryArray[0] + queryArray[1]);
                        if (queryArray[1] == " LIKE ")
                        {
                            sb.Append("'%" + queryDic[key] + "%'");
                        }
                        else
                        {
                            sb.Append("'" + queryDic[key] + "'");
                        }
                        if (key != queryDic.Keys.Last())
                        {
                            sb.Append(" AND ");
                        }
                    }

                }
            }
            return sb.ToString();
        }
        protected string[] GetQueryArray(string name)
        {
            if (name.Length > 3 && name.Substring(name.Length - 3) == "_eq")//相等
            {
                return new[] { name.Substring(0, name.Length - 3), "=" };
            }
            else if (name.Length > 3 && name.Substring(name.Length - 3) == "_lt")//小于 less than
            {
                return new[] { name.Substring(0, name.Length - 3), "<" };
            }
            else if (name.Length > 3 && name.Substring(name.Length - 3) == "_mt")//大于 more than
            {
                return new[] { name.Substring(0, name.Length - 3), ">" };
            }
            else if (name.Length > 4 && name.Substring(name.Length - 4) == "_lte")//小于等于
            {
                return new[] { name.Substring(0, name.Length - 4), "<=" };
            }
            else if (name.Length > 4 && name.Substring(name.Length - 4) == "_mte")//大于等于
            {
                return new[] { name.Substring(0, name.Length - 4), ">=" };
            }
            else if (name.Length > 5 && name.Substring(name.Length - 5) == "_null")//为空
            {
                return new[] { name.Substring(0, name.Length - 5), " IS NULL" };
            }
            else if (name.Length > 8 && name.Substring(name.Length - 8) == "_notnull")//不为空
            {
                return new[] { name.Substring(0, name.Length - 8), " IS NOT NULL" };
            }
            else if (name.Length > 3 && name.Substring(name.Length - 3) == "_in")//IN
            {
                return new[] { name.Substring(0, name.Length - 3), " IN " };
            }
            else if (name.Length > 5 && name.Substring(name.Length - 5) == "_like")//like
            {
                return new[] { name.Substring(0, name.Length - 5), " LIKE " };
            }
            else
            {
                return new[] { name, "=" };
            }
        }

        private string GetEasyUIJson(Dictionary<string, string> queryDic)
        {
            return BaseBLL.GetEasyUIJson(GetSqlWhere(sql, queryDic), orderStr, page, rows);
        }
        private bool ExportToExcel(System.Web.HttpResponse response, Dictionary<string, string> queryDic, string fileName, string sheetName, List<string[]> renameList)
        {
            return BaseBLL.ExportToExcel(response, fileName, sheetName, GetSqlWhere(sql, queryDic), orderStr, page, rows, renameList);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}
