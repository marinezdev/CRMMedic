using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Estadisticas : Repository.EstadisticasRepositorio
    {
        public mod.EstadisticasTablas Tablas_Contenido(string idconfiguracion)
        {
            return TablasContenido(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_EmpresasContactosOportunidadesTareas()
        {
            return SeleccionarEmpresasContactosOportunidadesTareas();
        }

        public List<mod.Modelos> Seleccionar_ContactosEmpresasOportunidadesTareas()
        {
            return SeleccionarContactosEmpresasOportunidadesTareas();
        }

        public List<mod.Modelos> Seleccionar_ContactosEmpresasOportunidades()
        {
            return SeleccionarContactosEmpresasOportunidades();
        }

        public List<mod.Modelos> Seleccionar_ContactosEmpresasTareas()
        {
            return SeleccionarContactosEmpresasTareas();
        }

        public List<mod.Modelos> Seleccionar_UsuariosTareasOportunidades()
        {
            return SeleccionarUsuariosTareasOportunidades();
        }

        public List<mod.Modelos> Seleccionar_AsuntosPorEmpresaGeneral(string idconfiguracion)
        {
            return SeleccionarAsuntosPorEmpresaGeneral(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_AsuntosPorEmpresaPorEstado(string estado, string idconfiguracion)
        {
            return SeleccionarAsuntosPorEmpresaPorEstado(estado, idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_AsuntosPorEmpresaPorEstado1_1(string idconfiguracion)
        { 
            return SeleccionarAsuntosPorEmpresaPorEstado1_1(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_AsuntosPorEmpresaPorEstado1_2(string idconfiguracion)
        { 
            return SeleccionarAsuntosPorEmpresaPorEstado1_2(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_AsuntosPorEmpresaPorEstado1_3(string idconfiguracion)
        { 
            return SeleccionarAsuntosPorEmpresaPorEstado1_3(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_AsuntosPorEmpresaPorEstado1_4(string idconfiguracion)
        { 
            return SeleccionarAsuntosPorEmpresaPorEstado1_4(idconfiguracion);
        }
    }
}
