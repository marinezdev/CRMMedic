using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class ArchivosBitacora : Repository.ArchivosBitacoraRepositorio
    {
        public mod.ArchivosBitacora Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public List<mod.ArchivosBitacora> Seleccionar_PorIdBitacora(string id)
        {
            return SeleccionarPorIdBitacora(id);
        }

        public string Seleccionar_PorNombre(string id)
        {
            return SeleccionarPorNombre(id);
        }

        public int Agregar_Registro(mod.ArchivosBitacora items)
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
