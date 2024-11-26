using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class ActividadesContacto : Repository.ActividadesContactoRepositorio
    {
        public int Agregar_Registro(mod.ActividadesContacto items)
        {
            return Agregar(items);
        }
    }
}
