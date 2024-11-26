using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class BitacoraUsuarios : Repository.BitacoraUsuariosRepositorio
    {
        public mod.BitacoraUsuarios Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public List<mod.BitacoraUsuarios> Seleccionar_IdOportunidad(string idoportunidad)
        {
            return SeleccionarIdOportunidad(idoportunidad);
        }

        public List<mod.BitacoraUsuarios> Seleccionar_IdOportunidad(string idusuario, string idoportunidad)
        {
            return SeleccionarIdOportunidad(idusuario, idoportunidad);
        }

        public List<mod.BitacoraUsuarios> Seleccionar_BitacoraResponsablesPorIdOportunidad(string idoportunidad)
        {
            return SeleccionarBitacoraResponsablesPorIdOportunidad(idoportunidad);
        }

        public int Seleccionar_ValidarSiUsuarioTieneBitacora(string idoportunidad, string idusuario)
        {
            return SeleccionarValidarSiUsuarioTieneBitacora(idoportunidad, idusuario);
        }

        public int Seleccionar_SiOportunidadTieneResponsablesAsignados(string idoportunidad)
        {
            return SeleccionarSiOportunidadTieneResponsablesAsignados(idoportunidad);
        }

        public int Agregar_Registro(mod.BitacoraUsuarios items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(string Id, string TipoActividad, string Fecha, string Notas) 
        {
            return Modificar(Id, TipoActividad, Fecha, Notas);
        }

    }
}
