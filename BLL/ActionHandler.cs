using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class ActionHandler
    {
        public delegate void ActionEventHandler(object sender, ActionEventArgs e);
        public event ActionEventHandler Action;
        public virtual void OnAction(ActionEventArgs e)
        {
            if (Action != null)
            {
                Action(this, e);
            }
        }
    }
    public class ActionEventArgs:EventArgs
    {
        public readonly HttpContext context;
        public readonly string action;
         public ActionEventArgs (HttpContext context,string action)
	    {
             this.context=context;
             this.action = action;
	    }
    }
}
