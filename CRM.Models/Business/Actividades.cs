using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    /// <summary>
    /// Catálogo de actividades
    /// </summary>
    public class Actividades : Repository.ActividadesRepositorio
    {
        public List<mod.Actividades> Seleccionar_Registros()
        {
            return Seleccionar();
        }

        /// <summary>
        /// Agrega un registro, devuelve todos los registros
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public List<mod.Actividades> Agregar_Seleccionar(mod.Actividades items)
        {
            Agregar(items);
            return Seleccionar();
        }

        //Agrega un registro, devuelve si se agregó o no
        public bool Agregar_Registro(mod.Actividades items, ref string mensaje)
        {
            if (Agregar(items))
            {
                mensaje = "Agregado";
                return true;
            }
            else
            {
                mensaje = "Fallo al agregar, revise sus datos.";
                return false;
            }
        }

        public void Metodo_Privado()
        {
            //Instancia hacia la clase
            Repository.ActividadesRepositorio ar = new Repository.ActividadesRepositorio();
            MetodoPublico();                //Accesible porque es público
            MetodoProtegidoInterno();       //Accesible por herencia
            ar.MetodoProtegidoInterno();    //Accesible por instancia
            ar.MetodoPublico();             //Accesible por instancia
            //Los métodos protected de la clase pueden accesarse por herencia pero no por instancia
            Seleccionar();
            Agregar(null);
            
        }
    }
}
