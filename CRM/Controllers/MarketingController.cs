using CRM.Models.Models;
using CRM.Utilerias;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ClosedXML.Excel;

using System.Data;
using System.Collections.Generic;


namespace CRM.Controllers
{
    public class MarketingController : Comun
    {
        private UsuariosRoles usuariosrolessesion;

        // GET: Marketing
        public ActionResult Index()
        {
            return View(n.marketing.Seleccionar_Todos());
        }

        public ActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Alta(string Consecutivo, string Nombre, string NuevaAnterior, string Inicio, string Fin, string Objetivo, string Medio, string Estado,
        string Correo, string Facebook, string Linkedin, string Llamada, string ASAE, string Envios, string InicioEvento, string FinEvento, string HoraInicio, 
        string HoraFin, string Ubicacion, string Descripcion, string UDN)
        {
            string fIncio = "";
            string fFinal = "";
            string fEnvios = "";
            string fInicioEvento = "";
            string fFinEvento = "";

            if (Envios.Length == 0) { Envios = "01/01/1900"; }
            if (InicioEvento.Length == 0) { InicioEvento = "01/01/1900"; }
            if (FinEvento.Length == 0) { FinEvento = "01/01/1900"; }

            fIncio = Inicio.Substring(6, 4) + "/" + Inicio.Substring(3, 2) + "/" + Inicio.Substring(0, 2);
            fFinal = Fin.Substring(6, 4) + "/" + Fin.Substring(3, 2) + "/" + Fin.Substring(0, 2); ;
            fEnvios = Envios.Substring(6, 4) + "/" + Envios.Substring(3, 2) + "/" + Envios.Substring(0, 2);
            fInicioEvento = InicioEvento.Substring(6, 4) + "/" + InicioEvento.Substring(3, 2) + "/" + InicioEvento.Substring(0, 2);
            fFinEvento = FinEvento.Substring(6, 4) + "/" + FinEvento.Substring(3, 2) + "/" + FinEvento.Substring(0, 2);

            Marketing items = new Marketing();
            items.Consecutivo = Consecutivo;
            items.Nombre = Nombre;
            items.NuevaAnterior = int.Parse(NuevaAnterior);
            items.Inicio = string.IsNullOrEmpty(Inicio) ? DateTime.Parse("1900/01/01") :DateTime.Parse(Inicio);
            items.Fin = string.IsNullOrEmpty(Fin) ? DateTime.Parse("1900/01/01") : DateTime.Parse(fFinal);
            items.Objetivo = Objetivo;
            items.Medio = 0;
            items.Estado = int.Parse(Estado);
            items.Usuario = int.Parse(Session["IdUsuario"].ToString());
            items.Correo = !string.IsNullOrEmpty(Correo) ? 1 : 0;
            items.Facebook = !string.IsNullOrEmpty(Facebook) ? 1 : 0;
            items.Linkedin = !string.IsNullOrEmpty(Linkedin) ? 1 : 0;
            items.Llamada = !string.IsNullOrEmpty(Llamada) ? 1 : 0;
            items.ASAE = !string.IsNullOrEmpty(ASAE) ? 1 : 0;
            items.Envios = string.IsNullOrEmpty(Envios) ? DateTime.Parse("1900/01/01") : DateTime.Parse(fEnvios);
            items.InicioEvento = string.IsNullOrEmpty(InicioEvento) ? DateTime.Parse("1900/01/01") : DateTime.Parse(InicioEvento);
            items.FinEvento = string.IsNullOrEmpty(FinEvento) ? DateTime.Parse("1900/01/01") : DateTime.Parse(fFinEvento);
            items.HoraInicio = HoraInicio;
            items.HoraFin = HoraFin;
            items.Ubicacion = string.IsNullOrEmpty(Ubicacion) ? "CDMX" : Ubicacion;
            items.Descripcion = Descripcion;
            items.IdUDN = string.IsNullOrEmpty(UDN) ?  3 : int.Parse(UDN);

            Session["IdCampaña"] = n.marketing.Agregar_Registro(items);

            if (Session["IdCampaña"].ToString() == "-2")
            {
                ViewData["Mensaje"] = "<div class='alert alert-danger' role='alert'>La clave de campaña <strong>" + Consecutivo + "</strong>  ya está siendo usada en otro evento, intente una diferente</div>";
                return View("Alta");
            }
            else
            {
                //Agregar también el archivo ICS para agregar en la agenda
                //siempre y cuando no se esté creando una campaña externa rápida
                if (!items.InicioEvento.ToString().Contains("1900"))
                    GenerarArchivoICS(Session["IdCampaña"].ToString());
                return RedirectToAction("Contactos");
            }
        }

        public ActionResult Contactos()
        {            
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.MarketingContactos = n.marketingcontactos.Seleccionar_PorIdCampaña(Session["IdCampaña"].ToString());
            ViewBag.Contactos = n.contactos.Seleccionar_TodosPorConfiguracion(usuariosrolessesion.Configuracion.Id.ToString()).ToList();
            ViewBag.EstadoActual = n.marketing.Seleccionar_PorId(Session["IdCampaña"].ToString()).Estado;
            TempData["CampañaId"] = Session["IdCampaña"];
            return View();
        }

        [HttpGet]
        public ActionResult Graficos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contactos(string IdCampaña, string[] contacto)
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            var boton1 = Request.Form["Boton1"];
            var boton2 = Request.Form["Boton2"];

            Session["IdCampaña"] = IdCampaña;

