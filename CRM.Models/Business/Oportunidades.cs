using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Oportunidades : Repository.OportunidadesRepositorio
    {
        public List<mod.Modelos> Seleccionar_Registros()
        { 
            return Seleccionar();
        }

        public List<mod.Modelos> Seleccionar_Top5(string IdConfiguracion, string IdRol)
        { 
            return SeleccionarTop5(IdConfiguracion, IdRol);
        }

        public List<mod.OportunidadesBuscar> Buscar_Registros(string Nombre, string Empresa, string Inicio, string Fin, string Clasificacion, string SubClasificacion)
        { 
            return Buscar(Nombre, Empresa, Inicio, Fin, Clasificacion, SubClasificacion);
        }

        public mod.OportunidadesECU Seleccionar_PorId(string id)
        { 
            return SeleccionarPorId(id);
        }

        public List<mod.Modelos> Seleccionar_OportunidadesPorIdResponsable(string id)
        { 
            return SeleccionarOportunidadesPorIdResponsable(id);
        }

        public string Seleccionar_OportunidadesPorIdResponsableEstado1(string id)
        { 
            return SeleccionarOportunidadesPorIdResponsableEstado1(id);
        }

        public string Seleccionar_NombreTemaPorIdoportunidad(string id)
        { 
            return SeleccionarNombreTemaPorIdoportunidad(id);
        }

        public string Seleccionar_NombreEmpresaPorIdOportunidad(string id)
        { 
            return SeleccionarNombreEmpresaPorIdOportunidad(id);
        }

        public string Seleccionar_VencimientoTemaPorIdoportunidad(string id)
        { 
            return SeleccionarVencimientoTemaPorIdoportunidad(id);
        }

        public List<mod.Modelos> Seleccionar_FechasOportunidad(string id)
        { 
            return SeleccionarFechasOportunidad(id);
        }

        public List<mod.Oportunidades> Seleccionar_ContactoEnOportunidades(string idcontacto)
        { 
            return SeleccionarContactoEnOportunidades(idcontacto);
        }

        public List<mod.Oportunidades> Seleccionar_EmpresasEnOportunidades(string idempresa)
        {
            return SeleccionarEmpresasEnOportunidades(idempresa);
        }

        public string Seleccionar_EstadoActual(string idoportunidad)
        {
            return SeleccionarEstadoActual(idoportunidad);
        }

        public mod.Modelos Seleccionar_1()
        { 
            return Seleccionar1();
        }

        public List<mod.Modelos> Seleccionar_2()
        { 
            return Seleccionar2();
        }

        public List<mod.Modelos> Seleccionar_TemasEnProceso(string idconfiguracion, string idrol, string idusuario)
        { 
            return SeleccionarTemasEnProceso(idconfiguracion, idrol, idusuario);
        }

        public List<mod.Modelos> Seleccionar_TemasEnProceso_filtrados(string idudn, string opc, string ann)
        {
            return SeleccionarTemasEnProcesoFiltrados(idudn, opc, ann);
        }

        public List<mod.Modelos> Seleccionar_TemasEnProcesoRedesSociales(string idconfiguracion, string idrol, string idusuario)
        {
             return SeleccionarTemasEnProcesoRedesSociales(idconfiguracion, idrol, idusuario);
        }

        public List<mod.Modelos> Seleccionar_TemasEnProcesoParaUsuarios(string idusuario)
        { 
            return SeleccionarTemasEnProcesoParaUsuarios(idusuario);
        }

        // Asuntos que van a repetirse MABE
        public int Seleccionar_AsuntosARepetirse(string repetir)
        {
            return SeleccionarAsuntosARepetirse(repetir);
        }

        public List<mod.Modelos> Seleccionar_AsuntosQueVanARepetirse(string repetir)
        {
            return SeleccionarAsuntosQueVanARepetirse(repetir);
        }

        // fin asuntos que van a repetirse MABE

        public List<mod.Oportunidades> Seleccionar_MedidoresUDN(string año)
        {
            return SeleccionarMedidoresUDN(año);
        }

        public int Seleccionar_MedidoresUDNGlobal(string año)
        {
            return SeleccionarMedidoresUDNGlobal(año);
        }

        public List<mod.Oportunidades> Seleccionar_MedidoresEstados(string idudn, string año)
        {
            return SeleccionarMedidoresEstados(idudn,año);
        }

        public List<mod.Oportunidades> Seleccionar_MedidoresEstadosGlobal(string ann)
        {
            return SeleccionarMedidoresEstadosGlobal(ann);
        }

        public List<mod.Oportunidades> Seleccionar_MedidoresEstadosAcumulado(string idudn)
        {
            return SeleccionarMedidoresEstadosAcumulado(idudn);
        }

        public List<mod.Oportunidades> Seleccionar_MedidoresEstadosEnElMes(string idudn)
        {
            return SeleccionarMedidoresEstadosEnElMes(idudn);
        }



        public List<mod.Oportunidades> AñosDisponibles()
        {
            return SeleccionarAñosDisponibles();
        }


        public int Agregar_Registro(mod.Oportunidades items)
        { 
            return Agregar(items);
        }

        public int Agregar_Completo(mod.Oportunidades items)
        { 
            return AgregarCompleto(items);
        }

        public int Agregar_OportunidadContactos(string IdOportunidad, string IdContacto, string IdTipoPersona)
        { 
            return AgregarOportunidadContactos(IdOportunidad, IdContacto, IdTipoPersona);
        }

        public int Modificar_Registro(mod.Oportunidades items)
        { 
            return Modificar(items);
        }

        public int Modificar_EstadoOportunidad(string id, string estado)
        { 
            return ModificarEstadoOportunidad(id, estado);
        }

        public int Modificar_EstadoOportunidadMasComentarios(string id, string estado, string comentarios)
        { 
            return ModificarEstadoOportunidadMasComentarios(id, estado, comentarios);
        }

        public int Modificar_ReasignacionOportunidad(string idresponsableanterior, string idoportunidad, string idnuevoresponsable)
        {
            Usuarios usuarios = new Usuarios();
            string nombreresponsableanterior = usuarios.Seleccionar_Nombre(idresponsableanterior);
            string nombrenuevoresponsable = usuarios.Seleccionar_Nombre(idnuevoresponsable);
            return ModificarReasignacionOportunidad(idresponsableanterior, idoportunidad, idnuevoresponsable, nombreresponsableanterior, nombrenuevoresponsable);
        }
    }
}
