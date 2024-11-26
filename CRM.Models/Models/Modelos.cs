using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    /// <summary>
    /// Clase concentradora de todos los modelos
    /// </summary>
    public class Modelos
    {
        public Actividades Actividades { get; set; }
        public ActividadesContactoDetalle ActividadesContactoDetalle { get; set; }
        public Aviso Aviso { get; set; }
        public Bitacora Bitacora { get; set; }
        public Clasificacion Clasificacion { get; set; }
        public Configuracion Configuracion { get; set; }
        public Contactos Contactos { get; set; }
        public ContactosActividades ContactosActividades { get; set; }
        public DocumentosASAE DocumentosASAE { get; set; }
        public DocumentosASAEHistorico DocumentosASAEHistorico { get; set; }
        public DocumentosASAEUsuarios DocumentosASAEUsuarios { get; set; }
        public Empresas Empresas { get; set; }
        public Escalacion Escalacion { get; set; }
        public Marketing Marketing { get; set; }
        public MarketingContactos MarketingContactos { get; set; }
        public MarketingSeguimiento MarketingSeguimiento { get; set; }
        public Mensajes Mensajes { get; set; }
        public Moneda Moneda { get; set; }
        public Oportunidades Oportunidades { get; set; }
        public OportunidadesActividades OportunidadesActividades { get; set; }
        public OportunidadesEmpresasContactosUsuarios OECU { get; set; }
        public OportunidadesResponsables OportunidadesResponsables { get; set; }
        public Roles Roles { get; set; }
        public Subclasificacion Subclasificacion { get; set; }
        public Tareas Tareas { get; set; }
        public TareasUsuariosContactosEmpresas TUCE { get; set; }
        public TipoPersona TipoPersona { get; set; }
        public UDN UDN { get; set; }
        public Usuarios Usuarios { get; set; }
        public UsuariosRoles UsuariosRoles { get; set; }

        public Modelos()
        {
            Actividades = new Actividades();
            ActividadesContactoDetalle = new ActividadesContactoDetalle();
            Aviso = new Aviso();
            Bitacora = new Bitacora();
            Clasificacion = new Clasificacion();
            Configuracion = new Configuracion();
            Contactos = new Contactos();
            ContactosActividades = new ContactosActividades();
            DocumentosASAE = new DocumentosASAE();
            DocumentosASAEHistorico = new DocumentosASAEHistorico();
            DocumentosASAEUsuarios = new DocumentosASAEUsuarios();
            Empresas = new Empresas();
            Escalacion = new Escalacion();
            Marketing = new Marketing();
            MarketingContactos = new MarketingContactos();
            MarketingSeguimiento = new MarketingSeguimiento();
            Mensajes = new Mensajes();
            Moneda = new Moneda();
            Oportunidades = new Oportunidades();
            OportunidadesActividades = new OportunidadesActividades();
            OECU = new OportunidadesEmpresasContactosUsuarios();
            OportunidadesResponsables = new OportunidadesResponsables();
            Roles = new Roles();
            Subclasificacion = new Subclasificacion();
            Tareas = new Tareas();
            TipoPersona = new TipoPersona();
            TUCE = new TareasUsuariosContactosEmpresas();
            UDN = new UDN();
            Usuarios = new Usuarios();
            UsuariosRoles = new UsuariosRoles();
        }
    }

    public class Mensajes
    { 
        public bool Ok { get; set; }
        public string Mensaje { get; set; }
    }
}