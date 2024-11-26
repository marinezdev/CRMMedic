using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class OportunidadesImportes
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public float Importe { get; set; }
        public int Moneda { get; set; }
        public float TipoCambio { get; set; }
        public int Rubro { get; set; }
        public string Observaciones { get; set; }

        public string MonedaNombre { get; set; }
        public string RubroNombre { get; set; }
    }
}
