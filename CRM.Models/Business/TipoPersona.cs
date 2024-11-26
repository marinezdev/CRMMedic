using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class TipoPersona : Repository.TipoPersonaRepositorio
    {
        public List<mod.TipoPersona> Seleccionar_Registros()
        { 
            return Seleccionar();
        }

        public int Agregar_Registro(string nombre)
        { 
            return Agregar(nombre);
        }
    }
}
