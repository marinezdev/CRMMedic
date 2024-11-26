using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class MarketingSeguimiento
    {
         public int Id { get; set; }
        public int IdCampaña { get; set; }
        public DateTime Fecha { get; set; }
        public string Seguimiento { get; set; }
    }
}
