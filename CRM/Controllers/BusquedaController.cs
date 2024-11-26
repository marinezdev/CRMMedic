using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models.Models;
using CRM.Models.Repository;

namespace CRM.Controllers
{
    public class BusquedaController : BaseController
    {
        ContactosRepositorio cr = new ContactosRepositorio();

        // GET: Busqueda
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Busca un contacto o una empresa por nombre
        /// </summary>
        /// <param name="Nombre"></param>
        /// <returns></returns>
        public ActionResult Buscar(string Nombre)
        {
            if (Nombre == "")
            {
                ViewData["Mensaje"] = "Vacio";
                ViewBag.encontrado = null;
            }
            else
                ViewBag.Encontrado = n.contactos.Buscar_Registros(Nombre);
            return View("Index");
        }

        
    }
}
