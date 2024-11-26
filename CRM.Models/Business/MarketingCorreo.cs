using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class MarketingCorreo : Repository.MarketingCorreoRepositorio
    {
        public mod.MarketingCorreo Seleccionar_PorIdCampaña(string idcampaña)
        {
            return SeleccionarPorIdCampaña(idcampaña);
        }

        public int Agregar_Registro(string idcampaña, string asunto, string cuerpo)
        {
            return Agregar(idcampaña, asunto, cuerpo);
        }

        public int Modificar_Registro(string idcampaña, string asunto, string cuerpo)
        {
            return Modificar(idcampaña, asunto, cuerpo);
        }

    }
}
