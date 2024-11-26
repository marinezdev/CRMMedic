using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class CodigoPostal : Repository.CodigoPostalRespositorio
    {
        public List<Repository.DireccionCompleta> Seleccionar_CodigoPostal(string cp)
        {
            return SeleccionarCodigoPostal(cp);
        }

        public string Seleccionar_EstadoPorCodigoPostal(string cp)
        {
            return SeleccionarEstadoPorCodigoPostal(cp);
        }
    }
}
