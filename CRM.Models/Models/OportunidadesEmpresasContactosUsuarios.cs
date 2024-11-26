using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class OportunidadesEmpresasContactosUsuarios
    {
        public int IdOportunidad { get; set; }
        public int IdEmpresa { get; set; }
        public int IdContacto { get; set; }
        public int IdUsuario { get; set; }
        public int IdBitacora { get; set; }
    }
}