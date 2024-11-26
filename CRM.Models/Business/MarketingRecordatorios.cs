using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class MarketingRecordatorios : CRM.Models.Repository.MarketingRecordatoriosRepository
    {
        public List<mod.MarketingRecordatorios> Seleccionar_PorIdCampaña(string idcampaña)
        {
            return SeleccionarPorIdCampaña(idcampaña);
        }

        public mod.MarketingRecordatorios Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public int Agregar_Registro(mod.MarketingRecordatorios items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(mod.MarketingRecordatorios items)
        {
            return Modificar(items);
        }
    }
}
