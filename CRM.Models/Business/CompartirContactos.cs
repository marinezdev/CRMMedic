using CRM.Models.Business.Interfaces;
using CRM.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class CompartirContactos : Repository.CompartirContactosRepositorio, IValidacion
    {
        
        public List<mod.CompartirUsuarios> Seleccionar_UsuariosCompartidos(string idcontacto)
        {
            return SeleccionarUsuariosCompartidos(idcontacto);
        }

        public bool Validar_SiUsuarioPuedeModificar(string idcontacto, string idusuario)
        {
            return ValidarSiUsuarioPuedeModificar(idcontacto, idusuario);
        }

        public bool PropietarioPermiso(string idcontacto, string idusuario)
        {
            Contactos contactos = new Contactos();
            CompartirContactos compartircontactos = new CompartirContactos();
            var idcreador = contactos.Seleccionar_CreadorContacto(idcontacto);
            var modificar = compartircontactos.Validar_SiUsuarioPuedeModificar(idcontacto, idusuario);
            if (idcreador == idusuario || modificar || idusuario == "2")
                return true;
            else
                return false;
        }

        public int Agregar_Registro(string idcontacto, string idusuario)
        {
            return Agregar(idcontacto, idusuario);
        }

        public int Eliminar_UsuarioCompartido(string idcontacto, string idusuario)
        {
            return EliminarUsuarioCompartido(idcontacto, idusuario);
        }
    }
}
