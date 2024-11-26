using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class UsuariosEmpresasOportunidades //TODO ESTA CLASE DEBE DESAPARECER
    {
        public int IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public int Idoportunidad { get; set; }
        public int IdBitacora { get; set; }

    }
}