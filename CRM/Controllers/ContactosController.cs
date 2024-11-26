using CRM.Models.Models;
using CRM.Models.Repository;
using CRM.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClosedXML.Excel;

namespace CRM.Controllers
{
    [SessionTimeOut]
    [HandleError]
    public class ContactosController : BaseController
    {
        private UsuariosRoles usuariorolsesion;

        public ActionResult NIndex()
        {
            usuariorolsesion = (UsuariosRoles)GranSesion["GranSesion"]; //(UsuariosRoles)Session["GranSesion"];
            return View();
        }

        [HttpPost]
        public JsonResult Contactos_Selecionar_Roles(Contactos contactos)
        {
            contactos.UsuarioAlta = Convert.ToInt32(Session["IdUsuario"].ToString());
            contactos.IdConfiguracion = Convert.ToInt32(Session["IdConfiguracion"].ToString());
            List<ContactosEmpresas> dtcontactos = n.contactos.Contactos_Selecionar_Roles(contactos);
            return Json(dtcontactos);
        }

        [HttpPost]
        public JsonResult Contactos_Compartidos(Contactos contactos)
        {
            contactos.UsuarioAlta = Convert.ToInt32(Session["IdUsuario"].ToString());
            List<ContactosEmpresas> dtcontactos = n.contactos.Contactos_Compartidos_Selecionar(contactos);
            return Json(dtcontactos);
        }











        public ActionResult NNuevoContacto()
        {
            usuariorolsesion = (UsuariosRoles)GranSesion["GranSesion"];
            ViewBag.Empresas = n.empresas.Seleccionar_Registros(Session["IdConfiguracion"].ToString(), Session["IdRol"].ToString(), Session["IdUsuario"].ToString());
            ViewBag.Roles = n.roles.Seleccionar_Registros();
            ViewBag.Area = n.area.Area_Seleccionar();
            ViewBag.Paises = n.paises.Seleccionar_Paises();
            ViewBag.UDN = n.udn.Seleccionar_Registros();
            ViewBag.TipoPersona = n.tipopersona.Seleccionar_Registros();
            ViewBag.Estados = n.estados.Estados_Seleccionar();

            return View();
        }

        [HttpPost]
        public JsonResult Agregar_Area(Area area)
        {
            Area narea = n.area.Area_Agregar(area);
            return Json(narea);
        }

        [HttpPost]
        public JsonResult Area_Seleccionar()
        {
            List<Area> narea = n.area.Area_Seleccionar();
            return Json(narea);
        }

        [HttpPost]
        public JsonResult Empresa_Agregar(Empresas empresas)
        {
            empresas.IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
            empresas.IdConfiguracion = Convert.ToInt32(Session["IdConfiguracion"]);
            Empresas nempresas = n.empresas.Empresa_Agregar(empresas);

            return Json(nempresas);
        }

        [HttpPost]
        public JsonResult Empresas_Seleccionar()
        {
            List<Empresas> nempresas = n.empresas.Seleccionar_Registros(Session["IdConfiguracion"].ToString(), Session["IdRol"].ToString(), Session["IdUsuario"].ToString());
            return Json(nempresas);
        }


        [HttpPost]
        public JsonResult Consulta_DelegacionMunicipio(Estados estados)
        {
            List<Poblaciones> npoblaciones = n.poblaciones.Poblaciones_Seleccionar(estados);
            return Json(npoblaciones);
        }

        [HttpPost]
        public JsonResult Consulta_Colonias(Poblaciones poblaciones)
        {
            List<Colonias> colonias = n.colonias.Colonias_Seleccionar(poblaciones);
            return Json(colonias);
        }

        [HttpPost]
        public JsonResult Consulta_CP(CP cP)
        {
            List<CP> colonias = n.cp.CP_Seleccionar(cP);
            return Json(colonias);
        }

        [HttpPost]
        public JsonResult CP_Busqueda(CP cP)
        {
            List <Direccion> direccion = n.cp.CP_Busqueda(cP);
            return Json(direccion);
        }

        [HttpPost]
        public JsonResult Consulta_Estados()
        {
            List<Estados> estados = n.estados.Estados_Seleccionar();
            return Json(estados);
        }

        [HttpPost]
        public JsonResult Buscar_Contacto(Contactos  contactos)
        {
            contactos.IdConfiguracion = Convert.ToInt32(Session["IdConfiguracion"].ToString());
            Contactos dtcontactos = n.contactos.Contato_Buscar_Registro(contactos);
            return Json(dtcontactos);
        }


        [HttpPost]
        public JsonResult Contato_Buscar_Correo(Contactos contactos)
        {
            List<Contactos> dtcontactos = n.contactos.Contato_Buscar_Correo(contactos);
            return Json(dtcontactos);
        }

