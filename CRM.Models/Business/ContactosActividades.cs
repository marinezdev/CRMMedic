using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class ContactosActividades : Repository.ContactosActividadesRepositorio
    {
        public List<mod.ContactosActividades> Seleccionar_PorIdBitacoraIdContacto(string idbitacora, string idcontacto)
        {
            return SeleccionarPorIdBitacoraIdContacto(idbitacora, idcontacto);
        }

        public mod.ContactosActividades Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public List<mod.Modelos> Seleccionar_ContactosConActividades(string idoportunidad)
        {
            return SeleccionarContactosConActividades(idoportunidad);
        }

        public int Agregar_Modificar(mod.ContactosActividades items)
        {
            return AgregarModificar(items);
        }

        public int Modificar_Registro(mod.ContactosActividades items)
        {
            return Modificar(items);
        }
    }
}
