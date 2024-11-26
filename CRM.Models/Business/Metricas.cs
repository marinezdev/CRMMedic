using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Metricas : Repository.MetricasRepository
    {
        /// <summary>
        /// Selecciona el detalle del registro por idoportunidad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public mod.Metricas Seleccionar_RegistroPorId(string id)
        {
            return SeleccionarPorIdOportunidad(id);
        }

        public int Agregar_Registro(mod.Metricas items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(mod.Metricas items)
        {
            return Modificar(items);
        }




    }
}
