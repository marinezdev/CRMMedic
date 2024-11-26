using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class MarketingModelos
    {
        public Marketing Marketing { get; set; }
        public MarketingContactos MarketingContactos { get; set; }
        public MarketingCorreo MarketingCorreo { get; set; }
        public MarketingSeguimiento MarketingSeguimiento { get; set; }

        public MarketingModelos()
        {
            Marketing = new Marketing();
            MarketingContactos = new MarketingContactos();
            MarketingCorreo = new MarketingCorreo();
            MarketingSeguimiento = new MarketingSeguimiento(); 
        }
    }
}