            if (boton1 != null)
            {
                if (contacto != null && contacto.Length > 0)
                {
                    foreach (var campo in contacto)
                    {
                        //Guarda los que traen valor (solamente)
                        n.marketingcontactos.Agregar_Registro(IdCampaña, campo);
                    }
                }
                //int cuenta = n.marketingcontactos.Agregar_Registros(contacto, IdCampaña); //Pruebas
            }
            else if (boton2 != null)
            {
                //Agregar todos los contactos
                var contactos = n.contactos.SeleccionarRegistros();
                foreach (var conts in contactos)
                {
                    n.marketingcontactos.Agregar_Registro(IdCampaña, conts.Id.ToString());
                }
            }

            ViewBag.MarketingContactos = n.marketingcontactos.Seleccionar_PorIdCampaña(IdCampaña);
            ViewBag.Contactos = n.contactos.Seleccionar_TodosPorConfiguracion(usuariosrolessesion.Configuracion.Id.ToString()).ToList();
            ViewBag.EstadoActual = n.marketing.Seleccionar_PorId(IdCampaña.ToString()).Estado;
            return RedirectToAction("Contactos");
        }

        public ActionResult Correo()
        {
            TempData["IdCampaña"] = TempData["CampañaId"];
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Correo(string IdCampaña, string asunto, string contenido)
        {
            var boton1 = Request.Form["Boton1"];
            var boton2 = Request.Form["Boton2"];
            if (boton1 != null)
            {
                n.marketingcorreo.Agregar_Registro(IdCampaña, asunto, contenido);
                return View("Index", n.marketing.Seleccionar_Todos());
            }
            else if (boton2 != null)
            {
                n.marketingcorreo.Agregar_Registro(IdCampaña, asunto, contenido);
                return RedirectToRoute(new { controller = "Marketing", action = "ModificarCorreo", IdCampaña = IdCampaña });
            }
            return View();
        }

        [HttpGet]
        public ActionResult ModificarCampaña(int IdCampaña)
        {
            TempData["IdCampaña"] = IdCampaña;
            return View(n.marketing.Seleccionar_PorId(IdCampaña.ToString()));
        }

        [HttpPost]
        public ActionResult ModificarCampaña(string IdCampaña, string Consecutivo, string Nombre, string NuevaAnterior, string Inicio, string Fin, string Objetivo,
        string Medio, string Estado, string Correo, string Facebook, string Linkedin, string Llamada, string ASAE, string Envios, string InicioEvento, string FinEvento, 
        string HoraInicio, string HoraFin, string Ubicacion, string Descripcion, string idudn)
        {
            Marketing items = new Marketing();
            items.Consecutivo = Consecutivo;
            items.Nombre = Nombre;
            items.NuevaAnterior = int.Parse(NuevaAnterior);
            items.Inicio = string.IsNullOrEmpty(Inicio) ? DateTime.Parse("1900/01/01") : DateTime.Parse(Inicio);
            items.Fin = string.IsNullOrEmpty(Fin) ? DateTime.Parse("1900/01/01") : DateTime.Parse(Fin);
            items.Objetivo = Objetivo.Trim();
            items.Medio = 0;
            items.Estado = int.Parse(Estado);
            items.Correo = !string.IsNullOrEmpty(Correo) ? 1 : 0;
            items.Facebook = !string.IsNullOrEmpty(Facebook) ? 1 : 0;
            items.Linkedin = !string.IsNullOrEmpty(Linkedin) ? 1 : 0;
            items.Llamada = !string.IsNullOrEmpty(Llamada) ? 1 : 0;
            items.ASAE = !string.IsNullOrEmpty(ASAE) ? 1 : 0;
            items.Envios =string.IsNullOrEmpty(Envios) ? DateTime.Parse("1900/01/01") : DateTime.Parse(Envios);
            items.InicioEvento = string.IsNullOrEmpty(InicioEvento) ? DateTime.Parse("1900/01/01") : DateTime.Parse(InicioEvento);
            items.FinEvento = string.IsNullOrEmpty(FinEvento) ? DateTime.Parse("1900/01/01") : DateTime.Parse(FinEvento);
            items.HoraInicio = HoraInicio;
            items.HoraFin = HoraFin;
            items.Ubicacion = Ubicacion;
            items.Descripcion = Descripcion;
            items.Id = int.Parse(IdCampaña);
            n.marketing.Modificar_Registro(items);
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            //Volver a crear el archivo ICS para agregar en la agenda por los cambios
            GenerarArchivoICS(IdCampaña);
            return View("Index", n.marketing.Seleccionar_Todos());
        }

        [HttpGet]
        public ActionResult ModificarContactos(int IdCampaña)
        {
            TempData["IdCampaña"] = IdCampaña;
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            ViewBag.MarketingContactos = n.marketingcontactos.Seleccionar_PorIdCampaña(IdCampaña.ToString());
            ViewBag.EstadoActual = n.marketing.Seleccionar_PorId(IdCampaña.ToString()).Estado;
            ViewBag.Contactos = n.contactos.Seleccionar_TodosPorConfiguracion(usuariosrolessesion.Configuracion.Id.ToString()).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult ModificarContactos(string IdCampaña, string[] contacto)
        {
            var boton1 = Request.Form["Boton1"];
            var boton2 = Request.Form["Boton2"];

            TempData["IdCampaña"] = IdCampaña;
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];

            if (boton1 != null)
            {
                if (contacto != null && contacto.Length > 0)
                {
                    foreach (var campo in contacto)
                    {
                        //Guarda los que traen valor (solamente)
                        n.marketingcontactos.Agregar_Registro(IdCampaña, campo);
                    }
                }
            }
            else if (boton2 != null)
            {
                //Agregar todos los contactos
                var contactos = n.contactos.SeleccionarRegistros();
                foreach (var conts in contactos)
                {
                    n.marketingcontactos.Agregar_Registro(IdCampaña, conts.Id.ToString());
                }
            }
            ViewBag.MarketingContactos = n.marketingcontactos.Seleccionar_PorIdCampaña(IdCampaña);
            ViewBag.Contactos = n.contactos.Seleccionar_TodosPorConfiguracion(usuariosrolessesion.Configuracion.Id.ToString()).ToList();
            ViewBag.EstadoActual = n.marketing.Seleccionar_PorId(IdCampaña.ToString()).Estado;
            return View();
        }

        [HttpGet]
        public ActionResult ModificarCorreo(int IdCampaña)
        {
            ViewBag.EstadoActual = n.marketing.Seleccionar_PorId(IdCampaña.ToString()).Estado;
            return View(n.marketingcorreo.Seleccionar_PorIdCampaña(IdCampaña.ToString()));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ModificarCorreo(string IdCampaña, string asunto, string contenido)
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            var boton1 = Request.Form["Boton1"];
            var boton2 = Request.Form["Boton2"];
            string resultado = "";
            var ok = n.marketingcorreo.Modificar_Registro(IdCampaña, asunto, contenido);
            if (ok == 0)
                n.marketingcorreo.Agregar_Registro(IdCampaña, asunto, contenido);
            if (boton1 != null)
            {
                ViewBag.EstadoActual = n.marketing.Seleccionar_PorId(IdCampaña.ToString()).Estado;
                return View("Index", n.marketing.Seleccionar_Todos());
            }
            else if (boton2 != null)
            {
                //string cuerpocorreo = n.marketingcorreo.Seleccionar_PorIdCampaña(IdCampaña).Cuerpo;
                //var usuario = n.usuarios.Seleccionar_PorId(usuariosrolessesion.Usuarios.Id.ToString());
                //if (cuerpocorreo.Contains("[nombre]"))
                //{
                //    string nombre = usuario.Usuarios.Nombre + " " + usuario.Usuarios.ApellidoPaterno + " " + usuario.Usuarios.ApellidoMaterno;
                //    string id = usuario.Usuarios.Id.ToString();
                //    cuerpocorreo = cuerpocorreo.Replace("[nombre]", nombre);
                //    if (cuerpocorreo.Contains("[campa]"))
                //        cuerpocorreo = cuerpocorreo.Replace("[campa]", Cifrado.Encriptar(IdCampaña));
                //    if (cuerpocorreo.Contains("[conta]"))
                //        cuerpocorreo = cuerpocorreo.Replace("[conta]", Cifrado.Encriptar(id));
                //    correo.EnviarCorreo(usuario.Usuarios.Correo, "Prueba de Visibilidad", asunto, cuerpocorreo);
                //}
                //else
                //{
                //    correo.EnviarCorreo(usuario.Usuarios.Correo, "Prueba de Visibilidad", asunto, cuerpocorreo);
                //}

                //Implementación con envío de archivo ics para que el contacto agende el evento en su correo
                var campagne = n.marketing.Seleccionar_PorId(IdCampaña);
                var composicioncorreo = n.marketingcorreo.Seleccionar_PorIdCampaña(IdCampaña);
                var usuario = n.usuarios.Seleccionar_PorId(usuariosrolessesion.Usuarios.Id.ToString());
                string fechainicioevento = campagne.InicioEvento.ToString("yyyy/MM/dd") + " " + campagne.HoraInicio + ":00";
                string fechafinevento = campagne.FinEvento.ToString("yyyy/MM/dd") + " " + campagne.HoraFin + ":00"; 
                if (composicioncorreo.Cuerpo.Contains("[nombre]"))
                {
                    string nombre = usuario.Usuarios.Nombre + " " + usuario.Usuarios.ApellidoPaterno + " " + usuario.Usuarios.ApellidoMaterno;
                    string id = usuario.Usuarios.Id.ToString();
                    composicioncorreo.Cuerpo = composicioncorreo.Cuerpo.Replace("[nombre]", nombre);
                    if (composicioncorreo.Cuerpo.Contains("[campa]"))
                        composicioncorreo.Cuerpo = composicioncorreo.Cuerpo.Replace("[campa]", Cifrado.Encriptar(IdCampaña));
                    if (composicioncorreo.Cuerpo.Contains("[conta]"))
                        composicioncorreo.Cuerpo = composicioncorreo.Cuerpo.Replace("[conta]", Cifrado.Encriptar(id));
                    if (composicioncorreo.Cuerpo.Contains("[agenda]"))
                    {
                        composicioncorreo.Cuerpo = composicioncorreo.Cuerpo.Replace("[agenda]", "");
                        correo.AgendaParaEventos(usuario.Usuarios.Correo
                            , ""
                            , "Eventos ASAE"
                            , "Prueba de Visibilidad"
                            , composicioncorreo.Cuerpo
                            , campagne.Consecutivo + ".ics"
                            , campagne.Nombre
                            , fechainicioevento
                            , fechafinevento
                            , nombre
                            , usuario.Usuarios.Correo
                            , campagne.Nombre
                            , campagne.Descripcion
                            , campagne.Ubicacion
                            );

                    }
                    else
                    {
                        correo.EnviarCorreo(usuario.Usuarios.Correo, "Prueba de Visibilidad", asunto, composicioncorreo.Cuerpo);
                    }
                }
                else
                {
                    correo.EnviarCorreo(usuario.Usuarios.Correo, "Prueba de Visibilidad", asunto, composicioncorreo.Cuerpo);
                }
                ViewBag.EstadoActual = n.marketing.Seleccionar_PorId(IdCampaña.ToString()).Estado;
                return View("ModificarCorreo", n.marketingcorreo.Seleccionar_PorIdCampaña(IdCampaña.ToString()));
            }
            return View(resultado);
        }

        public ActionResult SeguimientoCampaña(string IdCampaña)
        {
            ViewBag.Campaña = n.marketing.Seleccionar_PorId(IdCampaña).Nombre;
            return View(n.marketingseguimiento.Seleccionar_Por_IdCampaña(IdCampaña));
        }

        [HttpPost]
        public ActionResult SeguimientoCampaña(string IdCampaña, string Seguimiento)
        {
            n.marketingseguimiento.Agregar_Registro(IdCampaña, Seguimiento);

            //return View("Index", n.marketing.Seleccionar_Todos());
            return View("SeguimientoCampaña", n.marketingseguimiento.Seleccionar_Por_IdCampaña(IdCampaña));
        }

        [HttpGet]
        public ActionResult SeguimientoCampañaModificar(string Registro)
        {
            return View(n.marketingseguimiento.Seleccionar_PorId(Registro));
        }

        [HttpPost]
        public ActionResult SeguimientoCampañaModificar(string Registro, string IdCampaña, string Seguimiento)
        {
            n.marketingseguimiento.Modificar_Registro(Registro, Seguimiento);
            //return RedirectToAction("SeguimientoCampaña", n.marketingseguimiento.Seleccionar_Por_IdCampaña(IdCampaña));
            return RedirectToRoute(new { controller = "Marketing", action = "SeguimientoCampaña", IdCampaña = IdCampaña });
        }

        [HttpGet]
        public ActionResult Recordatorios(string idcampaña)
        {
            return View(n.marketingrecordatorios.Seleccionar_PorIdCampaña(idcampaña));
        }

        [HttpGet]
        public ActionResult RecordatoriosAgregar(string IdCampaña)
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RecordatoriosAgregar(string IdCampaña, string fecha, string asunto, string editor, string enviara, string agenda, string InicioEvento, string FinEvento, string HoraInicio, string HoraFin, string Ubicacion, string Descripcion)
        {
            string fechaEnvio = fecha.Substring(6, 4) + "/" + fecha.Substring(3, 2) + "/" + fecha.Substring(0, 2);
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];
            MarketingRecordatorios items = new MarketingRecordatorios();
            items.IdCampaña = int.Parse(IdCampaña);
            items.Envio = DateTime.Parse(fechaEnvio);
            items.Asunto = asunto;
            items.Cuerpo = editor;
            items.EnviarA = int.Parse(enviara);
            if (!string.IsNullOrEmpty(agenda))
            {
                items.InicioEvento = DateTime.Parse(InicioEvento);
                items.FinEvento = DateTime.Parse(FinEvento);
                items.HoraInicio = HoraInicio;
                items.HoraFin = HoraFin;
                items.Ubicacion = Ubicacion;
                items.Descripcion = Descripcion;
            }

            var boton1 = Request.Form["Boton1"];
            var boton2 = Request.Form["Boton2"];
            if (boton1 != null) //Guardar
            {
                n.marketingrecordatorios.Agregar_Registro(items);
                return View("Index", n.marketing.Seleccionar_Todos());
            }
            else if (boton2 != null) //Guardar y enviar para pruebas
            {
                int obtenido = n.marketingrecordatorios.Agregar_Registro(items);
                var cuerpocorreo = n.marketingrecordatorios.Seleccionar_PorId(obtenido.ToString());
                var usuario = n.usuarios.Seleccionar_PorId(usuariosrolessesion.Usuarios.Id.ToString());
                if (string.IsNullOrEmpty(agenda))
                {    
                    if (cuerpocorreo.Cuerpo.Contains("[nombre]"))
                    {
                        string nombre = usuario.Usuarios.Nombre + " " + usuario.Usuarios.ApellidoPaterno + " " + usuario.Usuarios.ApellidoMaterno;
                        string id = usuario.Usuarios.Id.ToString();
                        cuerpocorreo.Cuerpo = cuerpocorreo.Cuerpo.Replace("[nombre]", nombre);
                        if (cuerpocorreo.Cuerpo.Contains("[campa]"))
                            cuerpocorreo.Cuerpo = cuerpocorreo.Cuerpo.Replace("[campa]", Cifrado.Encriptar(IdCampaña));
                        if (cuerpocorreo.Cuerpo.Contains("[conta]"))
                            cuerpocorreo.Cuerpo = cuerpocorreo.Cuerpo.Replace("[conta]", Cifrado.Encriptar(id));
                        correo.EnviarCorreo(usuario.Usuarios.Correo, "Prueba de Visibilidad", asunto, cuerpocorreo.Cuerpo);
                    }
                    else
                    {
                        correo.EnviarCorreo(usuario.Usuarios.Correo, "Prueba de Visibilidad", asunto, cuerpocorreo.Cuerpo);
                    }
                }
                else
                {
                    if (cuerpocorreo.Cuerpo.Contains("[nombre]"))
                    {
                        usuario = n.usuarios.Seleccionar_PorId(usuariosrolessesion.Usuarios.Id.ToString());
                        string nombre = usuario.Usuarios.Nombre + " " + usuario.Usuarios.ApellidoPaterno + " " + usuario.Usuarios.ApellidoMaterno;
                        var clavecampaign = n.marketing.Seleccionar_PorId(obtenido.ToString());
                        cuerpocorreo.Cuerpo = cuerpocorreo.Cuerpo.Replace("[nombre]", nombre);
                        correo.AgendaParaEventos(usuario.Usuarios.Correo
                                , ""
                                , "Eventos ASAE"
                                , "Prueba de Visibilidad"
                                , cuerpocorreo.Cuerpo
                                , clavecampaign.Consecutivo + ".ics"
                                , asunto
                                , InicioEvento
                                , FinEvento
                                , nombre
                                , usuario.Usuarios.Correo
                                , asunto
                                , Descripcion
                                , Ubicacion
                                );
                        
                    }
                    else
                    {
                        usuario = n.usuarios.Seleccionar_PorId(usuariosrolessesion.Usuarios.Id.ToString());
                        string nombre = usuario.Usuarios.Nombre + " " + usuario.Usuarios.ApellidoPaterno + " " + usuario.Usuarios.ApellidoMaterno;
                        var clavecampaign = n.marketing.Seleccionar_PorId(IdCampaña);
                        correo.AgendaParaEventos(usuario.Usuarios.Correo
                                , ""
                                , "Eventos ASAE"
                                , "Prueba de Visibilidad"
                                , cuerpocorreo.Cuerpo
                                , clavecampaign.Consecutivo + ".ics"
                                , asunto
                                , InicioEvento
                                , FinEvento
                                , nombre
                                , usuario.Usuarios.Correo
                                , asunto
                                , Descripcion
                                , Ubicacion
                                );
                    }
                }

                //n.marketingrecordatorios.Agregar_Registro(items);
                return RedirectToRoute(new { controller = "Marketing", action = "Recordatorios", IdCampaña = IdCampaña });
            }
            return View();
        }

