using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Contactos : Repository.ContactosRepositorio
    {
        public List<mod.Contactos> SeleccionarRegistros()
        {
            return base.Seleccionar();
        }

        public List<mod.ContactosEmpresas> Seleccionar_Todos(string idconfiguracion, string idrol, string idusuario)
        {
            return SeleccionarTodos(idconfiguracion, idrol, idusuario);
        }

        public List<mod.ContactosEmpresas> Seleccionar_Todos_Filtrado(string opcion)
        {
            return SeleccionarTodosFiltrado(opcion);
        }

        public List<mod.ContactosEmpresas> Seleccionar_Todos_Filtrado_Conteos()
        {
            return SeleccionarTodosFiltradoConteos();
        }

        public List<mod.ContactosEmpresas> Seleccionar_TodosPorConfiguracion(string idconfiguracion)
        {
            return SeleccionarTodosPorConfiguracion(idconfiguracion);
        }

        public List<mod.ContactosEmpresas> Seleccionar_TodosPorTipoContacto(string idconfiguracion, string idrol, string idusuario, string tipocontacto)
        {
            return SeleccionarTodosPorTipoContacto(idconfiguracion, idrol, idusuario, tipocontacto);
        }

        public List<mod.ContactosEmpresas> Seleccionar_Compartidos(string idusuario)
        {
            return SeleccionarCompartidos(idusuario);
        }

        public List<mod.ContactosEmpresas> Seleccionar_CompartidosPorTipoContacto(string idusuario, string tipocontacto)
        {
            return SeleccionarCompartidosPorTipoContacto(idusuario, tipocontacto);
        }

        public List<mod.ContactosEmpresas> Seleccionar_GteProyectoComisionistas(string idconfiguracion, string idrol, string idusuario)
        {
            return SeleccionarGteProyectoComisionistas(idconfiguracion, idrol, idusuario);
        }

        public List<mod.ContactosEmpresas> Seleccionar_GteProyectoComisionistasPorTipoContacto(string idconfiguracion, string idrol, string idusuario, string tipocontacto)
        {
            return SeleccionarGteProyectoComisionistasPorTipoContacto(idconfiguracion, idrol, idusuario, tipocontacto);
        }

        public List<mod.ContactosEmpresas> Seleccionar_RedesSociales(string idconfiguracion, string idrol, string idusuario)
        {
            return SeleccionarRedesSociales(idconfiguracion, idrol, idusuario);
        }

        public List<mod.ContactosEmpresas> Seleccionar_RedesSocialesPorTipoContacto(string idconfiguracion, string idrol, string idusuario, string tipocontacto)
        {
            return SeleccionarRedesSocialesPorTipoContacto(idconfiguracion, idrol, idusuario, tipocontacto);
        }

        public List<mod.Modelos> Seleccionar_ContactosEnTemasEnProceso(string idconfiguracion)
        {
            return SeleccionarContactosEnTemasEnProceso(idconfiguracion);
        }

        public string Seleccionar_IdContactoDeIdUsuario(string idusuario)
        {
            return SeleccionarIdContactoDeIdUsuario(idusuario);
        }

        public List<mod.ContactosEmpresas> Buscar_Registros(string Nombre)
        {
            return Buscar(Nombre);
        }

        public List<mod.Modelos> Buscar_1(string Nombre)
        {
            return Buscar1(Nombre);
        }

        public List<mod.Contactos> Buscar_Parecidos(mod.Contactos items)
        {
            return BuscarParecidos(items);
        }

        public string Buscar_Parecido(mod.Contactos items)
        {
            return BuscarParecido(items);
        }

        public mod.ContactosEmpresas Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public List<mod.Contactos> Seleccionar_ContactosPorEmpresa(string id)
        {
            return SeleccionarContactosPorEmpresa(id);
        }

        public List<mod.Contactos> Seleccionar_ContactosPorConfiguracion(string id, string idrol, string idusuario)
        {
            return SeleccionarContactosPorConfiguracion(id, idrol, idusuario);
        }

        public List<mod.Contactos> Seleccionar_ContactosPorConfiguracion(string id, string idrol, string idusuario, string idempresa)
        {
            return SeleccionarContactosPorConfiguracion(id, idrol, idusuario, idempresa);
        }

        public List<mod.Modelos> Seleccionar_ContactosAsignadosAOportunidad(string idconfiguracion, string idoportunidad)
        {
            return SeleccionarContactosAsignadosAOportunidad(idconfiguracion, idoportunidad);
        }

        public string Seleccionar_ValidarSiContactoPerteneceAEmpresa(string idcontacto)
        {
            return SeleccionarValidarSiContactoPerteneceAEmpresa(idcontacto);
        }

        public string Seleccionar_CreadorContacto(string idcontacto)
        {
            return SeleccionarCreadorContacto(idcontacto);
        }

        public bool Seleccionar_ValidarCreadorContacto(string idcontacto, string idusuario)
        {
            return SeleccionarValidarCreadorContacto(idcontacto, idusuario);
        }

        public string Seleccionar_NombreContacto(string idcontacto)
        {
            return SeleccionarNombreContacto(idcontacto);
        }

        public mod.ContactosEmpresas Seleccionar_PorIdDetalleContactoEmpresa(string id)
        {
            return SeleccionarPorIdDetalleContactoEmpresa(id);
        }

        public List<mod.Contactos> Pre_Guardado1(mod.Contactos items)
        {
            return PreGuardado1(items);
        }

        public int Agregar_Registro(mod.Contactos items)
        {
            return Agregar(items);
        }

        public int Agregar_3(mod.Contactos items)
        {
            return Agregar3(items);
        }

        public int Agregar_Registro_Externo(mod.Contactos items)
        {
            return Agregar4(items);
        }

        public List<mod.Contactos> Pre_Guardado2(mod.Contactos items)
        {
            return PreGuardado2(items);
        }

        public int Agregar_2(mod.Contactos items)
        {
            return Agregar2(items);
        }

        public int Modificar_Registro(mod.Contactos items)
        {
            return Modificar(items);
        }

        public int Modificar_ContactoUsuario(mod.Contactos items)
        {
            return ModificarContactoUsuario(items);
        }

        public int Eliminar_ContactoDeOportunidad(string idcontacto, string idoportunidad)
        {
            return EliminarContactoDeOportunidad(idcontacto, idoportunidad);
        }


        public System.Data.DataSet ExportaXLS(int UsuarioAlta, int IdConfiguracion, int TipoContacto)
        {
            return ExportaXLSData(UsuarioAlta, IdConfiguracion, TipoContacto);
        }
    }
}
