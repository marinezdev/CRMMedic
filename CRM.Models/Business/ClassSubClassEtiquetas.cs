using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class ClassSubClassEtiquetas : Repository.ClassSubClassEtiquetasRepositorio
    {
        public mod.ClassSubClassEtiquetas Seleccionar_PorIdClasificacionIdSubClasificacion(string idC, string idSC)
        {
            return SeleccionarPorIdClasificacionIdSubClasificacion(idC, idSC);
        }

        public mod.UsuariosRoles Seleccionar_PorIdClasificacionIdSubClasificacionParaEdicion(string idC, string idSC)
        {
            return SeleccionarPorIdClasificacionIdSubClasificacionParaEdicion(idC, idSC);
        }

        public List<mod.UsuariosRoles> Seleccionar_ParaAdministracion()
        {
            return SeleccionarParaAdministracion();
        }

        public int Modificar_Registro(string etiqueta1, string etiqueta2, string etiqueta3, string etiqueta4, string idclasificacion, string idsubclasificacion)
        {
            return Modificar(etiqueta1, etiqueta2, etiqueta3, etiqueta4, idclasificacion, idsubclasificacion);
        }
    }
}
