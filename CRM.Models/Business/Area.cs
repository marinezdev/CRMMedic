using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Area : Repository.AreaRepositorio
    {
        public List<mod.Area> Seleccionar_Registros()
        {
            return Seleccionar();
        }

        public int Agregar_Registro(string nombre)
        {
            return Agregar(nombre);
        }
        public int Area_Agregar(Area area)
        {
            return Area_Agregar(area);
        }
    }
}
