using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Tareas : Repository.TareasRepositorio
    {
        public List<mod.TareasUsuarios> Buscar_Registros(string asunto, string responsable, string inicio, string fin)
        { 
            return Buscar(asunto, responsable, inicio, fin);
        }

        public mod.TareasUsuarios Seleccionar_PorId(string id)
        { 
            return SeleccionarPorId(id);
        }

        public int Agregar_Registro(mod.Tareas items)
        { 
            return Agregar(items);
        }

        public bool Modificar_Registro(mod.Tareas items)
        { 
            return Modificar(items);
        }
    }
}
