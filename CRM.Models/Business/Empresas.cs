using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Empresas : Repository.EmpresasRepositorio
    {
        public List<mod.Empresas> Seleccionar_Registros(string idconfiguracion, string IdRol, string idusuario)
        {
            return Seleccionar(idconfiguracion, IdRol, idusuario);
        }

        public List<mod.Empresas> Seleccionar_CompartidasPorUsuario(string idusuario)
        {
            return SeleccionarCompartidasPorUsuario(idusuario);
        }

        public List<mod.Empresas> Seleccionar_CompartidasPorUsuarioPorTipo(string idusuario, string tipo)
        {
            return SeleccionarCompartidasPorUsuarioPorTipo(idusuario, tipo);
        }

        public List<mod.Empresas> Seleccionar_Todas(string idconfiguracion, string idusuario)
        {
            return SeleccionarTodas(idconfiguracion, idusuario);
        }

        public List<mod.Empresas> Seleccionar_Todas(string idconfiguracion)
        {
            return SeleccionarTodas(idconfiguracion);
        }

        public List<mod.Empresas> Seleccionar_TodasFiltrado(string opcion)
        {
            return SeleccionarTodasFiltrado(opcion);
        }

        public List<mod.Empresas> Seleccionar_Todas_Filtrado_Conteos()
        {
            return SeleccionarTodasFiltradoConteos();
        }

        public List<mod.Empresas> Seleccionar_TodasPorTipo(string idconfiguracion, string idusuario, string tipo)
        {
            return SeleccionarTodasPorTipo(idconfiguracion, idusuario, tipo);
        }

        public List<mod.Empresas> Seleccionar_GteProyectoComisionistas()
        {
            return SeleccionarGteProyectoComisionistas();
        }

        public List<mod.Empresas> Seleccionar_GteProyectoComisionistasPorTipo(string tipo)
        {
            return SeleccionarGteProyectoComisionistasPorTipo(tipo);
        }

        public List<mod.Empresas> Seleccionar_RedesSociales()
        {
            return SeleccionarRedesSociales();
        }

        public List<mod.Empresas> Seleccionar_RedesSocialesPorTipo(string tipo)
        {
            return SeleccionarRedesSocialesPorTipo(tipo);
        }

        public string Seleccionar_RFCNoEsteOcupado(string rfc)
        {
            return SeleccionarRFCNoEsteOcupado(rfc);
        }

        public List<mod.Empresas> Buscar_Registros(string nombre)
        {
            return Buscar(nombre);
        }

        public mod.Empresas Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public List<mod.Usuarios> Seleccionar_usuariosPorEmpresaId(string id)
        {
            return SeleccionarusuariosPorEmpresaId(id);
        }

        public mod.Empresas Seleccionar_DireccionEmpresa(string id)
        {
            return SeleccionarDireccionEmpresa(id);
        }

        public List<mod.Empresas> Seleccionar_Parecidas(string nombre)
        {
            return SeleccionarParecidas(nombre);
        }

        public string Seleccionar_CreadorEmpresa(string idempresa)
        {
            return SeleccionarCreadorEmpresa(idempresa);
        }

        public string Seleccionar_NombreEmpresa(string idempresa)
        {
            return SeleccionarNombreEmpresa(idempresa);
        }

        public string Seleccionar_SiCamposIncompletos(string idempresa)
        {
            return SeleccionarSiCamposIncompletos(idempresa);
        }

        public List<mod.Modelos> Seleccionar_EmpresasEnTemasEnProceso(string idconfiguracion)
        {
            return SeleccionarEmpresasEnTemasEnProceso(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_EmpresasTemasDetalle1(string idconfiguracion)
        {
            return SeleccionarEmpresasTemasDetalle1(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_EmpresasTemasDetalle12(string idconfiguracion)
        {
            return SeleccionarEmpresasTemasDetalle12(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_EmpresasTemasDetalle13(string idconfiguracion)
        {
            return SeleccionarEmpresasTemasDetalle13(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_EmpresasTemasDetalle14(string idconfiguracion)
        {
            return SeleccionarEmpresasTemasDetalle14(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_EmpresasTemasDetalle2(string idconfiguracion)
        {
            return SeleccionarEmpresasTemasDetalle2(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_EmpresasTemasDetalle5(string idconfiguracion)
        {
            return SeleccionarEmpresasTemasDetalle5(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_EmpresasTemasDetalle6(string idconfiguracion)
        {
            return SeleccionarEmpresasTemasDetalle6(idconfiguracion);
        }

        public List<mod.Modelos> Seleccionar_EmpresasTemasDetalle7(string idconfiguracion)
        { 
            return SeleccionarEmpresasTemasDetalle7(idconfiguracion);
        }

        public int Agregar_Registro(mod.Empresas items)
        {
            return Agregar(items);
        }

        public void Agregar_EmpresasAGerente(string idusuario, string idconfiguracion)
        {
            AgregarEmpresasAGerente(idusuario, idconfiguracion);
        }

        public List<mod.Empresas> Pre_Guardado(mod.Empresas items)
        {
            return PreGuardado(items);
        }

        public int Agregar_Completo(mod.Empresas items)
        {
            return AgregarCompleto(items);
        }

        public int Agregar_Parcial(mod.Empresas items)
        {
            return AgregarParcial(items);
        }

        public int Agregar_YContinuar(mod.Empresas items)
        {
            return AgregarYContinuar(items);
        }

        public bool Modificar_Registro(mod.Empresas items)
        {
            return Modificar(items);
        }
    }
 }
