using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class OportunidadesImportes : Repository.OportunidadesImportesRepositorio
    {
        public List<mod.OportunidadesImportes> Seleccionar_ImportesPorIdOportunidad(string idoportunidad)
        { 
            return SeleccionarImportesPorIdOportunidad(idoportunidad);
        }

        public float Seleccionar_SumaImportesNacionalPorIdOportunidad(string idoportunidad)
        { 
            return SeleccionarSumaImportesNacionalPorIdOportunidad(idoportunidad);
        }

        public float Seleccionar_SumaImportesInternacionalPorIdOportunidad(string idoportunidad)
        { 
            return SeleccionarSumaImportesInternacionalPorIdOportunidad(idoportunidad);
        }

        public mod.OportunidadesImportes Seleccionar_PorId(string id)
        { 
            return SeleccionarPorId(id);
        }

        public int Agregar_Registro(string idoportunidad, string importe, string moneda, string tipocambio, string rubro, string observaciones)
        { 
            return Agregar(idoportunidad, importe, moneda, tipocambio, rubro, observaciones);
        }

        public int Modificar_Registro(string importe, string moneda, string tipocambio, string rubro, string observaciones, string id)
        { 
            return Modificar(importe, moneda, tipocambio, rubro, observaciones, id);
        }

        public int Modificar_ImporteTotalOportunidad(string idoportunidad, string importe)
        { 
            return ModificarImporteTotalOportunidad(idoportunidad, importe);
        }

        public int Eliminar_Registro(string id)
        { 
            return Eliminar(id);
        }
    }
}