        [HttpPost]
        public JsonResult Contato_Buscar(Contactos contactos)
        {
            List<Contactos> dtcontactos = n.contactos.Contato_Buscar(contactos);
            return Json(dtcontactos);
        }

        [HttpPost]
        public JsonResult Agregar_Contacto(Contactos contactos)
        {
            contactos.UsuarioAlta = Convert.ToInt32(Session["IdUsuario"].ToString());
            contactos.IdConfiguracion = Convert.ToInt32(Session["IdConfiguracion"].ToString());
            Contactos dtcontactos = n.contactos.Contactos_Agregar(contactos);
            return Json(dtcontactos);
        }


























        //Acciones

        public ActionResult Index()
        {
            usuariorolsesion = (UsuariosRoles)GranSesion["GranSesion"]; //(UsuariosRoles)Session["GranSesion"];

            //if (usuariorolsesion.Configuracion.Id == 3)
            //{
            //    ViewBag.Contactos = n.contactos.Seleccionar_Todos(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Roles.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString());
            //    ViewBag.ContactosCompartidos = n.contactos.Seleccionar_Compartidos(usuariorolsesion.Usuarios.Id.ToString());
            //}
            if (usuariorolsesion.Configuracion.Id == 2)
            {
                if (usuariorolsesion.Roles.Id != 7) //== 2 || usuariorolsesion.Roles.Id == 3 || usuariorolsesion.Roles.Id == 5)
                {
                    ViewBag.Contactos = n.contactos.Seleccionar_Todos(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Roles.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString());
                }
                else if (usuariorolsesion.Roles.Id == 7)//(usuariorolsesion.Roles.Id == 6 || usuariorolsesion.Roles.Id == 7) //Gerente de proyecto/comisionistas
                {
                    ViewBag.Contactos = n.contactos.Seleccionar_GteProyectoComisionistas(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Roles.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString());
                }
                //else if (usuariorolsesion.Roles.Id == 8) //Redes sociales
                //{
                //    ViewBag.Contactos = n.contactos.Seleccionar_RedesSociales(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Roles.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString());
                //}

                ViewBag.ContactosCompartidos = n.contactos.Seleccionar_Compartidos(usuariorolsesion.Usuarios.Id.ToString());
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(string tipocontacto)
        {
            usuariorolsesion = (UsuariosRoles)GranSesion["GranSesion"];

            if (tipocontacto == "1")
            {
                TempData["Checado"] = 1;
                if (usuariorolsesion.Roles.Id == 2 || usuariorolsesion.Roles.Id == 3 || usuariorolsesion.Roles.Id == 5)
                {
                    ViewBag.Contactos = n.contactos.Seleccionar_TodosPorTipoContacto(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Roles.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString(), "1");
                }
                else if (usuariorolsesion.Roles.Id == 6 || usuariorolsesion.Roles.Id == 7) //Gerente de proyecto/comisionistas
                {
                    ViewBag.Contactos = n.contactos.Seleccionar_GteProyectoComisionistasPorTipoContacto(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Roles.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString(), "1");
                }
                else if (usuariorolsesion.Roles.Id == 8) //Redes sociales
                {
                    ViewBag.Contactos = n.contactos.Seleccionar_RedesSocialesPorTipoContacto(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Roles.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString(), "1");
                }
            }
            else if (tipocontacto == "2")
            {
                TempData["Checado"] = 2;
                if (usuariorolsesion.Roles.Id == 2 || usuariorolsesion.Roles.Id == 3 || usuariorolsesion.Roles.Id == 5)
                {
                    ViewBag.Contactos = n.contactos.Seleccionar_TodosPorTipoContacto(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Roles.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString(), "2");
                }
                else if (usuariorolsesion.Roles.Id == 6 || usuariorolsesion.Roles.Id == 7) //Gerente de proyecto/comisionistas
                {
                    ViewBag.Contactos = n.contactos.Seleccionar_GteProyectoComisionistasPorTipoContacto(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Roles.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString(), "2");
                }
                else if (usuariorolsesion.Roles.Id == 8) //Redes sociales
                {
                    ViewBag.Contactos = n.contactos.Seleccionar_RedesSocialesPorTipoContacto(usuariorolsesion.Configuracion.Id.ToString(), usuariorolsesion.Roles.Id.ToString(), usuariorolsesion.Usuarios.Id.ToString(), "2");
                }
            }

            ViewBag.ContactosCompartidos = n.contactos.Seleccionar_CompartidosPorTipoContacto(usuariorolsesion.Usuarios.Id.ToString(), tipocontacto);

            return View();
        }

        [HttpGet]
        public ActionResult Graficos(string opc)
        {
            ViewBag.Contactos = n.contactos.Seleccionar_Todos_Filtrado(opc);
            return View();
        }

        public ActionResult Alta()
        {
            usuariorolsesion = (UsuariosRoles)GranSesion["GranSesion"];
            // ViewBag.Empresas = er.Seleccionar(usr.Configuracion.Id.ToString(), usr.Roles.Id.ToString(), usr.Usuarios.Id.ToString()).ToList();
            return View();
        }

        public ActionResult Alta2()
        {
            usuariorolsesion = (UsuariosRoles)GranSesion["GranSesion"];
            //ViewBag.Empresas = er.Seleccionar(usr.Configuracion.Id.ToString(), usr.Roles.Id.ToString(), usr.Usuarios.Id.ToString()).ToList();
            ViewBag.Roles = n.roles.Seleccionar_Registros();
            ViewBag.Estados = Funciones.Estados();
            ViewBag.Area = n.area.Seleccionar_Registros();
            ViewBag.Paises = n.paises.Seleccionar_Paises();
            ViewBag.UDN = n.udn.Seleccionar_Registros();
            return View();
        }

        /// <summary>
        /// Contactos de fuentes externas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Alta3(string IdCampaña)
        {
            usuariorolsesion = (UsuariosRoles)GranSesion["GranSesion"];
            //ViewBag.Empresas = er.Seleccionar(usr.Configuracion.Id.ToString(), usr.Roles.Id.ToString(), usr.Usuarios.Id.ToString()).ToList();
            ViewBag.Roles = n.roles.Seleccionar_Registros();
            ViewBag.Estados = Funciones.Estados();
            ViewBag.Area = n.area.Seleccionar_Registros();
            ViewBag.Paises = n.paises.Seleccionar_Paises();
            ViewBag.UDN = n.udn.Seleccionar_Registros();
            ViewBag.IdCampaña = IdCampaña;
            return View();
        }

        public ActionResult Editar(string Id)
        {
            int i = 0;
            bool resultado = int.TryParse(Id, out i);
            if (!resultado)
            {
                return RedirectToAction("Index");
            }
            usuariorolsesion = (UsuariosRoles)GranSesion["GranSesion"];
            //ViewBag.Empresas = er.Seleccionar(usr.Configuracion.Id.ToString(), usr.Roles.Id.ToString(), usr.Usuarios.Id.ToString()).ToList();
            ViewBag.Roles = n.roles.Seleccionar_Registros();
            ViewBag.Estados = Utilerias.Funciones.Estados();
            ViewBag.TipoPersona = n.tipopersona.Seleccionar_Registros();
            //ViewBag.TipoPersona = Utilerias.Funciones.TipoPersona();
            ViewBag.UDN = n.udn.Seleccionar_Registros();
            ViewBag.Paises = n.paises.Seleccionar_Paises();
            ViewBag.Area = n.area.Seleccionar_Registros();
            ViewBag.ResponsablesParaCompartir = n.usuarios.Seleccionar_UsuariosResponsablesASAE("2", usuariorolsesion.Usuarios.Id.ToString());
            ViewBag.ContactoEnOportunidades = n.oportunidades.Seleccionar_ContactoEnOportunidades(Id);
            ViewBag.ContactosEnOportunidadesContadas = n.oportunidades.Seleccionar_ContactoEnOportunidades(Id).Count();
            ViewBag.Marketing = n.marketingcontactos.Seleccionar_PorIdContacto(Id);
            return View();
        }

        /// <summary>
        /// Guarda un nuevo contacto
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="ApellidoPaterno"></param>
        /// <param name="ApellidoMaterno"></param>
        /// <param name="Correo"></param>
        /// <param name="Telefono"></param>
        /// <param name="Celular"></param>
        /// <param name="Direccion"></param>
        /// <param name="Ciudad"></param>
        /// <param name="Estado"></param>
        /// <param name="Cargo"></param>
        /// <param name="FechaCumpleaños"></param>
        /// <param name="Empresa"></param>
        /// <param name="TipoContacto"></param>
        /// <param name="Notas"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Agregar(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string Telefono, string Celular,
        string Direccion, string Ciudad, string Estado, string Cargo, string FechaCumpleaños, string Empresa, string TipoContacto, string Notas, string IdUsuario)
        {
            usuariorolsesion = (UsuariosRoles)GranSesion["GranSesion"];

            Contactos items = new Contactos();
            items.Nombre = Nombre;
            items.ApellidoPaterno = ApellidoPaterno;
            items.ApellidoMaterno = ApellidoMaterno;
            items.Correo = Correo;
            items.Telefono = Telefono;
            items.Celular = Celular;
            items.Direccion = Direccion;
            items.Ciudad = Ciudad;
            items.Estado = int.Parse(Estado);
            items.Cargo = Cargo;
            items.FechaCumpleaños = FechaCumpleaños;
            items.TipoContacto = int.Parse(TipoContacto);
            items.Notas = Notas;
            items.UsuarioAlta = int.Parse(IdUsuario);

            if (n.contactosempresas.Agregar_ContactoEmpresa(n.contactos.Agregar_Registro(items).ToString(), Empresa))
            {
                //ViewBag.Empresas = er.Seleccionar(usr.Configuracion.Id.ToString(), usr.Roles.Id.ToString(), usr.Usuarios.Id.ToString()).ToList();
                ViewData["Mensaje"] = "Agregado";
            }
            else
                ViewData["Mensaje"] = "Falla";
            return View("Index");
        }

        /// <summary>
        /// Cambia los datos de un contacto
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="ApellidoPaterno"></param>
        /// <param name="ApellidoMaterno"></param>
        /// <param name="Correo"></param>
        /// <param name="Telefono"></param>
        /// <param name="Celular"></param>
        /// <param name="Direccion"></param>
        /// <param name="Ciudad"></param>
        /// <param name="Estado"></param>
        /// <param name="Cargo"></param>
        /// <param name="FechaCumpleaños"></param>
        /// <param name="TipoContacto"></param>
        /// <param name="Notas"></param>
        /// <param name="IdContacto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Modificar(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string Telefono, string Celular,
        string Direccion, string Ciudad, string Estado, string Cargo, string FechaCumpleaños, string Edad, string TipoContacto, string Notas, string IdContacto)
        {
            usuariorolsesion = (UsuariosRoles)GranSesion["GranSesion"];

            Contactos items = new Contactos();
            items.Nombre = Nombre;
            items.ApellidoPaterno = ApellidoPaterno;
            items.ApellidoMaterno = ApellidoMaterno;
            items.Correo = Correo;
            items.Telefono = Telefono;
            items.Celular = Celular;
            items.Direccion = Direccion;
            items.Ciudad = Ciudad;
            items.Estado = int.Parse(Estado);
            items.Cargo = Cargo;
            items.FechaCumpleaños = FechaCumpleaños;
            items.Edad = string.IsNullOrEmpty(Edad) ? 0 : int.Parse(Edad);
            items.TipoContacto = int.Parse(TipoContacto);
            items.Notas = Notas;
            items.Id = int.Parse(IdContacto);

            if (n.contactos.Modificar_Registro(items) > 0)
            {
                //ViewBag.Empresas = er.Seleccionar(usr.Configuracion.Id.ToString(), usr.Roles.Id.ToString(), usr.Usuarios.Id.ToString()).ToList();
                ViewData["Mensaje"] = "Modificado";
            }
            else
                ViewData["Mensaje"] = "Fallo";
            return View("Index");
        }

