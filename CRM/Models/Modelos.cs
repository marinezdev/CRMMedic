using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    /// <summary>
    /// Clase concentradora de todos los modelos
    /// </summary>
    public class Modelos
    {
        public Actividades Actividades { get; set; }
        public Bitacora Bitacora { get; set; }
        public Clasificacion Clasificacion { get; set; }
        public Configuracion Configuracion { get; set; }
        public Contactos Contactos { get; set; }
        public ContactosActividades ContactosActividades { get; set; }
        public Empresas Empresas { get; set; }
        public Escalacion Escalacion { get; set; }
        public Oportunidades Oportunidades { get; set; }
        public OportunidadesActividades OportunidadesActividades { get; set; }
        public OportunidadesEmpresasContactosUsuarios OECU { get; set; }
        public OportunidadesResponsables OportunidadesResponsables { get; set; }
        public Roles Roles { get; set; }
        public Subclasificacion Subclasificacion { get; set; }
        public Tareas Tareas { get; set; }
        public TareasUsuariosContactosEmpresas TUCE { get; set; }
        public Usuarios Usuarios { get; set; }
        public UsuariosRoles UsuariosRoles { get; set; }

        public Modelos()
        {
            Actividades = new Actividades();
            Bitacora = new Bitacora();
            Clasificacion = new Clasificacion();
            Configuracion = new Configuracion();
            Contactos = new Contactos();
            ContactosActividades = new ContactosActividades();
            Empresas = new Empresas();
            Escalacion = new Escalacion();
            Oportunidades = new Oportunidades();
            OportunidadesActividades = new OportunidadesActividades();
            OECU = new OportunidadesEmpresasContactosUsuarios();
            OportunidadesResponsables = new OportunidadesResponsables();
            Roles = new Roles();
            Subclasificacion = new Subclasificacion();
            Tareas = new Tareas();
            TUCE = new TareasUsuariosContactosEmpresas();
            Usuarios = new Usuarios();
            UsuariosRoles = new UsuariosRoles();
        }
    }
}