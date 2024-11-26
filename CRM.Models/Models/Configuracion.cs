using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class Configuracion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public string TituloIntermedioPestaña { get; set; }
        public string ClaseLogo { get; set; }
        public string ClaseNavegacion { get; set; }
    }
}