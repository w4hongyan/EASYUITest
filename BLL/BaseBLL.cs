using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace BLL
{
    public class BaseBLL
    {
        /// <summary>
        /// 获取EasyUI所需的Json
        /// </summary>
        /// <returns></returns>
        public static string GetEasyUIJson(string sql, string orderStr, int pageIndex, int pageSize, params SqlParameter[] cmdParams)
        {
            string jsonResult;
            int rowsTotal;
            DataTable dt;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectingString))
            {
                rowsTotal = SqlHelper.GetRowsCount(conn, sql, cmdParams);
                dt = SqlHelper.GetPagedDataTable(conn, sql, orderStr, pageIndex, pageSize, cmdParams);
            }
            jsonResult = JSONHelper.DataTableToJson(dt);
            jsonResult = "{\"total\":" + "\"" + rowsTotal.ToString() + "\",\"rows\":" + jsonResult + "}";
            return jsonResult;
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="orderStr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
         public static bool ExportToExcel(System.Web.HttpResponse response,string fileName,string sheetName, string sql, string orderStr, int pageIndex, int pageSize,List<string[]> renameList, params SqlParameter[] cmdParams)
        {
            DataTable dt;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectingString))
            {
                dt = SqlHelper.GetPagedDataTable(conn, sql, orderStr, pageIndex, pageSize, cmdParams);
            }
            return NPOIHelper.CreateXls(response, fileName, sheetName, dt,renameList);
        }
    }
}
