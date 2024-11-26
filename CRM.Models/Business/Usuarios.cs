using CRM.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class Usuarios : UsuariosRepositorio
    {
        public List<mod.UsuariosRoles> Seleccionar_Registros()
        { 
            return Seleccionar();
        }

        /// <summary>
        /// Obtiene todos los usuarios del sistema
        /// </summary>
        /// <returns></returns>
        public List<mod.UsuariosRoles> Seleccionar_Todos()
        { 
            return SeleccionarTodos();
        }

        public List<mod.UsuariosRoles> Seleccionar_PorRol(string idconfiguracion, string idrol, string idusuario)
        {
            return SeleccionarPorRol(idconfiguracion, idrol, idusuario);
        }

        public List<mod.UsuariosRoles> Seleccionar_PorIdOportunidad(string idoportunidad)
        { 
            return SeleccionarPorIdOportunidad(idoportunidad);
        }

        public List<mod.Usuarios> Seleccionar_UsuariosGerentes(string idconfiguracion, string idusuario, string idrol)
        { 
            return SeleccionarUsuariosGerentes(idconfiguracion, idusuario, idrol);
        }
        public List<mod.Usuarios> Seleccionar_UsuariosGerentesSABE(string idusuario)
        {
            return SeleccionarUsuariosGerentesSABE(idusuario);
        }

        public List<mod.Usuarios> Seleccionar_UsuariosResponsables(string idconfiguracion)
        { 
            return SeleccionarUsuariosResponsables(idconfiguracion);
        }

        public List<mod.Usuarios> Seleccionar_UsuariosResponsables(string idconfiguracion, string idempresa, string idusuario, string idrol)
        { 
            return SeleccionarUsuariosResponsables(idconfiguracion, idempresa, idusuario, idrol);
        }

        public List<mod.Usuarios> Seleccionar_UsuariosResponsablesASAE(string idconfiguracion, string idusuario)
        { 
            return SeleccionarUsuariosResponsablesASAE(idconfiguracion, idusuario);
        }

        public List<mod.Usuarios> Seleccionar_UsuariosResponsablesdeASAE(string idusuario)
        {
            return SeleccionarUsuariosResponsablesdeASAE(idusuario);
        }

        public List<mod.Usuarios> Seleccionar_UsuariosPreGuardado(string idconfiguracion, string Nombre, string Correo)
        { 
            return SeleccionarUsuariosPreGuardado(idconfiguracion, Nombre, Correo);
        }

        public List<string> Selecccionar_CorreosCambiados(string idusuario)
        { 
            return SelecccionarCorreosCambiados(idusuario);
        }

        public string Seleccionar_ClaveGerente(string idusuario)
        { 
            return SeleccionarClaveGerente(idusuario);
        }

        public string Seleccionar_CorreoReponsable(string idusuario)
        { 
            return SeleccionarCorreoReponsable(idusuario);
        }

        public string Seleccionar_Nombre(string idusuario)
        { 
            return SeleccionarNombre(idusuario);
        }

        public string Seleccionar_CreadorOportunidad(string idoportunidad)
        { 
            return SeleccionarCreadorOportunidad(idoportunidad);
        }

        public mod.Usuarios Seleccionar_DetalleCreadorOportunidad(string idoportunidad)
        {
            return SeleccionarDetalleCreadorOportunidad(idoportunidad);
        }

        public int Seleccionar_UDNUsuario(string idusuario)
        {
            return SeleccionarUDNUsuario(idusuario);
        }

        public int Validar_ClaveContraseña(string clave, string contraseña)
        {
            return Validar(clave, contraseña);
        }

        public bool Validar_SiPuedeAgregarClasSubClasDocumentos(string idusuario)
        {
            if (ValidarSiPuedeAgregarClasSubClasDocumentos(idusuario) == 1)
                return true;
            else
                return false;
        }

        public List<mod.UsuariosRoles> Buscar_PorNombre(string nombre)
        { 
            return Buscar(nombre);
        }

        public mod.UsuariosRoles Seleccionar_PorId(string id)
        { 
            return SeleccionarPorId(id);
        }

        public mod.UsuariosRoles Seleccionar_PorClaveContraseña(string clave, string contraseña)
        { 
            return SeleccionarPorClaveContraseña(clave, contraseña);
        }

        public mod.UsuariosRoles Seleccionar_EmpresaPorUsuarioYaConectado(string idusuario, string idconfiguracion)
        { 
            return SeleccionarEmpresaPorUsuarioYaConectado(idusuario, idconfiguracion);
        }

        /// <summary>
        /// Verifica si el usuario puede bajar/ver archivos
        /// </summary>
        /// <param name="idusuario"></param>
        /// <returns></returns>
        public string Seleccionar_SiPuedeBajarVerArchivos(string idusuario)
        {
            return SeleccionarSiPuedeBajarVerArchivos(idusuario);
        }

        /// <summary>
        /// Valida si la contraseña es diferente de la que otorga el sistema al inicio para que el usuario la cambie
        /// </summary>
        /// <param name="clave"></param>
        /// <param name="contraseña"></param>
        /// <returns></returns>
        public int Contraseña_Cambiada(string clave, string contraseña)
        { 
            return ContraseñaCambiada(clave, contraseña);
        }

        public int Agregar_Registro(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string Clave, string Contraseña)
        { 
            return Agregar(Nombre, ApellidoPaterno, ApellidoMaterno, Correo, Clave, Contraseña);
        }

        public int Agregar_Responsable(string Nombre, string Correo)
        { 
            return AgregarResponsable(Nombre, Correo);
        }

        public int Agregar_Detalle(string IdUsuario, string Telefono, string Celular, string Empresa, string Direccion, string Ciudad, string Notas, string FisicaMoral, string RFC, string InternoExterno)
        { 
            return AgregarDetalle(IdUsuario, Telefono, Celular, Empresa, Direccion, Ciudad, Notas, FisicaMoral, RFC, InternoExterno);
        }

        public int Modificar_Detalle(string Telefono, string Celular, string Empresa, string Direccion, string Ciudad, string Notas, string IdUsuario, string FisicaMoral, string RFC, string InternoExterno)
        { 
            return ModificarDetalle(Telefono, Celular, Empresa, Direccion, Ciudad, Notas, IdUsuario, FisicaMoral, RFC, InternoExterno);
        }

        public int Modificar_Registro(string eNombre, string eApellidoPaterno, string eApellidoMaterno, string eCorreo, string eClave, string eContraseña, string eActivo, string eRol, string eId, string eArchivosPermiso, string eClasSubClasPermiso, string eIdUDN)
        { 
            return Modificar(eNombre, eApellidoPaterno, eApellidoMaterno, eCorreo, eClave, eContraseña, eActivo, eRol, eId, eArchivosPermiso, eClasSubClasPermiso, eIdUDN);
        }

        public int Modificar_Gerente(string eNombre, string eClave, string eActivo, string eId)
        { 
            return ModificarGerente(eNombre, eClave, eActivo, eId);
        }

        public int Modificar_Responsable(string Nombre, string Correo, string IdUsuario)
        { 
            return ModificarResponsable(Nombre, Correo, IdUsuario);
        }

        public int Modificar_Contraseña(string Anterior, string Nueva, string Id)
        { 
            return ModificarContraseña(Anterior, Nueva, Id);
        }

        public string Validar_CorreoUsuario(string correo)
        { 
            return ValidarCorreoUsuario(correo);
        }

        public int Reiniciar_Correo(string correo)
        { 
            return ReiniciarCorreo(correo);
        }

        public int Cambiar_CorreoResponsable(string idusuario, string correo)
        { 
            return CambiarCorreoResponsable(idusuario, correo);
        }

        public int Agregar_CorreoAHistorial(string idusuario, string correo)
        { 
            return AgregarCorreoAHistorial(idusuario, correo);
        }

        /// <summary>
        /// Calculos locales
        /// </summary>
        protected void Calcular()
        { 
        
        }

        public void Calculo()
        {
            Calcular();
        }


    }
}
