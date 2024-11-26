using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class BitacoraUsuariosDetalle
    {
        public int Id { get; set; }
        public int IdBitacora { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoActividad { get; set; }
        public string TipoActividadNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Notas { get; set; }
        public DateTime Creado { get; set; }
        public string Nombre { get; set; }
    }
}