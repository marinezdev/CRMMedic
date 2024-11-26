using CRM.Models.Models;
using CRM.Models.Repository;
using CRM.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRM.Controllers
{ 
    [SessionTimeOut]
    [HandleError]
    public class EmpresasController : BaseController
    {
        private UsuariosRoles usuariorolsesion;

        // GET: Empresas
        public ActionResult Index()
        {            
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            if (usuariorolsesion.Roles.Id == 7)
                ViewBag.Empresas = n.empresas.Seleccionar_Todas(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString());
            //else if ((usuariorolsesion.Roles.Id == 3 || usuariorolsesion.Roles.Id == 5 || usuariorolsesion.Roles.Id == 7) && usuariorolsesion.Configuracion.Id == 2)
            //    ViewBag.Empresas = n.empresas.Seleccionar_Todas(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString());
            //else if (usuariorolsesion.Roles.Id == 6) //Lo que ve el gerente de proyecto
            //    ViewBag.Empresas = n.empresas.Seleccionar_GteProyectoComisionistas();
            //else if (usuariorolsesion.Roles.Id == 8) //Redes sociales
            //    ViewBag.Empresas = n.empresas.Seleccionar_RedesSociales();
            else
                ViewBag.Empresas = n.empresas.Seleccionar_Todas(usuariorolsesion.Configuracion.Id.ToString());

            if (n.empresas.Seleccionar_CompartidasPorUsuario(usuariorolsesion.Usuarios.Id.ToString()).ToList().Count > 0)
                ViewBag.EmpresasCompartidas = n.empresas.Seleccionar_CompartidasPorUsuario(usuariorolsesion.Usuarios.Id.ToString());
            else
                ViewBag.EmpresasCompartidas = null;

            return View();
        }

        [HttpPost]
        public ActionResult Index(string tipo)
        {
            UsuariosRoles usuariorolsesion = (UsuariosRoles)Session["GranSesion"];

            if (tipo == "1")
            {
                TempData["Checado"] = 1;
                if (usuariorolsesion.Roles.Id == 7)
                    ViewBag.Empresas = n.empresas.Seleccionar_TodasPorTipo(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString(), "1");
                else if ((usuariorolsesion.Roles.Id == 3 || usuariorolsesion.Roles.Id == 5 || usuariorolsesion.Roles.Id == 7) && usuariorolsesion.Configuracion.Id == 2)
                    ViewBag.Empresas = n.empresas.Seleccionar_TodasPorTipo(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString(), "1");
                else if (usuariorolsesion.Roles.Id == 6) //Lo que ve el gerente de proyecto
                    ViewBag.Empresas = n.empresas.Seleccionar_GteProyectoComisionistasPorTipo("1");
                else if (usuariorolsesion.Roles.Id == 8) //Redes sociales
                    ViewBag.Empresas = n.empresas.Seleccionar_RedesSocialesPorTipo("1");
                else
                    ViewBag.Empresas = n.empresas.Seleccionar_TodasPorTipo(usuariorolsesion.Configuracion.Id.ToString(), "", "1");

                if (n.empresas.Seleccionar_CompartidasPorUsuarioPorTipo(usuariorolsesion.Usuarios.Id.ToString(), "1").ToList().Count > 0)
                    ViewBag.EmpresasCompartidas = n.empresas.Seleccionar_CompartidasPorUsuarioPorTipo(usuariorolsesion.Usuarios.Id.ToString(), "1");
                else
                    ViewBag.EmpresasCompartidas = null;
            }
            else if (tipo == "2")
            {
                TempData["Checado"] = 2;
                if (usuariorolsesion.Roles.Id == 7)
                    ViewBag.Empresas = n.empresas.Seleccionar_TodasPorTipo(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString(), "2");
                else if ((usuariorolsesion.Roles.Id == 3 || usuariorolsesion.Roles.Id == 5 || usuariorolsesion.Roles.Id == 7) && usuariorolsesion.Configuracion.Id == 2)
                    ViewBag.Empresas = n.empresas.Seleccionar_TodasPorTipo(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString(), "2");
                else if (usuariorolsesion.Roles.Id == 6) //Lo que ve el gerente de proyecto
                    ViewBag.Empresas = n.empresas.Seleccionar_GteProyectoComisionistasPorTipo("2");
                else if (usuariorolsesion.Roles.Id == 8) //Redes sociales
                    ViewBag.Empresas = n.empresas.Seleccionar_RedesSocialesPorTipo("2");
                else
                    ViewBag.Empresas = n.empresas.Seleccionar_TodasPorTipo(usuariorolsesion.Configuracion.Id.ToString(), "", "2");

                if (n.empresas.Seleccionar_CompartidasPorUsuarioPorTipo(usuariorolsesion.Usuarios.Id.ToString(), "2").ToList().Count > 0)
                    ViewBag.EmpresasCompartidas = n.empresas.Seleccionar_CompartidasPorUsuarioPorTipo(usuariorolsesion.Usuarios.Id.ToString(), "2");
                else
                    ViewBag.EmpresasCompartidas = null;
            }

            return View();
        }

        [HttpGet]
        public ActionResult Graficos(string opc)
        {
            ViewBag.Empresas = n.empresas.Seleccionar_TodasFiltrado(opc);
            return View();
        }

        public ActionResult Alta()
        {
            ViewBag.Estados = Utilerias.Funciones.Estados();
            return View();
        }

        public ActionResult Alta2()
        {
            ViewBag.Estados = Utilerias.Funciones.Estados();
            ViewBag.Sectores = Utilerias.Funciones.Sectores();
            ViewBag.Paises = n.paises.Seleccionar_Paises();
            ViewBag.UDN = n.udn.Seleccionar_Registros();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(string Id)
        {
            int i = 0;
            bool resultado = int.TryParse(Id, out i);
            if (!resultado)
            {
                return RedirectToAction("Index");
            }

            UsuariosRoles usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Estados = Utilerias.Funciones.Estados();
            ViewBag.Sectores = Utilerias.Funciones.Sectores();
            ViewBag.Paises = n.paises.Seleccionar_Paises();
            ViewBag.UDN = n.udn.Seleccionar_Registros();
            ViewBag.ResponsablesParaCompartir = n.usuarios.Seleccionar_UsuariosResponsablesASAE("2", usuariorolsesion.Usuarios.Id.ToString());
            ViewBag.EmpresaEnOportunidades = n.oportunidades.Seleccionar_EmpresasEnOportunidades(Id);            
            ViewBag.EmpresasEnOportunidadesContadas = n.oportunidades.Seleccionar_EmpresasEnOportunidades(Id).Count();
            ViewBag.ContactosEnEmpresas = n.contactos.Seleccionar_ContactosPorEmpresa(Id);
            ViewBag.ContactosEnEmpresasContados = n.contactos.Seleccionar_ContactosPorEmpresa(Id).Count();
            ViewBag.NombreEmpresa = n.empresas.Seleccionar_NombreEmpresa(Id);
            ViewBag.DireccionesAdicionales = n.empresasdirecciones.Seleccionar_DireccionesAdicionalesPorIdEmpresa(Id);
            return View();
        }

        /// <summary>
        /// Obtiene el detalle de una empresa para modificarlo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult SeleccionarPorId(string id)
        {
            return Json(n.empresas.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarAdicionalPorId(string id)
        {
            return Json(n.empresasdetalle.Seleccionar_PorIdEmpresa(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarUsuariosPorEmpresaId(string id)
        {
            return Json(n.empresas.Seleccionar_usuariosPorEmpresaId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarDireccionEmpresa(string id)
        {
            return Json(n.empresas.Seleccionar_DireccionEmpresa(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarEmpresasPorIdConfiguracion(string id)
        {
            UsuariosRoles usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            List<Empresas> em = new List<Empresas>();
            if (Session["IdRol"].ToString() == "7")
                em = n.empresas.Seleccionar_Todas(id, usuariorolsesion.Usuarios.Id.ToString());
            else
                em = n.empresas.Seleccionar_Todas(id, "");
            return Json(em, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Guardar(string Nombre, string RFC, string TipoEmpresa, string Direccion, string Ciudad, string Estado, string IdConfiguracion)
        {
            Empresas items = new Empresas();
            items.Nombre = Nombre;
            items.RFC = RFC.ToUpper();
            if (!string.IsNullOrEmpty(TipoEmpresa))
                items.InternaExterna = int.Parse(TipoEmpresa);
            else
                items.InternaExterna = 0;
            items.Direccion = Direccion;
            items.Ciudad = Ciudad;
            items.Estado = string.IsNullOrEmpty(Estado) ? 0 : int.Parse(Estado);
            items.IdConfiguracion = int.Parse(IdConfiguracion);
            items.IdUsuario = int.Parse(Session["IdUsuario"].ToString());

            int salida = 0;

            salida = n.empresas.Agregar_Registro(items);
            if (salida > 0)
            {
                //Guardar la nueva empresa para los roles 2 y 3
                foreach (var item in n.empresasusuarios.Seleccionar_UsuariosExistentesRol2(Session["IdConfiguracion"].ToString()))
                {
                    n.empresasusuarios.Agregar_Registro(salida.ToString(), item.Id.ToString(), "true");
                }
                foreach (var item in n.empresasusuarios.Seleccionar_UsuariosExistentesRol3(Session["IdConfiguracion"].ToString()))
                {
                    n.empresasusuarios.Agregar_Registro(salida.ToString(), item.Id.ToString(), "true");
                }
            }

            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PreGuardado(string Nombre, string IdConfiguracion)
        {
            Empresas items = new Empresas();
            items.Nombre = Nombre;
            items.IdConfiguracion = int.Parse(IdConfiguracion);

            return Json(n.empresas.Pre_Guardado(items), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarCompleto(string Nombre, string RFC, string TipoEmpresa, string IdConfiguracion,
        string Direccion, string CP, string Pais, string Ciudad, string Estado, string Sector, string Telefono, string Extension, string PaginaWeb, string IdUDN,
        string IdUsuario, string Tipo)
        {
            Empresas items = new Empresas();
            items.Nombre = Nombre;
            items.RFC = RFC.ToUpper();
            items.InternaExterna = int.Parse(TipoEmpresa);
            items.Direccion = Direccion;
            items.Ciudad = Ciudad;
            items.CP = CP;
            items.Estado = string.IsNullOrEmpty(Estado) ? 0 : int.Parse(Estado);
            items.Pais = int.Parse(Pais);
            items.Sector = string.IsNullOrEmpty(Sector) ? 0 : int.Parse(Sector);
            items.Telefono = Telefono;
            items.Extension = Extension;
            items.PaginaWeb = PaginaWeb;
            items.IdConfiguracion = int.Parse(IdConfiguracion);
            items.IdUDN = string.IsNullOrEmpty(IdUDN) ? n.usuarios.Seleccionar_UDNUsuario(Session["IdUsuario"].ToString()) : int.Parse(IdUDN);
            items.IdUsuario = int.Parse(Session["IdUsuario"].ToString());
            items.Tipo = string.IsNullOrEmpty(Tipo) ? 2 : int.Parse(Tipo);

            return Json(n.empresas.Agregar_Completo(items), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarParcial(string Nombre, string RFC, string TipoEmpresa, string IdConfiguracion, string IdUDN, string IdUsuario)
        {
            Empresas items = new Empresas();
            items.Nombre = Nombre;
            items.RFC = RFC.ToUpper();
            items.InternaExterna = int.Parse(TipoEmpresa);
            items.IdConfiguracion = int.Parse(IdConfiguracion);
            items.IdUDN = string.IsNullOrEmpty(IdUDN) ? 0 : int.Parse(IdUDN);
            items.IdUsuario = string.IsNullOrEmpty(IdUsuario) ? 0 : int.Parse(IdUsuario);

            return Json(n.empresas.Agregar_Parcial(items), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarAdicional(string idempresa, string idfuente, string idtipo, string idindustria)
        {
            if (n.empresas.Seleccionar_CreadorEmpresa(idempresa) == Session["IdUsuario"].ToString() 
                || n.compartirempresa.Validar_SiUsuarioPuedeModificar(idempresa, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                return Json(n.empresasdetalle.Agregar_Modificar(idempresa, idfuente, idtipo, idindustria), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GuardarDireccionAdicional(string idempresa, string idtipodireccion, string direccion, string cp, string ciudad, string idpais)
        {
            if (n.empresas.Seleccionar_CreadorEmpresa(idempresa) == Session["IdUsuario"].ToString() 
                || n.compartirempresa.Validar_SiUsuarioPuedeModificar(idempresa, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                n.empresasdirecciones.Agregar_Registro(idempresa, idtipodireccion, direccion, cp, ciudad, idpais);
                return Json(n.empresasdirecciones.Seleccionar_DireccionesAdicionalesConNombreDePaisPorIdEmpresa(idempresa), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SeleccionarDireccionAdicionalPorId(string iddireccionadicional)
        {
            return Json(n.empresasdirecciones.Seleccionar_DireccionAdicionalPorId(iddireccionadicional), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarDireccionAdicionalModificada(string idempresa, string idtipodireccion, string direccion, string cp, string ciudad, string idpais, string iddireccionadicional)
        {
            if (n.empresas.Seleccionar_CreadorEmpresa(idempresa) == Session["IdUsuario"].ToString() 
                || n.compartirempresa.Validar_SiUsuarioPuedeModificar(idempresa, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                n.empresasdirecciones.Modificar_Registro(idtipodireccion, direccion, cp, ciudad, idpais, iddireccionadicional);
                return Json(n.empresasdirecciones.Seleccionar_DireccionesAdicionalesConNombreDePaisPorIdEmpresa(idempresa), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Cambiar(string Nombre, string Telefono, string Correo, string PaginaWeb, string Direccion, string CP, string Ciudad, string Estado,
        string Prospecto, string Cliente, string Competidor, string Sector, string Id, string RFC, string UDN)
        {
            Empresas items = new Empresas();
            items.Nombre = Nombre;
            items.Telefono = Telefono;
            items.Correo = Correo;
            items.PaginaWeb = PaginaWeb;
            items.Direccion = Direccion;
            items.Ciudad = Ciudad;
            items.CP = CP;
            items.Estado = int.Parse(Estado);
            items.Prospecto = Prospecto == "on" ? true : false;
            items.Cliente = Cliente == "on" ? true : false;
            items.Competidor = Competidor == "on" ? true : false;
            items.Sector = int.Parse(Sector);
            items.Id = int.Parse(Id);
            items.RFC = RFC.ToUpper();
            items.IdUDN = string.IsNullOrEmpty(UDN) ? 0 : int.Parse(UDN);

            int salida = 0;
            if (n.empresas.Modificar_Registro(items))
                salida = 1;
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Guarda una nueva empresa
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="Telefono"></param>
        /// <param name="Correo"></param>
        /// <param name="PaginaWeb"></param>
        /// <param name="Direccion"></param>
        /// <param name="Ciudad"></param>
        /// <param name="Estado"></param>
        /// <param name="Prospecto"></param>
        /// <param name="Cliente"></param>
        /// <param name="Competidor"></param>
        /// <param name="Sector"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Agregar(string Nombre, string Telefono)
        {
            Empresas items = new Empresas();
            items.Nombre = Nombre;
            items.Telefono = Telefono;

            if (n.empresas.Agregar_Registro(items) > 0)
            {

            }


            return View("Alta");
        }

        /// <summary>
        /// Cambia los datos de una empresa
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="Telefono"></param>
        /// <param name="Correo"></param>
        /// <param name="PaginaWeb"></param>
        /// <param name="Direccion"></param>
        /// <param name="Ciudad"></param>
        /// <param name="Estado"></param>
        /// <param name="Prospecto"></param>
        /// <param name="Cliente"></param>
        /// <param name="Competidor"></param>
        /// <param name="Sector"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Modificar(string Nombre, string Telefono, string Extension, string Correo, string PaginaWeb, string Direccion, string CP, string Ciudad, string hCiudad, string Estado,
        string Prospecto, string Cliente, string Competidor, string Sector, string Id, string RFC, string optionsRadios, string Pais, string UDN, string Activo, string tipoemp)
        {
            UsuariosRoles usuariorolsesion = (UsuariosRoles)Session["GranSesion"];

            Empresas items = new Empresas();
            items.Nombre = Nombre;
            items.Telefono = Telefono;
            items.Extension = Extension;
            items.Correo = Correo;
            items.PaginaWeb = PaginaWeb;
            items.Direccion = Direccion;
            items.Ciudad = string.IsNullOrEmpty(Ciudad) ? hCiudad : Ciudad;
            items.CP = CP;
            items.Estado = string.IsNullOrEmpty(Estado) ? 0 : int.Parse(Estado);
            items.Pais = string.IsNullOrEmpty(Pais) ? 0 : int.Parse(Pais);
            items.Prospecto = Prospecto == "on" ? true : false;
            items.Cliente = Cliente == "on" ? true : false;
            items.Competidor = Competidor == "on" ? true : false;
            items.Sector = string.IsNullOrEmpty(Sector) ? 0 : int.Parse(Sector);
            items.Id = int.Parse(Id);
            items.RFC = RFC.ToUpper();
            if (!string.IsNullOrEmpty(optionsRadios))
            {
                items.InternaExterna = int.Parse(optionsRadios);
            }
                if (Session["IdRol"].ToString() == "7")
                items.IdUDN = 6;
            else
                items.IdUDN = string.IsNullOrEmpty(UDN) ? 0 : int.Parse(UDN);
            items.IdConfiguracion = int.Parse(Session["IdConfiguracion"].ToString());
            items.Activo = Activo == "1" || Activo == "True" || Activo == "on" || Activo == null ? true : false;
            items.Tipo = string.IsNullOrEmpty(tipoemp) ? 0 : int.Parse(tipoemp);

            //if (Session["IdConfiguracion"].ToString() == "3")
            //{
            //    n.empresas.Modificar_Registro(items);
            //}
            if (Session["IdConfiguracion"].ToString() == "2")
            {
                if (n.empresas.Seleccionar_CreadorEmpresa(Id) == Session["IdUsuario"].ToString() 
                    || n.compartirempresa.Validar_SiUsuarioPuedeModificar(Id, Session["IdUsuario"].ToString())
                    || Session["IdUsuario"].ToString() == "2")
                {
                    n.empresas.Modificar_Registro(items);
                    ViewData["Mensaje"] = "<div class='alert alert-success' role='alert'>Se guardó exitosamente el registro modificado</div>";
                }
                else
                {
                    ViewData["Mensaje"] = "<div class='alert alert-warning' role'alert'>No se modificó el registro, sólo puede hacerlo el que lo creó, solicítele cualquier cambio. <a href='#' onclick='SolicitarCambios(" + n.empresas.Seleccionar_CreadorEmpresa(Id) + "," + Id + ");'>Solicitar</a></div>";
                }
            }

            if (usuariorolsesion.Roles.Id.ToString() == "7")
                ViewBag.Empresas = n.empresas.Seleccionar_Todas(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString());
            else if ((usuariorolsesion.Roles.Id == 3 || usuariorolsesion.Roles.Id == 5 || usuariorolsesion.Roles.Id == 6 || usuariorolsesion.Roles.Id == 7) && usuariorolsesion.Configuracion.Id == 2)
                ViewBag.Empresas = n.empresas.Seleccionar_Todas(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString());
            else
                ViewBag.Empresas = n.empresas.Seleccionar_Todas(usuariorolsesion.Configuracion.Id.ToString(), "");

            if (n.empresas.Seleccionar_CompartidasPorUsuario(usuariorolsesion.Usuarios.Id.ToString()).ToList().Count > 0)
                ViewBag.EmpresasCompartidas = n.empresas.Seleccionar_CompartidasPorUsuario(usuariorolsesion.Usuarios.Id.ToString());
            else
                ViewBag.EmpresasCompartidas = null;

            return View("Index");
        }

        //Procesos Json

        public JsonResult EnviarCorreoCambios(string idusuario, string idempresa, string mensaje)
        {
            var idusuariocreador = n.empresas.Seleccionar_CreadorEmpresa(idempresa);
            var correocreador = n.usuarios.Seleccionar_CorreoReponsable(idusuariocreador);
            var nombreempresa = n.empresas.Seleccionar_NombreEmpresa(idempresa);
            Correo correo = new Correo();
            mensaje += "<p>Para el cliente: " + nombreempresa;
            mensaje += "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
            "<h4><a href='http://200.23.87.202:1052/Home/'>http://200.23.87.202:1052/Home/</a></h4>";
            var regresar = 0;
            if (!correo.EnviarCorreo(correocreador, "Solicitud de Cambios/Agregado/Compartir en CRM", "Solicitud de Cambios en CRM", mensaje))
            {
                regresar = 1;
            }

            return Json(regresar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompartirRegistro(string idempresa, string idusuario)
        {
            int regresar = 0;
            if (n.empresas.Seleccionar_CreadorEmpresa(idempresa) == Session["IdUsuario"].ToString() 
                || n.compartirempresa.Validar_SiUsuarioPuedeModificar(idempresa, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                regresar = n.compartirempresa.Agregar_Registro(idempresa, idusuario);
            }
            return Json(regresar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmpresasUsuariosCompartidos(string idempresa)
        {
            return Json(n.compartirempresa.Seleccionar_UsuariosCompartidos(idempresa), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarUsuarioCompartido(string idempresa, string idusuario)
        {
            int regresar = 0;
            if (n.empresas.Seleccionar_CreadorEmpresa(idempresa) == Session["IdUsuario"].ToString() 
                || n.compartirempresa.Validar_SiUsuarioPuedeModificar(idempresa, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                regresar = n.compartirempresa.Eliminar_UsuarioCompartido(idempresa, idusuario);
            }
            return Json(regresar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConteosFiltros()
        {            
            return Json(n.empresas.Seleccionar_Todas_Filtrado_Conteos(), JsonRequestBehavior.AllowGet);
        }

    }
}
