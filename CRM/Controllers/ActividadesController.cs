using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models.Models;
using CRM.Models.Repository;
using CRM.Utilerias;

namespace CRM.Controllers
{
    [SessionTimeOut]
    [HandleError]
    public class ActividadesController : Utilerias.Comun
    {
        /// <summary>
        /// Agrega una nueva actividad
        /// </summary>
        /// <param name="Nombre"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Agregar(string Nombre)
        {
            Actividades items = new Actividades();
            items.Nombre = Nombre;
            
            string mensaje = "";
            if (n.actividades.Agregar_Registro(items, ref mensaje))
            {
                ViewBag.Mensaje = mensaje;
            }
            else
                ViewBag.Mensaje = mensaje;
            return View("Index");
        }

        /// <summary>
        /// Agrega una nueva actividad
        /// </summary>
        /// <param name="eNombre"></param>
        /// <returns></returns>
        public JsonResult AgregarActividad(string eNombre)
        {
            Actividades item = new Actividades();
            item.Nombre = eNombre;
            return Json(n.actividades.Agregar_Seleccionar(item), JsonRequestBehavior.AllowGet);
        }
    }
}
