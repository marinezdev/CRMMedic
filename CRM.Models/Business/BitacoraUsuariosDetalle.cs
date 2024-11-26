using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class BitacoraUsuariosDetalle : Repository.BitacoraUsuariosDetalleRepositorio
    {
        public mod.BitacoraUsuariosDetalle Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public List<mod.BitacoraUsuariosDetalle> Seleccionar_PorIdOportunidad(string idoportunidad)
        {
            return SeleccionarPorIdOportunidad(idoportunidad);
        }

        public List<mod.BitacoraUsuariosDetalle> Seleccionar_PorIdUsuarioIdOportunidad(string idusuario, string idoportunidad)
        {
            return SeleccionarPorIdUsuarioIdOportunidad(idusuario, idoportunidad);
        }

        public int Seleccionar_SiOportunidadTieneUsuariosResponsablesAsignados(string idoportunidad)
        {
            return SeleccionarSiOportunidadTieneUsuariosResponsablesAsignados(idoportunidad);
        }

        public int Agregar_Registro(mod.BitacoraUsuariosDetalle items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(string id, string idtipoactividad, string notas)
        {
            return Modificar(id, idtipoactividad, notas);
        }
    }
}
