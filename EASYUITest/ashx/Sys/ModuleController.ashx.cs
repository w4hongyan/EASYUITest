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
    public class ModuleController : BLL.BaseController
    {
        public ModuleController()
       {
           OperTable = "TModule";
            Action += GetModuleListJson;
       }

        private void GetModuleListJson(object sender, BLL.ActionEventArgs e)
        {
            if (e.action == "getModule")
            {
                string json = ModuleBLL.GetModuleListJson(0);
                e.context.Response.Write(json);
            }
        }

    }
}