using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class Metricas
    {
        public int Id { get; set; }
        public string Metrica { get; set; }
        public string Comprador { get; set; }
        public string Criterios { get; set; }
        public string Proceso { get; set; }
        public string Necesidad { get; set; }
        public string Campeon { get; set; }
        public string Fulcro { get; set; }
        public string Otros { get; set; }
        public int IdOportunidad { get; set; }
    }
}
