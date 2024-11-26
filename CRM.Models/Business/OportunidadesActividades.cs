using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;
using CRM.Models.Business.Interfaces;

namespace CRM.Models.Business
{
    public class OportunidadesActividades : Repository.OportunidadesActividadesRepositorio, IValidacion
    {
        public mod.OportunidadesActividades Seleccionar_PorId(string id)
        { 
            return SeleccionarPorId(id);
        }

        public List<mod.OportunidadesActividades> Seleccionar_PorIdOportunidad(string id)
        { 
            return SeleccionarPorIdOportunidad(id);
        }

        public List<mod.OportunidadesActividades> Seleccionar_PorIdUsuario(string idusuario, string idoportunidad)
        { 
            return SeleccionarPorIdUsuario(idusuario, idoportunidad);
        }

        public int Seleccionar_CuantasActividadesTieneOportunidad(string idoportunidad)
        { 
            return SeleccionarCuantasActividadesTieneOportunidad(idoportunidad);
        }

        public int Seleccionar_SiUsuarioEstaAsignadoComoResponsableAActividad(string idoportunidad, string idusuario)
        { 
            return SeleccionarSiUsuarioEstaAsignadoComoResponsableAActividad(idoportunidad, idusuario);
        }

        public int Seleccionar_SiUsuarioEsCreadorActividad(string idactividad, string idusuario)
        {
            string consulta = "SELECT idusuario FROM oportunidadesactividades WHERE id=@idactividad AND idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idactividad", idactividad, System.Data.SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, System.Data.SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        public bool PropietarioPermiso(string idactividad, string idusuario)
        {
            var idcreador = Seleccionar_SiUsuarioEsCreadorActividad(idactividad, idusuario);
            if (idcreador == int.Parse(idusuario) || idusuario == "2")
                return true;
            else
                return false;
        }


        public int Agregar_Registro(mod.OportunidadesActividades items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(mod.OportunidadesActividades items)
        { 
            return Modificar(items);
        }

        public int Eliminar_Registro(string idactividad)
        {
            return Eliminar(idactividad);
        }
    }
}
