using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class TareasUsuariosContactosEmpresas : Repository.TareasUsuariosContactosEmpresasRepositorio
    {
        public bool Agregar_Registro(string idtarea, string idusuario, string idcontacto, string idempresa)
        { 
            return Agregar(idtarea, idusuario, idcontacto, idempresa);
        }
    }
}
