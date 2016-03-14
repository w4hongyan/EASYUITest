using BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Tools;

namespace EASYUITest.ashx
{
    /// <summary>
    /// UserListController 的摘要说明
    /// </summary>
    public class OrderController : BLL.BaseController
    {
        public OrderController()
        {
            sql = @"SELECT [Id]
                          ,[Weid]
                          ,[OrderMoney]
                          ,CASE OrderState WHEN 1 THEN '待发货' WHEN 2 THEN '待收货' WHEN 11 THEN '已退货' WHEN 9 THEN '已完成' ELSE '未知' END AS OrderState
                          ,[ReceiverName]
                          ,[Phone]
                          ,[Province]
                          ,[City]
                          ,[Area]
                          ,[Address]
                          ,[Remark]
                          ,[PostName]
                          ,[PostNumber]
                          ,[IsPay]
                          ,[PayType]
                          ,[PayOrder]
                          ,[CreateTime]
                          ,[AutoFinishedTime]
                          ,[SendTime]
                          ,[SuppWeid]
                          ,[YYGId]
                          ,[FromType]
                      FROM [DistributeShopZCKJ].[dbo].[MOrder] WHERE IsPay=1";
            Action += ImportSendExcel;
        }
        private void ImportSendExcel(object sender, ActionEventArgs e)
        {
            if (e.action == "doimport")
            {
                HttpPostedFile file = e.context.Request.Files["uploadfile"];
                string[] fileNameArray= file.FileName.Split('.');
                string extension= fileNameArray.Last<string>();
                string fileName = DateTime.Now.ToString("yyyyMMddhhmmss");
                string filePath = "/UploadFile/" + fileName + file.FileName;     //通过此对象获取文件名
                string AbsolutePath =e.context.Server.MapPath(filePath);
                file.SaveAs(AbsolutePath);              //将上传的东西保存                
            }
        }
    }
}