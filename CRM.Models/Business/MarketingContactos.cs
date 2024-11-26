using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;
using System.Data;

namespace CRM.Models.Business
{
    public class MarketingContactos : Repository.MarketingContactosRepositorio
    {
        public DataSet ProspectosActividadesReporte(int IdCampaña)
        {
            return ProspectosActividadesReporteData(IdCampaña);
        }

        public DataSet ProspectosActividadesResumen(int IdCampaña) {
            return ProspectosActividadesResumenData(IdCampaña);
        }

        public List<mod.Modelos> Seleccionar_PorIdCampaña(string idcampaña)
        {
            return SeleccionarPorIdCampaña(idcampaña);
        }

        public List<mod.Modelos> Seleccionar_PorIdCampañaFiltrado(string idcampaña, string filtro)
        {
            return SeleccionarPorIdCampañaFiltrado(idcampaña, filtro);
        }

        public mod.Modelos Seleccionar_PorIdCampañaIdContacto(string idcampaña, string idcontacto)
        {
            return SeleccionarPorIdCampañaIdContacto(idcampaña, idcontacto);
        }

        public List<mod.Modelos> Seleccionar_PorIdContacto(string idcontacto)
        {
            return SeleccionarPorIdContacto(idcontacto);
        }

        public List<mod.Modelos> Seleccionar_Listado01PorIdCampaña(string idcampaña)
        {
            return SeleccionarListado01PorIdCampaña(idcampaña);
        }
        public List<mod.Modelos> Seleccionar_Listado02PorIdCampaña(string idcampaña)
        {
            return SeleccionarListado02PorIdCampaña(idcampaña);
        }

        public List<mod.Modelos> Seleccionar_Listado03PorIdCampaña(string idcampaña)
        {
            return SeleccionarListado03PorIdCampaña(idcampaña);
        }
        public List<mod.Modelos> Seleccionar_Listado04PorIdCampaña(string idcampaña)
        {
            return SeleccionarListado04PorIdCampaña(idcampaña);
        }

        public mod.MarketingContactos Seleccionar_EstadisticasContactosPorIdEvento(string idevento)
        {
            return SeleccionarEstadisticasContactosPorIdEvento(idevento);
        }

        public mod.MarketingContactos Seleccionar_EstadisticasPorTipoIngreso(string idcampaña)
        {
            return SeleccionarEstadisticasPorTipoIngreso(idcampaña);
        }


        public int Agregar_Registros(string[] contacto, string idcampaña)
        {
            int contador = 0;
            if (contacto != null && contacto.Length > 0)
            {
                foreach (var campo in contacto)
                {
                    //Guarda los que traen valor (solamente)
                    Agregar_Registro(idcampaña, campo);
                    contador += 1;
                }
            }
            return contador;
        }

        public int Agregar_Registro(string idcampaña, string idcontacto)
        {
            return Agregar(idcampaña, idcontacto);
        }

        public int Modificar_Registro(string idcampaña, string idcontacto, string seguimiento, string asistencia)
        {
            return Modificar(idcampaña, idcontacto, seguimiento, asistencia);
        }

        public int Modificar_Asistencia(string idcontacto, string idcampaña, string estado)
        {
            return ModificarAsistencia(idcontacto, idcampaña, estado);
        }

        public int Modificar_Confirmacion(string idcampaña, string idcontacto)
        {
            return ModificarConfirmacion(idcontacto, idcampaña);
        }

        public int Eliminar_Registro(string idcampaña, string idcontacto)
        {
            return Eliminar(idcampaña, idcontacto);
        }


    }
}
