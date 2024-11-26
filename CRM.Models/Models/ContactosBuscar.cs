using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class ContactosBuscar
    {
        public int IdContacto { get; set; }
        public int IdEmpresa { get; set; }
        public string Contacto { get; set; }
        public string Empresa { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
    }
}