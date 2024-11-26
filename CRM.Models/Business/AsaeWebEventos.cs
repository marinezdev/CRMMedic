using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class AsaeWebEventos : CRM.Models.Repository.AsaeWebEventosRepository
    {
        public List<mod.AsaeWebEventos> Seleccionar_Registros()
        {
            return Seleccionar();
        }

        public mod.AsaeWebEventos Seleccionar_Registro_PorIdEvento(string idevento)
        {
            return SeleccionarPorId(idevento);
        }

      
    }
}
