using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class MarketingCorreoPlantillas : Repository.MarketingCorreoPlantillasRepositorio
    {
        public List<mod.MarketingCorreoPlantillas> Seleccionar_()
        {
            return Seleccionar();
        }

        public mod.MarketingCorreoPlantillas Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

    }
}
