using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class EtapasBitacora : Repository.EtapasBitacoraRepositorio
    {
        public List<mod.EtapasBitacora> Seleccionar_PorIdOportunidad(string id)
        { 
            return SeleccionarPorIdOportunidad(id);
        }

        public List<mod.Modelos> Seleccionar_ResponsablesHanLeido(string IdOportunidad)
        {
            return SeleccionarResponsablesHanLeido(IdOportunidad);
        }

        public List<mod.Modelos> Seleccionar_ResponsablesEnProceso(string IdOportunidad)
        { 
            return SeleccionarResponsablesEnProceso(IdOportunidad);
        }

        public List<mod.Modelos> Seleccionar_ResponsablesTerminado(string IdOportunidad)
        { 
            return SeleccionarResponsablesTerminado(IdOportunidad);
        }

        public string Seleccionar_ResponsablesPorOportunidad(string IdOportunidad)
        { 
            return SeleccionarResponsablesPorOportunidad(IdOportunidad);
        }
    }
}