        [HttpGet]
        public ActionResult RecordatoriosModificar(string rec, string idcamp)
        {
            return View(n.marketingrecordatorios.Seleccionar_PorId(rec));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RecordatoriosModificar(string recordatorio, string idcampaña, string fecha, string asunto, string contenido, string enviara, string agenda, string InicioEvento, string FinEvento, string HoraInicio, string HoraFin, string Ubicacion, string Descripcion)
        {
            usuariosrolessesion = (UsuariosRoles)Session["GranSesion"];            

            MarketingRecordatorios items = new MarketingRecordatorios();
            items.Id = int.Parse(recordatorio);
            items.Envio = DateTime.Parse(fecha);
            items.Asunto = asunto;
            items.Cuerpo = contenido;
            items.EnviarA = int.Parse(enviara);
            if (!string.IsNullOrEmpty(agenda))
            {
                items.Agenda = 1;
                items.InicioEvento = DateTime.Parse(InicioEvento);
                items.FinEvento = DateTime.Parse(FinEvento);
                items.HoraInicio = HoraInicio;
                items.HoraFin = HoraFin;
                items.Ubicacion = Ubicacion;
                items.Descripcion = Descripcion;
            }

            var boton1 = Request.Form["Boton1"];
            var boton2 = Request.Form["Boton2"];
            if (boton1 != null) //Guardar
            {
                n.marketingrecordatorios.Modificar_Registro(items);
                return View("Index", n.marketing.Seleccionar_Todos());
            }
            else if (boton2 != null) //Guardar y enviar para pruebas
            {
                if (string.IsNullOrEmpty(agenda)) //No incluye agenda
                {
                    if (contenido.Contains("[nombre]"))
                    {
                        var usuario = n.usuarios.Seleccionar_PorId(usuariosrolessesion.Usuarios.Id.ToString());
                        string nombre = usuario.Usuarios.Nombre + " " + usuario.Usuarios.ApellidoPaterno + " " + usuario.Usuarios.ApellidoMaterno;
                        contenido = contenido.Replace("[nombre]", nombre);
                        correo.EnviarCorreo(usuario.Usuarios.Correo, "Prueba de Visibilidad", asunto, contenido);
                        return RedirectToRoute(new { controller = "Marketing", action = "RecordatoriosModificar", rec = recordatorio, idcamp = idcampaña });
                    }
                    else
                    {
                        n.marketingrecordatorios.Modificar_Registro(items);
                        var usuario = n.usuarios.Seleccionar_PorId(usuariosrolessesion.Usuarios.Id.ToString());
                        correo.EnviarCorreo(usuario.Usuarios.Correo, "Prueba de Visibilidad", asunto, contenido);
                        return RedirectToRoute(new { controller = "Marketing", action = "RecordatoriosModificar", rec = recordatorio, idcamp = idcampaña });
                    }
                }
                else //Incluye agenda
                {
                    if (contenido.Contains("[nombre]"))
                    {
                        var usuario = n.usuarios.Seleccionar_PorId(usuariosrolessesion.Usuarios.Id.ToString());
                        string nombre = usuario.Usuarios.Nombre + " " + usuario.Usuarios.ApellidoPaterno + " " + usuario.Usuarios.ApellidoMaterno;
                        var clavecampaign = n.marketing.Seleccionar_PorId(idcampaña);
                        contenido = contenido.Replace("[nombre]", nombre);
                        string fechainicioevento = clavecampaign.InicioEvento.ToString("yyyy/MM/dd") + " " + clavecampaign.HoraInicio + ":00";
                        string fechafinevento = clavecampaign.FinEvento.ToString("yyyy/MM/dd") + " " + clavecampaign.HoraFin + ":00";
                        correo.AgendaParaEventos(usuario.Usuarios.Correo
                                , ""
                                , "Eventos ASAE"
                                , "Prueba de Visibilidad"
                                , contenido
                                , clavecampaign.Consecutivo + ".ics"
                                , asunto
                                , fechainicioevento
                                , fechafinevento
                                , nombre
                                , usuario.Usuarios.Correo
                                , asunto
                                , Descripcion
                                , Ubicacion
                                );
                        return RedirectToRoute(new { controller = "Marketing", action = "RecordatoriosModificar", rec = recordatorio, idcamp = idcampaña });
                    }
                    else
                    {
                        var usuario = n.usuarios.Seleccionar_PorId(usuariosrolessesion.Usuarios.Id.ToString());
                        string nombre = usuario.Usuarios.Nombre + " " + usuario.Usuarios.ApellidoPaterno + " " + usuario.Usuarios.ApellidoMaterno;
                        var clavecampaign = n.marketing.Seleccionar_PorId(idcampaña);
                        string fechainicioevento2 = clavecampaign.InicioEvento.ToString("yyyy/MM/dd") + " " + clavecampaign.HoraInicio + ":00";
                        string fechafinevento2 = clavecampaign.FinEvento.ToString("yyyy/MM/dd") + " " + clavecampaign.HoraFin + ":00";
                        correo.AgendaParaEventos(usuario.Usuarios.Correo
                                , ""
                                , "Eventos ASAE"
                                , "Prueba de Visibilidad"
                                , contenido
                                , clavecampaign.Consecutivo + ".ics"
                                , asunto
                                , fechainicioevento2
                                , fechafinevento2
                                , nombre
                                , usuario.Usuarios.Correo
                                , asunto
                                , Descripcion
                                , Ubicacion
                                );
                        return RedirectToRoute(new { controller = "Marketing", action = "RecordatoriosModificar", rec = recordatorio, idcamp = idcampaña });

                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult RecordatoriosEnviar(string IdCampaña, string rec)
        {
            var recordatoriocorreo = n.marketingrecordatorios.Seleccionar_PorId(rec);
            if (recordatoriocorreo.EnviarA == 1)
            {
                try
                {
                    string contactoNombre = "";
                    string contactoApellidos = "";
                    string mailBody = "";
                    string mailSubject = "";
                    bool emailSended = false;
                    int contadorEnviados = 0;
                    int contadorNoEnviado = 0;

                    foreach (var items in n.marketingcontactos.Seleccionar_PorIdCampaña(IdCampaña))
                    {

                        try
                        {
                            emailSended = false;
                            contactoNombre = items.Contactos.Nombre;
                            contactoApellidos = items.Contactos.ApellidoPaterno + " " + items.Contactos.ApellidoMaterno;
                            mailBody = recordatoriocorreo.Cuerpo.Replace("[nombre]", contactoNombre).Replace("[apellidos]", contactoApellidos);
                            mailSubject = recordatoriocorreo.Asunto.Replace("[nombre]", contactoNombre).Replace("[apellidos]", contactoApellidos);

                            emailSended = correo.EnviarCorreo(items.Contactos.Correo, "ASAE Consultores", mailSubject, mailBody);
                            correo.EnviarCorreo("ruben.marinez@protonmail.com", "ASAE Consultores", mailSubject, "Correo enviado a: " + contactoNombre + " " + contactoApellidos + " (" + items.Contactos.Correo + ") <br/><br/><br/>" + mailBody);

                            if (emailSended)
                            {
                                contadorEnviados += 1;
                            }
                            else
                            {
                                contadorNoEnviado += 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            // ### Pendiente: Agregar log de errores en envio de correos
                            // ### Pendiente: Agregar log de correos enviados correctamente

                            string _errorMessage = ex.Message.ToString();
                        }

                        //if (recordatoriocorreo.Cuerpo.Contains("[nombre]"))
                        //{
                        //    recordatoriocorreo.Cuerpo = recordatoriocorreo.Cuerpo.Replace("[nombre]", nombre);
                        //    correo.EnviarCorreo(items.Contactos.Correo, "ASAE Consultores", mailSubject, recordatoriocorreo.Cuerpo);
                        //}
                        //else
                        //{
                        //    correo.EnviarCorreo(items.Contactos.Correo, "Prueba de Visibilidad", recordatoriocorreo.Asunto, recordatoriocorreo.Cuerpo);
                        //}
                    }
                }
                catch (Exception ex)
                {
                    // ### Pendiente: Agregar log de errores en envio de correos
                    // ### Pendiente: Agregar log de correos enviados correctamente

                    string _errorMessage = ex.Message.ToString();
                }
            }
            else if (recordatoriocorreo.EnviarA > 1)
            {
                // Comentado para su revisión
                //foreach (var items in n.marketingcontactos.Seleccionar_PorIdCampañaFiltrado(IdCampaña, recordatoriocorreo.EnviarA.ToString()))
                //{
                //    if (recordatoriocorreo.Cuerpo.Contains("[nombre]"))
                //    {
                //        string nombre = items.Contactos.Nombre + " " + items.Contactos.ApellidoPaterno + " " + items.Contactos.ApellidoMaterno;
                //        recordatoriocorreo.Cuerpo = recordatoriocorreo.Cuerpo.Replace("[nombre]", nombre);
                //        correo.EnviarCorreo(items.Contactos.Correo, "Prueba de Visibilidad", recordatoriocorreo.Asunto, recordatoriocorreo.Cuerpo);
                //    }
                //    else
                //    {
                //        correo.EnviarCorreo(items.Contactos.Correo, "Prueba de Visibilidad", recordatoriocorreo.Asunto, recordatoriocorreo.Cuerpo);
                //    }
                //}
            }

            TempData["Mensaje"] = "<div class='alert alert-success' role'alert'>Se envió el correo a los contactos de la campaña</div>";
            return RedirectToRoute(new { controller = "Marketing", action = "Recordatorios", IdCampaña = IdCampaña });
        }

        [HttpGet]
        public ActionResult Estadisticas(string IdCampaña)
        {
            return View(n.marketing.Seleccionar_PorId(IdCampaña));
        }

        public ActionResult Listado01(string IdCampaña)
        {
            return View(n.marketingcontactos.Seleccionar_Listado01PorIdCampaña(IdCampaña));
        }

        public ActionResult Listado02(string IdCampaña)
        {
            return View(n.marketingcontactos.Seleccionar_Listado02PorIdCampaña(IdCampaña));
        }

        public ActionResult Listado03(string IdCampaña)
        {
            return View(n.marketingcontactos.Seleccionar_Listado03PorIdCampaña(IdCampaña));
        }

        public ActionResult Listado04(string IdCampaña)
        {
            return View(n.marketingcontactos.Seleccionar_Listado04PorIdCampaña(IdCampaña));
        }

        public ActionResult ArchivoICS(string IdCampaña)
        {
            GenerarArchivoICS(IdCampaña);
            return View();
        }

        [HttpPost]
        public ActionResult ExcelContactos(CRM.Models.Models.Marketing marketing)
        {
            string handle = Guid.NewGuid().ToString();
            DataSet dsProspectos = n.marketingcontactos.ProspectosActividadesReporte(marketing.Id);
            string folderPath = Path.Combine(Server.MapPath("~/Archivos"));
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dsProspectos);
                wb.SaveAs(folderPath + @"/" + handle.ToString() + ".xlsx");
            }

            // Note we are returning a filename as well as the handle
            string message = "SUCCESS";
            return Json(new { Message = message, FileGuid = handle.ToString() + ".xlsx", JsonRequestBehavior.AllowGet });
        }

        [HttpGet]
        [DeleteFileAttribute] //Action Filter, it will auto delete the file after download,  //I will explain it later
        public ActionResult Download(string fileName)
        {
            string folderPath = Path.Combine(Server.MapPath("~/Archivos"));
            folderPath += @"\" + fileName;

            //return the file for download, this is an Excel 
            //so I set the file content type to "application/vnd.ms-excel"
            return File(folderPath, "application/vnd.ms-excel", fileName);
        }


        #region ******************************** Procedimientos Json 

        public JsonResult MarketingContactoSeleccionar(string idcontacto, string idcampaña)
        {
            return Json(n.marketingcontactos.Seleccionar_PorIdCampañaIdContacto(idcampaña, idcontacto), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MarketingContactoActualizar(string idcontacto, string idcampaña, string seguimiento, string asistencia)
        {
            return Json(n.marketingcontactos.Modificar_Registro(idcampaña, idcontacto, seguimiento, asistencia), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MarketingContactoAsistencia(string idcontacto, string idcampaña, string estado)
        {
            return Json(n.marketingcontactos.Modificar_Asistencia(idcontacto, idcampaña, estado), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MarketingContactoEliminar(string idcontacto, string idcampaña)
        {
            return Json(n.marketingcontactos.Eliminar_Registro(idcampaña, idcontacto), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MarketingContactoAgregarACampaña(string idcampaña, string idcontacto)
        {
            n.marketingcontactos.Agregar_Registro(idcampaña, idcontacto);
            return Json(n.marketingcontactos.Seleccionar_PorIdCampaña(idcampaña), JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult MarketingRecordatoriosEnvioFiltrado(string idcampaña, string idregistro, string opcion)
        {
            int procesado = 0;
            var recordatoriocorreo = n.marketingrecordatorios.Seleccionar_PorId(idregistro);
            foreach (var items in n.marketingcontactos.Seleccionar_PorIdCampañaFiltrado(idcampaña, opcion))
            {
                if (recordatoriocorreo.Cuerpo.Contains("[nombre]"))
                {
                    string nombre = items.Contactos.Nombre + " " + items.Contactos.ApellidoPaterno + " " + items.Contactos.ApellidoMaterno;
                    recordatoriocorreo.Cuerpo = recordatoriocorreo.Cuerpo.Replace("[nombre]", nombre);
                    correo.EnviarCorreo(items.Contactos.Correo, "Prueba de Visibilidad", recordatoriocorreo.Asunto, recordatoriocorreo.Cuerpo);
                }
                else
                {
                    correo.EnviarCorreo(items.Contactos.Correo, "Prueba de Visibilidad", recordatoriocorreo.Asunto, recordatoriocorreo.Cuerpo);
                }
                procesado = 1;

            }
            //["Mensaje"] = "<div class='alert alert-success' role'alert'>Se envió el correo a todos los contactos de la campaña</div>";
            //return Json(n.marketingcontactos.Seleccionar_PorIdCampañaFiltrado(idcampaña, opcion), JsonRequestBehavior.AllowGet);
            return Json(procesado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EstadisticasProspectos(CRM.Models.Models.Marketing marketing)
        {
            DataSet dsResumenProspectos = n.marketingcontactos.ProspectosActividadesResumen(marketing.Id);
            int contactosTotal = 0;
            int actividadesTotal = 0;
            contactosTotal = int.Parse(dsResumenProspectos.Tables[0].Rows[0][0].ToString());
            actividadesTotal = int.Parse(dsResumenProspectos.Tables[1].Rows[0][0].ToString());
            string message = "SUCCESS";
            return Json(new { Message = message, totalProspectos = contactosTotal, totalActividades = actividadesTotal, JsonRequestBehavior.AllowGet });
        }


        [HttpPost]
        public JsonResult DetalleActividadesBar(CRM.Models.Models.Marketing marketing)
        {
            DataSet dsResumenProspectos = n.marketingcontactos.ProspectosActividadesResumen(marketing.Id);

            int _valorMaximo = int.Parse(dsResumenProspectos.Tables[1].Rows[0]["actividadesTotal"].ToString());
            int _Tarea = int.Parse(dsResumenProspectos.Tables[2].Rows[0]["Totales"].ToString());
            int _Llamada = int.Parse(dsResumenProspectos.Tables[2].Rows[1]["Totales"].ToString());
            int _EMail = int.Parse(dsResumenProspectos.Tables[2].Rows[2]["Totales"].ToString());
            int _Reunion = int.Parse(dsResumenProspectos.Tables[2].Rows[3]["Totales"].ToString());
            int _Videoconferencia = int.Parse(dsResumenProspectos.Tables[2].Rows[4]["Totales"].ToString());
            int _Comentario = int.Parse(dsResumenProspectos.Tables[2].Rows[5]["Totales"].ToString());
            int _Whatsapp = int.Parse(dsResumenProspectos.Tables[2].Rows[6]["Totales"].ToString());
            int _EMailMarketing = int.Parse(dsResumenProspectos.Tables[2].Rows[7]["Totales"].ToString());

            string message = "SUCCESS";
            return Json(new
            {
                Message = message
                , valorMaximo = _valorMaximo
                , Tarea = _Tarea
                , Llamada = _Llamada
                , EMail = _EMail
                , Reunion = _Reunion
                , Videoconferencia = _Videoconferencia
                , Comentario = _Comentario
                , Whatsapp = _Whatsapp
                , EMailMarketing = _EMailMarketing
                , JsonRequestBehavior.AllowGet
            });
        }

        public JsonResult EstadisticasContactos(string idevento)
        {
            return Json(n.marketingcontactos.Seleccionar_EstadisticasContactosPorIdEvento(idevento), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EstadisticasContactosPorTipoIngreso(string idcampaña)
        {
            return Json(n.marketingcontactos.Seleccionar_EstadisticasPorTipoIngreso(idcampaña), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AsaeWebExternasEstadisticasContactos(string idevento)
        {
            return Json(n.asaewebusuarioseventos.Seleccionar_EstadisticasContactosPorIdEvento(idevento), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MarketingCorreoPlantilla(string id)
        {
            return Json(n.marketingcorreoplantillas.Seleccionar_PorId(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubirImagenCorreo()
        {
            // checar si el usuario seleccionó un archivo para subir
            if (Request.Files.Count > 0)
            {
                try
                {
                    //var clasificado = Request.Form.GetValues(0);
                    //var descripcion = Request.Form.GetValues(1);
                    //var version = Request.Form.GetValues(2);
                    //var vigencia = Request.Form.GetValues(3);

                    HttpFileCollectionBase files = Request.Files;

                    HttpPostedFileBase file = files[0];
                    string fileName = file.FileName;

                    // crear un directorio para subir los archivos si no existe
                    string ruta1 = Path.Combine(Server.MapPath("~/ImagenesCorreo/"), fileName);

                    // guardar el archivo
                    file.SaveAs(ruta1);

                    //if (ruta1.Contains(fileName))
                    //{
                    //    //Agregar los archivos por aqui
                    //    Models.Models.DocumentosASAE items = new Models.Models.DocumentosASAE();
                    //    items.Nombre = fileName;
                    //    items.IdUsuario = int.Parse(Session["IdUsuario"].ToString());
                    //    items.Clasificacion = int.Parse(clasificado[0]);
                    //    items.Descripcion = descripcion[0];
                    //    items.Version = version[0];
                    //    items.Vigencia = vigencia[0] == "1" ? true : false;
                    //    n.documentosasae.Agregar_Registro(items);
                    //}
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json("error" + e.Message);
                }
            }

            return Json("Nada");
        }
        #endregion

        private void GenerarArchivoICS(string idcampaña)
        {
            var detalle = n.marketing.Seleccionar_PorId(idcampaña);

            var inicio = detalle.InicioEvento.ToString("yyyyMMdd") + "T" + detalle.HoraInicio.Replace(":","") + "00";
            var fin = detalle.FinEvento.ToString("yyyyMMdd") + "T" + detalle.HoraFin.Replace(":", "") + "00";

            var usuario = n.usuarios.Seleccionar_PorId(Session["IdUsuario"].ToString());
            var nombre = usuario.Usuarios.Nombre + " " + usuario.Usuarios.ApellidoPaterno + " " + usuario.Usuarios.ApellidoMaterno;
            var correo = usuario.Usuarios.Correo;
            
            string ruta = Path.Combine(Server.MapPath("~/ICS/"), detalle.Consecutivo + ".ics");

            try
            {
                //Si el archivo ya existe, borrarlo
                if (System.IO.File.Exists(ruta))
                {
                    System.IO.File.Delete(ruta);
                }

                using (StreamWriter file = new StreamWriter(ruta, true))
                {
                    file.WriteLine("BEGIN:VCALENDAR");
                    file.WriteLine("VERSION:2.0");
                    file.WriteLine("PRODID: -//hacksw/handcal//NONSGML v1.0//EN");
                    file.WriteLine("BEGIN:VEVENT");
                    file.WriteLine("UID:" + detalle.Consecutivo);
                    file.WriteLine("DTSTAMP:" + inicio); 
                    file.WriteLine("ORGANIZER;CN=" + nombre + " MAILTO:" + correo);
                    file.WriteLine("DTSTART:" + inicio);
                    file.WriteLine("DTEND:" + fin);
                    file.WriteLine("SUMMARY:" + detalle.Nombre);
                    file.WriteLine("DESCRIPTION:" + detalle.Descripcion);
                    file.WriteLine("LOCATION:" + detalle.Ubicacion);
                    file.WriteLine("END:VEVENT");
                    file.WriteLine("END:VCALENDAR");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class DeleteFileAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Flush();

            //convert the current filter context to file and get the file path
            string filePath = (filterContext.Result as FilePathResult).FileName;

            //delete the file after download
            System.IO.File.Delete(filePath);
        }
    }

    public class ReporteActividadesDetalle {
        public int ActividadId { get; set; }
        public string Actividad { get; set; }
        public int Totales { get; set; }
    }
}
