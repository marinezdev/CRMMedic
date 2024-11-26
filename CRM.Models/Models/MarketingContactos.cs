using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class MarketingContactos
    {
        public int Id { get; set; }
        public int IdContacto { get; set; }
        public int IdCampaña { get; set; }   
        public string Seguimiento { get; set; } 
        public int Ingreso { get; set; }
        public bool Asistencia { get; set; }
        public DateTime FechaAlta { get; set; }

        //Para estadísticas

        public int Totales { get; set; }
        public int Registrados { get; set; }
        public int Confirmados { get; set; }
        public int Asistieron { get; set; }

        //Para conteos de ingresos

        public int CRM { get; set; }
        public int Facebook { get; set; }
        public int Web { get; set; }
        public int Otros { get; set; }
    }
}
