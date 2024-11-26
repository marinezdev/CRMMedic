using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models.Models;
using CRM.Models.Repository;
using CRM.Utilerias;

namespace CRM.Controllers
{
    [SessionTimeOut]
    public class OportunidadesActividadesController : BaseController
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        /// <summary>
        /// Obtiene el detalle de una actividad que pertenece a una oportunidad para modificarlo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult SeleccionarPorId(string Id)
        {
            var obtenido = n.oportunidadesactividades.Seleccionar_PorId(Id);
            return Json(obtenido, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene todas las actividades que pertecen a una oportunidad
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult SeleccionarOportunidadesActividades(string Id)
        {
            var obtenidos = n.oportunidadesactividades.Seleccionar_PorIdOportunidad(Id);
            return Json(obtenidos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Agregar()
        {
            OportunidadesActividades oa = new OportunidadesActividades();
            oa.TipoActividad = int.Parse(Request.Form["TipoActividad"]);
            oa.Fecha = DateTime.Parse(Request.Form["Vencimiento"]);
            oa.Notas = Request.Form["Descripcion"];
            oa.IdOportunidad = int.Parse(Request.Form["IdOportunidad"]);
            oa.IdUsuario = int.Parse(Request.Form["IdUsuario"]);
            n.oportunidadesactividades.Agregar_Registro(oa);
            //ViewBag.Archivos = ar.SeleccionarPorIdOportunidad(Session["IdOportunidad"].ToString());
            return View("/Oportunidades/Actividades");
        }

        /// <summary>
        /// Guarda una nueva actividad que pertenecerá a una oportunidad
        /// </summary>
        /// <param name="aActividad"></param>
        /// <param name="aFecha"></param>
        /// <param name="aHora"></param>
        /// <param name="aUsuarioAsignado"></param>
        /// <param name="aNotas"></param>
        /// <param name="aIdOportunidad"></param>
        /// <param name="aIdUsuarioAltaActividad"></param>
        /// <returns></returns>
        public JsonResult AgregarActividadAOportunidad(string aActividad, string aFecha, string aHora, string aUsuarioAsignado, string aNotas, string aIdOportunidad, 
        string aIdUsuarioAltaActividad)
        {
            OportunidadesActividades items = new OportunidadesActividades();
            //items.Actividad = int.Parse(aActividad);
            //items.Fecha = DateTime.Parse(aFecha);
            //items.Hora = aHora;
            //items.IdUsuarioAsignado = int.Parse(aUsuarioAsignado);
            //items.Notas = aNotas;
            //items.IdOportunidad = int.Parse(aIdOportunidad);
            //items.IdUsuario = int.Parse(aIdUsuarioAltaActividad);
            //oar.Agregar(items);
            var obtenidos = n.oportunidadesactividades.Seleccionar_PorIdOportunidad(aIdOportunidad);
            return Json(obtenidos, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Modifica el detalle de una actividad que pertenece a una oportunidad
        /// </summary>
        /// <param name="eActividad"></param>
        /// <param name="eFecha"></param>
        /// <param name="eHora"></param>
        /// <param name="eUsuarioAsignado"></param>
        /// <param name="eNotas"></param>
        /// <param name="eIdOportunidad"></param>
        /// <param name="eIdOportunidadActividad"></param>
        /// <returns></returns>
        public JsonResult Modificar(string eActividad, string eFecha, string eHora, string eUsuarioAsignado, string eNotas, string eIdOportunidad, 
        string eIdOportunidadActividad)
        {
            OportunidadesActividades items = new OportunidadesActividades();
            //items.Actividad = int.Parse(eActividad);
            //items.Fecha = DateTime.Parse(eFecha);
            //items.Hora = eHora;
            //items.IdUsuarioAsignado = int.Parse(eUsuarioAsignado);
            //items.Notas = eNotas;
            //items.Id = int.Parse(eIdOportunidadActividad);
            n.oportunidadesactividades.Modificar_Registro(items);
            var obtenidos = n.oportunidadesactividades.Seleccionar_PorIdOportunidad(eIdOportunidad);
            return Json(obtenidos, JsonRequestBehavior.AllowGet);
        }




    }
}
