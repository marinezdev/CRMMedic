using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Business.Interfaces
{
    interface IEtiquetas
    {
        string Nombre();
        string Persona(); //Física/Moral

        string InternoExterno(); //Interno/Externo
    }
}
