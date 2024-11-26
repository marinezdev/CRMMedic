using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class OportunidadesUsuarios : Repository.OportunidadesUsuariosRepositorio
    {
        public List<mod.UsuariosRoles> Seleccionar_PorIdOportunidad(string idoportunidad)
        { 
            return SeleccionarPorIdOportunidad(idoportunidad);
        }

        public int Seleccionar_SiUsuarioYaEstaAsignadoAOportunidad(string idoportunidad, string idasignado)
        { 
            return SeleccionarSiUsuarioYaEstaAsignadoAOportunidad(idoportunidad, idasignado);
        }

        public int Agregar_Usuario(string idoportunidad, string idasignado, string clasificacion, string subclasificacion)
        { 
            return AgregarUsuario(idoportunidad, idasignado, clasificacion, subclasificacion);
        }

        public int Eliminar_UsuarioResponsableOportunidad(string idusuario, string idoportunidad)
        { 
            return EliminarUsuarioResponsableOportunidad(idusuario, idoportunidad);
        }
    }
}
