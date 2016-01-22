using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Tools;

namespace EASYUITest
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
            Exception ex =Server.GetLastError().GetBaseException();
            StringBuilder str = new StringBuilder();
            str.Append("\r\n" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            str.Append("\r\n客户信息：");


            string ip = "";
            if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
            {
                ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            }
            else
            {
                ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
            }
            str.Append("\r\nIp:" + ip);
            str.Append("\r\n浏览器:" + Request.Browser.Browser.ToString());
            str.Append("\r\n浏览器版本:" + Request.Browser.MajorVersion.ToString());
            str.Append("\r\n操作系统:" + Request.Browser.Platform.ToString());
            str.Append("\r\n错误信息：");
            str.Append("\r\n页面：" + Request.Url.ToString());
            str.Append("\r\n错误信息：" + ex.Message);
            str.Append("\r\n错误源：" + ex.Source);
            str.Append("\r\n异常方法：" + ex.TargetSite);
            str.Append("\r\n堆栈信息：" + ex.StackTrace);
            LogHelper.WriteLog(ex.GetType(), str.ToString());
            //处理完及时清理异常
            Server.ClearError();
            //跳转至出错页面
            Response.Redirect("~/error.html"); 
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}