using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Utilerias
{
    public class SessionTimeOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["NombreRol"] == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}