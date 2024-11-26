using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class ContactoRol : Repository.ContactoRolRepositorio
    {
        public List<mod.ContactoRol> Seleccionar_PorClasificacionRolesContacto(string id)
        {
            return SeleccionarPorClasificacionRolesContacto(id);
        }
    }
}
