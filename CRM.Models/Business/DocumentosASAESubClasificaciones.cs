using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class DocumentosASAESubClasificaciones : Repository.DocumentosASAESubClasificacionesRepository
    {
        public List<mod.Subclasificacion> Seleccionar_Registros()
        {
            return Seleccionar();
        }

        public mod.Subclasificacion Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public List<mod.Subclasificacion> Seleccionar_SubClasificacionesPorClasificacion(string idclasificacion)
        {
            return SeleccionarSubClasificacionesPorClasificacion(idclasificacion);
        }

        public int Agregar_Registro(mod.Subclasificacion items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(mod.Subclasificacion items)
        {
            return Modificar(items);
        }

        public int Eliminar_Registro(string id)
        {
            return Eliminar(id);
        }

        public int Eliminar_Clasificacion(string id)
        {
            return EliminarClasificacion(id);
        }

    }
}
