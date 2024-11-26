using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Archivos : Repository.ArchivosRepositorio
    {
        public mod.Archivos Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public string Seleccionar_PorNombre(string id)
        {
            return SeleccionarPorNombre(id);
        }

        public int Cuantos_ArchivosTieneOportunidad(string idoportunidad)
        {
            return CuantosArchivosTieneOportunidad(idoportunidad);
        }

        public List<mod.Archivos> Seleccionar_PorIdOportunidad(string id)
        {
            return SeleccionarPorIdOportunidad(id);
        }

        public bool Agregar_Registro(mod.Archivos items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(string Notas, string Version, string Id)
        {
            return Modificar(Notas, Version, Id);
        }

        public int Eliminar_Registro(string id)
        {
            return Eliminar(id);
        }

    }
}
