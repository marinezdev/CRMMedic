using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Archivos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdOportunidad { get; set; }
    }
}