using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class CompartirEmpresas
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int IdUsuario { get; set; }
        public bool? Activo { get; set; }
    }
}
