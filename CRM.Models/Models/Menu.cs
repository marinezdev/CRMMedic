using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string IdJquery { get; set; }
        public string Ruta { get; set; }
        public string Icono { get; set; }
        public string Nombre {get; set;}
        public int IdConfiguracion { get; set; }
        public bool Disponible { get; set; }

    }
}
