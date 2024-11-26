using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Clasificacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdConfiguracion { get; set; }
    }
}