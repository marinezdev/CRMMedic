using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    /// <summary>
    /// Guarda un registro para avisar a un usuario en una fecha determinada via correo electronico que esté
    /// pendiente para llevar a cabo una tarea en el sistema en la oportunidad indicada.
    /// </summary>
    public class Aviso : Repository.AvisoRepositorio
    {
        public List<mod.Modelos> Seleccionar_ContactoPorIdOportunidad(string idoportunidad)
        {
            return SeleccionarContactoPorIdOportunidad(idoportunidad);
        }

        public List<mod.Usuarios> Selecccionar_UsuariosParaEnviarAviso()
        {
            return SelecccionarUsuariosParaEnviarAviso();
        }

        public int Agregar_Registro(string idoportunidad, string fecha, string idusuariocontacto)
        {
            return Agregar(idoportunidad, fecha, idusuariocontacto);
        }

        public int Eliminar_Registro(string idoportunidad, string idusuario)
        {
            return Eliminar(idoportunidad, idusuario);
        }
    }
}
