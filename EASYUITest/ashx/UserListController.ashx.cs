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

namespace EASYUITest.ashx
{
    /// <summary>
    /// UserListController 的摘要说明
    /// </summary>
    public class UserListController : BLL.BaseController
    {
       public UserListController()
       {
           base.sql = @"SELECT  [id]
                      ,[weid]
                      ,[name]
                      ,[nickname]
                      ,CASE [sex] WHEN 1 THEN '男' WHEN 2 THEN '女' ELSE '未知' END AS sex 
                      ,[birthday]
                      ,[province]
                      ,[city]
                      ,[country]
                      ,[headimgurl]
                      ,[state]
                      ,[bossid]
                      ,[ismaster]
                      ,[createtime]
                      ,[isdelete]
                      ,[buyurl]
                      ,[mobilephone]
                      ,[RegPartnerID]
                      ,[PartnerID]
                      ,[IDNum]
                      ,[XieYiTime]
                      ,[BanZhang]
                      ,[XueHao]
                      ,[mastertime]
                      ,[latitude]
                      ,[longitude]
                      ,[HasMachine]
                      ,[IsSupplier]
                      ,[ktvid]
                      ,[regtype]
                      ,[isblack]
                      ,[IsStock]
                      ,[stocktime]
                  FROM Uuser WHERE nickname IS NOT NULL";
           base.orderStr = "mastertime desc";
       }
    }
}