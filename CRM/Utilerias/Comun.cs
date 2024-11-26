using System.Web.Mvc;

namespace CRM.Utilerias
{
    // RMF 210816 test

    /// <summary>
    /// Clase general contenedora instanciadora de procesos 
    /// </summary>
    public class Comun : Controller
    {
        public Correo correo;
        /// <summary>
        /// Instancia de Acceso a negocio
        /// </summary>
        public Models.Business.Business n;

        
        public Comun()
        {
            n = new Models.Business.Business();
            correo = new Correo();

        }
    }
}