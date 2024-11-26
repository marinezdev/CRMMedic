using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class DocumentosASAETipoArchivo : Repository.DocumentosASAETipoArchivo
    {

        public List<mod.DocumentosASAETipoArchivo> Seleccionar_Registros()
        {
            return Seleccionar();
        }

        public mod.DocumentosASAETipoArchivo Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public bool ValidarSiPermitido(string extension)
        {
            if (SeleccionarSiPermitido(extension) > 1)
                return true;
            else
                return false;
        }

        public int Agregar_Registro(mod.DocumentosASAETipoArchivo items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(mod.DocumentosASAETipoArchivo items)
        {
            return Modificar(items);
        }

    }
}
