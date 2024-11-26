using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class DocumentosASAEUsuarios
    {
        public int Id { get; set; }
        public int IdDocumento { get; set; }
        public int IdUsuario { get; set; }
        public int IdClasificacion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
