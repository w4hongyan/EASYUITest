using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class WeiXinHelper
    {
        static WeiXinHelper()
        {
            //注册
            AccessTokenContainer.Register(ConfigurationManager.AppSettings["weixinAppID"], ConfigurationManager.AppSettings["weixinAppSecret"]);
        }
        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="template">模板Id</param>
        /// <param name="data">发送数据</param>
        /// <param name="touser">用户的openId</param>
        public static SendTemplateMessageResult SendTemplateMessage(TemplateType type, object data, string touserOpenId, string url, string topcolor = "#FF0000")
        {
            var openId = touserOpenId;//换成已经关注用户的openId
            string templateId = "";//换成已经在微信后台添加的模板Id
            switch (type)
            {
                case TemplateType.故障通报通知:
                    templateId = System.Configuration.ConfigurationManager.AppSettings["WarningTemplateData"];
                    break;
            }
            var accessToken = AccessTokenContainer.GetAccessToken(ConfigurationManager.AppSettings["weixinAppID"]);
            var result = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(accessToken, openId, templateId, topcolor, url, data);
            return result;
        }
        /// <summary>
        /// 发送故障信息
        /// </summary>
        /// <param name="first"></param>
        /// <param name="performance"></param>
        /// <param name="time"></param>
        /// <param name="remark"></param>
        /// <param name="toUserOpenId"></param>
        public static void SendWarning(string first,string performance,string time,string remark,string[] toUserOpenIds)
        {
            foreach (var UserOpenId in toUserOpenIds)
            {
                SendTemplateMessage(TemplateType.故障通报通知, new WarningTemplateData { first = new TemplateDataItem(first), performance = new TemplateDataItem(performance), time = new TemplateDataItem(time), remark = new TemplateDataItem(remark) }, UserOpenId, "");
            }
           
        }
        /// <summary>
        /// 发送客服消息
        /// </summary>
        /// <param name="content">发送数据</param>
        /// <param name="touser">用户的openId</param>
        public static WxJsonResult SendTextMessage(string touserOpenId, string content)
        {
            var openId = touserOpenId;//换成已经关注用户的openId
            var accessToken = AccessTokenContainer.GetAccessToken(ConfigurationManager.AppSettings["weixinAppID"]);
            var result = Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendText(accessToken, openId, content);
            return result;
        }
    }
    public enum TemplateType
    {
        故障通报通知
    }
    /// <summary>
    /// 订单支付成功模板
    /// </summary>
    public class WarningTemplateData
    {
        public TemplateDataItem first { get; set; }//第一行信息
        public TemplateDataItem performance { get; set; }//现象
        public TemplateDataItem time { get; set; }//故障时间
        public TemplateDataItem remark { get; set; }//备注

    }
}
