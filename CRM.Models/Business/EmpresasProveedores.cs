using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class EmpresasProveedores : Repository.EmpresasProveedoresRepositorio
    {
        public List<mod.EmpresasProveedores> Seleccionar_Registros()
        {
            return Seleccionar();
        }

        public mod.EmpresasProveedores Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }


        public int Agregar_Registro(mod.EmpresasProveedores items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(mod.EmpresasProveedores items)
        {
            return Modificar(items);
        }
    }
}
