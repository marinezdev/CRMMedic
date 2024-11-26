using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class EstadoOportunidad
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int Estado { get; set; }
        public string EstadoNombre { get; set; }
        public string Comentarios { get; set; }
        public DateTime Fecha { get; set; }
    }
}
