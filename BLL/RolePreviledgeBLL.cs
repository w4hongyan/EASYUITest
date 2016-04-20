using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Tools;

namespace BLL
{
    public class RolePreviledgeBLL
    {
        public static string GetRolePreviledgeListByRoleId(int roleId)
        {
            var sql = "Select * from TRolePreviledge where RoleId=@RoleId";
            var rolePreviledgeList = SqlHelper.GetList<RolePreviledge>(sql, new SqlParameter("@RoleId", roleId));
            StringBuilder sb=new StringBuilder();
            if (rolePreviledgeList.Count > 0)
            {
                foreach (var rolePreviledge in rolePreviledgeList)
                {
                    sb.Append(rolePreviledge.PreviledgeId + ",");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
    }
}
