using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class ContactosDetalle : Repository.ContactosDetalleRepositorio
    {
        public mod.ContactosDetalle Seleccionar_PorIdContacto(string idcontacto)
        {
            return SeleccionarPorIdContacto(idcontacto);
        }

        public int Agregar_Modificar(string idcontacto, string idpuesto, string iddepartamento)
        {
            return AgregarModificar(idcontacto, idpuesto, iddepartamento);
        }
    }
}
