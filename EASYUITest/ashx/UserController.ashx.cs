﻿using BLL;
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

namespace EASYUITest.ashx
{
    /// <summary>
    /// UserListController 的摘要说明
    /// </summary>
    public class UserController : BLL.BaseController
    {
        public UserController()
       {
           OperTable = "TUser";
       }
       
    }
}