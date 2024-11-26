using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class OportunidadesEmpresasContactosUsuarios : Repository.OportunidadesEmpresasContactosUsuariosRepositorio
    {
        public string Seleccionar_EmpresaPorOportunidad(string id)
        { 
            return SeleccionarEmpresaPorOportunidad(id);
        }

        public string Seleccionar_IdResponsableCreadorOportunidad(string idoportunidad)
        { 
            return SeleccionarIdResponsableCreadorOportunidad(idoportunidad);
        }

        public string Seleccionar_NombreCreadorOportunidad(string idoportunidad)
        { 
            return SeleccionarNombreCreadorOportunidad(idoportunidad);
        }

        public bool Agregar_Registro(string idoportunidad, string idempresa, string idcontacto, string idusuario)
        { 
            return Agregar(idoportunidad, idempresa, idcontacto, idusuario);
        }

        public int Agregar_SoloEmpresa(string idempresa, string idoportunidad)
        { 
            return AgregarSoloEmpresa(idempresa, idoportunidad);
        }

        public int Agregar_Solocontacto(string idcontacto, string idoportunidad)
        { 
            return AgregarSolocontacto(idcontacto, idoportunidad);
        }

        public int Agregar_SoloOportunidad(string idoportunidad, string idusuario, string idconfiguracion)
        { 
            return AgregarSoloOportunidad(idoportunidad, idusuario, idconfiguracion);
        }

        public int Agregar_Bitacora(string idoportunidad, string idempresa, string idusuario, string idbitacora)
        { 
            return AgregarBitacora(idoportunidad, idempresa, idusuario, idbitacora);
        }


    }
}
