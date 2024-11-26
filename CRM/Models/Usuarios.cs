using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Clave { get; set; }
        public string Contraseña { get; set; }
        public string Correo { get; set; }
        public bool Activo { get; set; }
    }
}