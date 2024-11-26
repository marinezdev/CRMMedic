using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class EmpresasUsuarios : Repository.EmpresasUsuariosRepositorio
    {
        public List<mod.Modelos> Seleccionar_EmpresasPorUsuario(string idusuario)
        {
            return SeleccionarEmpresasPorUsuario(idusuario);
        }

        public List<mod.Usuarios> Seleccionar_UsuariosExistentesRol2(string idconfiguracion)
        {
            return SeleccionarUsuariosExistentesRol2(idconfiguracion);
        }

        public List<mod.Usuarios> Seleccionar_UsuariosExistentesRol3(string idconfiguracion)
        {
            return SeleccionarUsuariosExistentesRol3(idconfiguracion);
        }

        public string Seleccionar_ValidarSiUsuarioPerteneceAEmpresa(string idusuario)
        {
            return SeleccionarValidarSiUsuarioPerteneceAEmpresa(idusuario);
        }

        public int Agregar_Registro(string idempresa, string idusuario, string activo)
        {
            return Agregar(idempresa, idusuario, activo);
        }

        public int Modificar_EmpresaPorUsuario(string idusuario, string idempresa)
        {
            return ModificarEmpresaPorUsuario(idusuario, idempresa);
        }

        public int Modificar_EmpresasPorUsuario(string idusuario, string idempresa, string activo)
        {
            return ModificarEmpresasPorUsuario(idusuario, idempresa, activo);
        }
    }
}
