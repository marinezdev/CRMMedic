using CRM.Models.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class CompartirEmpresa : Repository.CompartirEmpresaRepository, IValidacion
    {
        public List<mod.CompartirUsuarios> Seleccionar_UsuariosCompartidos(string idempresa)
        {
            return SeleccionarUsuariosCompartidos(idempresa);
        }

        public bool Validar_SiUsuarioPuedeModificar(string idempresa, string idusuario)
        {
            return ValidarSiUsuarioPuedeModificar(idempresa, idusuario);
        }

        public bool Validar_EmpresaUsuarioPermiso(string idempresa, string idusuario)
        {
            return ValidarEmpresaUsuarioPermiso(idempresa, idusuario);
        }

        public bool PropietarioPermiso(string idempresa, string idusuario)
        {
            return ValidarEmpresaUsuarioPermiso(idempresa, idusuario);
        }

        public int Agregar_Registro(string idempresa, string idusuario)
        {
            return Agregar(idempresa, idusuario);
        }

        public int Eliminar_UsuarioCompartido(string idempresa, string idusuario)
        {
            return EliminarUsuarioCompartido(idempresa, idusuario);
        }
    }
}
