using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class UsuariosRoles: Repository.UsuariosRolesRepositorio
    {
        public string Seleccionar_RolUsuario(string idusuario)
        { 
            return SeleccionarRolUsuario(idusuario);
        }

        public bool Agregar_Registro(string IdUsuario, string IdRol)
        { 
            return Agregar(IdUsuario, IdRol);
        }
    }
}
