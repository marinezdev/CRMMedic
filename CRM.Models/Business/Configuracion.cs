using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Configuracion : Repository.ConfiguracionRepositorio
    {
        /// <summary>
        /// Obtiene todas las configuraciones de empresas
        /// </summary>
        /// <returns></returns>
        public List<mod.Configuracion> Seleccionar_Registros()
        {
            return Seleccionar();
        }

        public mod.Configuracion Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public List<mod.Configuracion> Seleccionar_Todo()
        {
            return SeleccionarTodo();
        }

        public int Agregar_Registro(string nombre, string logo, string titulointermediopestaña)
        {
            return Agregar(nombre, logo, titulointermediopestaña);
        }

        public int Modificar_Registro(string nombre, string logo, string titulointermediopestaña, string id)
        {
            return Modificar(nombre, logo, titulointermediopestaña, id);
        }
    }
}
