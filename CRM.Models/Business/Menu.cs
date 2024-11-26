using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Menu : Repository.MenuRepositorio
    {
        public List<mod.Menu> Seleccionar_Registros()
        {
            return Seleccionar();
        }

        public List<mod.MenuMenuRoles> Seleccionar_Registros(string idrol, string idconfiguracion)
        {
            return SeleccionarMenuPorIdRol(idrol, idconfiguracion); 
        }

        public List<mod.MenuMenuRoles> Seleccionar_MenuRol()
        {
            return SeleccionarMenuRol();
        }

        public mod.Menu Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public mod.Menu Seleccionar_MenuRolPorId(string id)
        {
            return SeleccionarMenuRolPorId(id);
        }

        public int Agregar_Registro(mod.Menu items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(mod.Menu items)
        {
            return Modificar(items);
        }

        public int AgregarMenuRol_registro(mod.MenuRoles items)
        {
            return AgregarMenuRol(items);
        }

        public int Modificar_MenuRol(mod.MenuRoles items)
        {
            return ModificarMenuRol(items);
        }

    }
}