        //Metodos Json

        public JsonResult SeleccionInicial(string id)
        {
            return Json(n.contactos.Seleccionar_Todos(Session["IdConfiguracion"].ToString(), Session["IdRoles"].ToString(), Session["IdUsuario"].ToString()), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Selecciona el detalle del contacto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult SeleccionarDetalleContactoEdicion(string id)
        {
            return Json(n.contactos.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Selecciona el detalle de la empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult SeleccionarDetalleContactoEmpresaPorId(string id)
        {
            return Json(n.contactos.Seleccionar_PorIdDetalleContactoEmpresa(id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Selecciona todos los contactos por empresa
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult SeleccionarContactosPorEmpresa(string Id)
        {
            return Json(n.contactos.Seleccionar_ContactosPorEmpresa(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarContactosPorConfiguracion(string Id)
        {
            return Json(n.contactos.Seleccionar_ContactosPorConfiguracion(Id, Session["IdRol"].ToString(), Session["IdUsuario"].ToString()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscarParecidos(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string Telefono, string Celular)
        {
            Contactos items = new Contactos();
            items.Nombre = Nombre;
            items.ApellidoPaterno = ApellidoPaterno;
            items.ApellidoMaterno = ApellidoMaterno;
            items.Correo = Correo;
            items.Telefono = Telefono;
            items.Celular = Celular;

            return Json(n.contactos.Buscar_Parecidos(items), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscarParecido(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string Telefono, string Celular)
        {
            Contactos items = new Contactos();
            items.Nombre = Nombre;
            items.ApellidoPaterno = ApellidoPaterno;
            items.ApellidoMaterno = ApellidoMaterno;
            items.Correo = Correo;
            items.Telefono = Telefono;
            items.Celular = Celular;

            return Json(n.contactos.Buscar_Parecido(items), JsonRequestBehavior.AllowGet);
        }

        //PreGuardado (para SABE)
        public JsonResult PreGuardado1(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string IdConfiguracion)
        {
            Contactos items = new Contactos();
            items.Nombre = Nombre;
            items.ApellidoPaterno = ApellidoPaterno;
            items.ApellidoMaterno = ApellidoMaterno;
            items.Correo = Correo;
            items.IdConfiguracion = int.Parse(IdConfiguracion);

            return Json(n.contactos.Pre_Guardado1(items), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Agregar1(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string Telefono, string Celular,
        string Empresa, string Direccion, string Ciudad, string Notas, string IdConfiguracion)
        {
            Contactos items = new Contactos();
            items.Nombre = Nombre;
            items.ApellidoPaterno = ApellidoPaterno;
            items.ApellidoMaterno = ApellidoMaterno;
            items.Correo = Correo;
            items.Telefono = Telefono;
            items.Celular = Celular;
            items.Direccion = Direccion;
            items.Ciudad = Ciudad;
            items.Notas = Notas;
            items.IdConfiguracion = int.Parse(IdConfiguracion);

            var salida = 0; //cr.Agregar(items);
                            //if (salida > 0)
                            //{
                            //Agregar el nuevo contacto como usuario del sistema rol 5
            var salida2 = n.usuarios.Agregar_Registro(items.Nombre, "", "", Correo, Correo, Funciones.ClaveSalida());
            if (salida2 > 0)
            {
                n.usuarioconfiguracion.Agregar_Registro(salida2.ToString(), IdConfiguracion);
                n.usuariosroles.Agregar_Registro(salida2.ToString(), "5");

                if (!string.IsNullOrEmpty(Empresa))
                {
                    //Agregar el detalle
                    n.usuarios.Agregar_Detalle(salida2.ToString(), items.Telefono, items.Celular, Empresa, items.Direccion, items.Ciudad, items.Notas, "0", "", "");
                    //if (cer.AgregarContactoEmpresa(salida.ToString(), Empresa))
                    //    {
                    if (n.empresasusuarios.Agregar_Registro(Empresa, salida2.ToString(), "false") > 0) //Ahora se agrega a empresasusuario para un mejor seguimiento, todo desactivado
                    {
                        salida = 1; //Agregado correctamente
                    }
                    else
                    {
                        salida = 0;
                    }
                    //}
                    //else
                    //{
                    //    salida = 0;
                    //}
                }
                else
                {
                    salida = 0;
                }
                var correoresponsable = n.usuarios.Seleccionar_CorreoReponsable(salida2.ToString());
                var titulocorreo = Titulos.TDashboard(Session["IdConfiguracion"].ToString());
                //Enviar correo de alta al sistema                    
                correo.EnviarCorreoAltaResponsable(items.Nombre, correoresponsable, titulocorreo, Session["IdConfiguracion"].ToString());
            }
            else
            {
                if (n.contactosempresas.Agregar_SoloContacto(salida.ToString()) > 0)
                {
                    salida = 1;
                }
            }
            //}
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        //PreGuardado (para ASAE)
        public JsonResult PreGuardado2(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string IdConfiguracion)
        {
            Contactos items = new Contactos();
            items.Nombre = Nombre;
            items.ApellidoPaterno = ApellidoPaterno;
            items.ApellidoMaterno = ApellidoMaterno;
            items.Correo = Correo;
            items.IdConfiguracion = int.Parse(IdConfiguracion);

            return Json(n.contactos.Pre_Guardado2(items), JsonRequestBehavior.AllowGet);
        }

        //Agregado completo (Para ASAE)
        public JsonResult Agregar2(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string Telefono, string Celular,
        string Direccion, string Ciudad, string CP, string Estado, string Cargo, string FechaCumpleaños, string Empresa, string TipoContacto, string Notas,
        string IdConfiguracion, string IdUDN, string IdArea, string IdUsuario, string Pais, string Edad, string Sexo)
        {
            Contactos items = new Contactos();
            items.Nombre = Nombre;
            items.ApellidoPaterno = ApellidoPaterno;
            items.ApellidoMaterno = ApellidoMaterno;
            items.Correo = Correo;
            items.Telefono = Telefono;
            items.Celular = Celular;
            items.Direccion = Direccion;
            items.CP = CP;
            items.Ciudad = Ciudad;
            items.Estado = string.IsNullOrEmpty(Estado) ? 9 : int.Parse(Estado);
            items.Pais = string.IsNullOrEmpty(Pais) ? 9 : int.Parse(Pais);
            items.Cargo = Cargo;
            items.FechaCumpleaños = FechaCumpleaños;
            items.Edad = string.IsNullOrEmpty(Edad) ? 0 : int.Parse(Edad);
            items.Sexo = Sexo;
            items.TipoContacto = string.IsNullOrEmpty(TipoContacto) ? 2 : int.Parse(TipoContacto);
            items.Notas = Notas;
            items.IdConfiguracion = int.Parse(IdConfiguracion);
            if (Session["IdRol"].ToString() == "7")
                items.IdUDN = 6;
            else
                items.IdUDN = string.IsNullOrEmpty(IdUDN) ? n.usuarios.Seleccionar_UDNUsuario(Session["IdUsuario"].ToString()) : int.Parse(IdUDN);
            items.IdArea = string.IsNullOrEmpty(IdArea) ? 9 : int.Parse(IdArea);
            items.UsuarioAlta = string.IsNullOrEmpty(IdUsuario) ? 9 : int.Parse(IdUsuario);

            int obtenido = n.contactos.Agregar_3(items);
            if (obtenido > 0)
            {
                //Agregar la empresa si se ha elegido una
                if (Empresa != "")
                {
                    n.contactosempresas.Agregar_ContactoEmpresa(obtenido.ToString(), Empresa);
                }
                else
                {
                    n.contactosempresas.Agregar_SoloContacto(obtenido.ToString());
                }
                return Json(obtenido, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(obtenido, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Agregar3(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string Telefono, string Celular, string IdConfiguracion, string UsuarioAlta)
        {
            Contactos items = new Contactos();
            items.Nombre = Nombre;
            items.ApellidoPaterno = ApellidoPaterno;
            items.ApellidoMaterno = ApellidoMaterno;
            items.Correo = Correo;
            items.Telefono = Telefono;
            items.Celular = Celular;
            items.IdConfiguracion = int.Parse(IdConfiguracion);
            items.UsuarioAlta = int.Parse(UsuarioAlta);

            int salida = n.contactos.Agregar_2(items);
            if (salida > 0)
            {
                if (n.contactosempresas.Agregar_SoloContacto(salida.ToString()) > 0)
                {
                    salida = 1; //Agregado correctamente
                }
            }
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        //Agregado completo (Para Contactos que vienen de otras fuentes)
        public JsonResult Agregar4(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string Telefono, string Celular,
        string Direccion, string Ciudad, string CP, string Estado, string Cargo, string FechaCumpleaños, string Empresa, string TipoContacto, string Notas,
        string IdConfiguracion, string IdUDN, string IdArea, string IdUsuario, string Pais, string Ingreso)
        {
            Contactos items = new Contactos();
            items.Nombre = Nombre;
            items.ApellidoPaterno = ApellidoPaterno;
            items.ApellidoMaterno = ApellidoMaterno;
            items.Correo = Correo;
            items.Telefono = Telefono;
            items.Celular = Celular;
            items.Direccion = Direccion;
            items.CP = CP;
            items.Ciudad = Ciudad;
            items.Estado = string.IsNullOrEmpty(Estado) ? 9 : int.Parse(Estado);
            items.Pais = string.IsNullOrEmpty(Pais) ? 9 : int.Parse(Pais);
            items.Cargo = Cargo;
            items.FechaCumpleaños = FechaCumpleaños;
            items.TipoContacto = string.IsNullOrEmpty(TipoContacto) ? 2 : int.Parse(TipoContacto);
            items.Notas = Notas;
            items.IdConfiguracion = int.Parse(IdConfiguracion);
            if (Session["IdRol"].ToString() == "7")
                items.IdUDN = 6;
            else
                items.IdUDN = string.IsNullOrEmpty(IdUDN) ? 0 : int.Parse(IdUDN);
            items.IdArea = string.IsNullOrEmpty(IdArea) ? 9 : int.Parse(IdArea);
            items.UsuarioAlta = string.IsNullOrEmpty(IdUsuario) ? 9 : int.Parse(IdUsuario);
            items.Ingreso = int.Parse(Ingreso);

            int obtenido = n.contactos.Agregar_Registro_Externo(items);
            if (obtenido > 0)
            {
                //Agregar la empresa si se ha elegido una
                if (Empresa != "")
                {
                    n.contactosempresas.Agregar_ContactoEmpresa(obtenido.ToString(), Empresa);
                }
                else
                {
                    n.contactosempresas.Agregar_SoloContacto(obtenido.ToString());
                }
                return Json(obtenido, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(obtenido, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Modificar1(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string Telefono, string Celular,
        string Direccion, string Ciudad, string CP, string Estado, string Pais, string Cargo, string FechaCumpleaños, string Empresa, string TipoContacto, string Notas, string IdContacto,
        string UDN, string IdArea, string Activo, string Edad, string Sexo, string TipoPersona)
        {
            Contactos items = new Contactos();
            items.Nombre            = Nombre;
            items.ApellidoPaterno   = ApellidoPaterno;
            items.ApellidoMaterno   = ApellidoMaterno;
            items.Correo            = Correo;
            items.Telefono          = Telefono;
            items.Celular           = Celular;
            items.Direccion         = Direccion;
            items.CP                = CP;
            items.Ciudad            = Ciudad;
            if (!string.IsNullOrEmpty(Estado))
                items.Estado = Estado == "" ? 0 : int.Parse(Estado);
            items.Cargo             = Cargo;
            items.FechaCumpleaños   = FechaCumpleaños;
            items.Edad = string.IsNullOrEmpty(Edad) ? 0 : int.Parse(Edad);
            if (!string.IsNullOrEmpty(TipoContacto))
                items.TipoContacto = string.IsNullOrEmpty(TipoContacto) ? 2 : int.Parse(TipoContacto);
            items.Notas             = Notas;
            items.Id                = int.Parse(IdContacto);
            items.IdUDN             = string.IsNullOrEmpty(UDN) ? 0 : int.Parse(UDN);
            items.IdArea            = string.IsNullOrEmpty(IdArea) ? 0 : int.Parse(IdArea);
            items.Pais              = string.IsNullOrEmpty(Pais) ? 0 : int.Parse(Pais);
            items.Activo            = Activo == "1" || Activo == "True" ? true : false;
            items.Sexo              = Sexo;
            items.TipoPersona = string.IsNullOrEmpty(TipoPersona) ? 0 : int.Parse(TipoPersona);

            int salida = 0;
            if (n.contactos.Seleccionar_ValidarCreadorContacto(IdContacto, Session["IdUsuario"].ToString()) 
                || n.compartircontactos.Validar_SiUsuarioPuedeModificar(IdContacto, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                if (n.contactos.Modificar_Registro(items) > 0)
                {
                    //Agregar la empresa si se ha elegido una
                    if (Empresa != "")
                        n.contactosempresas.Actualizar_ContactoEmpresa(IdContacto, Empresa);
                    salida = 1;
                    return Json(salida, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(salida, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                salida = 2;
                return Json(salida, JsonRequestBehavior.AllowGet);
            }
            

        }

        public JsonResult ModificarContactoUsuario(string IdContacto, string NuevoId)
        {
            Contactos items = new Contactos();
            items.Id = int.Parse(IdContacto);
            items.IdUsuarioResponsable = int.Parse(NuevoId);

            int salida = 0;
            if (n.contactos.Modificar_ContactoUsuario(items) > 0)
            {
                salida = 1;
                return Json(salida, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(salida, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarArea(string Nombre)
        {
            n.area.Agregar_Registro(Nombre);
            return Json(n.area.Seleccionar_Registros(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarActividadContacto(string idusuario, string idcontacto, string idtipoactividad, string fecha, string notas)
        {
            ActividadesContacto item1 = new ActividadesContacto();
            item1.IdUsuario = int.Parse(idusuario);
            item1.IdContacto = int.Parse(idcontacto);
            var obtenido = n.actividadescontacto.Agregar_Registro(item1);
            ActividadesContactoDetalle item2 = new ActividadesContactoDetalle();
            item2.IdActividadContacto = obtenido;
            item2.IdTipoActividad = int.Parse(idtipoactividad);
            item2.Fecha = DateTime.Parse(fecha);
            item2.Notas = notas;
            n.actividadescontactodetalle.Agregar_Registro(item2);
            return Json(n.actividadescontactodetalle.Seleccionar_ActividadesPorIdContacto(idcontacto), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarActividadesContacto(string idcontacto)
        {
            return Json(n.actividadescontactodetalle.Seleccionar_ActividadesPorIdContacto(idcontacto), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarActividadId(string idactividad)
        {
            return Json(n.actividadescontactodetalle.Seleccionar_PorId(idactividad), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarAdicionalPorId(string idcontacto)
        {
            return Json(n.contactosdetalle.Seleccionar_PorIdContacto(idcontacto), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarAdicional(string idcontacto, string idpuesto, string iddepartamento)
        {
            if (n.contactos.Seleccionar_CreadorContacto(idcontacto) == Session["IdUsuario"].ToString() 
                || n.compartircontactos.Validar_SiUsuarioPuedeModificar(idcontacto, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                return Json(n.contactosdetalle.Agregar_Modificar(idcontacto, idpuesto, iddepartamento), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ModificarActividad(string idtipoactividad, string fecha, string notas, string id)
        {
            return Json(n.actividadescontactodetalle.Modificar_Registro(idtipoactividad, fecha, notas, id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EnviarCorreoCambios(string idcontacto, string mensaje)
        {
            var creadorcontacto = n.contactos.Seleccionar_CreadorContacto(idcontacto);
            var correocreador = n.usuarios.Seleccionar_CorreoReponsable(creadorcontacto);
            var nombrecontacto = n.contactos.Seleccionar_NombreContacto(idcontacto);
            //Correo correo = new Correo();
            mensaje += "<p>Para el contacto: " + nombrecontacto;
            mensaje += "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
            "<h4><a href='http://200.23.87.202:1052/Home/'>http://200.23.87.202:1052/Home/</a></h4>";
            var regresar = 0;
            if (!correo.EnviarCorreo(correocreador, "Solicitud de Cambios/Agregado en Contactos CRM", "Solicitud de Cambios en Contacto", mensaje))
            {
                regresar = 1;
            }

            return Json(regresar, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CompartirRegistro(string idcontacto, string idusuario)
        {
            int regresar = 0;
            if (n.contactos.Seleccionar_CreadorContacto(idcontacto) == Session["IdUsuario"].ToString() 
                || n.compartircontactos.Validar_SiUsuarioPuedeModificar(idcontacto, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                regresar = n.compartircontactos.Agregar_Registro(idcontacto, idusuario);
            }
            return Json(regresar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ContactoUsuariosCompartidos(string idcontacto)
        {
            return Json(n.compartircontactos.Seleccionar_UsuariosCompartidos(idcontacto), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarUsuarioCompartido(string idcontacto, string idusuario)
        {
            int regresar = 0;
            if (n.contactos.Seleccionar_CreadorContacto(idcontacto) == Session["IdUsuario"].ToString() 
                || n.compartircontactos.Validar_SiUsuarioPuedeModificar(idcontacto, Session["IdUsuario"].ToString())
                || Session["IdUsuario"].ToString() == "2")
            {
                regresar = n.compartircontactos.Eliminar_UsuarioCompartido(idcontacto, idusuario);
            }
            return Json(regresar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EnviarCorreoCambios(string idusuario, string idcontacto, string mensaje)
        {
            var idusuariocreador = n.contactos.Seleccionar_CreadorContacto(idcontacto);
            var correocreador = n.usuarios.Seleccionar_CorreoReponsable(idusuariocreador);
            var nombrecontacto = n.contactos.Seleccionar_NombreContacto(idcontacto);
            //Correo correo = new Correo();
            mensaje += "<p>Para el cliente: " + nombrecontacto;
            mensaje += "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
            "<h4><a href='http://200.23.87.202:1052/Home/'>http://200.23.87.202:1052/Home/</a></h4>";
            var regresar = 0;
            if (!correo.EnviarCorreo(correocreador, "Solicitud de Cambios/Agregado/Compartir en CRM", "Solicitud de Cambios en CRM", mensaje))
            {
                regresar = 1;
            }

            return Json(regresar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarSiEmpresaCompartida(string idcontacto, string idempresa, string idusuario)
        {
            if (n.empresas.Seleccionar_CreadorEmpresa(idempresa) == Session["IdUsuario"].ToString() 
                || n.compartirempresa.Validar_SiUsuarioPuedeModificar(idempresa, idusuario)
                || Session["IdUsuario"].ToString() == "2")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ConteosFiltros()
        {
            return Json(n.contactos.Seleccionar_Todos_Filtrado_Conteos(), JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult ExcelContactos(CRM.Models.Models.Marketing marketing)
        {
            int UsuarioAlta = Convert.ToInt32(Session["IdUsuario"].ToString());
            int IdConfiguracion = Convert.ToInt32(Session["IdConfiguracion"].ToString());
            int TipoContacto = marketing.Id;

            string handle = Guid.NewGuid().ToString();
            System.Data.DataSet dsContactos = n.contactos.ExportaXLS(UsuarioAlta, IdConfiguracion, TipoContacto);

            string folderPath = System.IO.Path.Combine(Server.MapPath("~/Archivos"));
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dsContactos);
                wb.SaveAs(folderPath + @"/" + handle.ToString() + ".xlsx");
            }

            // Note we are returning a filename as well as the handle
            string message = "SUCCESS";
            return Json(new { Message = message, FileGuid = handle.ToString() + ".xlsx", JsonRequestBehavior.AllowGet });
        }

    }
}
