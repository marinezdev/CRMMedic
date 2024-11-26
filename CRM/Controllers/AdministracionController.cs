using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CRM.Models.Models;
using CRM.Models.Repository;
using CRM.Utilerias;
using CRM.Utilerias.WSCorreo;

namespace CRM.Controllers
{
    [SessionTimeOut]
    public class AdministracionController : BaseController
    {
        private UsuariosRoles usuariorolsesion;

        // GET: Permisos
        public ActionResult Permisos()
        {
            return View("Permisos");
        }

        // GET: Usuarios
        public ActionResult Usuarios()
        {
            ViewBag.Roles = n.roles.Seleccionar_Registros();
            ViewBag.ConfiguracionEmpresas = n.configuracion.Seleccionar_Registros();
            ViewBag.UDN = n.udn.Seleccionar_Registros();
            return View(n.usuarios.Seleccionar_Todos());           
        }

        public ActionResult Clasificacion()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Empresas = n.configuracion.Seleccionar_Registros();
            dynamic modeloClas = new ExpandoObject();
            //ViewBag.Clasificacion = clar.SeleccionarParaAdministracion();
            //ViewBag.SubClasificacion = scr.SeleccionarParaAdministracion();
            //ViewBag.Etiquetas = cscer.SeleccionarParaAdministracion();
            modeloClas.Clasificacion = n.clasificacion.Seleccionar_ParaAdministracion();
            modeloClas.SubClasificacion = n.subclasificacion.Seleccionar_ParaAdministracion();
            modeloClas.Etiquetas = n.classsubclassetiquetas.Seleccionar_ParaAdministracion();
            return View(modeloClas);
        }

        public ActionResult Configuracion()
        {
            return View(n.configuracion.Seleccionar_Todo());
        }

