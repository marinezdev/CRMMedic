using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Bitacora
    {
        public int Id { get; set; }
        public string Notas { get; set; }
        public DateTime Fecha { get; set; }
        public int Estado { get; set; }
        public string NombreEstado { get; set; } //Para visualizar el nombre del estado en lugar del numero
        public int IdResponsable { get; set; }
        public int IdOportunidad { get; set; }
        public int IdClasificacionResponsable { get; set; }
        public int IdSubclasificacionRespponsable { get; set; }
    }
}