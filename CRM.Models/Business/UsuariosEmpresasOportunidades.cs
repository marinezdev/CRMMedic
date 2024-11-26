using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class UsuariosEmpresasOportunidades : Repository.UsuariosEmpresasOportunidadesRepositorio
    {
        public List<mod.UsuariosEmpresasOportunidadesDetalle> Seleccionar_ResponsablesPorOportunidad(string id)
        { 
            return SeleccionarResponsablesPorOportunidad(id);
        }

        public List<mod.Usuarios> Seleccionar_ResponsablesPorEmpresa(string id)
        { 
            return SeleccionarResponsablesPorEmpresa(id);
        }

        public int Agregar_Registro(mod.UsuariosEmpresasOportunidades items)
        { 
            return Agregar(items);
        }
    }
}
