using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Contactos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public int Estado { get; set; }        
        public string Cargo { get; set; }
        public string FechaCumpleaños { get; set; }
        public int TipoContacto { get; set; }
        public string Notas { get; set; }
        public DateTime Alta { get; set; }
        public int UsuarioAlta { get; set; }
        public int ContactoUsuario { get; set; }
        public int IdUsuarioResponsable { get; set; }
        public int IdConfiguracion { get; set; }
    }
}