using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class ManejadorModelos
    {
        private Usuarios _usuarios;

        public Usuarios Usuarios
        {
            get { return _usuarios; }
            set { _usuarios = value; }
        }

        public void Inicializar()
        {
            _usuarios = new Usuarios();
        }
    }
}
