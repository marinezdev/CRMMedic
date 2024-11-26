using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class TareasUsuariosContactosEmpresas
    {
        public Tareas Tareas { get; set; }
        public Usuarios Usuarios { get; set; }
        public Contactos Contactos { get; set; }
        public Empresas Empresas { get; set; }

        public TareasUsuariosContactosEmpresas()
        {
            Tareas = new Tareas();
            Usuarios = new Usuarios();
            Contactos = new Contactos();
            Empresas = new Empresas();
        }

    }
}