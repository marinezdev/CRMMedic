using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class UsuarioConfiguracion : Repository.UsuarioConfiguracionRepositorio
    {
        public int Agregar_Registro(string idusuario, string idconfiguracion)
        { 
            return Agregar(idusuario, idconfiguracion);
        }

        public int Modificar_Configuracion(string idusuario, string idconfiguracion)
        {
            return Modificar(idusuario, idconfiguracion);
        }
    }
}
