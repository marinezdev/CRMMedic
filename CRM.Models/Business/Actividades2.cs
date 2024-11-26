using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Actividades2 : Repository.Actividades2Repository
    {
        public List<mod.Actividades> Seleccionar_Registros()
        {
            return Seleccionar();
        }



        //Agrega un registro, devuelve si se agregó o no
        public bool Agregar_Registro(mod.Actividades items)
        {
            return Agregar(items);
        }

        //Nuevas
        public void Listado_91()
        { 
            
        }

        public void Metodo_ProtegidoInterno()
        {
            MetodoProtegidoInterno();
        }
    }
}
