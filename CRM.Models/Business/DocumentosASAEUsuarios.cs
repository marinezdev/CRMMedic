using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class DocumentosASAEUsuarios : Repository.DocumentosASAEUsuariosRepositorio
    {
        public int Seleccionar_Posicion(string idusuario, string iddocumento)
        {
            return SeleccionarPosicion(idusuario, iddocumento);
        }

        public List<mod.Modelos> Seleccionar_UsuariosAsignados(string iddocumento)
        {
            return SeleccionarUsuariosAsignados(iddocumento);
        }

        public bool ValidarSiUsuarioEstaAsignadoADocumento(string iddocumento, string idusuario)
        {
            if (SeleccionarSiUsuarioEstaAsignadoADocumento(iddocumento, idusuario) >= 1)
                return true;
            else
                return false;
        }

        public int Agregar_Registro(mod.DocumentosASAEUsuarios items)
        {
            return Agregar(items);
        }

        public int Eliminar_Registro(string iddocumento)
        {
            return Eliminar(iddocumento);
        }

    }
}
