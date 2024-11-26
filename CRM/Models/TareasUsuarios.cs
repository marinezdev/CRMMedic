using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class TareasUsuarios
    {
        public Tareas Tareas { get; set; }
        public Usuarios Usuarios { get; set; }

        public TareasUsuarios()
        {
            Tareas = new Tareas();
            Usuarios = new Usuarios();
        }
    }
}