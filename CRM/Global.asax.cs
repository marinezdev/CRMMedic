using CRM.Controllers;
using Microsoft.Ajax.Utilities;
using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace CRM
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

       protected void Application_Error()
        {
            Exception ex = Server.GetLastError();
            HttpException httpexception = new HttpException();
            string accion = "";
            if (httpexception.GetHttpCode() == 404)
            {
                accion = "NotFound";
            }
            else if (httpexception.GetHttpCode() == 500)
            {
                accion = "NotFunction";
            }
            else
            {
                accion = "Index";
            }
            Context.ClearError();
            RouteData rutaerror = new RouteData();
            rutaerror.Values.Add("controller", "Error");
            rutaerror.Values.Add("action", accion);
            IController controlador = new ErrorController();
            controlador.Execute(new RequestContext(new HttpContextWrapper(Context), rutaerror));
        }

        public void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["Usuario"];
            if (cookie != null)
            {
                string datos = cookie.Value;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(datos);
                string rol = ticket.UserData;
                GenericPrincipal principal = new GenericPrincipal(new GenericIdentity(ticket.Name), new string[] { rol });
                HttpContext.Current.User = principal;
            }
        }

    }
}
