using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Escalacion : Repository.EscalacionRepositorio
    {
        public List<mod.Escalacion> Seleccionar_PorIdOportunidad(string idoportunidad)
        {
            return SeleccionarPorIdOportunidad(idoportunidad);
        }

        public List<mod.Modelos> Seleccionar_UsuariosContactosPorIdOportunidad(string idoportunidad)
        {
            return SeleccionarUsuariosContactosPorIdOportunidad(idoportunidad);
        }

        public int Agregar_Registro(string idoportunidad, string fecha, string idusuariocontacto)
        {
            return Agregar(idoportunidad, fecha, idusuariocontacto);
        }

        public int Eliminar_Registro(string idoportunidad, string idusuario)
        {
            return Eliminar(idoportunidad, idusuario);
        }
    }
}
