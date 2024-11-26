using CRM.Models.Business.Interfaces;
using System.Collections.Generic;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class CompartirOportunidades : Repository.CompartirOportunidadesRepositorio, IValidacion
    {
        public List<mod.CompartirUsuarios> Seleccionar_UsuariosCompartidos(string idoportunidad)
        {
            return SeleccionarUsuariosCompartidos(idoportunidad);
        }

        public bool Validar_SiUsuarioPuedeModificar(string idoportunidad, string idusuario)
        {
            return ValidarSiUsuarioPuedeModificar(idoportunidad, idusuario);
        }

        public bool Validar_OportunidadUsuarioPermiso(string idoportunidad, string idusuario)
        {
            return ValidarOportunidadUsuarioPermiso(idoportunidad, idusuario);
        }

        public bool PropietarioPermiso(string idoportunidad, string idusuario)
        {
            return ValidarOportunidadUsuarioPermiso(idoportunidad, idusuario);
        }

        public int Agregar_Registro(string idoportunidad, string idusuario)
        {          
            return Agregar(idoportunidad, idusuario);
        }

        public int Eliminar_UsuarioCompartido(string idoportunidad, string idusuario)
        {
            return EliminarUsuarioCompartido(idoportunidad, idusuario);
        }


    }
}
