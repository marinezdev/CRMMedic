using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class FechaVencimientoCambios
    {
        public int Id { get; set; }
        public DateTime FechaVencimientoAnterior { get; set; }
        public int IdOportunidad { get; set; }
        public int IdUsuario { get; set; }
    }
}
