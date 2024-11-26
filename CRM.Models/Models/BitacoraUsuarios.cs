using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class BitacoraUsuarios
    {
        public int Id { get; set; }
        public string Notas { get; set; }
        public DateTime Fecha { get; set; }
        public int Estado { get; set; }
        public string NombreEstado { get; set; } //Para visualizar el nombre del estado en lugar del numero
        public int IdResponsable { get; set; }
        public int IdOportunidad { get; set; }
        public string Responsable { get; set; }
        public int IdTipoActividad { get; set; }
        public string TipoActividad { get; set; }
    }
}