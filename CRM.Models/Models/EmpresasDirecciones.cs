using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class EmpresasDirecciones
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int IdTipoDireccion { get; set; }
        public string Direccion { get; set; }
        public string CP { get; set; }
        public string Ciudad { get; set; }
        public int IdEstado { get; set; }
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
    }
}
