using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using CRM.Models.Models;
using CRM.Models.Repository;
using CRM.Utilerias;

namespace CRM.Controllers
{
    [SessionTimeOut]
    [HandleError]
    public class OportunidadesController : Utilerias.Comun
    {
        private UsuariosRoles usuariosrolessesion;        

        public ActionResult Index()
        {
           usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            //ViewBag.Asuntos = or.Seleccionar();
            string gerentes = "";
            if (usuariosrolessesion.Roles.Id == 3)  //Operadores
                gerentes = usuariosrolessesion.Usuarios.Id.ToString();
            if (usuariosrolessesion.Roles.Id == 6)  //Gerentes de proyecto
                gerentes = "6";
            if (usuariosrolessesion.Roles.Id == 7)  //Comisionistas
                gerentes = usuariosrolessesion.Usuarios.Id.ToString();

            if (usuariosrolessesion.Roles.Id == 8) //if (usuariosrolessesion.Configuracion.Id == 2 && usuariosrolessesion.Roles.Id == 8)
                ViewBag.Asuntos = n.oportunidades.Seleccionar_TemasEnProcesoRedesSociales(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), gerentes);
            else
                ViewBag.Asuntos = n.oportunidades.Seleccionar_TemasEnProceso(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), gerentes);

            ViewBag.AsuntosUsuarios = n.oportunidades.Seleccionar_TemasEnProcesoParaUsuarios(usuariosrolessesion.Usuarios.Id.ToString());
          
