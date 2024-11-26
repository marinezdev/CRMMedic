using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class OportunidadesResponsables : Repository.OportunidadesResponsablesRepositorio
    {
        public List<mod.UsuariosEmpresasOportunidadesDetalle> Seleccionar_ResponsablesPorOportunidad(string id, string idconfiguracion)
        { 
            return SeleccionarResponsablesPorOportunidad(id, idconfiguracion);
        }

        public string Contar_ResponsablesPorIdOportunidad(string id)
        { 
            return ContarResponsablesPorIdOportunidad(id);
        }

        public int Seleccionar_SiOportunidadTieneResponsablesAsignados(string idoportunidad)
        {
            return SeleccionarSiOportunidadTieneResponsablesAsignados(idoportunidad);
        }

        public List<mod.Modelos> Usuarios_QuehanVistoBitacora()
        { 
            return UsuariosQuehanVistoBitacora();
        }

        public int Seleccionar_SiResponsableYaEstaAsignadoAOportunidad(string idoportunidad, string idasignado)
        {
            return SeleccionarSiResponsableYaEstaAsignadoAOportunidad(idoportunidad, idasignado);
        }

        public int Agregar_Registro(string idoportunidad, string idasignado, string clasificacion, string subclasificacion)
        { 
            return Agregar(idoportunidad, idasignado, clasificacion, subclasificacion);
        }

        public int Eliminar_Registro(string idoportunidad, string idusuario)
        {
            return Eliminar(idoportunidad, idusuario);
        }

  
    }
}
