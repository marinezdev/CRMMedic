using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class Oportunidades
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteOtro { get; set; }
        public int IdTipoMoneda { get; set; }
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

        public string Caracteristicas { get; set; }
        public int UDN { get; set; }

        public int RepetirProximoAño { get; set; }

        public DateTime FechaProximoVencimiento { get; set; }

        //Contadores de oportunidades

        public int outsourcing { get; set; }
        public int servicios { get; set; }
        public int soluciones { get; set; }
        public int valdemar { get; set; }
        public int comisionista { get; set; }
        public int redes { get; set; }
        public int sistemas { get; set; }
        public int enproceso { get; set; }
        public int cerradoganado { get; set; }
        public int cerradoperdido { get; set; }
        public int terminado { get; set; }
        public int cancelado { get; set; }
        public int suspendido { get; set; }



    }
}