using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class BaseController : Utilerias.Comun
    {
        /// <summary>
        /// Sesion para transportar entre controladores
        /// </summary>
        public System.Web.SessionState.HttpSessionState GranSesion
        {
            get
            {
                return System.Web.HttpContext.Current.Session;
            }
        }


    }
}
