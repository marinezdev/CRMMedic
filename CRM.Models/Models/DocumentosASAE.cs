using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class DocumentosASAE
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; } 
        public int Clasificacion { get; set; }
        public int SubClasificacion { get; set; }
        public string Descripcion { get; set; }
        public string Version { get; set; }
        public bool Vigencia { get; set; }
        public string VersionUsuarios { get; set; }
        public DateTime FechaOficial { get; set; }


    }
}
