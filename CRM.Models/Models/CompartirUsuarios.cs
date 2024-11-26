using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class CompartirUsuarios
    {
        public CompartirContactos CompartirContactos;
        public CompartirEmpresas CompartirEmpresas;
        public CompartirOportunidades CompartirOportunidades;
        public Usuarios Usuarios;

        public CompartirUsuarios()
        {
            CompartirContactos = new CompartirContactos();
            CompartirEmpresas = new CompartirEmpresas();
            CompartirOportunidades = new CompartirOportunidades();
            Usuarios = new Usuarios();
        }
    }
}
