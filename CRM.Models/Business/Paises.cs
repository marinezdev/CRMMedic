using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fun = CRM.Utilerias;

namespace CRM.Models.Business
{
    public class Paises :  Repository.PaisesRepository
    {
        public List<fun.Generico> Seleccionar_Paises()
        { 
            return SeleccionarPaises();
        }
    }
}
