using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class ModelosLista
    {
        public List<Actividades> Actividades { get; set; }
        public List<Aviso> Aviso { get; set; }
        public List<Bitacora> Bitacora { get; set; }
        public List<Clasificacion> Clasificacion { get; set; }
        public List<Configuracion> Configuracion { get; set; }
        public List<Contactos> Contactos { get; set; }
        public List<ContactosActividades> ContactosActividades { get; set; }
        public List<DocumentosASAE> DocumentosASAE { get; set; }
        public List<Empresas> Empresas { get; set; }
        public List<Escalacion> Escalacion { get; set; }
        public List<Marketing> Marketing { get; set; }
        public List<MarketingContactos> MarketingContactos { get; set; }
        public List<MarketingSeguimiento> MarketingSeguimiento { get; set; }
        public List<Mensajes> Mensajes { get; set; }
        public List<Moneda> Moneda { get; set; }
        public List<Oportunidades> Oportunidades { get; set; }
        public List<OportunidadesActividades> OportunidadesActividades { get; set; }
        public List<OportunidadesEmpresasContactosUsuarios> OECU { get; set; }
        public List<OportunidadesResponsables> OportunidadesResponsables { get; set; }
        public List<Roles> Roles { get; set; }
        public List<Subclasificacion> Subclasificacion { get; set; }
        public List<Tareas> Tareas { get; set; }
        public List<TareasUsuariosContactosEmpresas> TUCE { get; set; }
        public List<TipoPersona> TipoPersona { get; set; }
        public List<UDN> UDN { get; set; }
        public List<Usuarios> Usuarios { get; set; }
        public List<UsuariosRoles> UsuariosRoles { get; set; }

        public ModelosLista()
        {
            Actividades = new List<Actividades>();
            Aviso = new List<Aviso>();
            Bitacora = new List<Bitacora>();
            Clasificacion = new List<Clasificacion>();
            Configuracion = new List<Configuracion>();
            Contactos = new List<Contactos>();
            ContactosActividades = new List<ContactosActividades>();
            DocumentosASAE = new List<DocumentosASAE>();
            Empresas = new List<Empresas>();
            Escalacion = new List<Escalacion>();
            Marketing = new List<Marketing>();
            MarketingContactos = new List<MarketingContactos>();
            MarketingSeguimiento = new List<MarketingSeguimiento>();
            Mensajes = new List<Mensajes>();
            Moneda = new List<Moneda>();
            Oportunidades = new List<Oportunidades>();
            OportunidadesActividades = new List<OportunidadesActividades>();
            OECU = new List<OportunidadesEmpresasContactosUsuarios>();
            OportunidadesResponsables = new List<OportunidadesResponsables>();
            Roles = new List<Roles>();
            Subclasificacion = new List<Subclasificacion>();
            Tareas = new List<Tareas>();
            TipoPersona = new List<TipoPersona>();
            TUCE = new List<TareasUsuariosContactosEmpresas>();
            UDN = new List<UDN>();
            Usuarios = new List<Usuarios>();
            UsuariosRoles = new List<UsuariosRoles>();
        }

    }
}
