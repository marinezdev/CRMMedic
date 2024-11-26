using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class EmpresasDirecciones : Repository.EmpresasDireccionesRepositorio
    {
        public mod.EmpresasDirecciones Seleccionar_DireccionAdicionalPorId(string iddireccionadicional)
        {
            return SeleccionarDireccionAdicionalPorId(iddireccionadicional);
        }

        public List<mod.EmpresasDirecciones> Seleccionar_DireccionesAdicionalesPorIdEmpresa(string idempresa)
        {
            return SeleccionarDireccionesAdicionalesPorIdEmpresa(idempresa);
        }

        public List<mod.EmpresasDirecciones> Seleccionar_DireccionesAdicionalesConNombreDePaisPorIdEmpresa(string idempresa)
        {
            return SeleccionarDireccionesAdicionalesConNombreDePaisPorIdEmpresa(idempresa);
        }

        public int Agregar_Registro(string idempresa, string idtipodireccion, string direccion, string cp, string ciudad, string idpais)
        {
            return Agregar(idempresa, idtipodireccion, direccion, cp, ciudad, idpais);
        }

        public int Modificar_Registro(string idtipodireccion, string direccion, string cp, string ciudad, string idpais, string iddireccionadicional)
        {
            return Modificar(idtipodireccion, direccion, cp, ciudad, idpais, iddireccionadicional);
        }
    }
}
