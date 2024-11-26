using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Business.Abstractas
{
    abstract class Usuario
    {
        protected string nombre;

        public Usuario(string Nombre)
        {
            nombre = Nombre;
        }
    }
}