            return View();
        }

        [HttpGet]
        public ActionResult Graficos(string udn, string opc, string ann)
        {
            udn = string.IsNullOrEmpty(udn) ? "1" : udn;
            opc = string.IsNullOrEmpty(opc) ? "1" : opc;
            ann = string.IsNullOrEmpty(ann) ? DateTime.Now.Year.ToString() : ann;
            ViewBag.Asuntos = n.oportunidades.Seleccionar_TemasEnProceso_filtrados(udn, opc, ann);
            ViewBag.Ann = n.oportunidades.AñosDisponibles();

            string nombreudn = "";
            if (udn == "1")
                nombreudn = "Outsourcing";
            else if (udn == "2")
                nombreudn = "Servicios";
            else if (udn == "3")
                nombreudn = "Soluciones Empresariales";
            else if (udn == "4")
                nombreudn = "Tecnología";
            else if (udn == "6")
                nombreudn = "Comisionista";
            else if (udn == "7")
                nombreudn = "Redes Sociales";
            else if (udn == "8")
                nombreudn = "Sistemas";
            else if (udn == "99")
                nombreudn = "Global";

            if (opc == "1")
                ViewBag.Opcion = "(" + nombreudn + " - En Proceso)";
            else if (opc == "3")
                ViewBag.Opcion = "(" + nombreudn + " - Cerrados Perdidos)";
            else if (opc == "4")
                ViewBag.Opcion = "(" + nombreudn + " - Cerrados Ganados)";
            else if (opc == "6")
                ViewBag.Opcion = "(" + nombreudn + " - Cancelados)";
            else if (opc == "7")
                ViewBag.Opcion = "(" + nombreudn + " - Suspendidos)";


            return View();
        }

        public ActionResult Busqueda()
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            //ViewBag.Empresas = er.Seleccionar(usr.Configuracion.Id.ToString(), usr.Roles.Id.ToString(), usr.Usuarios.Id.ToString());
            ViewBag.Clasificacion = n.clasificacion.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString()); //Ejemplo: comun.crud.clasificacion_.Seleccionar(usr.Configuracion.Id.ToString());
            return View("Busqueda");
        }

        /// <summary>
        /// Alta SABE
        /// </summary>
        /// <returns></returns>
        public ActionResult Alta()
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            //ViewBag.Empresas = er.Seleccionar(usr.Configuracion.Id.ToString());
            ViewBag.Usuarios = n.usuarios.Seleccionar_Registros().Where(us => us.Roles.Id == 4);
            ViewBag.Clasificacion = n.clasificacion.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString());
            ViewBag.Clasificacion2 = n.clasificacion.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString());

            return View();
        }

        /// <summary>
        /// Alta ASAE
        /// </summary>
        /// <returns></returns>
        public ActionResult Alta2()
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Empresas = n.empresas.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString(), usuariosrolessesion.Roles.Id.ToString(), usuariosrolessesion.Usuarios.Id.ToString());
            ViewBag.Usuarios = n.usuarios.Seleccionar_Registros().Where(us => us.Roles.Id == 4);
            ViewBag.Clasificacion = n.clasificacion.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString());
            ViewBag.Atencion = Utilerias.Funciones.PeriodoAtencion();
            ViewBag.Estado = Utilerias.Funciones.Estado();
            ViewBag.Clasificacion2 = n.clasificacion.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString());
            ViewBag.Monedas = n.moneda.Seleccionar_Registros();
            //ViewBag.UDN = udnr.Seleccionar();
            return View();
        }

        public ActionResult AgregarArchivo(string Id)
        {
            int i = 0;
            bool resultado = int.TryParse(Id, out i);
            if (!resultado)
            {
                return RedirectToAction("Index");
            }
            Session["IdOportunidad"] = Id;
            //ViewBag.Archivos = ar.SeleccionarPorIdOportunidad(Id);
            return View();
        }

        public ActionResult EliminarArchivo(string id)
        {
            var _NombreArchivo = n.archivos.Seleccionar_PorNombre(id);
            string _ruta = Path.Combine(Server.MapPath("~/Archivos"), _NombreArchivo);
            if (System.IO.File.Exists(_ruta))
            {
                System.IO.File.Delete(_ruta);
                //Eliminar tambien de la bd
                n.archivos.Eliminar_Registro(id);
                ViewBag.Procesado = "<div class='alert alert-success' role='alert' style='width:100%'>Se eliminó el archivo seleccionado.</div><br />";
            }
            ViewBag.Archivos = n.archivos.Seleccionar_PorIdOportunidad(Session["IdOportunidad"].ToString());
            return View("AgregarArchivo");
        }

        public ActionResult AgregarArchivoResponsable()
        {
            Session["IdBitacora"] = Request["IdB"];
            Session["IdOportunidad"] = Request["IdO"];
            ViewBag.ArchivosBitacora = n.archivosbitacora.Seleccionar_PorIdBitacora(Session["IdBitacora"].ToString());
            return View();
        }

        public ActionResult AgregarArchivoResponsable2(string id)
        {
            Session["IdOportunidad"] = id;
            ViewBag.Archivos = n.archivos.Seleccionar_PorIdOportunidad(id);
            return View();
        }

        public ActionResult EliminarArchivoBitacora(string id)
        {
            var _NombreArchivo = n.archivosbitacora.Seleccionar_PorNombre(id);
            string _ruta = Path.Combine(Server.MapPath("~/ArchivosBitacora"), _NombreArchivo);
            if (System.IO.File.Exists(_ruta))
            {
                System.IO.File.Delete(_ruta);
                //Eliminar tambien de la bd
                n.archivos.Eliminar_Registro(id);
                ViewBag.Procesado = "<div class='alert alert-success' role='alert' style='width:100%'>Se eliminó el archivo seleccionado.</div><br />";
            }
            ViewBag.Archivos = n.archivosbitacora.Seleccionar_PorIdBitacora(Session["IdBitacora"].ToString());
            return View("AgregarArchivoResponsable");
        }

        public ActionResult Responsables(string Id)
        {
            int i = 0;
            bool resultado = int.TryParse(Id, out i);
            if (!resultado)
            {
                return RedirectToAction("Index");
            }
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Contactos = n.contactos.Seleccionar_ContactosPorConfiguracion(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), Session["IdUsuario"].ToString());
            ViewBag.Usuarios = n.usuarios.Seleccionar_PorRol(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), usuariosrolessesion.Usuarios.Id.ToString());
            ViewBag.ClasificacionRolesContactos = n.clasificacionrolescontactos.Seleccionar_Registros();

            ViewBag.Responsables = n.oportunidadesresponsables.Seleccionar_ResponsablesPorOportunidad(Id, Session["IdConfiguracion"].ToString());
            //ViewBag.UsuariosAsignados = ur.SeleccionarPorIdOportunidad(Id);

            //if (Session["IdConfiguracion"].ToString() == "3")
            //{
            //    ViewBag.UsuariosAsignados = ur.SeleccionarPorIdOportunidad(IdOportunidad);
            //}
            //else if (Session["IdConfiguracion"].ToString() == "2")
            //{
            //    ViewBag.UsuariosAsignados = our.SeleccionarPorIdOportunidad(IdOportunidad);
            //}

            return View();
        }

        public ActionResult EliminarResponsable(string idresponsable, string idoportunidad)
        {
            n.oportunidadesusuarios.Eliminar_UsuarioResponsableOportunidad(idresponsable, idoportunidad);
            n.bitacora.Eliminar_UsuarioDeBitacora(idresponsable, idoportunidad);
            n.oportunidadesresponsables.Eliminar_Registro(idoportunidad, idresponsable);
            ViewBag.Procesado = "<div class='alert alert-success' role='alert' style='width:100%'>Se eliminó el responsable seleccionado.</div><br />";
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Contactos = n.contactos.Seleccionar_ContactosPorConfiguracion(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), Session["IdUsuario"].ToString());
            ViewBag.Usuarios = n.usuarios.Seleccionar_PorRol(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), usuariosrolessesion.Usuarios.Id.ToString());
            ViewBag.ClasificacionRolesContactos = n.clasificacionrolescontactos.Seleccionar_Registros();
            ViewBag.Responsables = n.oportunidadesresponsables.Seleccionar_ResponsablesPorOportunidad(idoportunidad, Session["IdConfiguracion"].ToString());
            ViewBag.UsuariosAsignados = n.usuarios.Seleccionar_PorIdOportunidad(idoportunidad);

            //Enviar correo de desasignación

            var titulocorreo = "Desasignación de Responsbilidad en CRM - Control de Gestión";
            var correoresponsable = n.usuarios.Seleccionar_CorreoReponsable(idresponsable);
            var nombre = n.usuarios.Seleccionar_Nombre(idresponsable);
            if (correoresponsable.Contains("@") || correoresponsable.Contains("."))
            {
                if (correo.EnviarCorreoBajaResponsable(nombre, correoresponsable, titulocorreo, Session["IdConfiguracion"].ToString()))
                    ViewBag.CorreoEnviado = "Envio exitoso";
                else
                    ViewBag.CorreoEnviado = "Envío Fallido";
            }

            return View("Responsables");
        }

        public ActionResult Editar(string Id)
        {
            int i = 0;
            bool resultado = int.TryParse(Id, out i);
            if (!resultado)
            {
                return RedirectToAction("Index");
            }
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            //Session["UsuarioComoResponsable"] = oar.SeleccionarSiUsuarioEstaAsignadoComoResponsableAActividad(Id, usr.Usuarios.Id.ToString()) > 0 ? true : false;
            ViewBag.Empresas = n.empresas.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString(), usuariosrolessesion.Roles.Id.ToString(), usuariosrolessesion.Usuarios.Id.ToString());
            ViewBag.Usuarios = n.usuarios.Seleccionar_Registros().Where(us => us.Roles.Id == 4);
            ViewBag.Clasificacion = n.clasificacion.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString());
            ViewBag.Atencion = Utilerias.Funciones.PeriodoAtencion();
            ViewBag.Responsables = n.oportunidadesresponsables.Seleccionar_ResponsablesPorOportunidad(Id, Session["IdConfiguracion"].ToString());
            ViewBag.ResponsablesParaReasignar = n.usuarios.Seleccionar_UsuariosResponsablesASAE("2", usuariosrolessesion.Usuarios.Id.ToString());
            if (usuariosrolessesion.Configuracion.Id == 3)
                ViewBag.Estado = Utilerias.Funciones.EstadosMabe();
            else if (usuariosrolessesion.Configuracion.Id == 2)
                ViewBag.Estado = Utilerias.Funciones.Estado();
            ViewBag.Clasificacion2 = n.clasificacion.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString());
            ViewBag.UDN = n.udn.Seleccionar_Registros();

            ViewBag.ContactosEsc = n.contactos.Seleccionar_ContactosPorConfiguracion(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), Session["IdUsuario"].ToString());
            ViewBag.UsuariosEsc = n.usuarios.Seleccionar_PorRol(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), usuariosrolessesion.Usuarios.Id.ToString());

            ViewBag.Monedas = n.moneda.Seleccionar_Registros();

            ViewBag.ResponsablesParaCompartir = n.usuarios.Seleccionar_UsuariosResponsablesASAE("2", usuariosrolessesion.Usuarios.Id.ToString());

            Session["TieneBitacora"] = n.bitacorausuarios.Seleccionar_ValidarSiUsuarioTieneBitacora(Id, usuariosrolessesion.Usuarios.Id.ToString());

            var usuariosroles = @Session["GranSesion"] as UsuariosRoles;

            Modelos sesionmodelos = new Modelos();
            if (Id == null)
            {
                var x = Session["Editar"];
            }
            else
            {
                //Carga por primera vez
                sesionmodelos.Oportunidades.Id = int.Parse(Id);
                sesionmodelos.Empresas.Id = n.oecu.Seleccionar_EmpresaPorOportunidad(Id) == "" ? 0 : int.Parse(n.oecu.Seleccionar_EmpresaPorOportunidad(Id));
                sesionmodelos.Oportunidades.Nombre = n.oportunidades.Seleccionar_NombreTemaPorIdoportunidad(Id);
                Session["Editar"] = sesionmodelos;
            }

            Session["IdOportunidad"] = Id;
            Session["IdEmpresa"] = n.oecu.Seleccionar_EmpresaPorOportunidad(Id) == "" ? "0" : n.oecu.Seleccionar_EmpresaPorOportunidad(Id);
            Session["NombreTema"] = n.oportunidades.Seleccionar_NombreTemaPorIdoportunidad(Id);
            Session["EstadoOportunidad"] = n.oportunidades.Seleccionar_EstadoActual(Id);

            return View("Editar", sesionmodelos);
        }

        public ActionResult Edicion(string Id)
        {
            int i = 0;
            bool resultado = int.TryParse(Id, out i);
            if (!resultado)
            {
                return RedirectToAction("Index");
            }
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            //Session["UsuarioComoResponsable"] = oar.SeleccionarSiUsuarioEstaAsignadoComoResponsableAActividad(Id, usr.Usuarios.Id.ToString()) > 0 ? true : false;
            ViewBag.Empresas = n.empresas.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString(), usuariosrolessesion.Roles.Id.ToString(), usuariosrolessesion.Usuarios.Id.ToString());
            ViewBag.Usuarios = n.usuarios.Seleccionar_Registros().Where(us => us.Roles.Id == 4);
            ViewBag.Clasificacion = n.clasificacion.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString());
            ViewBag.Atencion = Utilerias.Funciones.PeriodoAtencion();
            ViewBag.Responsables = n.oportunidadesresponsables.Seleccionar_ResponsablesPorOportunidad(Id, Session["IdConfiguracion"].ToString());
            ViewBag.ResponsablesParaReasignar = n.usuarios.Seleccionar_UsuariosResponsablesASAE("2", usuariosrolessesion.Usuarios.Id.ToString());
            if (usuariosrolessesion.Configuracion.Id == 3)
                ViewBag.Estado = Utilerias.Funciones.EstadosMabe();
            else if (usuariosrolessesion.Configuracion.Id == 2)
                ViewBag.Estado = Utilerias.Funciones.Estado();
            ViewBag.Clasificacion2 = n.clasificacion.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString());
            ViewBag.UDN = n.udn.Seleccionar_Registros();

            ViewBag.ContactosEsc = n.contactos.Seleccionar_ContactosPorConfiguracion(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), Session["IdUsuario"].ToString());
            ViewBag.UsuariosEsc = n.usuarios.Seleccionar_PorRol(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), usuariosrolessesion.Usuarios.Id.ToString());

            ViewBag.Monedas = n.moneda.Seleccionar_Registros();

            ViewBag.ResponsablesParaCompartir = n.usuarios.Seleccionar_UsuariosResponsablesASAE("2", usuariosrolessesion.Usuarios.Id.ToString());

            Session["TieneBitacora"] = n.bitacorausuarios.Seleccionar_ValidarSiUsuarioTieneBitacora(Id, usuariosrolessesion.Usuarios.Id.ToString());

            var usuariosroles = @Session["GranSesion"] as UsuariosRoles;

            Modelos sesionmodelos = new Modelos();
            if (Id == null)
            {
                var x = Session["Editar"];
            }
            else
            {
                //Carga por primera vez
                sesionmodelos.Oportunidades.Id = int.Parse(Id);
                sesionmodelos.Empresas.Id = n.oecu.Seleccionar_EmpresaPorOportunidad(Id) == "" ? 0 : int.Parse(n.oecu.Seleccionar_EmpresaPorOportunidad(Id));
                sesionmodelos.Oportunidades.Nombre = n.oportunidades.Seleccionar_NombreTemaPorIdoportunidad(Id);
                Session["Editar"] = sesionmodelos;
            }

            Session["IdOportunidad"] = Id;
            Session["IdEmpresa"] = n.oecu.Seleccionar_EmpresaPorOportunidad(Id) == "" ? "0" : n.oecu.Seleccionar_EmpresaPorOportunidad(Id);
            Session["NombreTema"] = n.oportunidades.Seleccionar_NombreTemaPorIdoportunidad(Id) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + CustomHelpers.EstadosdeProcesoSABE(n.oportunidades.Seleccionar_EstadoActual(Id), "") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + n.usuarios.Seleccionar_Nombre(n.usuarios.Seleccionar_CreadorOportunidad(Id));

            return View("Edicion", sesionmodelos);
        }

        public ActionResult Contactos()
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];

            //if (usuariosrolessesion.Configuracion.Id.ToString() == "3")
            //{
            //    ViewBag.Contactos = n.contactos.Seleccionar_ContactosPorConfiguracion(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), Session["IdUsuario"].ToString());
            //}
            //else 
            //if (usuariosrolessesion.Configuracion.Id.ToString() == "2")
            //{
                var idemp = "0";
                if (Session["IdEmpresa"].ToString() != "0")
                {
                    idemp = Session["IdEmpresa"].ToString();
                }
                else //(Request["IdE"] != null)
                {
                    int i = 0;
                    bool resultado = int.TryParse(Request["IdE"].ToString(), out i);
                    if (!resultado)
                    {
                        return RedirectToAction("Index");
                    }
                    idemp = Request["IdE"].ToString();
                }
                ViewBag.Contactos = n.contactos.Seleccionar_ContactosPorConfiguracion(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), Session["IdUsuario"].ToString(), idemp);
                if (Session["IdEmpresa"].ToString() == "0")
                {
                    ViewBag.Mensaje = "<div class='alert alert-warning' role='alert'>Datos incompletos: debe seleccionar una empresa en el detalle.</div>";
                }
            //}
            ViewBag.Asignados = n.contactos.Seleccionar_ContactosAsignadosAOportunidad(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdOportunidad"].ToString());
            ViewBag.TipoPersona = n.tipopersona.Seleccionar_Registros();
            return View();
        }

        public ActionResult EliminarContacto(string idcontacto, string idoportunidad)
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            n.contactos.Eliminar_ContactoDeOportunidad(idcontacto, idoportunidad);
            ViewBag.Procesado = "<div class='alert alert-success' role='alert' style='width:100%'>Se eliminó el contacto seleccionado.</div><br />";
            ViewBag.Contactos = n.contactos.Seleccionar_ContactosPorConfiguracion(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), Session["IdUsuario"].ToString());
            ViewBag.Asignados = n.contactos.Seleccionar_ContactosAsignadosAOportunidad(usuariosrolessesion.Configuracion.Id.ToString(), Session["IdOportunidad"].ToString());
            ViewBag.TipoPersona = n.tipopersona.Seleccionar_Registros();

            return View("Contactos");
        }

        public ActionResult Actividades(string Id)
        {
            int i = 0;
            bool resultado = int.TryParse(Id, out i);
            if (!resultado)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Actividades = n.oportunidadesactividades.Seleccionar_PorIdOportunidad(Id);
            return View();
        }

        public ActionResult BitacoraUsuario(string Id)
        {
            int i = 0;
            bool resultado = int.TryParse(Id, out i);
            if (!resultado)
            {
                return RedirectToAction("Index");
            }
            //ViewBag.ActividadesUsuarios = budr.SeleccionarPorIdUsuarioIdOportunidad(Session["IdUsuario"].ToString(), Session["IdOportunidad"].ToString());

            //ViewBag.Bitacora = bur.SeleccionarIdOportunidad(Session["IdOportunidad"].ToString()); //Si es por usuario, solo agregarlo
            ViewBag.Bitacora = n.bitacorausuariosdetalle.Seleccionar_PorIdOportunidad(Session["IdOportunidad"].ToString());
            return View();
        }

        /// <summary>
        /// Busca todas las oportunidades que coinciden con los datos buscados
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        public ActionResult Buscar(string Nombre, string Empresa, string Inicio, string Fin, string Clasificacion, string SubClasificacion)
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            //ViewBag.Empresas = er.Seleccionar(usr.Configuracion.Id.ToString(), usr.Roles.Id.ToString(), usr.Usuarios.Id.ToString());
            ViewBag.Clasificacion = n.clasificacion.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString());
            List<OportunidadesBuscar> o = new List<OportunidadesBuscar>();
            if (Nombre == "")
            {
                ViewData["Mensaje"] = "Vacio";
                ViewBag.encontrado = null;
            }
            else
                ViewBag.Encontrado = n.oportunidades.Buscar_Registros(Nombre, Empresa, Inicio, Fin, Clasificacion == null ? "" : Clasificacion, SubClasificacion == null ? "" : SubClasificacion);
            return View("Busqueda");
        }

        public ActionResult Prueba()
        {
            var boton1 = Request.Form["Boton1"];
            var boton2 = Request.Form["Boton2"];
            var obtenido = Request.Form["Valor"];

            string resultado = "";
            if (boton1 != null)
                resultado = "Se presionó el botón 1 y su valor es " + obtenido;
            else if (boton2 != null)
                resultado = "Se presionó el botón 2 y su valor es " + obtenido;
            return View("Index", resultado);
        }

        public ActionResult ResponsableActividades(string id)
        {
            Session["IdOportunidad"] = id;
            Session["NombreTema"] = n.oportunidades.Seleccionar_NombreTemaPorIdoportunidad(id);
            ViewBag.Actividades = n.bitacorausuariosdetalle.Seleccionar_PorIdOportunidad(id); //bur.SeleccionarIdOportunidad(id); // oar.SeleccionarPorIdOportunidad(id);

            return View();
        }

        public ActionResult Calendario()
        {
            return View();
        }

        public ActionResult Metodologia()
        {
            return View();
        }

        public JsonResult Buscar2(string Nombre, string Empresa, string Inicio, string Fin, string Clasificacion, string SubClasificacion)
        {
            List<OportunidadesBuscar> o = new List<OportunidadesBuscar>();
            o = n.oportunidades.Buscar_Registros(
                string.IsNullOrEmpty(Nombre) ? "" : Nombre, 
                string.IsNullOrEmpty(Empresa) ? "" : Empresa, 
                string.IsNullOrEmpty(Inicio) ? "" : Inicio, 
                string.IsNullOrEmpty(Fin) ? "" : Fin, 
                string.IsNullOrEmpty(Clasificacion) ? "" : Clasificacion, 
                string.IsNullOrEmpty(SubClasificacion) ? "" : SubClasificacion);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene el detalle de una oportunidad para modificarlo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult SeleccionarDetalleOportunidadEdicion(string id)
        {
            return Json(n.oportunidades.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Agregar(string Nombre, string Importe, string Cierre, string Asignado, string Avance, string Notas, 
        string IdClasificacion, string IdSubclasificacion, string PeriodoAtencion, string Estado, string Contraparte, 
        string Caracteristicas, string IdUDN, string Empresa, string IdUsuario, string idconfiguracion)
        {
            Oportunidades items = new Oportunidades();
            items.Nombre = Nombre;
            items.Importe = string.IsNullOrEmpty(Importe) ? 0 : decimal.Parse(Importe);
            items.Cierre = string.IsNullOrEmpty(Cierre) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Cierre);
            items.Asignado = string.IsNullOrEmpty(Asignado) ? 0 : int.Parse(Asignado);
            items.Avance = string.IsNullOrEmpty(Avance) ? 0 : int.Parse(Avance);
            items.Notas = Notas;
            items.IdClasificacion = string.IsNullOrEmpty(IdClasificacion) ? 0 : int.Parse(IdClasificacion);
            items.IdSubClasificacion = string.IsNullOrEmpty(IdSubclasificacion) ? 0 : int.Parse(IdSubclasificacion);
            items.PeriodoAtencion = string.IsNullOrEmpty(PeriodoAtencion) ? 0 : int.Parse(PeriodoAtencion);
            //items.Aviso = string.IsNullOrEmpty(Aviso) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Aviso);
            items.Estado = string.IsNullOrEmpty(Estado) ? 0 : int.Parse(Estado);
            items.Contraparte = Contraparte;
            items.Caracteristicas = Caracteristicas;
            items.UDN = string.IsNullOrEmpty(IdUDN) ? 0 : int.Parse(IdUDN);
            items.IdConfiguracion = int.Parse(idconfiguracion);

            int idoportunidadAlta = n.oportunidades.Agregar_Registro(items);
            List<Modelos> listado = new List<Modelos>();            
            if (n.oecu.Agregar_SoloOportunidad(idoportunidadAlta.ToString(), IdUsuario, idconfiguracion) > 0)
            {
                n.oecu.Agregar_SoloEmpresa(Empresa, idoportunidadAlta.ToString());
                //listado = or.SeleccionarTop5(idconfiguracion, Session["IdRol"].ToString());
                n.estadooportunidad.Agregar_Registro(idoportunidadAlta.ToString(), "1", "Alta de Oportunidad");
            }
            
            Session["IdOportunidad"] = idoportunidadAlta.ToString();
            Session["IdEmpresa"] = n.oecu.Seleccionar_EmpresaPorOportunidad(idoportunidadAlta.ToString());
            Session["NombreTema"] = n.oportunidades.Seleccionar_NombreTemaPorIdoportunidad(idoportunidadAlta.ToString());
            Session["TieneBitacora"] = "0";

            return Json(idoportunidadAlta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarCompleto(string Nombre, string Clasificacion, string SubClasificacion, string Empresa, string PeriodoAtencion, 
        string Vencimiento, string Notas, string IdUsuario, string IdConfiguracion, string IdUDN,
        string Importe, string TipoMoneda, string TipoCambio, string Rubro)
        {
            Oportunidades items = new Oportunidades();
            items.Nombre = Nombre;
            items.IdClasificacion = string.IsNullOrEmpty(Clasificacion) ? 0 : int.Parse(Clasificacion);
            items.IdSubClasificacion = string.IsNullOrEmpty(SubClasificacion) ? 0 : int.Parse(SubClasificacion);
            if (Importe.Contains("$"))
            {
                Importe = Importe.Replace("$", "");
            }
            if (Importe.Contains(","))
            {
                Importe = Importe.Replace(",", "");
            }
            items.Importe = string.IsNullOrEmpty(Importe) ? 0 : decimal.Parse(Importe);            
            items.PeriodoAtencion = string.IsNullOrEmpty(PeriodoAtencion) ? 0 : int.Parse(PeriodoAtencion);
            items.Cierre = string.IsNullOrEmpty(Vencimiento) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Vencimiento);
            items.Notas = Notas;
            items.IdConfiguracion = int.Parse(IdConfiguracion);
            items.UDN = string.IsNullOrEmpty(IdUDN) ? n.usuarios.Seleccionar_UDNUsuario(Session["IdUsuario"].ToString()) : int.Parse(IdUDN);

            //Agrega la oportunidad
            int idoportunidadAlta = n.oportunidades.Agregar_Completo(items);

            //Agrega el importe
            n.oportunidadesimportes.Agregar_Registro(idoportunidadAlta.ToString(), items.Importe.ToString(), TipoMoneda, TipoCambio, "", "");

            //Valida si se agrega la oportunidad para dar de alta la empresa a la que se asignó la oportunidad
            if (n.oecu.Agregar_SoloOportunidad(idoportunidadAlta.ToString(), IdUsuario, IdConfiguracion) > 0)
            {
                n.oecu.Agregar_SoloEmpresa(Empresa, idoportunidadAlta.ToString());
               n.estadooportunidad.Agregar_Registro(idoportunidadAlta.ToString(), "1", "Alta de Oportunidad");
            }

            //Ejemplo: comun.basae.AgregarOportunidad(items, IdUsuario, IdConfiguracion, Empresa);
            return Json(idoportunidadAlta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregaryContinuar(string Nombre, string Importe, string Cierre, string Asignado, string Probabilidad, string Empresa,
        string Contacto, string IdUsuario, string IdClasificacion, string IdSubClasificacion)
        {
            Oportunidades items = new Oportunidades();
            items.Nombre = Nombre;
            items.Importe = decimal.Parse(Importe);
            if (Cierre != "")
                items.Cierre = DateTime.Parse(Cierre);
            items.Asignado = int.Parse(Asignado);
            items.Etapa = 1;
            items.Probabilidad = int.Parse(Probabilidad);
            items.IdUsuario = int.Parse(IdUsuario);
            items.IdClasificacion = int.Parse(IdClasificacion);
            items.IdSubClasificacion = int.Parse(IdSubClasificacion);

            int idoportunidadAlta = n.oportunidades.Agregar_Registro(items);

            n.oecu.Agregar_Registro(idoportunidadAlta.ToString(), Empresa, Contacto, IdUsuario);
            return Json(idoportunidadAlta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarContactosAsignadosAOportunidad(string IdConfiguracion, string IdOportunidad)
        {
            var datos = n.contactos.Seleccionar_ContactosAsignadosAOportunidad(IdConfiguracion, IdOportunidad);
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarOportunidadContactos(string IdOportunidad, string IdContacto, string IdTipoPersona, string IdConfiguracion, string IdEmpresa)
        {
            var obtenido = 0;
            if (n.oportunidades.Agregar_OportunidadContactos(IdOportunidad, IdContacto, IdTipoPersona) > 0)
            {
                //var datos = cr.SeleccionarContactosAsignadosAOportunidad(IdConfiguracion, IdOportunidad);
                obtenido = 1;
                Session["IdEmpresa"] = IdEmpresa;
            }
            return Json(obtenido, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerArchivos(string id)
        {
            return Json(n.archivos.Seleccionar_PorIdOportunidad(id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Guarda una nueva oportunidad
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="Importe"></param>
        /// <param name="Cierre"></param>
        /// <param name="Asignado"></param>
        /// <param name="Probabilidad"></param>
        /// <param name="Empresa"></param>
        /// <param name="Contacto"></param>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Agregar1(string Nombre, string Importe, string Caracteristicas, string Cierre, string Asignado, string Probabilidad, string Empresa, 
        string Contacto, string IdUsuario, string IdClasificacion, string IdSubClasificacion)
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];

            Oportunidades items = new Oportunidades();
            items.Nombre = Nombre;
            if (Importe.Contains("$"))
            {
                Importe = Importe.Replace("$", "");
            }
            if (Importe.Contains(","))
            {
                Importe = Importe.Replace(",", "");
            }
            items.Importe = decimal.Parse(Importe);
            items.Caracteristicas = Caracteristicas;
            if (Cierre != "")
                items.Cierre = DateTime.Parse(Cierre);
            items.Asignado = int.Parse(Asignado);
            items.Etapa = 1;
            items.Probabilidad = int.Parse(Probabilidad);
            items.IdUsuario = int.Parse(IdUsuario);
            items.IdClasificacion = int.Parse(IdClasificacion);
            items.IdSubClasificacion = int.Parse(IdSubClasificacion);

            //ViewBag.Empresas = er.Seleccionar(usr.Configuracion.Id.ToString(), usr.Roles.Id.ToString(), usr.Usuarios.Id.ToString()).ToList();
            ViewBag.Usuarios = n.usuarios.Seleccionar_Registros().Where(us => us.Roles.Id == 4);

            if (n.oecu.Agregar_Registro(n.oportunidades.Agregar_Registro(items).ToString(), Empresa, Contacto, IdUsuario))
                ViewData["Mensaje"] = "Agregado";
            else
                ViewData["Mensaje"] = "Falla";
            return View("Index");
        }

        /// <summary>
        /// Cambia el detalle de una oportunidad
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="Importe"></param>
        /// <param name="Cierre"></param>
        /// <param name="Asignado"></param>
        /// <param name="Probabilidad"></param>
        /// <param name="Etapa"></param>
        /// <param name="Avance"></param>
        /// <param name="Notas"></param>
        /// <param name="IdOportunidad"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Modificar1(string Nombre, string Importe, string Caracteristicas, string Cierre, string Asignado, string Probabilidad, string Etapa, string Avance, string Notas,
        string Clasificacion, string SubClasificacion, string IdOportunidad)
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];

            Oportunidades items = new Oportunidades();
            items.Nombre                = Nombre;
            items.Caracteristicas       = Caracteristicas;
            items.Importe               = decimal.Parse(Importe);
            if (Importe.Contains("$"))
            {
                Importe = Importe.Replace("$", "");
            }
            if (Importe.Contains(","))
            {
                Importe = Importe.Replace(",", "");
            }
            items.Cierre                = DateTime.Parse(Cierre);
            items.Asignado              = int.Parse(Asignado);
            items.Probabilidad          = int.Parse(Probabilidad);
            items.Etapa                 = int.Parse(Etapa);
            items.Avance                = int.Parse(Avance);
            items.Notas                 = Notas;
            items.IdClasificacion       = int.Parse(Clasificacion);
            items.IdSubClasificacion    = int.Parse(SubClasificacion);
            items.Id                    = int.Parse(IdOportunidad);

            //ViewBag.Empresas = er.Seleccionar(usr.Configuracion.Id.ToString(), usr.Roles.Id.ToString(), usr.Usuarios.Id.ToString()).ToList();
            ViewBag.Usuarios = n.usuarios.Seleccionar_Registros().Where(us => us.Roles.Id == 4);
            ViewBag.Clasificacion = n.clasificacion.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString());

            if (n.oportunidades.Modificar_Registro(items) > 0)
                ViewData["Mensaje"] = "Modificado";
            else
                ViewData["Mensaje"] = "Fallo";
            return View("Index");
        }

        public JsonResult GuardarModificado(string Nombre, string Importe, string Caracteristicas, string Cierre, string Asignado, string Probabilidad, string Etapa, string Avance, string Notas,
        string Clasificacion, string SubClasificacion, string FechaProximoVencimiento, string IdOportunidad)
        {
            Oportunidades items = new Oportunidades();
            items.Nombre = Nombre;
            items.Caracteristicas = Caracteristicas;
            if (Importe.Contains("$"))
            {
                Importe = Importe.Replace("$", "");
            }
            if (Importe.Contains(","))
            {
                Importe = Importe.Replace(",", "");
            }
            items.Importe = decimal.Parse(Importe);
            items.Cierre = DateTime.Parse(Cierre);
            items.Asignado = int.Parse(Asignado);
            items.Probabilidad = int.Parse(Probabilidad);
            items.Etapa = int.Parse(Etapa);
            items.Avance = int.Parse(Avance);
            items.Notas = Notas;
            items.IdClasificacion = int.Parse(Clasificacion);
            items.IdSubClasificacion = int.Parse(SubClasificacion);
            items.FechaProximoVencimiento = string.IsNullOrEmpty(FechaProximoVencimiento) ? DateTime.Parse("1900/01/01") : DateTime.Parse(FechaProximoVencimiento);
            items.Id = int.Parse(IdOportunidad);

            n.oportunidades.Modificar_Registro(items);
            return Json(JsonRequestBehavior.AllowGet);

        }

        public JsonResult Modificar(string Nombre, string Clasificacion, string SubClasificacion, string Caracteristicas, string Importe, string ImporteOtro, string IdTipoMoneda, 
        string Empresa, string Cierre, string Notas, string Contraparte, string Atencion, string Aviso, string IdOportunidad, string UDN, string RepetirProximo, string FechaProximoVencimiento)
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];

            Oportunidades items = new Oportunidades();
            items.Nombre                = Nombre;
            items.IdClasificacion       = Clasificacion == "" ? 0 : int.Parse(Clasificacion);
            items.IdSubClasificacion    = SubClasificacion == "" ? 0 : int.Parse(SubClasificacion);
            items.Caracteristicas       = Caracteristicas;
            if (!string.IsNullOrEmpty(Importe))
            {
                if (Importe.Contains("$"))
                {
                    Importe = Importe.Replace("$", "");
                }
                //if (Importe.Contains(","))
                //{
                //    Importe = Importe.Replace(",", "");
                //}
                items.Importe           = decimal.Parse(Importe);
            }
            else
                items.Importe           = string.IsNullOrEmpty(Importe) ? 0 : decimal.Parse(Importe);
            if (!string.IsNullOrEmpty(ImporteOtro))
            {
                if (ImporteOtro.Contains("$"))
                {
                    ImporteOtro         = ImporteOtro.Replace("$", "");
                }
                //if (Importe.Contains(","))
                //{
                //    Importe = Importe.Replace(",", "");
                //}
                items.ImporteOtro       = decimal.Parse(ImporteOtro);
            }
            else
                items.ImporteOtro       = string.IsNullOrEmpty(ImporteOtro) ? 0 : decimal.Parse(ImporteOtro);
            items.IdTipoMoneda          = string.IsNullOrEmpty(IdTipoMoneda) ? 0 : int.Parse(IdTipoMoneda);
            items.Cierre                = Cierre == "" ? DateTime.Parse("01/01/1900") : DateTime.Parse(Cierre);
            items.PeriodoAtencion       = Atencion == "" || Atencion == null ? 0 : int.Parse(Atencion);
            items.Aviso                 = string.IsNullOrEmpty(Aviso) ? DateTime.Parse("01/01/1900") : DateTime.Parse(Aviso);
            items.Notas                 = Notas;
            items.Contraparte           = Contraparte;
            items.Id                    = int.Parse(IdOportunidad);
            items.UDN                   = string.IsNullOrEmpty(UDN) ? 0 : int.Parse(UDN);
            items.RepetirProximoAño     = string.IsNullOrEmpty(RepetirProximo) || RepetirProximo=="0" ? 4 : int.Parse(RepetirProximo);
            items.FechaProximoVencimiento = string.IsNullOrEmpty(FechaProximoVencimiento) ? DateTime.Parse("01/01/1900") : DateTime.Parse(FechaProximoVencimiento);

            //ViewBag.Empresas            = er.Seleccionar(usr.Configuracion.Id.ToString(), usr.Roles.Id.ToString(), usr.Usuarios.Id.ToString()).ToList();
            ViewBag.Usuarios            = n.usuarios.Seleccionar_Registros().Where(us => us.Roles.Id == 4);
            ViewBag.Clasificacion       = n.clasificacion.Seleccionar_Registros(usuariosrolessesion.Configuracion.Id.ToString());

            int procesado = 0;
            //if (Session["IdConfiguracion"].ToString() == "3")
            //{
            //    if (n.oportunidades.Modificar_Registro(items) > 0)
            //    {
            //        //Agregar la empresa
            //        if (Empresa != "")
            //            n.oecu.Agregar_SoloEmpresa(Empresa, IdOportunidad);
            //        procesado = 1;
            //    }
            //}
            //else if (Session["IdConfiguracion"].ToString() == "2")
            //{
                //var validacion = new ValidacionPropietarioPermiso().OportunidadValidarPropietarioPermiso(IdOportunidad, Session["IdUsuario"].ToString());
                //if (n.usuarios.Seleccionar_CreadorOportunidad(IdOportunidad) == Session["IdUsuario"].ToString() 
                //    || n.compartiroportunidades.Validar_SiUsuarioPuedeModificar(IdOportunidad, Session["IdUsuario"].ToString())
                //    || Session["IdUsuario"].ToString() == "2")
                if (new ValidacionPropietarioPermiso().OportunidadValidarPropietarioPermiso(IdOportunidad, Session["IdUsuario"].ToString()))
                {
                    if (n.oportunidades.Modificar_Registro(items) > 0)
                    {
                        //Agregar la empresa
                        if (Empresa != "")
                            n.oecu.Agregar_SoloEmpresa(Empresa, IdOportunidad);
                        //if (Contacto != "")
                        //    oaecur.AgregarSolocontacto(Contacto, IdOportunidad);
                        procesado = 1;
                    }
                }
            //}
            return Json(procesado,JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarEstado(string Estado, string IdOportunidad)
        {
            int procesado = 0;
            if (n.oportunidades.Modificar_EstadoOportunidad(IdOportunidad, Estado) > 0)
            {
                procesado = 1;
            }
            return Json(procesado, JsonRequestBehavior.AllowGet);
        }

        #region Clasificación y subClasificación **************************************************************************

        //Obtener la clasificacion
        public JsonResult SeleccionarClasificacionPorIdConfiguracion(string id)
        {
            return Json(n.clasificacion.Seleccionar_Registros(id), JsonRequestBehavior.AllowGet);
        }

        // SubClasificacion

        public JsonResult SeleccionarSubclasificacionPorClasificacion(string id)
        {
            List<Subclasificacion> sc = new List<Subclasificacion>();
            if (int.Parse(id) > 0)
                sc = n.subclasificacion.Seleccionar_PorId(id);
            else
                sc = null;
            return Json(sc, JsonRequestBehavior.AllowGet);
        }

        //Clasificacion-Subclasificacion Etiquetas

        public JsonResult SeleccionarClasificacionSubclasificacionEtiquetas(string idClas, string idSubClas)
        {
            return Json(n.classsubclassetiquetas.Seleccionar_PorIdClasificacionIdSubClasificacion(idClas, idSubClas), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Archivos **************************************************************************

        public JsonResult ArchivosSeleccionarPorId(string Id)
        {
            return Json(n.archivos.Seleccionar_PorId(Id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AgregarArchivo(HttpPostedFileBase archivo, string IdOportunidad2)
        {

            if (archivo == null)
            {
                ViewBag.Archivos = n.archivos.Seleccionar_PorIdOportunidad(IdOportunidad2);
                return View();
            }
            if (archivo.ContentLength > 5242880)
            {
                ViewBag.Procesado = "<div class='alert alert-warning' role='alert' style='width:100%'>El archivo excede la carga máxima de 5 mb.</div><br />";
                ViewBag.Archivos = n.archivos.Seleccionar_PorIdOportunidad(IdOportunidad2);
                return View();
            }
            try
            {
                if (archivo.ContentLength > 0)
                {
                    string _NombreArchivo = Path.GetFileName(archivo.FileName);
                    string _ruta = Path.Combine(Server.MapPath("~/Archivos"), _NombreArchivo);
                    archivo.SaveAs(_ruta);
                    Archivos items = new Archivos();
                    items.Nombre = _NombreArchivo;
                    items.IdOportunidad = int.Parse(IdOportunidad2);
                    n.archivos.Agregar_Registro(items);
                }
                else
                {
                    return View();
                }
                ViewBag.Archivos = n.archivos.Seleccionar_PorIdOportunidad(IdOportunidad2);
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AgregarArchivoRes2(HttpPostedFileBase archivo, string IdOportunidad2)
        {

            if (archivo == null)
            {
                ViewBag.Archivos = n.archivos.Seleccionar_PorIdOportunidad(IdOportunidad2);
                return View();
            }
            if (archivo.ContentLength > 5242880)
            {
                ViewBag.Procesado = "<div class='alert alert-warning' role='alert' style='width:100%'>El arhivo excede la carga máxima de 5 mb.</div><br />";
                ViewBag.Archivos = n.archivos.Seleccionar_PorIdOportunidad(IdOportunidad2);
                return View();
            }
            try
            {
                if (archivo.ContentLength > 0)
                {
                    string _NombreArchivo = Path.GetFileName(archivo.FileName);
                    string _ruta = Path.Combine(Server.MapPath("~/Archivos"), _NombreArchivo);
                    archivo.SaveAs(_ruta);
                    Archivos items = new Archivos();
                    items.Nombre = _NombreArchivo;
                    items.IdOportunidad = int.Parse(IdOportunidad2);
                    n.archivos.Agregar_Registro(items);
                }
                else
                {
                    return View("AgregarArchivoResponsable2");
                }
                ViewBag.Archivos = n.archivos.Seleccionar_PorIdOportunidad(IdOportunidad2);
                return View("AgregarArchivoResponsable2");
            }
            catch
            {
                return View("AgregarArchivoResponsable2");
            }
        }

        public JsonResult AgregarNotas(string Notas, string Version, string Id, string idoportunidad)
        {
            n.archivos.Modificar_Registro(Notas, Version, Id);
            return Json(n.archivos.Seleccionar_PorIdOportunidad(idoportunidad), JsonRequestBehavior.AllowGet);
        }

        public JsonResult QuitarArchivo(string idarchivo, string idoportunidad)
        {
            var _NombreArchivo = n.archivos.Seleccionar_PorNombre(idarchivo);
            string _ruta = Path.Combine(Server.MapPath("~/Archivos"), _NombreArchivo);
            if (System.IO.File.Exists(_ruta))
            {
                System.IO.File.Delete(_ruta);
                //Eliminar tambien de la bd
                n.archivos.Eliminar_Registro(idarchivo);                
            }
            var datos = n.archivos.Seleccionar_PorIdOportunidad(idoportunidad);
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        #endregion

        // Archivos Bitácora

        public JsonResult ArchivosBitacoraSeleccionarPorId(string Id)
        {
            return Json(n.archivosbitacora.Seleccionar_PorId(Id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AgregarArchivoResponsable(HttpPostedFileBase file, string IdBitacora)
        {
            if (file == null)
            {
                ViewBag.ArchivosBitacora = n.archivosbitacora.Seleccionar_PorIdBitacora(Session["IdBitacora"].ToString());
                return View();
            }
            if (file.ContentLength > 5242880)
            {
                ViewBag.Procesado = "<div class='alert alert-warning' role='alert' style='width:100%'>El arhivo excede la carga máxima de 5 mb.</div><br />";
                ViewBag.ArchivosBitacora = n.archivosbitacora.Seleccionar_PorIdBitacora(Session["IdBitacora"].ToString());
                return View();
            }
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/ArchivosBitacora"), _FileName);
                    file.SaveAs(_path);
                    ArchivosBitacora items = new ArchivosBitacora();
                    items.Nombre = _FileName;
                    items.IdBitacora = int.Parse(IdBitacora);
                    n.archivosbitacora.Agregar_Registro(items);
                }
                ViewBag.ArchivosBitacora = n.archivosbitacora.Seleccionar_PorIdBitacora(Session["IdBitacora"].ToString());
                return View();
            }
            catch
            {
                return View();
            }
        }

        public JsonResult AgregarNotasArchivosBitacora(string Notas, string Version, string Id)
        {
            if (n.archivosbitacora.Modificar_Registro(Notas, Version, Id) > 0)
                return Json(1, JsonRequestBehavior.AllowGet);
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }

        #region Bitacora **************************************************************************

        [HttpPost]
        public ActionResult AgregarBitacora(string Empresa, string Responsable, string IdOportunidad, string Contactos, string Clasificacion1, string Clasificacion2)
        {
            Bitacora items = new Bitacora();
            items.IdResponsable = int.Parse(Contactos); //Contacto
            items.IdOportunidad = int.Parse(IdOportunidad);
            items.IdClasificacionResponsable = int.Parse(Clasificacion1);
            items.IdSubclasificacionRespponsable = int.Parse(Clasificacion2);
            int obtenido = n.bitacora.Agregar_Registro(items);

            //UsuariosEmpresasOportunidades items2 = new UsuariosEmpresasOportunidades(); //TODO QUITAR REFERENCIA A ESTA CLASE
            //items2.IdUsuario = int.Parse(Responsable);
            //items2.IdEmpresa = int.Parse(Empresa);
            //items2.Idoportunidad = int.Parse(IdOportunidad);
            //items2.IdBitacora = obtenido;
            //ueor.Agregar(items2);

            //OportunidadesEmpresasContactosUsuarios items3 = new OportunidadesEmpresasContactosUsuarios(); //SE QUEDA
            //items3.IdUsuario     = int.Parse(Responsable);
            //items3.IdEmpresa     = int.Parse(Empresa);
            //items3.IdOportunidad = int.Parse(IdOportunidad);
            //items3.IdBitacora    = obtenido;
            //oaecur.AgregarBitacora(IdOportunidad, Empresa, Responsable, obtenido.ToString());

            ViewBag.Asignar = n.ueo.Seleccionar_ResponsablesPorEmpresa(Session["IdEmpresa"].ToString());
            ViewBag.Responsables = n.ueo.Seleccionar_ResponsablesPorOportunidad(IdOportunidad);

            return View("Responsables");

        }

        public JsonResult ActualizarBitacora(string IdBitacora, string Estado, string Notas)
        {
            Bitacora items = new Bitacora();
            items.Id = int.Parse(IdBitacora);
            items.Estado = int.Parse(Estado);
            items.Notas = Notas;
            if (n.bitacora.Modificar_Registro(items) >= 1)
                return Json(1,JsonRequestBehavior.AllowGet);
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Para que el propietario del asunto dé seguimiento al responsable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult BitacoraSeleccionarPorId(string idContacto, string idBitacora)
        {
            return Json(n.bitacora.Seleccionar_PorId(idContacto, idBitacora), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Para ver el detalle por el responsable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult BitacoraSeleccionarPorIdBitacora(string id)
        {
            Bitacora bi = new Bitacora();
            bi = n.bitacora.Seleccionar_PorIdBitacora(id);
            return Json(bi, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ArchivosDeBitacora(string id)
        {
            return Json(n.bitacora.Seleccionar_Archivos(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RevisoAsunto(string id)
        {
            return Json(n.bitacora.Reviso_Asunto(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CrearNuevaBitacora(string IdContacto, string IdOportunidad)
        {
            Bitacora items = new Bitacora();
            items.IdResponsable = int.Parse(IdContacto); //Usuario
            items.IdOportunidad = int.Parse(IdOportunidad);
            return Json(n.bitacora.Agregar_Registro(items), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarBitacoraUsuarioPorId(string id)
        {
            //return Json(bur.SeleccionarPorId(id), JsonRequestBehavior.AllowGet);
            return Json(n.bitacorausuariosdetalle.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarModificadoBitacoraUsuario(string Id, string TipoActividad, string Fecha, string Notas)
        {
            //return Json(bur.Modificar(Id, TipoActividad, Fecha, Notas), JsonRequestBehavior.AllowGet);
            return Json(n.bitacorausuariosdetalle.Modificar_Registro(Id, TipoActividad, Notas), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarResponsablesAsignados(string idoportunidad)
        {
            return Json(n.bitacora.Seleccionar_ResponsablesPorOportunidad(idoportunidad), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region OportunidadesActividades **************************************************************************

        public ActionResult ActividadesAgregar()
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            OportunidadesActividades oa = new OportunidadesActividades();
            oa.TipoActividad    = string.IsNullOrEmpty(Request.Form["TipoActividad"]) ? 0 : int.Parse(Request.Form["TipoActividad"]);
            oa.Fecha            = string.IsNullOrEmpty(Request.Form["Vencimiento"]) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Request.Form["Vencimiento"]);
            oa.Notas            = Request.Form["Descripcion"];
            oa.IdOportunidad    = int.Parse(Session["IdOportunidad"].ToString());
            oa.IdUsuario        = usuariosrolessesion.Usuarios.Id;
            if (oa.TipoActividad == 9)
                oa.IdUsuarioAsignado = Request.Form["Responsables"].ToString() == "" ? 0 : int.Parse(Request.Form["Responsables"].ToString());
            n.oportunidadesactividades.Agregar_Registro(oa);
            if (oa.TipoActividad == 9 && oa.Fecha.ToShortDateString() == DateTime.Now.ToShortDateString() && oa.IdUsuarioAsignado > 0)
            {
                //Enviar correo al responsable
                var correousuario = n.usuarios.Seleccionar_CorreoReponsable(oa.IdUsuarioAsignado.ToString());
                var titulo = "Aviso-Recordatorio";
                var asunto = "Atención";
                correo.EnviarCorreo(correousuario, titulo, asunto, oa.Notas);
            }
            else
            { 
                //guardar y enviarlo despues
            }
            ViewBag.Actividades = n.oportunidadesactividades.Seleccionar_PorIdOportunidad(Session["IdOportunidad"].ToString());
            return View("Actividades");
        }

        public JsonResult SeleccionarActividadPorId(string id)
        {
            return Json(n.oportunidadesactividades.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult seleccionarbitacoraporid(string id)
        //{
        //    oportunidadesactividades oa = new oportunidadesactividades();
        //    oa = oar.seleccionarporid(id);
        //    return json(oa, jsonrequestbehavior.allowget);
        //}

        public JsonResult SeleccionarActividadesPorIdOportunidad(string id)
        {
            return Json(n.oportunidadesactividades.Seleccionar_PorIdOportunidad(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarActividadesPorIdUsuario(string idusuario, string idoportunidad)
        {
            return Json(n.oportunidadesactividades.Seleccionar_PorIdUsuario(idusuario, idoportunidad), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarActividadModificada(string TipoActividad, string Fecha, string Notas, string Id, string IdUsuario)
        {
            //var x = new ValidacionPropietarioPermiso().DocumentoValidarPropiedad(null, null);
            if (new ValidacionPropietarioPermiso().ActividadValidarPropiedad(Id, IdUsuario))
            {
                OportunidadesActividades items = new OportunidadesActividades();
                items.TipoActividad = string.IsNullOrEmpty(TipoActividad) ? 0 : int.Parse(TipoActividad);
                items.Fecha = string.IsNullOrEmpty(Fecha) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha);
                items.Notas = Notas;
                items.Id = int.Parse(Id);
                if (n.oportunidadesactividades.Modificar_Registro(items) >= 1)
                    return Json(1, JsonRequestBehavior.AllowGet);
                else
                    return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarActividad(string IdActividad, string idusuario)
        {
            if (new ValidacionPropietarioPermiso().ActividadValidarPropiedad(IdActividad, idusuario))
            {
                if (n.oportunidadesactividades.Eliminar_Registro(IdActividad) >= 1)
                    return Json(1, JsonRequestBehavior.AllowGet);
                else
                    return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Responsables **************************************************************************

        [HttpPost]
        public ActionResult AgregarResponsable(string IdOportunidad, string Responsables, string Clasificacion1, string Clasificacion2, string optionsRadios)
        {
            if (string.IsNullOrEmpty(IdOportunidad))
                IdOportunidad = Session["IdOportunidad"].ToString();

            //Obtener el rol del usuario
            var idrolusuario    = n.usuariosroles.Seleccionar_RolUsuario(Responsables);
            var tituloasunto    = n.oportunidades.Seleccionar_NombreTemaPorIdoportunidad(IdOportunidad);
            var empresaasignada = n.oportunidades.Seleccionar_NombreEmpresaPorIdOportunidad(IdOportunidad);
            var vencimiento     = n.oportunidades.Seleccionar_VencimientoTemaPorIdoportunidad(IdOportunidad);

            if (n.oecu.Seleccionar_IdResponsableCreadorOportunidad(IdOportunidad) != Responsables)
            {

                if (idrolusuario == "5") //Usuarios responsables
                {
                    if (n.oportunidadesresponsables.Seleccionar_SiResponsableYaEstaAsignadoAOportunidad(IdOportunidad, Responsables) == 0)
                    {
                        //Verificar que el contacto esté asignado a una empresa
                        //if (cr.SeleccionarValidarSiContactoPerteneceAEmpresa(Responsables) != "0") //Antes
                        if (n.empresasusuarios.Seleccionar_ValidarSiUsuarioPerteneceAEmpresa(Responsables) != "0")
                        {
                            // Asigna un responsable a la oportunidad
                            n.oportunidadesresponsables.Agregar_Registro(IdOportunidad, Responsables, Clasificacion1, Clasificacion2);

                            // Asignarlo a bitacora
                            Bitacora items1 = new Bitacora();
                            items1.IdResponsable = int.Parse(Responsables);
                            items1.IdOportunidad = int.Parse(IdOportunidad);
                            n.bitacora.Agregar_Registro(items1);

                            //Enviarle un correo de aviso para que se prepare.
                            //obtener el correo del responsable
                            var titulocorreo = Titulos.TDashboard(Session["IdConfiguracion"].ToString());
                            var correoresponsable = n.usuarios.Seleccionar_CorreoReponsable(Responsables);
                            var nombre = n.usuarios.Seleccionar_Nombre(Responsables);
                            if (correoresponsable.Contains("@") || correoresponsable.Contains("."))
                            {
                                if (correo.EnviarCorreoAsignadoAAsunto(nombre, tituloasunto, empresaasignada, vencimiento, correoresponsable, titulocorreo, Session["IdConfiguracion"].ToString()))
                                    ViewBag.CorreoEnviado = "Envio exitoso";
                                else
                                    ViewBag.CorreoEnviado = "Envío Fallido";
                            }
                        }
                        else
                        {
                            ViewBag.Advertencia = "<div class='alert alert-warning' role='alert'>El Responsable NO está asignado a una empresa y no puede asignarse como responsable.</div>";
                        }
                    }
                }
                else //Roles superiores
                {
                    if (n.oportunidadesusuarios.Seleccionar_SiUsuarioYaEstaAsignadoAOportunidad(IdOportunidad, Responsables) == 0)
                    {
                        n.oportunidadesusuarios.Agregar_Usuario(IdOportunidad, Responsables, Clasificacion1, Clasificacion2);
                        //Agregar la bitácora de seguimiento para el usuario asignado
                        //BitacoraUsuarios items = new BitacoraUsuarios();
                        //items.IdResponsable = int.Parse(Responsables);
                        //items.IdOportunidad = int.Parse(IdOportunidad);
                        //bur.Agregar(items);

                        Bitacora items1 = new Bitacora();
                        items1.IdResponsable = int.Parse(Responsables);
                        items1.IdOportunidad = int.Parse(IdOportunidad);
                        n.bitacora.Agregar_Registro(items1);

                        //Enviarle un correo de aviso para que se prepare.
                        //obtener el correo del responsable
                        var titulocorreo = Titulos.TDashboard(Session["IdConfiguracion"].ToString());
                        var correoresponsable = n.usuarios.Seleccionar_CorreoReponsable(Responsables);
                        var nombre = n.usuarios.Seleccionar_Nombre(Responsables);
                        if (correoresponsable.Contains("@") || correoresponsable.Contains("."))
                        {
                            if (correo.EnviarCorreoAsignadoAAsunto(nombre, tituloasunto, empresaasignada, vencimiento, correoresponsable, titulocorreo, Session["IdConfiguracion"].ToString()))
                                ViewBag.CorreoEnviado = "Envio exitoso";
                            else
                                ViewBag.CorreoEnviado = "Envío Fallido";
                        }
                    }
                }
            }
            else
            {
                ViewBag.Advertencia = "<div class='alert alert-warning' role='alert'>No se puede asignar como responsable al creador de la oportunidad.</div>";
            }
            UsuariosRoles usr = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Contactos = n.contactos.Seleccionar_ContactosPorConfiguracion(usr.Configuracion.Id.ToString(), Session["IdRol"].ToString(), Session["IdUsuario"].ToString());  //cr.SeleccionarTodos(usr.Configuracion.Id.ToString());
            ViewBag.Responsables = n.oportunidadesresponsables.Seleccionar_ResponsablesPorOportunidad(IdOportunidad, Session["IdConfiguracion"].ToString());
            ViewBag.ClasificacionRolesContactos = n.clasificacionrolescontactos.Seleccionar_Registros();

            //Obtener los usuarios asignados configuración 3
            //if (Session["IdConfiguracion"].ToString() == "3")
            //{
            //    ViewBag.UsuariosAsignados = n.usuarios.Seleccionar_PorIdOportunidad(IdOportunidad);
            //}
            //else if (Session["IdConfiguracion"].ToString() == "2")
            //{
                ViewBag.UsuariosAsignados = n.oportunidadesusuarios.Seleccionar_PorIdOportunidad(IdOportunidad);
            //}

            ViewBag.Responsables = n.oportunidadesresponsables.Seleccionar_ResponsablesPorOportunidad(IdOportunidad, Session["IdConfiguracion"].ToString());

            return View("Responsables");
        }

        public JsonResult AgregarResponsables(string IdOportunidad, string Responsables, string Clasificacion1, string Clasificacion2)
        {
            //En preparación
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarContactoRol(string id)
        {
            return Json(n.contactorol.Seleccionar_PorClasificacionRolesContacto(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarResponsablesActividades(string idoportunidad)
        {
            return Json(n.contactosactividades.Seleccionar_ContactosConActividades(idoportunidad), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResponsableGuardarActividades()
        {
            //Guardar las actividades del responsable dentro de las actividades de la responsabilidad
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            //OportunidadesActividades oa = new OportunidadesActividades();
            //oa.TipoActividad = string.IsNullOrEmpty(Request.Form["TipoActividad"]) ? 0 : int.Parse(Request.Form["TipoActividad"]);
            //oa.Fecha = string.IsNullOrEmpty(Request.Form["Vencimiento"]) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Request.Form["Vencimiento"]);
            //oa.Notas = Request.Form["Descripcion"];
            //oa.IdOportunidad = int.Parse(Request.Form["IdOportunidad"]);
            //oa.IdUsuario = usr.Usuarios.Id;
            //oar.Agregar(oa);
            //ViewBag.Actividades = oar.SeleccionarPorIdOportunidad(Session["IdOportunidad"].ToString());
            //BitacoraUsuarios buitems = new BitacoraUsuarios();
            //buitems.IdTipoActividad = string.IsNullOrEmpty(Request.Form["TipoActividad"]) ? 0 : int.Parse(Request.Form["TipoActividad"]);
            //buitems.Fecha = DateTime.Now; //string.IsNullOrEmpty(Request.Form["Vencimiento"]) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Request.Form["Vencimiento"]);
            //buitems.Notas = Request.Form["Descripcion"];
            //buitems.IdResponsable = usr.Usuarios.Id;
            //buitems.IdOportunidad = int.Parse(Request.Form["IdOportunidad"]);
            //bur.Agregar(buitems);


            Bitacora items1 = new Bitacora();
            items1.IdResponsable = int.Parse(Request.Form["IdUsuario"]);
            items1.IdOportunidad = int.Parse(Request.Form["IdOportunidad"]);
            var obtenido = n.bitacora.Agregar_Registro(items1);

            BitacoraUsuariosDetalle items = new BitacoraUsuariosDetalle();
            items.IdBitacora = obtenido;
            //items.IdTipoActividad = int.Parse(Request.Form["TipoActividad"]);
            //items.Fecha = DateTime.Now; //DateTime.Parse(Request.Form["Vencimiento"]);
            items.Notas = Request.Form["Descripcion"];
            items.IdUsuario = int.Parse(Request.Form["IdUsuario"]);
            n.bitacorausuariosdetalle.Agregar_Registro(items);

            var usuarioaltaenbitacora = n.usuarios.Seleccionar_Nombre(Request.Form["IdUsuario"].ToString());

            bool salida;
            //Enviar un correo al creador de la oportunidad o al asignado como responsable que un responsable ha enviado un correo
            foreach (var itms in n.bitacorausuarios.Seleccionar_BitacoraResponsablesPorIdOportunidad(Request.Form["IdOportunidad"].ToString()))
            {
                
                var correoresponsable = n.usuarios.Seleccionar_CorreoReponsable(n.usuarios.Seleccionar_CreadorOportunidad(Request.Form["IdOportunidad"].ToString()));
                var nombre = n.usuarios.Seleccionar_Nombre(itms.IdResponsable.ToString());
                var nombreasunto = n.oportunidades.Seleccionar_NombreTemaPorIdoportunidad(Request.Form["IdOportunidad"].ToString());
                if (correo.EnviarCorreoAvisoAltaBitacoraAResponsable(correoresponsable, usuarioaltaenbitacora, nombreasunto))
                    salida = true;
                else
                    salida = false;
            }


            ViewBag.Actividades = n.bitacorausuariosdetalle.Seleccionar_PorIdOportunidad(Session["IdOportunidad"].ToString()); //bur.SeleccionarIdOportunidad(Session["IdOportunidad"].ToString());
            
            return View("ResponsableActividades");
        }

        #endregion

        #region Etapas Bitacora **************************************************************************

        public JsonResult SeleccionarEtapasOportunidad(string id)
        {
            return Json(n.etapasoportunidad.Seleccionar_PorIdOportunidad(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarEtapaEstadoPorOportunidad(string id)
        {
            return Json(n.etapasoportunidad.Seleccionar_EtapasEstadoPorIdOportunidad(id), JsonRequestBehavior.AllowGet);
        }

        //Etapa 1
        public JsonResult CerrarEtapasOportunidad1(string IdOportunidad, string Etapa)
        {
            int salida = 0;
            if (n.etapasoportunidad.Modificar_Registro(IdOportunidad, Etapa) > 0)
            {
                n.etapasoportunidad.Agregar_Registro(IdOportunidad, "2"); //Agrega la etapa 2
                salida = 1;
            }
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerificarEstadoEtapa(string IdOportunidad, string Etapa)
        {
            int salida = 0;
            if (n.etapasoportunidad.Selecccionar_SiEtapaCompletada(IdOportunidad, Etapa) == "1")
            {
                salida = 1;
            }
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        //Etapa 2

        public JsonResult SeleccionarReponsablesHanLeido(string id)
        {
            return Json(n.etapasbitacora.Seleccionar_ResponsablesHanLeido(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CerrarEtapasOportunidad2(string IdOportunidad, string Etapa)
        {
            int salida = 0;
            if (n.etapasoportunidad.Modificar_Registro(IdOportunidad, Etapa) > 0) //Cerrar la etapa 2
            {
                n.etapasoportunidad.Agregar_Registro(IdOportunidad, "3"); //Agrega la etapa 3
                salida = 1;
            }
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        //Etapa 3

        public JsonResult SeleccionarReponsablesEnProceso(string id)
        {
            return Json(n.etapasbitacora.Seleccionar_ResponsablesEnProceso(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ResponsablesEnProcesoContados()
        {
            return Json(n.etapasbitacora.Seleccionar_ResponsablesPorOportunidad(Session["IdOportunidad"].ToString()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CerrarEtapasOportunidad3(string IdOportunidad, string Etapa)
        {
            int salida = 0;
            if (n.etapasoportunidad.Modificar_Registro(IdOportunidad, Etapa) > 0)
            {
                n.etapasoportunidad.Agregar_Registro(IdOportunidad, "4"); //Agrega la etapa 4
                salida = 1;
            }
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        //Etapa 4

        public JsonResult SeleccionarReponsablesTerminado(string id)
        {
            return Json(n.etapasbitacora.Seleccionar_ResponsablesTerminado(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarFechas(string id)
        {
            return Json(n.oportunidades.Seleccionar_FechasOportunidad(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CerrarEtapasOportunidad4(string IdOportunidad, string Etapa)
        {
            int salida = 0;
            if (n.etapasoportunidad.Modificar_Registro(IdOportunidad, Etapa) > 0)
            {
                n.etapasoportunidad.Agregar_Registro(IdOportunidad, "5"); //Agrega la etapa 5
                salida = 1;
            }
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        //Etapa 5
        public JsonResult CerrarEtapasOportunidad5(string IdOportunidad, string Etapa, string ComentariosFinales)
        {
            int salida = 0;
            if (n.etapasoportunidad.Modificar_Registro(IdOportunidad, Etapa) > 0)
            {
                n.oportunidades.Modificar_EstadoOportunidadMasComentarios(IdOportunidad, "5", ComentariosFinales);
                salida = 1;
            }
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        #endregion **************************************************************************

        //Cambiar el estado a terminado, suspendido,etc.
        public JsonResult CambiarEstadoOportunidad(string IdOportunidad, string Estado, string ComentariosFinales)
        {
            int salida = 0;
            //if (or.ModificarEstadoOportunidadMasComentarios(IdOportunidad, Estado, ComentariosFinales) > 0)
            //{
            //    salida = 1;
            //}
            if (n.estadooportunidad.Agregar_Registro(IdOportunidad, Estado, ComentariosFinales) > 0)
            {
                n.oportunidades.Modificar_EstadoOportunidadMasComentarios(IdOportunidad, Estado, "");
                salida = 1;
            }

            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarEstadosOportunidad(string idoportunidad)
        {
            return Json(n.estadooportunidad.Seleccionar_PorIdOportunidad(idoportunidad), JsonRequestBehavior.AllowGet);
        }

        //Modificar el estado de la oportunidad

        public JsonResult SeleccionarEstadoPorId(string id)
        {
            return Json(n.estadooportunidad.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarEstadoOportunidad(string idestado, string comentarios, string fecha, string id, string idoportunidad)
        {
            var validacion = new ValidacionPropietarioPermiso().OportunidadValidarPropietarioPermiso(idoportunidad, Session["IdUsuario"].ToString());
            if (validacion)
            {
                return Json(n.estadooportunidad.Modificar_Registro(idestado, comentarios, fecha, id), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult ReasignacionOportunidad(string responsableanterior, string idnuevoresponsable, string idoportunidad)
        {
            return Json(n.oportunidades.Modificar_ReasignacionOportunidad(responsableanterior, idoportunidad, idnuevoresponsable), JsonRequestBehavior.AllowGet);
        }

        //Clasificacion/subclasificacion (Procesos dinamicos)

        public JsonResult AgregarClassSubClass(string IdOportunidad, string Campo1, string Campo2, string Campo3, string Campo4, string IdClasificacion, string IdSubClasificacion)
        {
            ClassSubClass items = new ClassSubClass();
            items.IdOportunidad = int.Parse(IdOportunidad);
            items.Campo1 = Campo1;
            items.Campo2 = Campo2;
            items.Campo3 = Campo3;
            items.Campo4 = Campo4;
            items.IdClasificacion = IdClasificacion == null || IdClasificacion == "" ? 0 : int.Parse(IdClasificacion);
            items.IdSubClasificacion = IdSubClasificacion == null || IdSubClasificacion == "" ? 0 : int.Parse(IdSubClasificacion); 
            int salida = 0;
            if (n.classsubclass.Agregar_Modificar(items) > 0)
            {
                salida = 1;
            }
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        #region Aviso **************************************************************************

        public JsonResult SeleccionarAviso(string id)
        {
            return Json(n.aviso.Seleccionar_ContactoPorIdOportunidad(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarAviso(string IdOportunidad, string FechaAviso, string IdUsuario)
        {
            List<Modelos> lista = new List<Modelos>();
            
            if (n.aviso.Agregar_Registro(IdOportunidad, FechaAviso, IdUsuario) > 0)
            {
                //Enviar un mensaje de correo al responsable 
                //var tituloasunto = or.SeleccionarNombreTemaPorIdoportunidad(IdOportunidad);
                //var empresaasignada = or.SeleccionarNombreEmpresaPorIdOportunidad(IdOportunidad);
                //var vencimiento = or.SeleccionarVencimientoTemaPorIdoportunidad(IdOportunidad);                
                //var titulocorreo = Titulos.TDashboard(Session["IdConfiguracion"].ToString());
                //var correoresponsable = ur.SeleccionarCorreoReponsable(IdUsuario);
                //var nombre = ur.SeleccionarNombre(IdUsuario);
                //if (correo.EnviarCorreoAviso(nombre, tituloasunto, empresaasignada, vencimiento, correoresponsable, titulocorreo, Session["IdConfiguracion"].ToString()))
                //    ViewData["Mensaje"] = "Asignado";
                //else
                //    ViewData["Mensaje"] = "Fallo";
                lista = n.aviso.Seleccionar_ContactoPorIdOportunidad(IdOportunidad);
                
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarUsuarioDeAviso(string IdOportunidad, string IdUsuario)
        {
            return Json(n.aviso.Eliminar_Registro(IdOportunidad, IdUsuario), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Escalación **************************************************************************

        public JsonResult SeleccionarEscalacion(string id)
        {
            return Json(n.escalacion.Seleccionar_UsuariosContactosPorIdOportunidad(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarEscalacion(string IdOportunidad, string FechaEscalacion, string IdUsuario)
        {
            List<Modelos> lista = new List<Modelos>();
            if (n.escalacion.Agregar_Registro(IdOportunidad, FechaEscalacion, IdUsuario) > 0)
            {
                //Enviar un mensaje de correo al responsable  //TODO Se quita, solo se guardará y no se enviará nada
                //var tituloasunto = or.SeleccionarNombreTemaPorIdoportunidad(IdOportunidad);
                //var empresaasignada = or.SeleccionarNombreEmpresaPorIdOportunidad(IdOportunidad);
                //var vencimiento = or.SeleccionarVencimientoTemaPorIdoportunidad(IdOportunidad);                
                //var titulocorreo = Titulos.TDashboard(Session["IdConfiguracion"].ToString());
                //var correoresponsable = ur.SeleccionarCorreoReponsable(IdUsuario);
                //if (correo.EnviarCorreoEscalacion(tituloasunto, empresaasignada, vencimiento, correoresponsable, titulocorreo, Session["IdConfiguracion"].ToString()))
                //    ViewBag.CorreoEnviado = "Asignado Correctamente";
                //else
                //    ViewBag.CorreoEnviado = "Fallo en el correo";
                lista = n.escalacion.Seleccionar_UsuariosContactosPorIdOportunidad(IdOportunidad);
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarUsuarioDeEscalacion(string IdOportunidad, string IdUsuario)
        {
            return Json(n.escalacion.Eliminar_Registro(IdOportunidad, IdUsuario), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Contacto Actividades **************************************************************************

        public ActionResult ContactoActividades()
        {
            Session["IdBitacora"] = Request["IdB"];
            Session["IdOportunidad"] = Request["IdO"];
            if (Session["IdBitacora"] == null) 
                Session["IdBitacora"] = Session["IdBitacora2"];
            if (Session["IdOportunidad"] == null) 
                Session["IdOportunidad"] = Session["IdOportunidad2"];
            Session["NombreTema"] = n.oportunidades.Seleccionar_NombreTemaPorIdoportunidad(Session["IdOportunidad"].ToString());
            ViewBag.Actividades = n.contactosactividades.Seleccionar_PorIdBitacoraIdContacto(Session["IdBitacora"].ToString(), Session["IdUsuario"].ToString());
            //Actualizar bitacora a estado 2, en proceso
            n.bitacora.Modificar_BitacoraEstado2(Session["IdBitacora"].ToString());
            return View();
        }

        public JsonResult ContactoActividadSeleccionarDetalle(string id)
        {
            return Json(n.contactosactividades.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ContactoActividadAgregarModificar(string IdBitacora, string IdUsuario, string TipoActividad, string Vencimiento, string Descripcion, string IdActividad)
        {
            ContactosActividades items = new ContactosActividades();
            items.IdBitacora            = int.Parse(IdBitacora);
            items.IdContacto            = int.Parse(IdUsuario);
            items.TipoActividad         = string.IsNullOrEmpty(TipoActividad) ? 0 : int.Parse(TipoActividad);
            items.Fecha                 = string.IsNullOrEmpty(Vencimiento) ? DateTime.Parse("1900/01/01") :  DateTime.Parse(Vencimiento);
            items.Notas                 = Descripcion;
            items.Id                    = IdActividad == "" ? 0 : int.Parse(IdActividad);
            Session["IdBitacora2"]      = items.IdBitacora;
            Session["IdOportunidad2"]   = Session["IdOportunidad"];
            n.contactosactividades.Agregar_Modificar(items);
            return Redirect("ContactoActividades");
        }

        #endregion

        #region Usuarios del sistema, bitacora y su manejo **************************************************************************

        public JsonResult SeleccionarUsuariosPorRol(string idconfiguracion, string idrol, string idusuario)
        {
            return Json(n.usuarios.Seleccionar_PorRol(idconfiguracion, idrol, idusuario), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActividadesUsuariosAgregar()
        {
            Bitacora items1 = new Bitacora();
            items1.IdResponsable = int.Parse(Request.Form["IdUsuario"]);
            items1.IdOportunidad = int.Parse(Request.Form["IdOportunidad"]);
            var obtenido = n.bitacora.Agregar_Registro(items1);
            
            BitacoraUsuariosDetalle items = new BitacoraUsuariosDetalle();
            items.IdBitacora = obtenido;
            //items.IdTipoActividad = int.Parse(Request.Form["TipoActividad"]);
            //items.Fecha = DateTime.Now; //DateTime.Parse(Request.Form["Vencimiento"]);
            items.Notas = Request.Form["Descripcion"];
            items.IdUsuario = int.Parse(Request.Form["IdUsuario"]);
            n.bitacorausuariosdetalle.Agregar_Registro(items);

            //ViewBag.ActividadesUsuarios = budr.SeleccionarPorIdUsuarioIdOportunidad(Session["IdUsuario"].ToString(), Session["IdOportunidad"].ToString());
            ViewBag.Bitacora = n.bitacorausuariosdetalle.Seleccionar_PorIdOportunidad(Session["IdOportunidad"].ToString()); //bur.SeleccionarIdOportunidad(Session["IdOportunidad"].ToString()); //Si es por usuario, solo agregarlo            
            string correoresponsable = n.usuarios.Seleccionar_CorreoReponsable(n.usuarios.Seleccionar_CreadorOportunidad(Session["IdOportunidad"].ToString()));
            string usuario = n.usuarios.Seleccionar_Nombre(Request.Form["IdUsuario"]);
            string nombreasunto = n.oportunidades.Seleccionar_NombreTemaPorIdoportunidad(Session["IdOportunidad"].ToString());
            if (correo.EnviarCorreoAvisoAltaBitacoraAResponsable(correoresponsable, usuario, nombreasunto))
            { 
                //Ok
            }
            else
            { 
                //No Ok
            }
            return View("BitacoraUsuario");
        }

        public JsonResult BitacoraUsuarioSeleccionarPorOportunidad(string idoportunidad)
        {
            return Json(n.bitacora.Seleccionar_BitacoraPorOportunidad(idoportunidad, Session["IdConfiguracion"].ToString()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BitacoraUsuarioSeleccionar(string idusuario, string idoportunidad)
        {
            return Json(n.bitacora.Seleccionar_BitacoraPorOportunidadPorUsuario(idusuario, idoportunidad), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Tipo Persona **************************************************************************

        public JsonResult AgregarTipoPersona(string Nombre)
        {
            List<TipoPersona> lista = new List<TipoPersona>();
            if (n.tipopersona.Agregar_Registro(Nombre) > 0)
            {
                lista = n.tipopersona.Seleccionar_Registros();
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public JsonResult CodigoPostal(string numero)
        {
            return Json(n.codigopostal.Seleccionar_CodigoPostal(numero), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EnviarCorreoCambios(string idoportunidad, string mensaje)
        {
            var creadoroportunidad = n.usuarios.Seleccionar_CreadorOportunidad(idoportunidad);
            var correocreador = n.usuarios.Seleccionar_CorreoReponsable(creadoroportunidad);
            var nombreoportunidad = n.oportunidades.Seleccionar_NombreTemaPorIdoportunidad(idoportunidad);
            //Correo correo = new Correo();
            mensaje += "<p>Para la oportunidad: " + nombreoportunidad;
            mensaje += "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
            "<h4><a href='http://200.23.87.202:1053/Home/'>http://200.23.87.202:1053/Home/</a></h4>";
            var regresar = 0;
            if (!correo.EnviarCorreo(correocreador, "Solicitud de Cambios/Agregado en Oportunidades CRM", "Solicitud de Cambios en Oportunidad", mensaje))
            {
                regresar = 1;
            }

            return Json(regresar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompartirRegistro(string idoportunidad, string idusuario)
        {
            int regresar = 0;
            var validacion = new ValidacionPropietarioPermiso().OportunidadValidarPropietarioPermiso(idoportunidad, idusuario);
            if (n.usuarios.Seleccionar_CreadorOportunidad(idoportunidad) == Session["IdUsuario"].ToString() 
                || n.compartiroportunidades.Validar_SiUsuarioPuedeModificar(idoportunidad, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                regresar = n.compartiroportunidades.Agregar_Registro(idoportunidad, idusuario);
            }

            return Json(regresar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OportunidadesUsuariosCompartidos(string idoportunidad)
        {
            return Json(n.compartiroportunidades.Seleccionar_UsuariosCompartidos(idoportunidad), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarUsuarioCompartido(string idoportunidad, string idusuario)
        {
            int regresar = 0;
            var validacion = new ValidacionPropietarioPermiso().OportunidadValidarPropietarioPermiso(idoportunidad, idusuario);
            if (n.usuarios.Seleccionar_CreadorOportunidad(idoportunidad) == Session["IdUsuario"].ToString() 
                || n.compartiroportunidades.Validar_SiUsuarioPuedeModificar(idoportunidad, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                regresar = n.compartiroportunidades.Eliminar_UsuarioCompartido(idoportunidad, idusuario);
            }
            return Json(regresar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarFechasVencimientosAnteriores(string idoportunidad)
        {            
            return Json(n.fechavencimientocambios.Seleccionar_Registros(idoportunidad), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CambiarFechaVencimiento(string fecha, string idoportunidad, string idusuario)
        {
            int cambios = n.fechavencimientocambios.Agregar_Registro(fecha, idoportunidad, idusuario);
            if (cambios > 0)
            {
                //Enviar correo a los involucrados
                var tituloasunto = n.oportunidades.Seleccionar_NombreTemaPorIdoportunidad(idoportunidad);
                var empresaasignada = n.oportunidades.Seleccionar_NombreEmpresaPorIdOportunidad(idoportunidad);
                var vencimiento = n.oportunidades.Seleccionar_VencimientoTemaPorIdoportunidad(idoportunidad);
                var titulocorreo = "Cambio de Fecha de Vencimiento";

                //Obtener los responsables asignados en el asunto

                foreach (var items in n.bitacorausuarios.Seleccionar_BitacoraResponsablesPorIdOportunidad(idoportunidad))
                {                    
                    var correoresponsable = n.usuarios.Seleccionar_CorreoReponsable(items.IdResponsable.ToString());
                    var nombre = n.usuarios.Seleccionar_Nombre(items.IdResponsable.ToString());
                    bool salida;
                    if (correo.EnviarCorreoCambioFechaVencimiento(nombre, tituloasunto, empresaasignada, vencimiento, correoresponsable, titulocorreo))
                        salida = true;
                    else
                        salida = false;
                }                
            }
            return Json(cambios, JsonRequestBehavior.AllowGet);
        }

        //Importes
        public JsonResult SeleccionarOportunidadImportes(string idoportunidad)
        {
            return Json(n.oportunidadesimportes.Seleccionar_ImportesPorIdOportunidad(idoportunidad), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarSumaImportesOportunidad(string idoportunidad)
        {
            float sumanacional = n.oportunidadesimportes.Seleccionar_SumaImportesNacionalPorIdOportunidad(idoportunidad);
            float sumainternacional = n.oportunidadesimportes.Seleccionar_SumaImportesInternacionalPorIdOportunidad(idoportunidad);
            float sumatotal = sumanacional + sumainternacional;
            n.oportunidadesimportes.Modificar_ImporteTotalOportunidad(idoportunidad, sumatotal.ToString());
            return Json(sumatotal, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarOportunidadImporteParaModificarId(string id)
        {
            return Json(n.oportunidadesimportes.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarOportunidadImporte(string idoportunidad, string importe, string tipomoneda, string tipocambio, string rubro, string observaciones)
        {
            //agregar
            var validacion = new ValidacionPropietarioPermiso().OportunidadValidarPropietarioPermiso(idoportunidad, Session["IdUsuario"].ToString());
            if (n.usuarios.Seleccionar_CreadorOportunidad(idoportunidad) == Session["IdUsuario"].ToString() 
                || n.compartiroportunidades.Validar_SiUsuarioPuedeModificar(idoportunidad, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                n.oportunidadesimportes.Agregar_Registro(idoportunidad, importe, tipomoneda, tipocambio, rubro, observaciones);
            }
            return Json(n.oportunidadesimportes.Seleccionar_ImportesPorIdOportunidad(idoportunidad), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarOportunidadImporte(string idoportunidad, string importe, string tipomoneda, string tipocambio, string rubro, string observaciones, string id)
        {
            //modificar
            var validacion = new ValidacionPropietarioPermiso().OportunidadValidarPropietarioPermiso(idoportunidad, Session["IdUsuario"].ToString());
            if (n.usuarios.Seleccionar_CreadorOportunidad(idoportunidad) == Session["IdUsuario"].ToString() 
                || n.compartiroportunidades.Validar_SiUsuarioPuedeModificar(idoportunidad, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                n.oportunidadesimportes.Modificar_Registro(importe, tipomoneda, tipocambio, rubro, observaciones, id);
            }
            return Json(n.oportunidadesimportes.Seleccionar_ImportesPorIdOportunidad(idoportunidad), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarOportunidadImporte(string idoportunidad, string id)
        {
            //Eliminar
            var validacion = new ValidacionPropietarioPermiso().OportunidadValidarPropietarioPermiso(idoportunidad, Session["IdUsuario"].ToString());
            if (n.usuarios.Seleccionar_CreadorOportunidad(idoportunidad) == Session["IdUsuario"].ToString() 
                || n.compartiroportunidades.Validar_SiUsuarioPuedeModificar(idoportunidad, Session["IdUsuario"].ToString()) 
                || Session["IdUsuario"].ToString() == "2")
            {
                n.oportunidadesimportes.Eliminar_Registro(id);
            }
            return Json(n.oportunidadesimportes.Seleccionar_ImportesPorIdOportunidad(idoportunidad), JsonRequestBehavior.AllowGet);
        }

        //Mostrar los archivos existentes para la oportunidad

        public JsonResult CargaInicialArchivos(string idoportunidad)
        {
            return Json(n.archivos.Seleccionar_PorIdOportunidad(idoportunidad), JsonRequestBehavior.AllowGet);
        }

        //Subir archivos
        public JsonResult SubirArchivo()
        {
            //int archivosactuales = 0;
            //int totalnuevosarchivos = 0;
            // checar si el usuario seleccionó un archivo para subir
            if (Request.Files.Count > 0)
            {
                try
                {                    
                    var idoportunidad = Request.Form.GetValues(0);
                    //archivosactuales = ar.SeleccionarPorIdOportunidad(idoportunidad[0]).Count();
                    HttpFileCollectionBase files = Request.Files;

                    HttpPostedFileBase file = files[0];
                    string fileName = file.FileName;

                    // crear un directorio para subir los archivos si no existe
                    string ruta1 = Path.Combine(Server.MapPath("~/Archivos/"), fileName);

                    // guardar el archivo
                    file.SaveAs(ruta1);

                    if (ruta1.Contains(fileName))
                    {
                        Archivos items = new Archivos();
                        items.Nombre = fileName;
                        items.IdOportunidad = int.Parse(idoportunidad[0]);
                        n.archivos.Agregar_Registro(items);
                        //totalnuevosarchivos = ar.SeleccionarPorIdOportunidad(idoportunidad[0]).Count();
                    }
                    return Json(n.archivos.Seleccionar_PorIdOportunidad(idoportunidad[0]), JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json("error" + e.Message);
                }
            }

            return Json("Nada");
        }

        //Seleccionar Medidores
        public JsonResult SeleccionMedidoresUDN(string año)
        {
            return Json(n.oportunidades.Seleccionar_MedidoresUDN(año), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarMedidoresUDNGlobal(string año)
        {
            return Json(n.oportunidades.Seleccionar_MedidoresUDNGlobal(año), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionMedidoresEstados(string idudn, string anne)
        {
            return Json(n.oportunidades.Seleccionar_MedidoresEstados(idudn, anne), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionMedidoresEstadosGlobal(string ann)
        {
            return Json(n.oportunidades.Seleccionar_MedidoresEstadosGlobal(ann), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionMedidoresEstadosAcumulado(string idudn)
        {
            return Json(n.oportunidades.Seleccionar_MedidoresEstadosAcumulado(idudn), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionMedidoresEstadosEnElMes(string idudn)
        {
            return Json(n.oportunidades.Seleccionar_MedidoresEstadosEnElMes(idudn), JsonRequestBehavior.AllowGet);
        }


        #region Metricas ***********************************************************************************************

        public JsonResult SeleccionarMetricaPorId(string id)
        {
            return Json(n.metricas.Seleccionar_RegistroPorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarMetrica(string metrica, string comprador, string criterios, string proceso, string necesidad, string campeon, string fulcro, string otros, string idoportunidad)
        {
            Metricas items = new Metricas();
            items.Metrica = metrica;
            items.Comprador = comprador;
            items.Criterios = criterios;
            items.Proceso = proceso;
            items.Necesidad = necesidad;
            items.Campeon = campeon;
            items.Fulcro = fulcro;
            items.Otros = otros;
            items.IdOportunidad = int.Parse(idoportunidad);
            return Json(n.metricas.Agregar_Registro(items), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarMetrica(string metrica, string comprador, string criterios, string proceso, string necesidad, string campeon, string fulcro, string otros, string idoportunidad)
        {
            Metricas items = new Metricas();
            items.Metrica = metrica;
            items.Comprador = comprador;
            items.Criterios = criterios;
            items.Proceso = proceso;
            items.Necesidad = necesidad;
            items.Campeon = campeon;
            items.Fulcro = fulcro;
            items.Otros = otros;
            items.IdOportunidad = int.Parse(idoportunidad);
            if (new ValidacionPropietarioPermiso().OportunidadValidarPropietarioPermiso(idoportunidad, Session["IdUsuario"].ToString()))
            {
                return Json(n.metricas.Modificar_Registro(items), JsonRequestBehavior.AllowGet);
            }
            else 
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion 



        public ActionResult Metas()
        {
            return View();
        }
    }
}