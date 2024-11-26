using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class UsuariosEmpresasOportunidadesDetalle
    {
        public OportunidadesEmpresasContactosUsuarios oecu { get; set; }
        public OportunidadesResponsables OportunidadesResponsables { get; set; }
        public UsuariosEmpresasOportunidades ueo { get; set; } //TODO SE VA
        public Usuarios Usuarios { get; set; }

        public UsuariosRoles UsuariosRoles { get; set; }
        public Contactos Contactos { get; set; }
        public Oportunidades Oportunidades { get; set; }
        public Empresas Empresas { get; set; }
        public Bitacora Bitacora { get; set; }
        public BitacoraUsuariosDetalle BitacoraUsuariosDetalle { get; set; }
        public ArchivosBitacora ArchivosBitacora { get; set; }
        public ClasificacionRolesContactos ClasificacionRolesContactos { get; set; }
        public ContactoRol ContactoRol { get; set; }

        public UsuariosEmpresasOportunidadesDetalle()
        {
            oecu = new OportunidadesEmpresasContactosUsuarios();
            OportunidadesResponsables = new OportunidadesResponsables();
            ueo = new UsuariosEmpresasOportunidades(); //TODO SE VA
            Usuarios = new Usuarios();
            UsuariosRoles = new UsuariosRoles();
            Contactos = new Contactos();
            Oportunidades = new Oportunidades();
            Empresas = new Empresas();
            Bitacora = new Bitacora();
            BitacoraUsuariosDetalle = new BitacoraUsuariosDetalle();
            ArchivosBitacora = new ArchivosBitacora();
            ClasificacionRolesContactos = new ClasificacionRolesContactos();
            ContactoRol = new ContactoRol();
        }
    }
}