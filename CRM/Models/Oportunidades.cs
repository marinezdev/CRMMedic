using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Oportunidades
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Importe { get; set; }
        public DateTime Cierre { get; set; }
        public int Asignado { get; set; }
        public int Probabilidad { get; set; }
        public int Etapa { get; set; }
        public string EtapaNombre { get; set; }
        public int Avance { get; set; }
        public string Notas { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }

        public int IdClasificacion { get; set; }
        public int IdSubClasificacion { get; set; }

        public int PeriodoAtencion { get; set; }
        public string PeriodoAtencionNombre { get; set; } //Sólo para proceso de visualizar el detalle
        public DateTime Aviso { get; set; }

        public int Estado { get; set; }

        public string ComentariosFinales { get; set; }

        public int IdConfiguracion { get; set; }

        public string Contraparte { get; set; }
    }
}