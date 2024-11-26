using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class FechaVencimientoCambios : Repository.FechaVencimientoCambiosRepositorio
    {
        public List<mod.FechaVencimientoCambios> Seleccionar_Registros(string idoportunidad)
        { 
            return Seleccionar(idoportunidad);
        }

        public int Agregar_Registro(string fecha, string idoportunidad, string idusuario)
        { 
            return Agregar(fecha, idoportunidad, idusuario);
        }
    }
}
