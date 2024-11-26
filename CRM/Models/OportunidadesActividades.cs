using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class OportunidadesActividades
    {
        public int Id { get; set; }
        public int TipoActividad { get; set; }
        public string ActividadNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int IdUsuarioAsignado { get; set; }
        public string Notas { get; set; }
        public int IdOportunidad { get; set; }
        public DateTime Agregado { get; set; }
        public int IdUsuario { get; set; }
    }
}