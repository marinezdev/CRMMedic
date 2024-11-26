using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Clasificacion : Repository.ClasificacionRepositorio
    {
        public List<mod.Clasificacion> Seleccionar_Registros(string idconfiguracion)
        {
            return Seleccionar(idconfiguracion);
        }

        public mod.Clasificacion Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public List<mod.UsuariosRoles> Seleccionar_ParaAdministracion()
        {
            return SeleccionarParaAdministracion();
        }

        public List<mod.Clasificacion> Seleccionar_Todo()
        {
            return SeleccionarTodo();
        }

        public List<mod.Clasificacion> Seleccionar_Conteo(string idconfiguracion)
        {
            return SeleccionarConteo(idconfiguracion);
        }

        public int Agregar_Registro(string Nombre, string IdConfiguracion)
        {
            return Agregar(Nombre, IdConfiguracion);
        }

        public int Modificar_Registro(string Nombre, string Id)
        {
            return Modificar(Nombre, Id);
        }


    }
}
