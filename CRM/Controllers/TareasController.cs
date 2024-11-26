using CRM.Models.Models;
using CRM.Models.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class TareasController : Utilerias.Comun
    {
        // GET: Tareas Agregar
        public ActionResult Index()
        {            
            ViewBag.Usuarios = n.usuarios.Seleccionar_Registros().Where(us => us.Roles.Id == 4).ToList();
            ViewBag.Actividades = Models.Catalogos.Seleccionar("Actividades"); //n.actividades.Seleccionar_Registros().ToList();
            return View();
        }

        public ActionResult Busqueda()
        {
            ViewBag.Usuarios = n.usuarios.Seleccionar_Registros().Where(us => us.Roles.Id == 4).ToList();
            return View("Busqueda");
        }

        public ActionResult Editar(string Id)
        {
            ViewBag.Usuarios = n.usuarios.Seleccionar_Registros().Where(us => us.Roles.Id == 4).ToList();
            ViewBag.Actividades = Models.Catalogos.Seleccionar("Actividades"); //n.actividades.Seleccionar_Registros().ToList();
            return View("Editar");
        }

        /// <summary>
        /// Obtiene el detalle de una tarea para modificarlo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult SeleccionarTareaEdicionPorId(string Id)
        {
            TareasUsuarios items = new TareasUsuarios();
            items = n.tareas.Seleccionar_PorId(Id);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Guarda una nueva tarea
        /// </summary>
        /// <param name="Asunto"></param>
        /// <param name="Responsable"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <param name="HoraInicio"></param>
        /// <param name="HoraFin"></param>
        /// <param name="Actividad"></param>
        /// <param name="Estado"></param>
        /// <param name="Notas"></param>
        /// <param name="Avance"></param>
        /// <param name="IdContacto"></param>
        /// <param name="IdEmpresa"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Agregar(string Asunto, string Responsable, string FechaInicio, string FechaFin, string HoraInicio, string HoraFin, string Actividad, 
        string Estado, string Notas, string Avance, string Prioridad, string IdContacto, string IdEmpresa)
        {
            Tareas items = new Tareas();

            items.Asunto = Asunto;
            items.Responsable = int.Parse(Responsable);
            items.Inicio = DateTime.Parse(FechaInicio);
            items.Fin = DateTime.Parse(FechaFin);
            items.HoraInicio = HoraInicio;
            items.HoraFin = HoraFin;
            items.Actividad = int.Parse(Actividad);
            items.Estado = int.Parse(Estado);
            items.Notas = Notas;
            items.Avance = int.Parse(Avance);
            items.Prioridad = int.Parse(Prioridad);

            ViewBag.Usuarios = n.usuarios.Seleccionar_Registros().Where(us => us.Roles.Id == 4).ToList();
            ViewBag.Actividades = n.actividades.Seleccionar_Registros().ToList();

            if (n.tuce.Agregar_Registro(n.tareas.Agregar_Registro(items).ToString(), Responsable, IdContacto, IdEmpresa))
                ViewData["Mensaje"] = "Agregado";
            else
                ViewData["Mensaje"] = "Fallo";
            ViewBag.Actividades = Models.Catalogos.Seleccionar("Actividades"); //n.actividades.Seleccionar_Registros().ToList();
            return View("Index");
        }

        /// <summary>
        /// Cambia los datos de una tarea
        /// </summary>
        /// <param name="Asunto"></param>
        /// <param name="Responsable"></param>
        /// <param name="Inicio"></param>
        /// <param name="Fin"></param>
        /// <param name="HoraInicio"></param>
        /// <param name="HoraFin"></param>
        /// <param name="Actividad"></param>
        /// <param name="Estado"></param>
        /// <param name="Notas"></param>
        /// <param name="Avance"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Modificar(string Asunto, string Responsable, string Inicio, string Fin, string HoraInicio, string HoraFin, string Actividad, string Estado, string Notas, string Avance, string Prioridad, string Id)
        {
            Tareas items = new Tareas();
            items.Asunto = Asunto;
            items.Responsable = int.Parse(Responsable);
            items.Inicio = DateTime.Parse(Inicio);
            items.Fin = DateTime.Parse(Fin);
            items.HoraInicio = HoraInicio;
            items.HoraFin = HoraFin;
            items.Actividad = int.Parse(Actividad);
            items.Estado = int.Parse(Estado);
            items.Notas = Notas;
            items.Avance = int.Parse(Avance);
            items.Prioridad = int.Parse(Prioridad);
            items.Id = int.Parse(Id);
            if (n.tareas.Modificar_Registro(items))
                ViewData["Mensaje"] = "Modificado";
            else
                ViewData["Mensaje"] = "Fallo";
            ViewBag.Actividades = Models.Catalogos.Seleccionar("Actividades"); //n.actividades.Seleccionar_Registros().ToList();
            return View();
        }

        //Tareas completadas

        public ActionResult Buscar(string Asunto, string Responsable, string Inicio, string Fin)
        {
            ViewBag.Usuarios = n.usuarios.Seleccionar_Registros().Where(us => us.Roles.Id==4).ToList();
            if (Asunto == "" && Responsable == "" && Inicio == "" && Fin == "")
            {
                ViewData["Mensaje"] = "Vacio";
            }
            else
            {
                ViewBag.Encontrado = n.tareas.Buscar_Registros(Asunto, Responsable, Inicio, Fin).ToList();
            }
            return View("Busqueda");
        }
    }
}
