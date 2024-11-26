using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Moneda : Repository.MonedaRepositorio
    {
        public List<mod.Moneda> Seleccionar_Registros()
        { 
            return Seleccionar();
        }
    }
}
