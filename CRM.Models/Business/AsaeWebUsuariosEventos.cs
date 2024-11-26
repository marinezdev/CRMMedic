using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class AsaeWebUsuariosEventos : CRM.Models.Repository.AsaeWebUsuariosEventosRepository
    {
        public List<mod.AsaeWebUsuariosEventos> Seleccionar_PorIdEvento(string idevento)
        {
            return SeleccionarPorIdEvento(idevento);
        }

        public mod.AsaeWebUsuariosEventos Seleccionar_EstadisticasContactosPorIdEvento(string idevento)
        {
            return SeleccionarEstadisticasContactosPorIdEvento(idevento);
        }
    }
}
