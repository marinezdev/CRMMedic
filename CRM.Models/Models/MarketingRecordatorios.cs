using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class MarketingRecordatorios
    {
        public int Id { get; set; }
        public int IdCampaña { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; } 
        public DateTime Envio { get; set; }
        public int EnviarA { get; set; }

        public int Agenda { get; set; }
        public DateTime InicioEvento { get; set; }
        public DateTime FinEvento { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }
    }
}
