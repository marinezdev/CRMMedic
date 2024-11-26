using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public bool? Activo { get; set; }
        public string PaginaInicio { get; set; }

    }
}