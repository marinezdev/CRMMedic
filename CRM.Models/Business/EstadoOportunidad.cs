using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class EstadoOportunidad : Repository.EstadoOportunidadRepositorio
    {
        public mod.EstadoOportunidad Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public List<mod.EstadoOportunidad> Seleccionar_PorIdOportunidad(string idoportunidad)
        { 
            return SeleccionarPorIdOportunidad(idoportunidad);
        }

        public int Agregar_Registro(string idoportunidad, string estado, string comentarios)
        {
            return Agregar(idoportunidad, estado, comentarios);
        }

        public int Modificar_Registro(string estado, string comentarios, string fecha, string id)
        {
            return Modificar(estado, comentarios, fecha, id);
        }
    }
}
