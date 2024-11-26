using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class EtapasOportunidad : Repository.EtapasOportunidadRepositorio
    {
        public List<mod.EtapasOportunidad> Seleccionar_PorIdOportunidad(string id)
        { 
            return SeleccionarPorIdOportunidad(id);
        }

        public List<mod.EtapasOportunidad> Seleccionar_EtapasEstadoPorIdOportunidad(string id)
        { 
            return SeleccionarEtapasEstadoPorIdOportunidad(id);
        }

        public string Selecccionar_SiEtapaCompletada(string id, string etapa)
        { 
            return SelecccionarSiEtapaCompletada(id, etapa);
        }

        public int Agregar_Registro(string idoportunidad, string etapa)
        { 
            return Agregar(idoportunidad, etapa);
        }

        public int Modificar_Registro(string id, string etapa)
        { 
            return Modificar(id, etapa);
        }
    }
}
