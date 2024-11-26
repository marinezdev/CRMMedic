using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class ActividadesContactoDetalle : Repository.ActividadesContactoDetalleRepositorio
    {
        public List<mod.ActividadesContactoDetalleTipo> Seleccionar_ActividadesPorIdContacto(string idcontacto)
        {
            return SeleccionarActividadesPorIdContacto(idcontacto);
        }

        public mod.ActividadesContactoDetalleTipo Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public int Agregar_Registro(mod.ActividadesContactoDetalle items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(string idtipoactividad, string fecha, string notas, string id)
        {
            return Modificar(idtipoactividad,fecha,notas, id);
        }

    }
}