        public ActionResult Gerentes()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            //if (usuariorolsesion.Configuracion.Id == 2)
            ViewBag.Gerentes = n.usuarios.Seleccionar_UsuariosGerentes(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString(), usuariorolsesion.Roles.Id.ToString());
            //else if (usuariorolsesion.Configuracion.Id == 3)
            //    ViewBag.Gerentes = n.usuarios.Seleccionar_UsuariosGerentesSABE(usuariorolsesion.Usuarios.Id.ToString());
            return View();
        }

        public ActionResult UDN()
        {
            return View(n.udn.Seleccion_Registros());
        }

        /// <summary>
        /// Multiempresa
        /// </summary>
        /// <returns></returns>
        public ActionResult Multi()
        {
            //Proceso de pruebas
            List<Multi2> ee = new List<Multi2>();
            Multi2 e = new Multi2();
            e.Multi.IdConfiguracion = 2;
            e.Multi.IdUsuario = 2;
            e.Usuarios.Nombre = "Juan Perez";
            e.Configuracion.Nombre = "Médico de Prueba";
            ee.Add(e);
            return View(ee);
        }

        public ActionResult UsuariosR()
        {
            usuariorolsesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.Usuarios = n.usuarios.Seleccionar_UsuariosResponsables(usuariorolsesion.Configuracion.Id.ToString());
            return View();
        }

        public ActionResult UsuariosRAlta()
        {
            return View();
        }

        public ActionResult UsuariosREdicion(string IdUsuario)
        {
            ViewBag.DetalleUsuario = n.usuarios.Seleccionar_PorId(IdUsuario);
            if (ViewBag.DetalleUsuario.Usuarios.InternoExterno == 1)
            {
                ViewBag.Empresas = n.empresas.Seleccionar_Registros("3", "5", IdUsuario);
            }
            else 
            {
                ViewBag.Empresas = n.empresasproveedores.Seleccionar_Registros();
            }
            
            ViewBag.PersonasEnAsuntos = n.oportunidades.Seleccionar_OportunidadesPorIdResponsable(IdUsuario);
            return View();
        }

        public ActionResult Catalogos()
        {
            return View();
        }

        public ActionResult Empresas()
        {
            ViewBag.EmpresasProveedores = n.empresasproveedores.Seleccionar_Registros();
            return View();
        }

        public ActionResult Extensiones()
        {
            return View(n.documentosasaetipoarchivo.Seleccionar_Registros());
        }

        public ActionResult DocsSubClas()
        {
            ViewBag.SubClas = n.documentosasaesubclasificaciones.Seleccionar_Registros();
            return View();
        }

        public ActionResult Menu()
        {
            ViewBag.Registros = n.menu.Seleccionar_Registros();
            ViewBag.ConfiguracionEmpresas = n.configuracion.Seleccionar_Registros();
            return View();
        }

        public ActionResult MenuRoles()
        {
            ViewBag.Registros = n.menu.Seleccionar_MenuRol();
            return View();
        }

        [HttpPost]
        public ActionResult DocsSubClasGuardar(string nombre, int clasificacion)
        {
            Subclasificacion items = new Subclasificacion();
            items.Nombre = nombre;
            items.IdClasificacion = clasificacion;
            n.documentosasaesubclasificaciones.Agregar_Registro(items);
            ViewBag.SubClas = n.documentosasaesubclasificaciones.Seleccionar_Registros();
            return View("DocsSubClas");
        }

        /// <summary>
        /// Obtiene el detalle del usuario
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult SeleccionarPorId(string Id)
        {
            return Json(n.usuarios.Seleccionar_PorId(Id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Agrega un nuevo usuario
        /// </summary>
        /// <param name="aNombre"></param>
        /// <param name="aClave"></param>
        /// <param name="aContraseña"></param>
        /// <param name="aActivo"></param>
        /// <param name="aRol"></param>
        /// <returns></returns>
        public JsonResult AgregarUsuario(string aNombre, string aApellidoPaterno, string aApellidoMaterno, string aCorreo, string aClave, string aContraseña, string aRol, string IdConfiguracion, string aIdUDN)
        {
            List<UsuariosRoles> datos = new List<UsuariosRoles>();
            int idusuario = n.usuarios.Agregar_Registro(aNombre, aApellidoPaterno, aApellidoMaterno, aCorreo, aClave, aContraseña);
            if (n.usuariosroles.Agregar_Registro(idusuario.ToString(), aRol))
            {
                //Agregar a Usuarios Configuracion
                n.usuarioconfiguracion.Agregar_Registro(idusuario.ToString(), IdConfiguracion);
                datos = n.usuarios.Seleccionar_Todos().ToList();
            }
            else
                datos = null;
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PreguardadoResponsables(string IdConfiguracion, string Nombre, string Correo)
        {
            return Json(n.usuarios.Seleccionar_UsuariosPreGuardado(IdConfiguracion, Nombre, Correo), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarResponsable(string Nombre, string Correo, string Telefono, string Celular, string Empresa, string Direccion, string Ciudad, string Notas, string Rol, string IdConfiguracion, string FisicaMoral, string RFC, string InternoExterno)
        {
            var respuesta = 0;
            var idusuario = n.usuarios.Agregar_Responsable(Nombre, Correo);
            if (idusuario > 0)
            {
                
                n.usuarios.Agregar_Detalle(idusuario.ToString(), Telefono, Celular, Empresa, Direccion, Ciudad, Notas, FisicaMoral, RFC, InternoExterno); //Detalle del usuario
                n.usuarioconfiguracion.Agregar_Registro(idusuario.ToString(), IdConfiguracion);                         //Configuracion
                n.usuariosroles.Agregar_Registro(idusuario.ToString(), "5");                                            //Roles
                n.empresasusuarios.Agregar_Registro(Empresa, idusuario.ToString(), "false");                            //EmpresasUsuarios
                var correoresponsable = n.usuarios.Seleccionar_CorreoReponsable(idusuario.ToString());
                var titulocorreo = Titulos.TDashboard(Session["IdConfiguracion"].ToString());
                //Enviar correo de alta al sistema                    
                correo.EnviarCorreoAltaResponsable(Nombre, correoresponsable, titulocorreo, Session["IdConfiguracion"].ToString());
                respuesta = 1;                    
            }

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarResponsableModificado(string Nombre, string Correo, string Telefono, string Celular, string Empresa, string Direccion, string Ciudad, string Notas, string IdUsuario, string FisicaMoral, string RFC, string InternoExterno)
        {
            n.usuarios.Modificar_Responsable(Nombre, Correo, IdUsuario);
            var obtenido = n.usuarios.Modificar_Detalle(Telefono,Celular,Empresa,Direccion,Ciudad,Notas,IdUsuario, FisicaMoral, RFC, InternoExterno);
            if (!string.IsNullOrEmpty(Empresa))
                n.empresasusuarios.Modificar_EmpresaPorUsuario(IdUsuario, Empresa);
            return Json(obtenido, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarUDNPorId(string Id)
        {
            return Json(n.udn.Seleccionar_PorId(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarUDN(string aNombre, string aActivo)
        {
            UDN items = new UDN();
            List<UDN> datos = new List<UDN>();
            items.Nombre = aNombre;
            items.Activo =aActivo == "on" ? true : false;
            if (n.udn.Agregar_Registro(items) > 0)
            {
                datos = n.udn.Seleccion_Registros().ToList();                
            }
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarUDN(string nombre, string activo, string id)
        {
            var exitoso = 0;
            if (n.udn.Modificar_Registro(nombre, activo, id) > 0)
            {
                exitoso = 1;
            }
            return Json(exitoso, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Agrega un Contacto como un usuario del sistema
        /// </summary>
        /// <param name="aNombre"></param>
        /// <param name="aApellidoPaterno"></param>
        /// <param name="aApellidoMaterno"></param>
        /// <param name="aClave"></param>
        /// <param name="aContraseña"></param>
        /// <param name="aActivo"></param>
        /// <param name="aRol"></param>
        /// <returns></returns>
        public JsonResult AgregarUsuarioContacto(string aNombre, string aApellidoPaterno, string aApellidoMaterno, string aCorreo, string aClave, string aContraseña, string eEmpresa, string aRol, string IdConfiguracion)
        {
            int obtenido = n.usuarios.Agregar_Registro(aNombre, aApellidoPaterno, aApellidoMaterno, aCorreo, aClave, aContraseña);
            if (obtenido > 0)
            {
                n.usuarioconfiguracion.Agregar_Registro(obtenido.ToString(), IdConfiguracion);
                n.usuariosroles.Agregar_Registro(obtenido.ToString(), aRol);
            }
            return Json(obtenido, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Modifica los datos del usuario
        /// </summary>
        /// <param name="eNombre"></param>
        /// <param name="eClave"></param>
        /// <param name="eContraseña"></param>
        /// <param name="eActivo"></param>
        /// <param name="eRol"></param>
        /// <param name="eId"></param>
        /// <returns></returns>
        public JsonResult ModificarUsuario(string eNombre, string eApellidoPaterno, string eApellidoMaterno, string eCorreo,  string eClave, string eContraseña, string eActivo, string eRol, string eId, string eArchivosPermiso, string eClasSubClasPermiso, string eIdUDN, string eEmpresa)
        {
            List<UsuariosRoles> datos = new List<UsuariosRoles>();
            if (n.usuarios.Modificar_Registro(eNombre, eApellidoPaterno, eApellidoMaterno, eCorreo, eClave, eContraseña, eActivo, eRol, eId, eArchivosPermiso, eClasSubClasPermiso, eIdUDN) > 0)
            {
                //Modificar la empresa de la configuracion
                n.usuarioconfiguracion.Modificar_Configuracion(eId, eEmpresa);
                datos = n.usuarios.Seleccionar_Todos().ToList();
            }
            else
                datos = null;
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        // GET: Roles
        public ActionResult Roles()
        {
            return View(n.roles.Seleccionar_Registros().ToList());
        }

        //Gerentes
        public JsonResult AgregarGerente(string aNombre,string aClave, string aTelefono, string aConfiguracion)
        {
            //Agregado a usuarios
            int obtenido = n.usuarios.Agregar_Registro(aNombre, "", "", aClave, aClave, Funciones.ClaveSalida());
            if (obtenido > 0)
            {
                //Agregar telefono a usuario detalle
                n.usuarios.Agregar_Detalle(obtenido.ToString(), aTelefono, "", "", "", "", "","0", "", "");
                //Agregado a usuario configuracion
                n.usuarioconfiguracion.Agregar_Registro(obtenido.ToString(), aConfiguracion);
                //Agregado usuario rol
                n.usuariosroles.Agregar_Registro(obtenido.ToString(), "3");
                //Enviar correo de acceso para el nuevo gerente
                var titulocorreo = Titulos.TDashboard(Session["IdConfiguracion"].ToString());
                var correogerente = n.usuarios.Seleccionar_ClaveGerente(obtenido.ToString());
                //Enviar correo de alta al sistema                
                correo.EnviarCorreoAltaGerente(aNombre, correogerente, titulocorreo, Session["IdConfiguracion"].ToString());
                //Agregar las empresas para el gerente
                n.empresas.Agregar_EmpresasAGerente(obtenido.ToString(), aConfiguracion);
            }
            return Json(obtenido, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarGerentePorId(string id)
        {
            return Json(n.usuarios.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarGerente(string eNombre, string eClave, string eActivo, string eTelefono, string eId)
        {
            var resultado = 0;
            if (n.usuarios.Modificar_Gerente(eNombre, eClave, eActivo, eId) > 0)
            {
                if (n.usuarios.Modificar_Detalle(eTelefono, "", "0", "", "", "", eId, "", "", "") > 0)
                    resultado = 1;
                else
                {
                    n.usuarios.Agregar_Detalle(eId, eTelefono, "", "0", "", "", "","0", "", "");
                    resultado = 1;
                }
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarEmpresasPorGerente(string id)
        {
            return Json(n.empresasusuarios.Seleccionar_EmpresasPorUsuario(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarEmpresasPorGerente(string[] listado, string[] listado2, string idusuario)
        {
            var resultado = 0;

            //primero, los no seleccionados
            foreach (var itm in listado2)
            {
                n.empresasusuarios.Modificar_EmpresasPorUsuario(idusuario, itm, "false");
            }


            //segundo, los seleccionados
            foreach (var itm in listado)
            {
                n.empresasusuarios.Modificar_EmpresasPorUsuario(idusuario, itm, "true");
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Agrega un nuevo rol
        /// </summary>
        /// <param name="aNombre"></param>
        /// <param name="aActivo"></param>
        /// <returns></returns>
        public JsonResult AgregarRol(string aNombre, string aObservaciones, string aActivo, string aPaginaInicio)
        {
            List<Roles> datos = new List<Roles>();
            if (n.roles.Agregar_Registro(aNombre, aObservaciones, aActivo, aPaginaInicio) > 0)
                datos = n.roles.Seleccionar_Registros().ToList();
            else
                datos = null;
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarRolPorId(string id)
        {
            return Json(n.roles.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarRol(string nombre, string observaciones, string activo, string paginainicio, string id)
        {
            return Json(n.roles.Modificar_Registro(nombre, observaciones, activo, paginainicio, id), JsonRequestBehavior.AllowGet);
        }

        //Clasificacion
        public JsonResult AgregarClasificacion(string nombre, string empresa)
        {
            var resultado = new Resultado();
            if (nombre != string.Empty && empresa != string.Empty)
            {
                if (n.clasificacion.Agregar_Registro(nombre, empresa) > 0)
                {
                    resultado.Ok = true;
                    resultado.Mensaje = "Agregado exitosamente";
                }
            }
            else
            {
                resultado.Ok = false;
                resultado.Mensaje = "Algo falló en la inserción, revise";
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SeleccionarClasificacionEditar(string id)
        {
            return Json(n.clasificacion.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ModificarClasificacion(string nombre, string id)
        {
            var resultado = new Resultado();
            if (nombre != string.Empty && id != string.Empty)
            {
                if (n.clasificacion.Modificar_Registro(nombre, id) > 0)
                {
                    resultado.Ok = true;
                    resultado.Mensaje = "Modificado exitosamente";
                }
            }
            else
            {
                resultado.Ok = false;
                resultado.Mensaje = "Algo falló en la modificación, revise";
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        //Subclasificacion

        [HttpGet]
        public JsonResult AgregarSubClasificacion(string nombre, string clasificacion)
        {
            var resultado = new Resultado();
            if (nombre != string.Empty && clasificacion != string.Empty)
            {
                if (n.subclasificacion.Agregar_Registro(nombre, clasificacion) >= 1)
                {
                    resultado.Ok = true;
                    resultado.Mensaje = "Todo bien";
                }
                else
                {
                    resultado.Ok = false;
                    resultado.Mensaje = "Algo falló, revise";
                }
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SeleccionarSubClasificacionEditar(string id)
        {
            return Json(n.subclasificacion.Seleccionar_PorIdEditar(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ModificarSubClasificacion(string nombre, string idclasificacion, string id)
        {
            var resultado = new Resultado();
            if (nombre != string.Empty && idclasificacion != string.Empty && id != string.Empty)
            {
                if (n.subclasificacion.Modificar_Registro(nombre, idclasificacion, id) > 0)
                {
                    resultado.Ok = true;
                    resultado.Mensaje = "Modificado exitosamente";
                }
            }
            else
            {
                resultado.Ok = false;
                resultado.Mensaje = "Algo falló en la modificación, revise";
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        //Etiquetas

        public JsonResult SeleccionarEtiquetas(string clasificacion, string subclasificacion)
        {
            return Json(n.classsubclassetiquetas.Seleccionar_PorIdClasificacionIdSubClasificacionParaEdicion(clasificacion, subclasificacion), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarEtiquetas(string etiqueta1, string etiqueta2, string etiqueta3, string etiqueta4, string idclasificacion, string idsubclasificacion)
        {
            var resultado = new Resultado();
            if (idclasificacion != string.Empty && idsubclasificacion != string.Empty)
            {
                if (n.classsubclassetiquetas.Modificar_Registro(etiqueta1, etiqueta2, etiqueta3, etiqueta4, idclasificacion, idsubclasificacion) > 0)
                {
                    resultado.Ok = true;
                    resultado.Mensaje = "Modificado exitosamente";
                }
            }
            else
            {
                resultado.Ok = false;
                resultado.Mensaje = "Algo falló en la modificación, revise";
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        //Configuracion
        [HttpGet]
        public JsonResult SeleccionarConfiguracionEditar(string id)
        {
            return Json(n.configuracion.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AgregarConfiguracion(string nombre, string logo, string intermedio)
        {
            var resultado = new Resultado();
            if (nombre != string.Empty && logo != string.Empty && intermedio != string.Empty)
            {
                if (n.configuracion.Agregar_Registro(nombre, logo, intermedio) > 0)
                {
                    resultado.Ok = true;
                    resultado.Mensaje = "Todo bien";
                }
                else
                {
                    resultado.Ok = false;
                    resultado.Mensaje = "Datos incorrectos";
                }
            }
            else
            {
                resultado.Ok = false;
                resultado.Mensaje = "Algo falló, revise";
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarConfiguracion(string nombre, string logo, string intermedio, string id)
        {
            var resultado = new Resultado();
            if (nombre != string.Empty && logo != string.Empty && intermedio != string.Empty && id != string.Empty)
            {
                if (n.configuracion.Modificar_Registro(nombre, logo, intermedio, id) > 0)
                {
                    resultado.Ok = true;
                    resultado.Mensaje = "Todo bien";
                }
                else
                {
                    resultado.Ok = false;
                    resultado.Mensaje = "Datos incorrectos";
                }
            }
            else
            {
                resultado.Ok = false;
                resultado.Mensaje = "Algo falló, revise";
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        //Personas

        public JsonResult CambiarCorreo(string idusuario, string correo)
        {
            Correo correo2 = new Correo();
            string Nombre = n.usuarios.Seleccionar_Nombre(idusuario);
            string correoresponsable = n.usuarios.Seleccionar_CorreoReponsable(idusuario);
            //Enviar correo de modificación en el sistema
            correo2.EnviarCorreoModificacionResponsable(Nombre, correoresponsable, "Cambio de acceso, modificación de correo");
            n.usuarios.Agregar_CorreoAHistorial(idusuario, correoresponsable);
            return Json(n.usuarios.Cambiar_CorreoResponsable(idusuario, correo), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarCorreosCambiados(string idusuario, string correo)
        {
            return Json(n.usuarios.Selecccionar_CorreosCambiados(idusuario), JsonRequestBehavior.AllowGet);
        }

        //Catalogos

        public JsonResult SeleccionarTabla(string tabla)
        {
            return Json(Models.Catalogos.Seleccionar(tabla), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TablasSeleccionarPorId(string tabla, string id)
        {
            return Json(Models.Catalogos.SeleccionarPorId(tabla,"Id",id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TablasModificarRegistro(string tabla, string id, string nombre) 
        {
            Models.Catalogos.ModificarNombre(tabla, id, nombre);
            return Json(Models.Catalogos.Seleccionar(tabla), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TablasAgregarNuevo(string tabla, string nombre)
        {
            Models.Catalogos.Guardar(tabla, "Nombre", nombre);
            return Json(Models.Catalogos.Seleccionar(tabla), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarOportunidadSiUSuarioEsPropietarioOTienePermisoParaModificar(string idoportunidad, string idusuario)
        {
            //var validacion = new ValidacionPropietarioPermiso().OportunidadValidarPropietarioPermiso(idoportunidad, idusuario);
            //if (n.usuarios.Seleccionar_CreadorOportunidad(idoportunidad) == idusuario
            //    || n.compartiroportunidades.Validar_SiUsuarioPuedeModificar(idoportunidad, idusuario)
            //    || idusuario == "2")
            if (new ValidacionPropietarioPermiso().OportunidadValidarPropietarioPermiso(idoportunidad, idusuario))
                return Json(1, JsonRequestBehavior.AllowGet);
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarEmpresaSiUSuarioEsPropietarioOTienePermisoParaModificar(string idempresa, string idusuario)
        {
            var validacion = new ValidacionPropietarioPermiso().EmpresaValidarPropietarioPermiso(idempresa, idusuario);
            if (n.empresas.Seleccionar_CreadorEmpresa(idempresa) == idusuario
                || n.compartirempresa.Validar_SiUsuarioPuedeModificar(idempresa, idusuario)
                || idusuario == "2")
                return Json(1, JsonRequestBehavior.AllowGet);
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarContactosSiUSuarioEsPropietarioOTienePermisoParaModificar(string idcontacto, string idusuario)
        {
            var validacion = new ValidacionPropietarioPermiso().ContactoValidarPropietarioPermiso(idcontacto, idusuario);
            if (n.contactos.Seleccionar_CreadorContacto(idcontacto) == idusuario
                || n.compartircontactos.Validar_SiUsuarioPuedeModificar(idcontacto, idusuario)
                || idusuario == "2")
                return Json(1, JsonRequestBehavior.AllowGet);
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }

        //Empresas Proveedores
        public JsonResult SeleccionarEmpresasProveedores()
        {
            return Json(n.empresasproveedores.Seleccionar_Registros(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarEmpresaProveedorPorId(string id)
        { 
            return Json(n.empresasproveedores.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarEmpresaProveedor(string nombre, string rfc, string telefono)
        {
            EmpresasProveedores items = new EmpresasProveedores();
            items.Nombre = nombre;
            items.RFC = rfc;
            items.Telefono = telefono;
            return Json(n.empresasproveedores.Agregar_Registro(items), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarEmpresaProveedor(string nombre, string rfc, string telefono, string id)
        {
            EmpresasProveedores items = new EmpresasProveedores();
            items.Nombre = nombre;
            items.RFC = rfc;
            items.Telefono = telefono;
            items.Id = int.Parse(id);
            return Json(n.empresasproveedores.Modificar_Registro(items), JsonRequestBehavior.AllowGet);
        }

        //Guardar extensiones de archivos

        public JsonResult ExtensionesSeleccionarPorId(string id)
        {
            return Json(n.documentosasaetipoarchivo.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExtensionesGuardarNuevo(string extensiones, string tamaño)
        {
            Models.Models.DocumentosASAETipoArchivo items = new Models.Models.DocumentosASAETipoArchivo();
            items.Extensiones = extensiones;
            items.TamMaxPermitido = int.Parse(tamaño);
            items.Permitir = true;
            n.documentosasaetipoarchivo.Agregar_Registro(items);
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExtensionesGuardarModificado(string extensiones, string tamaño, string permitido, string id)
        {
            Models.Models.DocumentosASAETipoArchivo items = new Models.Models.DocumentosASAETipoArchivo();
            items.Extensiones = extensiones;
            items.TamMaxPermitido = int.Parse(tamaño);
            items.Permitir = permitido == "1" ? true : false;
            items.Id = int.Parse(id);
            n.documentosasaetipoarchivo.Modificar_Registro(items);
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        //DocumentosASAE

        public JsonResult SeleccionarDocumentosASAESubClasificacionPorId(string id)
        {
            return Json(n.documentosasaesubclasificaciones.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarDocumentosASAEModificacionSubClasificacion(string nombre, int clasificacion, int id)
        {
            Subclasificacion items = new Subclasificacion();
            items.Nombre = nombre;
            items.IdClasificacion = clasificacion;
            items.Id = id;
            return Json(n.documentosasaesubclasificaciones.Modificar_Registro(items), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarDocumentosASAESubClasificacion(string nombre, int clasificacion)
        {
            Subclasificacion items = new Subclasificacion();
            items.Nombre = nombre;
            items.IdClasificacion = clasificacion;
            n.documentosasaesubclasificaciones.Agregar_Registro(items);
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarDocumentosASAESubClasificacion(string id)
        {
            return Json(n.documentosasaesubclasificaciones.Eliminar_Registro(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarDocumentosASAEClasificacion(string id)
        {
            return Json(n.documentosasaesubclasificaciones.Eliminar_Clasificacion(id), JsonRequestBehavior.AllowGet);
        }

        //Menu
        public JsonResult SeleccionarMenuPorId(string id)
        {
            return Json(n.menu.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarMenu(string idjquery, string ruta, string icono, string nombre, string idconfiguracion, string disponible)
        {
            Menu items = new Menu();
            items.IdJquery = idjquery;
            items.Ruta = ruta;
            items.Icono = icono;
            items.Nombre = nombre;
            items.IdConfiguracion = int.Parse(idconfiguracion);
            items.Disponible = disponible == "1";
            return Json(n.menu.Agregar_Registro(items), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarMenu(string idjquery, string ruta, string icono, string nombre, string idconfiguracion, string disponible, string id)
        {
            Menu items = new Menu();
            items.IdJquery = idjquery;
            items.Ruta = ruta;
            items.Icono = icono;
            items.Nombre = nombre;
            items.IdConfiguracion = int.Parse(idconfiguracion);
            items.Disponible = disponible == "1";
            items.Id = int.Parse(id);
            return Json(n.menu.Modificar_Registro(items), JsonRequestBehavior.AllowGet);
        }

        //MenuRol

        public JsonResult SeleccionarMenuRol()
        {
            return Json(n.menu.Seleccionar_MenuRol(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarMenuRolPorId(string id)
        {
            return Json(n.menu.Seleccionar_MenuRolPorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarMenuRol(string idmenu, string idrol, string accesible)
        {
            MenuRoles items = new MenuRoles();
            items.IdMenu = int.Parse(idmenu);
            items.IdRol = int.Parse(idrol);
            items.Accesible = accesible == "1";
            return Json(n.menu.AgregarMenuRol_registro(items), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarMenuRol(string idmenu, string idrol, string accesible, string id)
        {
            MenuRoles items = new MenuRoles();
            items.IdMenu = int.Parse(idmenu);
            items.IdRol = int.Parse(idrol);
            items.Accesible = accesible == "1";
            items.Id = int.Parse(id);
            return Json(n.menu.Modificar_MenuRol(items), JsonRequestBehavior.AllowGet);
        }
    }

    public class Resultado
    { 
        public bool Ok { get; set; }
        public string Mensaje { get; set; }
    }
}
