using BLL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Tools;

namespace EASYUITest.ashx.Sys
{
    /// <summary>
    /// UserListController 的摘要说明
    /// </summary>
    public class PriviledgeController : BLL.BaseController
    {
        public PriviledgeController()
       {
            OperTable = "TPriviledge";
            sql = "SELECT tp.*,tm.Name AS ModuleName  FROM TPriviledge tp LEFT JOIN dbo.TModule tm ON tp.ModuleId=tm.Id";
            Action += GetHasPriviledge;

       }

        void GetHasPriviledge(object sender,BLL.ActionEventArgs e)
        {
            if (e.action == "getHasPriviledge")
            {
                e.context.Response.Write("123");
            }
        }

    }
}