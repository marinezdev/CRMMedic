using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRM.Models.Business
{
    //Clase privada no accesible
    class AAAPruebas
    {
        public void Prueba01()
        {
            Repository.ActividadesRepositorio ar = new Repository.ActividadesRepositorio();
            ar.MetodoProtegidoInterno();    //Accesible por instanciación
            ar.MetodoPublico();             //Accesible por instanciación

        }

        public void Pruebaa02()
        {
            
        }

    }
}
