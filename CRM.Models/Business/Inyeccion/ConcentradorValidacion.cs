using CRM.Models.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Business.Inyeccion
{
    public class ConcentradorValidacion
    {
        protected IValidacion validacion;

        public ConcentradorValidacion(IValidacion _ivalidacion)
        {
            validacion = _ivalidacion;
        }

        public bool PropietarioPermiso(string id, string idusuario)
        {
            return validacion.PropietarioPermiso(id, idusuario);
        }
    }
}
