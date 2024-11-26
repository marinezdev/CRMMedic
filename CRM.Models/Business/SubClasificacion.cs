using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class SubClasificacion : Repository.SubclasificacionRepositorio
    {
        public List<mod.Subclasificacion> Seleccionar_PorId(string Id)
        { 
            return SeleccionarPorId(Id);
        }

        public mod.Subclasificacion Seleccionar_PorIdEditar(string id)
        { 
            return SeleccionarPorIdEditar(id);
        }

        public List<mod.UsuariosRoles> Seleccionar_ParaAdministracion()
        { 
            return SeleccionarParaAdministracion();
        }

        public int Agregar_Registro(string nombre, string idclasificacion)
        { 
            return Agregar(nombre, idclasificacion);
        }

        public int Modificar_Registro(string nombre, string idclasificacion, string id)
        { 
            return Modificar(nombre, idclasificacion, id);
        }
    }
}
