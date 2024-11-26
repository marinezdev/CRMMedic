using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Negocio
{
    public class Actividades : Models.Repository.ActividadesRepositorio
    {
        public new List<mod.Actividades> Seleccionar()
        {
            return Seleccionar();
        }

        public new bool Agregar(mod.Actividades items)
        {
            return Agregar(items);
        }
    }
}
