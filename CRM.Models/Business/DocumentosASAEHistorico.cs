using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class DocumentosASAEHistorico : Repository.DocumentosASAEHistoricoRepositorio
    {
        /// <summary>
        /// Obtiene el histórico de un documento
        /// </summary>
        /// <param name="iddocumento"></param>
        /// <returns></returns>
        public List<mod.Modelos> Seleccionar_PorIdDocumento(string iddocumento)
        {
            return SeleccionarPorIdDocumento(iddocumento);
        }

        public int Seleccionar_UltimaVersionDocumento(string iddocumento)
        {
            return SeleccionarUltimaVersionDocumento(iddocumento);
        }

        public mod.DocumentosASAEHistorico Seleccionar_DetalleUltimaVersionDocumento(string iddocumento)
        {
            return SeleccionarDetalleUltimaVersionDocumento(iddocumento);
        }

        public int Agregar_Registro(mod.DocumentosASAEHistorico items)
        {
            return Agregar(items);
        }

        public int Agregar_Autorizacion(mod.DocumentosASAEHistorico items)
        {
            return AgregarAutorizacion(items);
        }

        public int Modificar_Registro(mod.DocumentosASAEHistorico items)
        {
            return Modificar(items);
        }

        public int Modificar_AnteriorAlaUltimaVersion(mod.DocumentosASAEHistorico items)
        {
            return ModificarAnteriorAlaUltimaVersion(items);
        }

        public int Eliminar_Registro(string iddocumento)
        {
            return Eliminar(iddocumento);
        }


    }
}
