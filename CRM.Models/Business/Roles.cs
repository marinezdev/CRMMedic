using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Roles : Repository.RolesRepositorio
    {
        /// <summary>
        /// Obtiene todos los roles
        /// </summary>
        /// <returns></returns>
        public List<mod.Roles> Seleccionar_Registros()
        {
            return Seleccionar();
        }

        public mod.Roles Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public int Agregar_Registro(string Nombre, string Observaciones, string Activo, string PaginaInicio)
        {
            return Agregar(Nombre, Observaciones, Activo, PaginaInicio);
        }

        public int Modificar_Registro(string nombre, string observaciones, string activo, string paginainicio, string id)
        {
            return Modificar(nombre, observaciones, activo, paginainicio, id);
        }
    }
}
