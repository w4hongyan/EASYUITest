using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Model;
using Tools;

namespace BLL
{
    public class ModuleBLL
    {
        public static List<Module> GetModuleListByParentId(int parentId)
        {
            var sql = "Select * from TModule where ParentId=@ParentId";
            var moduleList = SqlHelper.GetList<Module>(sql, new SqlParameter("@ParentId", parentId));
            return moduleList;
        }

        public static string GetModuleListJson(int parentId)
        {
            var modulelList = GetModuleListByParentId(parentId);
            List<Module> childModuleList;
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var module in modulelList)
            {
                sb.Append("{");
                sb.Append("\"id\":\"" + module.Id + "\",");
                sb.Append("\"text\":\"" + module.Name + "\",");
                sb.Append("\"url\":\"" + module.Url + "\",");
                sb.Append("\"iconCls\":\"" + module.Ico + "\"");

                childModuleList = GetModuleListByParentId(module.Id);
                if (childModuleList != null && childModuleList.Count > 0)
                {
                    sb.Append(",\"children\":");

                    sb.Append(GetModuleListJson(module.Id));
                }
                sb.Append("},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }
    }
}