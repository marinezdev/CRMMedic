using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class ClassSubClass
    {
        public int IdOportunidad { get; set; }
        public string Campo1 { get; set; }
        public string Campo2 { get; set; }
        public string Campo3 { get; set; }
        public string Campo4 { get; set; }
        public int IdClasificacion { get; set; }
        public int IdSubClasificacion { get; set; }
    }
}