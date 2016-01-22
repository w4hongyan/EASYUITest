using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Tools
{
    public class LogHelper
    {
        public static void WriteLog(Type t, Exception ex)
        {
            ILog log = LogManager.GetLogger(t);
            log.Error("Error", ex);
        }
        public static void WriteLog(Type t,string msg)
        {
         ILog log=LogManager.GetLogger(t);
            log.Error(msg);
        }
    }
}
