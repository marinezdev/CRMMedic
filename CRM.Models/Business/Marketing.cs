using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Marketing : Repository.MarketingRepositorio
    {
        public List<mod.Marketing> Seleccionar_Todos()
        {
            return Seleccionar();
        }

        public mod.Marketing Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public bool Seleccionar_EstadoCampaña(string idcampaña)
        {
            if (SeleccionarEstadoCampaña(idcampaña) == 1)
                return true;
            else
                return false;
        }

        public int Agregar_Registro(mod.Marketing items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(mod.Marketing items)
        {
            return Modificar(items);
        }



    }
}
