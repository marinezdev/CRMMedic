using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class ContactosActividades
    {
        public int Id { get; set; }
        public int IdBitacora { get; set; }
        public int IdContacto { get; set; }
        public int TipoActividad { get; set; }
        public string TipoActividadNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Notas { get; set; }
        public DateTime Creado { get; set; }
    }
}