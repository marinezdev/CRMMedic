using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class Multi2
    {
        public Multi Multi { get; set; }
        public Usuarios Usuarios { get; set; }
        public Configuracion Configuracion { get; set; }

        public Multi2()
        {
            Multi = new Multi();
            Usuarios = new Usuarios();
            Configuracion = new Configuracion();
        }
    }
}
