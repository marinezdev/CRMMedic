using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class EmpresasDetalle : Repository.EmpresasDetalleRepositorio
    {
        public mod.EmpresasDetalle Seleccionar_PorIdEmpresa(string idempresa)
        {
            return SeleccionarPorIdEmpresa(idempresa);
        }

        public int Agregar_Modificar(string idempresa, string idfuente, string idtipo, string idindustria)
        {
            return AgregarModificar(idempresa, idfuente, idtipo, idindustria);
        }

    }
}
