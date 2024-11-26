using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Tareas
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public int Responsable { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int Actividad { get; set; }
        public int Estado { get; set; }
        public string Notas { get; set; }
        public int Avance { get; set; }
        public int Prioridad { get; set; }
        public DateTime Alta { get; set; }

        public Usuarios Usuarios { get; set; }

    }
}