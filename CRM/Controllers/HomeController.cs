using CRM.Models.Models;
using CRM.Models.Repository;
using CRM.Utilerias;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CRM.Controllers
{
    public class HomeController : BaseController
    {
        private UsuariosRoles usuariorolsesion;

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        // GET: Home
        [SessionTimeOut]
        public ActionResult Index()
        {
            return View();
        }

        [SessionTimeOut]
        public ActionResult Index2()
        {
            //Obtener asignados del responsable
            ViewBag.Resultado = n.oportunidades.Seleccionar_OportunidadesPorIdResponsable(Session["IdUsuario"].ToString());
            //Mostrar los pendientes del responsable en la navegacion superior
            Session["Pendientes"] = n.oportunidades.Seleccionar_OportunidadesPorIdResponsableEstado1(Session["IdUsuario"].ToString());
            return View();
        }

        public ActionResult Index3()
        {
            return View();
        }

        public ActionResult Index4()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        /// <summary>
        /// Valida si los datos de entrada permitirán el acceso a la aplicación
        /// </summary>
        /// <param name="Clave"></param>
        /// <param name="Contraseña"></param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult Login(string Clave, string Contraseña, string rememberme)
        {
            if (n.usuarios.Validar_ClaveContraseña(Clave, Contraseña) > 0)
            {
                usuariorolsesion                = new UsuariosRoles();
                usuariorolsesion                = n.usuarios.Seleccionar_PorClaveContraseña(Clave, Contraseña);
                Session["GranSesion"]           = usuariorolsesion;  //carga de sesion eficiente
                GranSesion["GranSesion"]        = usuariorolsesion;  //pruebas
                Session["NombreUsuario"]        = usuariorolsesion.Usuarios.Nombre + " " + usuariorolsesion.Usuarios.ApellidoPaterno + " " + usuariorolsesion.Usuarios.ApellidoMaterno;
                Session["NombreRol"]            = usuariorolsesion.Roles.Nombre;
                Session["IdUsuario"]            = usuariorolsesion.Usuarios.Id;
                Session["IdRol"]                = usuariorolsesion.Roles.Id;
                Session["Logo"]                 = usuariorolsesion.Configuracion.Logo;
                Session["IdConfiguracion"]      = usuariorolsesion.Configuracion.Id;
                Session["ContraseñaCambiada"]   = n.usuarios.Contraseña_Cambiada(Clave, Contraseña);
                Session["PermisosVerArchivos"]  = n.usuarios.Seleccionar_SiPuedeBajarVerArchivos(usuariorolsesion.Usuarios.Id.ToString());
                Session["CrearClasSubClasDocs"] = usuariorolsesion.Usuarios.ClasSubClasPermiso == true ? true : false;

                //Menú dinámico
                //Session["Menu"]                 = CustomHelpers.Menu(Session["IdRol"].ToString(), Session["IdConfiguracion"].ToString());
                //string paginadeinicio           = usuariorolsesion.Roles.PaginaInicio;
                //return RedirectToAction(paginadeinicio);

                //Manejo de cookies
                //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(usuariorolsesion.Usuarios.Clave, true, 10);
                //string datos = FormsAuthentication.Encrypt(ticket);
                //HttpCookie cookie = new HttpCookie("Usuario", datos);
                //Response.Cookies.Add(cookie);

                if (usuariorolsesion.Roles.Id == 1)
                {
                    return RedirectToAction("Index3");
                }
                if (usuariorolsesion.Roles.Id == 2 || usuariorolsesion.Roles.Id == 8) //Supervisor, Redes Sociales
                {
                    //if (usuariorolsesion.Configuracion.Id == 2 || usuariorolsesion.Configuracion.Id == 3) //Asae, SABE
                        return RedirectToAction("Index");
                    //else if (usuariorolsesion.Configuracion.Id == 4)                         //Otras (Arben)
                    //ProyectosAcumuladosAlMes    return RedirectToAction("Index4");
                }
                else if (usuariorolsesion.Roles.Id == 3 || usuariorolsesion.Roles.Id == 6 || usuariorolsesion.Roles.Id == 7) //Operacion, Gte proyectos, Comisionista
                {
                    return RedirectToAction("../Oportunidades/Index");
                }
                else if (usuariorolsesion.Roles.Id == 4) //Comercial
                { 
                    //Por definir
                }
                else if (usuariorolsesion.Roles.Id == 5) //Responsable (MABE)
                {
                    return RedirectToAction("Index2");
                }
            }

            ViewData["Mensaje"] = "Falso";
            return View();
        }

        [SessionTimeOut]
        public ActionResult CambiarContraseña()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarContraseña(string ClaveActual, string ClaveNueva, string Repetir)
        {
            if (ClaveNueva.Length < 12 || Repetir.Length < 12)
            {
                ViewData["Mensaje"] = "<div class='alert alert-warning' role='alert' style='width:100%'>La contraseña no tiene la longitud requerida, intente de nuevo.</div>";
            }
            else 
            { 
                if (ClaveNueva == Repetir)
                    if (n.usuarios.Modificar_Contraseña(ClaveActual, ClaveNueva, Session["IdUsuario"].ToString()) > 0)
                        ViewData["Mensaje"] = "<div class='alert alert-success' role='alert' style='width:100%'>Se actualizó la contraseña correctamente, ahora debe salir del sistema y volver a entrar para que los cambios tengan efecto, de lo contrario, sus datos se perderán.</div>";
                    else
                        ViewData["Mensaje"] = "<div class='alert alert-danger' role='alert' style='width:100%'>No se actualizó la contraseña, hay un error en el sistema, intente de nuevo más tarde.</div>";
                else
                    ViewData["Mensaje"] = "<div class='alert alert-warning' role='alert' style='width:100%'>Hay un error en las contraseñas, intente de nuevo.</div>"; ;
            }
            return View();
        }

        [SessionTimeOut]
        public ActionResult ActualizarContraseña(string ClaveActual, string ClaveNueva, string Repetir)
        {
            if (ClaveNueva == Repetir)
                if (n.usuarios.Modificar_Contraseña(ClaveActual, ClaveNueva, Session["IdUsuario"].ToString()) > 0)
                    ViewData["Mensaje"] = "Modificado";
                else
                    ViewData["Mensaje"] = "Fallo";
            else
                ViewData["Mensaje"] = "NoCoincidentes";
            return View("CambiarContraseña");
        }

        public ActionResult RecuperarContraseña()
        {
            return View();
        }

        [SessionTimeOut]
        public ActionResult TemasEmpresas()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Encontrado = n.empresas.Seleccionar_EmpresasEnTemasEnProceso(usuariorolsesion.Configuracion.Id.ToString());
            return View();
        }

        [SessionTimeOut]
        public ActionResult TemasContactos()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Encontrado = n.contactos.Seleccionar_ContactosEnTemasEnProceso(usuariorolsesion.Configuracion.Id.ToString());
            return View();
        }

        [SessionTimeOut]
        public ActionResult Temas()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            string gerentes = "";
            if (usuariorolsesion.Roles.Id == 3)
                gerentes = usuariorolsesion.Usuarios.Id.ToString();
            ViewBag.Encontrado = n.oportunidades.Seleccionar_TemasEnProceso(usuariorolsesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), gerentes);
            return View();
        }

        [SessionTimeOut]
        public ActionResult Clasificacion()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            string gerentes = "";
            if (usuariorolsesion.Roles.Id == 3)
                gerentes = usuariorolsesion.Usuarios.Id.ToString();
            ViewBag.Resultado = n.oportunidades.Seleccionar_TemasEnProceso(usuariorolsesion.Configuracion.Id.ToString(), Session["IdRol"].ToString(), gerentes);
            return View();
        }

        public ActionResult Salir()
        {
            //terminar sesion
            Session["GranSesion"] = null;
            Session["NombreUsuario"] = null;
            Session["NombreRol"] = null;
            Session["IdUsuario"] = null;
            Session["IdRol"] = null;
            //FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult RecuperaContraseña(string Correo)
        {
            //Crear una nueva contraseña y enviársela al usuario para que la recupere.
            if (n.usuarios.Validar_CorreoUsuario(Correo) == "1")
            {
                if (n.usuarios.Reiniciar_Correo(Correo) > 0)
                {
                    if (correo.EnviarCorreoRecuperacionContraseña(Correo))
                    {
                        ViewBag.CorreoEnviado = "<div class='alert alert-warning' role='alert' style='width:100%'>Error en el envío de correo.</div>";
                    }
                    TempData["Mensaje"] = "<div class='alert alert-success' role='alert' style='width:100%'>La contraseña se ha enviado al correo indicado.</div>";
                }
                else
                {
                    TempData["Mensaje"] = "<div class='alert alert-warning' role='alert' style='width:100%'>Correo incorrecto.</div>";
                }
            }
            else 
            {
                TempData["Mensaje"] = "<div class='alert alert-danger' role='alert' style='width:100%'>Correo desconocido.</div>";
            }
            return View("RecuperarContraseña");
        }

        public ActionResult Otras()
        {
            return View();
        }

        public ActionResult ReasignarEmpresa(string mxm)
        {
            //Cambiar los datos del usuario para asignarlo a la empresa seleccionada
            usuariorolsesion = new UsuariosRoles();
            usuariorolsesion = n.usuarios.Seleccionar_EmpresaPorUsuarioYaConectado(Session["IdUsuario"].ToString(), mxm);
            Session["GranSesion"] = usuariorolsesion;  //carga de sesion eficiente
            GranSesion["GranSesion"] = usuariorolsesion;  //pruebas
            Session["NombreUsuario"] = usuariorolsesion.Usuarios.Nombre + " " + usuariorolsesion.Usuarios.ApellidoPaterno + " " + usuariorolsesion.Usuarios.ApellidoMaterno;
            Session["NombreRol"] = usuariorolsesion.Roles.Nombre;
            Session["IdUsuario"] = usuariorolsesion.Usuarios.Id;
            Session["IdRol"] = usuariorolsesion.Roles.Id;
            Session["Logo"] = usuariorolsesion.Configuracion.Logo;
            Session["IdConfiguracion"] = usuariorolsesion.Configuracion.Id;
            string vista = "";
            if (mxm == "2")
                vista = "Index";
            else if (mxm == "4")
                vista = "Index4";

            return RedirectToAction(vista);
        }

        public ActionResult  Invitado()
        {
            return View();
        }

        public ActionResult Graficos()
        {
            return View();
        }

        //Estadisticas Iniciales

        public JsonResult DatosTablas(string idconfiguracion)
        {
            return Json(n.estadisticas.Tablas_Contenido(idconfiguracion), JsonRequestBehavior.AllowGet);
        }

        //Estadisticas Detalle
        [SessionTimeOut]
        public ActionResult Presentacion(string p = "1")
        {
            ViewBag.Empresas = null;
            ViewBag.Contactos = null;
            ViewBag.Oportunidades = null;
            ViewBag.Tareas = null;
            ViewBag.Comerciales = null;
            if (p == "1")
                ViewBag.Empresas = n.estadisticas.Seleccionar_EmpresasContactosOportunidadesTareas();
            else if (p == "2")
                ViewBag.Contactos = n.estadisticas.Seleccionar_ContactosEmpresasOportunidadesTareas();
            else if (p == "3")
                ViewBag.Oportunidades = n.estadisticas.Seleccionar_ContactosEmpresasOportunidades();
            else if (p == "4")
                ViewBag.Tareas = n.estadisticas.Seleccionar_ContactosEmpresasTareas();
            else if (p == "5")
                ViewBag.Comerciales = n.estadisticas.Seleccionar_UsuariosTareasOportunidades(); ;
            return View();
        }

        [HttpGet]
        public ActionResult AsuntosARepetirse(string repetir)
        {
            return View(n.oportunidades.Seleccionar_AsuntosQueVanARepetirse(repetir));
        }

        [HttpGet]
        public ActionResult Proyectos(string ed, string idudn)
        {
            return Json(n.udn.Seleccionar_ProyectosImportesAcumuladoPorEstado(ed, idudn), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProyectosEnElMesEnCurso(string ed, string idudn)
        {
            return Json(n.udn.Seleccionar_ProyectosImportesEnElMesPorEstado(ed, idudn), JsonRequestBehavior.AllowGet);
        }

        [SessionTimeOut]
        public JsonResult SeleccionarEmpresasConexiones()
        {
            List<Modelos> mod = new List<Modelos>();
            mod = n.estadisticas.Seleccionar_EmpresasContactosOportunidadesTareas();
            return Json(mod, JsonRequestBehavior.AllowGet);
        }
        [SessionTimeOut]
        public JsonResult SeleccionarContactosConexiones()
        {
            List<Modelos> mod = new List<Modelos>();
            mod = n.estadisticas.Seleccionar_ContactosEmpresasOportunidadesTareas();
            return Json(mod, JsonRequestBehavior.AllowGet);
        }
        [SessionTimeOut]
        public JsonResult SeleccionarOportunidadesConexiones()
        {
            List<Modelos> mod = new List<Modelos>();
            mod = n.estadisticas.Seleccionar_ContactosEmpresasOportunidades();
            return Json(mod, JsonRequestBehavior.AllowGet);
        }
        [SessionTimeOut]
        public JsonResult SeleccionarTareasConexiones()
        {
            List<Modelos> mod = new List<Modelos>();
            mod = n.estadisticas.Seleccionar_ContactosEmpresasTareas();
            return Json(mod, JsonRequestBehavior.AllowGet);
        }
        [SessionTimeOut]
        public JsonResult SeleccionarUsuariosConexiones()
        {
            List<Modelos> mod = new List<Modelos>();
            mod = n.estadisticas.Seleccionar_UsuariosTareasOportunidades();
            return Json(mod, JsonRequestBehavior.AllowGet);
        }

        //Estadísticas empresas
        [SessionTimeOut]
        public JsonResult AsuntosPorEmpresaGeneral(string idconfiguracion)
        {
            return Json(n.estadisticas.Seleccionar_AsuntosPorEmpresaGeneral(idconfiguracion), JsonRequestBehavior.AllowGet);
        }
        [SessionTimeOut]
        public JsonResult AsuntosPorEmpresaGeneralPorEstado(string estado, string idconfiguracion)
        {
            return Json(n.estadisticas.Seleccionar_AsuntosPorEmpresaPorEstado(estado, idconfiguracion), JsonRequestBehavior.AllowGet);
        }
        [SessionTimeOut]
        public JsonResult AsuntosPorEmpresaGeneralPorEstado1_1(string idconfiguracion)
        {
            return Json(n.estadisticas.Seleccionar_AsuntosPorEmpresaPorEstado1_1(idconfiguracion), JsonRequestBehavior.AllowGet);
        }
        [SessionTimeOut]
        public JsonResult AsuntosPorEmpresaGeneralPorEstado1_2(string idconfiguracion)
        {
            return Json(n.estadisticas.Seleccionar_AsuntosPorEmpresaPorEstado1_2(idconfiguracion), JsonRequestBehavior.AllowGet);
        }
        [SessionTimeOut]
        public JsonResult AsuntosPorEmpresaGeneralPorEstado1_3(string idconfiguracion)
        {
            return Json(n.estadisticas.Seleccionar_AsuntosPorEmpresaPorEstado1_3(idconfiguracion), JsonRequestBehavior.AllowGet);
        }
        [SessionTimeOut]
        public JsonResult AsuntosPorEmpresaGeneralPorEstado1_4(string idconfiguracion)
        {
            return Json(n.estadisticas.Seleccionar_AsuntosPorEmpresaPorEstado1_4(idconfiguracion), JsonRequestBehavior.AllowGet);
        }

        //Detalle por empresa desde gráfico
        [SessionTimeOut]
        public ActionResult TemasDetalleEmpresas1()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Encontrado = n.empresas.Seleccionar_EmpresasTemasDetalle1(usuariorolsesion.Configuracion.Id.ToString());
            return View();
        }
        [SessionTimeOut]
        public ActionResult TemasDetalleEmpresas12()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Encontrado = n.empresas.Seleccionar_EmpresasTemasDetalle12(usuariorolsesion.Configuracion.Id.ToString());
            return View();
        }
        [SessionTimeOut]
        public ActionResult TemasDetalleEmpresas13()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Encontrado = n.empresas.Seleccionar_EmpresasTemasDetalle13(usuariorolsesion.Configuracion.Id.ToString());
            return View();
        }
        [SessionTimeOut]
        public ActionResult TemasDetalleEmpresas14()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Encontrado = n.empresas.Seleccionar_EmpresasTemasDetalle14(usuariorolsesion.Configuracion.Id.ToString());
            return View();
        }
        [SessionTimeOut]
        public ActionResult TemasDetalleEmpresas2()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Encontrado = n.empresas.Seleccionar_EmpresasTemasDetalle14(usuariorolsesion.Configuracion.Id.ToString());
            return View();
        }
        [SessionTimeOut]
        public ActionResult TemasDetalleEmpresas5()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Encontrado = n.empresas.Seleccionar_EmpresasTemasDetalle5(usuariorolsesion.Configuracion.Id.ToString());
            return View();
        }
        [SessionTimeOut]
        public ActionResult TemasDetalleEmpresas6()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Encontrado = n.empresas.Seleccionar_EmpresasTemasDetalle6(usuariorolsesion.Configuracion.Id.ToString());
            return View();
        }
        [SessionTimeOut]
        public ActionResult TemasDetalleEmpresas7()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Encontrado = n.empresas.Seleccionar_EmpresasTemasDetalle7(usuariorolsesion.Configuracion.Id.ToString());
            return View();
        }

        //**************** Asuntos que van a repetirse MABE **********************************
        public JsonResult AsuntosARepetir(string repetir)
        {
            return Json(n.oportunidades.Seleccionar_AsuntosARepetirse(repetir), JsonRequestBehavior.AllowGet);
        }

        //**************** Fin asuntos que van a repetirse MABE ******************************

        //Estadísticas Clasificaciones
        [SessionTimeOut]
        public JsonResult Clasificaciones(string idconfiguracion)
        {
            return Json(n.clasificacion.Seleccionar_Conteo(idconfiguracion), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EventosAEfectuarse(string idconfiguracion)
        {
            return Json(n.clasificacion.Seleccionar_Conteo(idconfiguracion), JsonRequestBehavior.AllowGet);
        }

        //**************** Estadísticas ASAE **********************************

        [SessionTimeOut]
        public JsonResult ProyectosXUDN()
        {
            return Json(n.udn.Seleccionar_ProyectosPorUDN(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarProyectosAcumuladosAlMesPorUDN(string estado)
        {
            return Json(n.udn.Seleccionar_ProyectosAcumuladosAlMesPorUDN(estado), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SeleccionarProyectosAcumuladosAlMesPorUDN()
        //{
        //    return Json(n.udn.seleccionar.Seleccionar_ProyectosAcumuladosAlMesPorUDN(), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult ImportesXUDN()
        {
            return Json(n.udn.Seleccionar_ImportesPorUDN(), JsonRequestBehavior.AllowGet);
        }

        /* Proyectos e importes por mes acumulado y por mes en curso obtenidos por estado ***********/

        public JsonResult ProyectosAcumuladosAlMes(string estado)
        {
            return Json(n.udn.Seleccionar_ProyectosAcumuladosAlMesPorUDNPorEstado(estado), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProyectosEnElMes(string estado)
        {
            return Json(n.udn.Seleccionar_ProyectosPorUDNEnElMesPorEstado(estado), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProyectosEnElMes34(string estado)
        {
            return Json(n.udn.Seleccionar_ProyectosAcumuladosAlMesPorUDNPorEstado34(estado), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ImportesAcumuladosAlMes(string estado)
        {
            return Json(n.udn.Seleccionar_ImportesPorUDNAlMesPorEstado(estado), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ImportesEnElMes(string estado)
        {
            return Json(n.udn.Seleccionar_ImportesPorUDNEnElMesPorEstado(estado), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ImportesEnElMes34(string estado)
        {
            return Json(n.udn.Seleccionar_ImportesPorUDNEnElMesPorEstado34(estado), JsonRequestBehavior.AllowGet);
        }

        /***********************************************************************************************/

        public JsonResult SeleccionarImportesAcumuladosAlMesPorUDN()
        {
            return Json(n.udn.Seleccionar_ImportesAcumuladosAlMesPorUDN(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProyectosXUDNCerrados()
        {
            return Json(n.udn.Seleccionar_ProyectosPorUDNCerrados(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarProyectosAcumuladosAlMesPorUDNCerrados()
        {
            return Json(n.udn.Seleccionar_ProyectosAcumuladosAlMesPorUDNCerrados(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ImportesXUDNCerrados()
        {
            return Json(n.udn.Seleccionar_ImportesPorUDNCerrados(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarImportesAcumuladosAlMesPorUDNCerrados()
        {
            return Json(n.udn.Seleccionar_ImportesAcumuladosAlMesPorUDNCerrados(), JsonRequestBehavior.AllowGet);
        }

        //Proyectos
        //Cerrados Ganados/ Perdidos acumulados al mes
        public JsonResult SeleccionarCerradosGanadosAcumuladosAlMes()
        {
            return Json(n.udn.Seleccionar_ProyectosPorUDNCerradosGanadosAlMes(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarCerradosPerdidosAcumuladosAlMes()
        {
            return Json(n.udn.Seleccionar_ProyectosPorUDNCerradosPerdidosAlMes(), JsonRequestBehavior.AllowGet);
        }

        //Cerrados ganados/perdidos en el mes
        public JsonResult SeleccionarCerradosGanadosEnElMes()
        {
            return Json(n.udn.Seleccionar_ProyectosPorUDNCerradosGanadosEnElMes(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SeleccionarCerradosPerdidosEnElMes()
        {
            return Json(n.udn.Seleccionar_ProyectosPorUDNCerradosPerdidosEnElMes(), JsonRequestBehavior.AllowGet);
        }

        //Montos Cerrados Ganados
        public JsonResult SeleccionarImportesCerradosGanadosAcumuladosAlMes()
        {
            return Json(n.udn.Seleccionar_ImportesAcumuladosAlMesPorUDNCerradosGanados(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarImportesCerradosGanadosAcumuladosEnElMes()
        {
            return Json(n.udn.Seleccionar_ImportesEnElMesPorUDNCerradosGanados(), JsonRequestBehavior.AllowGet);
        }

        //Montos Cerrados Perdidos
        public JsonResult SeleccionarImportesCerradosPerdidosAcumuladosAlMes()
        {
            return Json(n.udn.Seleccionar_ImportesAcumuladosAlMesPorUDNCerradosPerdidos(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarImportesCerradosPerdidosEnElMes()
        {
            return Json(n.udn.Seleccionar_ImportesEnElMesPorUDNCerradosPerdidos(), JsonRequestBehavior.AllowGet);
        }

        [SessionTimeOut]
        public JsonResult SeleccionarProyectosXMes()
        {
            return Json(n.udn.Seleccionar_ProyectosPorMes(), JsonRequestBehavior.AllowGet);
        }

        [SessionTimeOut]
        public JsonResult SeleccionarProyectosXMesXAño()
        {
            return Json(n.udn.Seleccionar_ProyectosPorMesPorAño(), JsonRequestBehavior.AllowGet);
        }

        [SessionTimeOut]
        public JsonResult SeleccionarImportesXMesXAño()
        {
            return Json(n.udn.SeleccionarImportesPorMesPorAño(), JsonRequestBehavior.AllowGet);
        }

        [SessionTimeOut]
        public JsonResult SeleccionarImporteXMes()
        {
            return Json(n.udn.Seleccionar_ImportesPorMes(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarProyectosImportes()
        {
            n.udn.Actualizar_ProyectosImportes();
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        //Métodos privados

        




    }
}
