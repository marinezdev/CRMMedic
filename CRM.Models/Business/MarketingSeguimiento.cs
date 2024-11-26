using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class MarketingSeguimiento : Repository.MarketingSeguimientoRepositorio
    {
        public mod.MarketingModelos Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public List<mod.MarketingSeguimiento> Seleccionar_Por_IdCampaña(string idcampaña)
        {
            return SeleccionarPorIdCampaña(idcampaña);
        }

        public int Agregar_Registro(string idcampaña, string seguimiento)
        {
            return Agregar(idcampaña, seguimiento);
        }

        public int Modificar_Registro(string idcampaña, string seguimiento)
        {
            return Modificar(idcampaña, seguimiento);
        }
    }
}
