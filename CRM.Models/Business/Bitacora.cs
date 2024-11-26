using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Bitacora : Repository.BitacoraRespositorio
    {
        public List<mod.UsuariosEmpresasOportunidadesDetalle> Seleccionar_PorUsuario(string id)
        {
            return SeleccionarPorUsuario(id);
        }
        public List<mod.ContactosActividades> Seleccionar_PorId(string idcon, string idbit)
        {
            return SeleccionarPorId(idcon, idbit);
        }

        public mod.Bitacora Seleccionar_PorIdBitacora(string id)
        {
            return SeleccionarPorIdBitacora(id);
        }

        public List<mod.Bitacoras> Seleccionar_BitacoraPorOportunidadPorUsuario(string idusuario, string idoportunidad)
        {
            return SeleccionarBitacoraPorOportunidadPorUsuario(idusuario, idoportunidad);
        }

        public List<mod.Bitacoras> Seleccionar_BitacoraPorOportunidad(string idoportunidad, string idconfiguracion)
        {
            return SeleccionarBitacoraPorOportunidad(idoportunidad, idconfiguracion);
        }

        public int Seleccionar_SiOportunidadTieneResponsablesAsignados(string idoportunidad)
        {
            return SeleccionarSiOportunidadTieneResponsablesAsignados(idoportunidad);
        }

        public List<mod.ArchivosBitacora> Seleccionar_Archivos(string id)
        {
            return SeleccionarArchivos(id);
        }

        public string Seleccionar_PendientesEstado1(string id)
        {
            return SeleccionarPendientesEstado1(id);
        }

        public List<mod.Usuarios> Seleccionar_ResponsablesPorOportunidad(string idoportunidad)
        {
            return SeleccionarResponsablesPorOportunidad(idoportunidad);
        }

        public int Agregar_Registro(mod.Bitacora items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(mod.Bitacora items)
        {
            return Modificar(items);
        }

        public int Modificar_BitacoraEstado2(string idbitacora)
        {
            return ModificarBitacoraEstado2(idbitacora);
        }

        public int Reviso_Asunto(string id)
        {
            return RevisoAsunto(id);
        }

        public int Eliminar_UsuarioDeBitacora(string idusuario, string idoportunidad)
        {
            return EliminarUsuarioDeBitacora(idusuario, idoportunidad);
        }
    }
}
