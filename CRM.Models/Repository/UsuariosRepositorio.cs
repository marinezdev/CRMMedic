using CRM.Models.Models;
using CRM.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class UsuariosRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<UsuariosRoles> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT us.id,us.nombre, ISNULL(us.apellidopaterno, '') AS ApellidoPaterno, ISNULL(us.apellidomaterno, '') AS ApellidoMaterno, ro.Id AS Rol, conf.Nombre AS Empresa " +
            "FROM usuarios us " +
            "INNER JOIN UsuariosRoles ur ON us.Id = ur.IdUsuario " +
            "INNER JOIN Roles ro ON ur.IdRol = ro.Id " +
            "INNER JOIN UsuarioConfiguracion uc ON uc.IdUsuario=us.id " +
            "INNER JOIN Configuracion conf ON conf.Id = uc.IdConfiguracion");
            List<UsuariosRoles> resultado = new List<UsuariosRoles>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UsuariosRoles item              = new UsuariosRoles();
                item.Usuarios.Id                = int.Parse(reader["Id"].ToString());
                item.Usuarios.Nombre            = reader["Nombre"].ToString();
                item.Usuarios.ApellidoPaterno   = reader["ApellidoPaterno"].ToString();
                item.Usuarios.ApellidoMaterno   = reader["ApellidoMaterno"].ToString();
                item.Roles.Id                   = int.Parse(reader["Rol"].ToString());
                item.Configuracion.Nombre       = reader["Empresa"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UsuariosRoles> SeleccionarTodos()
        {
            b.ExecuteCommandQuery("SELECT usu.id, usu.Nombre, usu.ApellidoPaterno, ISNULL(usu.ApellidoMaterno, '') AS ApellidoMaterno, usu.Clave, usu.Activo, usu.Correo, usu.archivospermiso, usu.ClasSubClasPermiso, " +
            "conf.Nombre AS Empresa " +
            "FROM usuarios usu " +
            "INNER JOIN UsuarioConfiguracion uc ON uc.IdUsuario=usu.id " +
            "INNER JOIN Configuracion conf ON conf.Id=uc.IdConfiguracion " +
            "WHERE usu.activo=1 " +
            "ORDER BY usu.nombre ");
            List<UsuariosRoles> resultado = new List<UsuariosRoles>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UsuariosRoles item              = new UsuariosRoles();
                item.Usuarios.Id                = int.Parse(reader["Id"].ToString());
                item.Usuarios.Nombre            = reader["Nombre"].ToString();
                item.Usuarios.ApellidoPaterno   = reader["ApellidoPaterno"].ToString();
                item.Usuarios.ApellidoMaterno   = reader["ApellidoMaterno"].ToString();
                item.Usuarios.Correo            = reader["Correo"].ToString();
                item.Usuarios.Clave             = reader["Clave"].ToString();
                item.Usuarios.Activo            = bool.Parse(reader["Activo"].ToString());
                item.Usuarios.ArchivosPermiso   = bool.Parse(reader["archivospermiso"].ToString());
                item.Usuarios.ClasSubClasPermiso = bool.Parse(reader["classubclaspermiso"].ToString());
                item.Configuracion.Nombre       = reader["Empresa"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UsuariosRoles> SeleccionarPorRol(string idconfiguracion, string idrol, string idusuario)
        {
            string consulta = "SELECT usu.id, usu.Nombre, usu.ApellidoPaterno , usu.ApellidoMaterno " +
            "FROM usuarios usu " +
            "INNER JOIN UsuarioConfiguracion uc ON uc.IdUsuario = usu.id " +
            "INNER JOIN Configuracion conf ON conf.Id = uc.IdConfiguracion " +
            "INNER JOIN UsuariosRoles ur ON ur.IdUsuario = usu.Id " +
            "WHERE conf.id=@idconfiguracion AND usu.activo=1 ";
            if (!string.IsNullOrEmpty(idrol) && idrol=="2")
                consulta += "AND ur.IdRol IN (2, 3) ";
            //else if (!string.IsNullOrEmpty(idrol) && idrol == "7")
            //    consulta += "AND ur.IdRol=6 ";
            else
                consulta += "AND ur.IdRol=@idrol ";
            consulta += "AND usu.id NOT IN (@idusuario) ORDER BY usu.nombre";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            List<UsuariosRoles> resultado = new List<UsuariosRoles>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UsuariosRoles item              = new UsuariosRoles();
                item.Usuarios.Id                = int.Parse(reader["Id"].ToString());
                item.Usuarios.Nombre            = reader["Nombre"].ToString();                
                item.Usuarios.ApellidoPaterno   = reader["ApellidoPaterno"].ToString();
                item.Usuarios.ApellidoMaterno   = reader["ApellidoMaterno"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UsuariosRoles> SeleccionarPorIdOportunidad(string idoportunidad)
        {
            string consulta = "SELECT opor.idoportunidad, " +
            "usu.id AS IdUsuario, usu.Nombre, usu.apellidopaterno, ISNULL(usu.ApellidoMaterno, '') AS ApellidoMaterno, " +
            "bic.id AS idbitacora " +
            "FROM oportunidadesresponsables opor " +
            "INNER JOIN usuarios usu ON usu.id = opor.idasignado " +
            "LEFT JOIN Bitacora bic ON (bic.IdResponsable=usu.id AND bic.IdOportunidad=opor.IdOportunidad) " +
            "WHERE opor.idoportunidad = @id AND usu.activo=1 ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", idoportunidad, SqlDbType.Int);
            List<UsuariosRoles> resultado = new List<UsuariosRoles>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UsuariosRoles item              = new UsuariosRoles();
                item.Usuarios.Id                = int.Parse(reader["IdUsuario"].ToString());
                item.Usuarios.Nombre            = reader["Nombre"].ToString();
                item.Usuarios.ApellidoPaterno   = reader["ApellidoPaterno"].ToString();
                item.Usuarios.ApellidoMaterno   = reader["ApellidoMaterno"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<Usuarios> SeleccionarUsuariosGerentes(string idconfiguracion, string idusuario, string idrol)
        {
            string consulta = "SELECT usu.id, LTRIM(usu.nombre) AS Nombre, usu.ApellidoPaterno, usu.ApellidoMaterno, usu.clave, ud.Telefono  " +
            "FROM usuarios usu " +
            "INNER JOIN UsuarioConfiguracion usuc ON usuc.IdUsuario=usu.id " +
            "INNER JOIN UsuariosRoles ur ON ur.IdUsuario=usu.Id " +
            "LEFT JOIN UsuariosDetalle ud ON ud.IdUsuario=usu.Id " +
            "WHERE usuc.IdConfiguracion=@idconfiguracion " +
            "AND usu.activo=1 ";
            if (idrol == "2")
                consulta += "AND ur.idrol IN (2,3, 5, 6, 7) ";
            if (idrol == "3")
                consulta += "AND ur.idrol IN (3, 5, 6, 7) ";
            if (idrol == "5")
                consulta += "AND ur.idrol IN (5, 6, 7) ";
            if (idrol == "6")
                consulta += "AND ur.idrol IN (6, 7) ";
            if (idrol == "7")
                    consulta += "AND ur.idrol = 6 ";
            //else
            //consulta += "AND ur.idrol IN (2,3) " +
            consulta += "AND usu.id<>@idusuario " +
            "ORDER BY usu.Nombre, usu.apellidopaterno, ur.idrol";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                item.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                item.Clave = reader["Clave"].ToString();
                item.Telefono = reader["telefono"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<Usuarios> SeleccionarUsuariosGerentesSABE(string idusuario)
        {
            string consulta = "SELECT usu.id, LTRIM(usu.nombre) AS Nombre, usu.ApellidoPaterno, usu.ApellidoMaterno, usu.correo, usu.clave, ud.Telefono  " +
            "FROM usuarios usu " +
            "INNER JOIN UsuarioConfiguracion usuc ON usuc.IdUsuario=usu.id " +
            "INNER JOIN UsuariosRoles ur ON ur.IdUsuario=usu.Id " +
            "LEFT JOIN UsuariosDetalle ud ON ud.IdUsuario=usu.Id " +
            "WHERE usuc.IdConfiguracion=3 " +
            "AND usu.activo=1 " +
            "AND ur.idrol = 3 " +
            "AND usu.id<>@idusuario " +
            "ORDER BY usu.Nombre, usu.apellidopaterno";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                item.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                item.Correo = reader["correo"].ToString();
                item.Clave = reader["Clave"].ToString();
                item.Telefono = reader["telefono"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<Usuarios> SeleccionarUsuariosResponsables(string idconfiguracion)
        {
            string consulta = "SELECT usu.id, LTRIM(usu.nombre) AS Nombre, usu.ApellidoPaterno, usu.ApellidoMaterno, eu.IdEmpresa, " +
            "usu.Correo, ud.Telefono, ud.Celular, emp.Nombre AS Empresa, ud.FisicaMoral, ud.RFC, ud.internoexterno " +
            "FROM usuarios usu " +
            "INNER JOIN UsuarioConfiguracion usuc ON usuc.IdUsuario = usu.id " +
            "LEFT JOIN EmpresasUsuarios eu ON eu.IdUsuario = usu.id " +
            "LEFT JOIN UsuariosRoles ur ON ur.IdUsuario = usu.Id " +
            "LEFT JOIN usuariosdetalle ud ON ud.idusuario=usu.id " +
            "LEFT JOIN empresas emp ON emp.id=eu.idempresa " +
            "WHERE usuc.IdConfiguracion = @idconfiguracion " +
            "AND usu.activo=1 " +
            "AND ur.IdRol=5 ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                item.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                item.Telefono = reader["Telefono"].ToString();
                item.Celular = reader["Celular"].ToString();
                item.Correo = reader["Correo"].ToString();
                item.EmpresaNombre = reader["Empresa"].ToString();
                item.FisicaMoral = reader["fisicamoral"].ToString() == "" ? 0 : int.Parse(reader["fisicamoral"].ToString());
                item.RFC = reader["rfc"].ToString();
                item.InternoExterno = reader["internoexterno"].ToString() == "" ? 0 : int.Parse(reader["internoexterno"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }
        /// <summary>
        /// Selecciona usuarios responsables
        /// </summary>
        /// <remarks>
        /// 29/09/20 Se ha modificado la consulta para que los usuarios de mabe puedan seleccioanr cualquier responsable sin importar la empresa 
        /// </remarks>
        /// <param name="idconfiguracion"></param>
        /// <param name="idempresa"></param>
        /// <param name="idusuario"></param>
        /// <param name="idrol"></param>
        /// <returns></returns>
        protected List<Usuarios> SeleccionarUsuariosResponsables(string idconfiguracion, string idempresa, string idusuario, string idrol)
        {
            string consulta_ant = "SELECT usu.id, LTRIM(usu.nombre) AS Nombre, usu.ApellidoPaterno, usu.ApellidoMaterno, eu.IdEmpresa " +
            "FROM usuarios usu " +
            "INNER JOIN UsuarioConfiguracion usuc ON usuc.IdUsuario = usu.id " +
            "LEFT JOIN EmpresasUsuarios eu ON eu.IdUsuario = usu.id " +
            "INNER JOIN UsuariosRoles ur ON ur.IdUsuario = usu.Id " +
            "WHERE usuc.IdConfiguracion = @idconfiguracion " +
            "AND usu.activo=1 ";
            if (!string.IsNullOrEmpty(idempresa))
                consulta_ant += "AND eu.IdEmpresa = @idempresa ";
            consulta_ant += "AND usu.id<> @idusuario " +
            "AND ur.IdRol>=@idrol " +
            "ORDER BY usu.nombre, usu.ApellidoPaterno, ";

            string consulta = "";
            if (idconfiguracion == "2")
            {
                consulta = "SELECT usu.id, LTRIM(usu.nombre) AS Nombre, usu.ApellidoPaterno, usu.ApellidoMaterno " +
                "FROM usuarios usu " +
                "INNER JOIN UsuarioConfiguracion usuc ON usuc.IdUsuario = usu.id " +
                "INNER JOIN UsuariosRoles ur ON ur.IdUsuario = usu.Id " +
                "WHERE usuc.IdConfiguracion = @idconfiguracion " +
                "AND usu.activo=1 " +
                "AND usu.id<> @idusuario " +
                "ORDER BY ur.idrol, usu.nombre, usu.ApellidoPaterno";
            }
            else if (idconfiguracion == "3")
            {
                consulta = "SELECT usu.id, LTRIM(usu.nombre) + ' ' +  " + 
                    "CASE " +
                    "    WHEN ur.IdRol = 2 THEN '[Supervisión]' " +
                    "    WHEN ur.idrol = 3 THEN '[Administrador Asuntos]' " +
                    "    WHEN ur.idrol = 5 THEN '[Proveedor]' " +
                    "    END AS Nombre, usu.ApellidoPaterno, usu.ApellidoMaterno " +
                    "FROM usuarios usu " +
                    "INNER JOIN UsuarioConfiguracion usuc ON usuc.IdUsuario = usu.id " +
                    "INNER JOIN UsuariosRoles ur ON ur.IdUsuario = usu.Id " +
                    "WHERE usuc.IdConfiguracion = @idconfiguracion " +
                    "AND usu.activo=1 " +
                    "AND usu.id<> @idusuario " +
                    "ORDER BY ur.idrol, usu.nombre, usu.ApellidoPaterno";
            }
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            if (!string.IsNullOrEmpty(idempresa))
                b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios();
                item.Id              = int.Parse(reader["Id"].ToString());
                item.Nombre          = reader["Nombre"].ToString();
                item.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<Usuarios> SeleccionarUsuariosResponsablesASAE(string idconfiguracion, string idusuario)
        {
            string consulta = "SELECT usu.id, LTRIM(usu.nombre) AS Nombre, usu.ApellidoPaterno, usu.ApellidoMaterno " +
            "FROM usuarios usu " +
            "INNER JOIN UsuarioConfiguracion usuc ON usuc.IdUsuario = usu.id " +
            "LEFT JOIN UsuariosRoles ur ON ur.IdUsuario = usu.Id " +
            "WHERE usuc.IdConfiguracion = @idconfiguracion " +
            "AND usu.id <> @idusuario " +
            "AND ur.IdRol IN(2,3,5,6,7) " +
            "AND usu.activo=1";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios();
                item.Id              = int.Parse(reader["Id"].ToString());
                item.Nombre          = reader["Nombre"].ToString();
                item.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<Usuarios> SeleccionarUsuariosResponsablesdeASAE(string idusuario)
        {
            string consulta = "SELECT usu.id, LTRIM(usu.nombre) AS Nombre, usu.ApellidoPaterno, usu.ApellidoMaterno " +
            "FROM usuarios usu " +
            "INNER JOIN UsuarioConfiguracion usuc ON usuc.IdUsuario = usu.id " +
            "LEFT JOIN UsuariosRoles ur ON ur.IdUsuario = usu.Id " +
            "WHERE usuc.IdConfiguracion = 2 " +
            "AND usu.id <> @idusuario " +
            "AND ur.IdRol IN(2,3,5,6,8) " +
            "AND usu.activo=1";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                item.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<Usuarios> SeleccionarUsuariosPreGuardado(string idconfiguracion, string Nombre, string Correo)
        {
            string consulta = "SELECT LTRIM(usu.nombre) AS Nombre, usu.correo " +
            "FROM usuarios usu " +
            "INNER JOIN usuarioconfiguracion uc ON uc.idusuario=usu.id " +
            "WHERE usu.nombre LIKE @nombre OR usu.correo LIKE @correo " +
            "AND uc.idconfiguracion=@idconfiguracion " +
            "AND usu.activo=1";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", "%" + Nombre + "%", SqlDbType.NVarChar);
            b.AddParameter("@correo", "%" + Correo + "%", SqlDbType.NVarChar);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios();
                item.Nombre = reader["Nombre"].ToString();
                item.Correo = reader["Correo"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<string> SelecccionarCorreosCambiados(string idusuario)
        {
            string consulta = "SELECT correo FROM otroscorreos WHERE idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            List<string> resultado = new List<string>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Add(reader["correo"].ToString());
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected string SeleccionarClaveGerente(string idusuario)
        {
            string consulta = "SELECT clave FROM usuarios WHERE id=@idusuario AND activo=1";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.SelectString();
        }

        protected string SeleccionarCorreoReponsable(string idusuario)
        {
            string consulta = "SELECT correo FROM usuarios WHERE id=@id AND activo=1";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", idusuario, SqlDbType.NVarChar);
            return b.SelectString();
        }

        protected string SeleccionarNombre(string idusuario)
        {
            string consulta = "SELECT nombre + ' ' + isnull(apellidopaterno, '') + ' ' + isnull(apellidomaterno,'') AS Nombre FROm usuarios WHERE id=@id AND activo=1";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", idusuario, SqlDbType.Int);
            return b.SelectString();
        }

        protected string SeleccionarCreadorOportunidad(string idoportunidad)
        {
            string consulta = "SELECT oecu.IdUsuario " +
            "FROM usuarios usu " +
            "INNER JOIN oecu ON oecu.IdUsuario = usu.id " +
            "WHERE oecu.IdOportunidad=@idoportunidad ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            return b.SelectString();
        }

        protected Usuarios SeleccionarDetalleCreadorOportunidad(string idoportunidad)
        {
            string consulta = "SELECT usu.id, usu.nombre + ' ' + usu.ApellidoPaterno +  ' ' + usu.ApellidoMaterno As Nombre, usu.correo " +
            "FROM usuarios usu " +
            "INNER JOIN oecu ON oecu.IdUsuario = usu.id " +
            "WHERE oecu.IdOportunidad = @idoportunidad " +
            "AND usu.Activo = 1";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            Usuarios resultado = new Usuarios();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.Nombre = reader["nombre"].ToString();
                resultado.Correo = reader["correo"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected int SeleccionarUDNUsuario(string idusuario)
        {
            b.ExecuteCommandQuery("SELECT idudn FROM usuarios WHERE id=@id");
            b.AddParameter("@id", idusuario, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected int Validar(string clave, string contraseña)
        {
            b.ExecuteCommandQuery("SELECT 1 FROM usuarios WHERE clave=@clave AND contraseña=@contraseña AND activo=1");
            b.AddParameter("@clave", clave, SqlDbType.NVarChar);
            b.AddParameter("@contraseña", Utilerias.Cifrado.Encriptar(contraseña), SqlDbType.NVarChar);
            return int.Parse(b.SelectString());
        }

        protected int ValidarSiPuedeAgregarClasSubClasDocumentos(string idusuario)
        {
            b.ExecuteCommandQuery("SELECT 1 FROM usuarios WHERE id=@idusuario AND classubclaspermiso=1");
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected List<UsuariosRoles> Buscar(string nombre)
        {
            b.ExecuteCommandQuery("SELECT usu.id,usu.nombre, us.apellidopaterno, ISNULL(us.apellidomaterno, '') AS ApellidoMaterno, usu.clave, usu.contraseña, " +
            "rol.id as IdRol, rol.nombre AS NombreRol " +
            "FROM usuarios usu " +
            "INNER JOIN usuariosroles usurol ON usu.id = usurol.idusuario " +
            "INNER JOIN roles rol ON rol.id = usurol.idrol WHERE usu.Nombre LIKE @nombre " +
            "WHERE usu.activo=1");
            b.AddParameter("@nombre", "%" + nombre + "%", SqlDbType.NVarChar);
            List<UsuariosRoles> resultado = new List<UsuariosRoles>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UsuariosRoles item              = new UsuariosRoles();
                item.Usuarios.Id                = int.Parse(reader["Id"].ToString());
                item.Usuarios.Nombre            = reader["Nombre"].ToString();
                item.Usuarios.ApellidoPaterno   = reader["ApellidoPaterno"].ToString();
                item.Usuarios.ApellidoMaterno   = reader["ApellidoMaterno"].ToString();
                item.Usuarios.Clave             = reader["Clave"].ToString();
                item.Usuarios.Contraseña        = Utilerias.Cifrado.Encriptar(reader["Contraseña"].ToString());
                item.Usuarios.Activo            = bool.Parse(reader["Activo"].ToString());
                item.Roles.Id                   = int.Parse(reader["IdRol"].ToString());
                item.Roles.Nombre               = reader["NombreRol"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Obtiene el detalle del usuario para modificarlo (ahora incluye el detalle del usuario si se necesita)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected UsuariosRoles SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT usu.id,usu.nombre, usu.apellidopaterno, ISNULL(usu.apellidomaterno, '') AS ApellidoMaterno, usu.Correo, usu.clave, usu.contraseña, usu.Activo, usu.archivospermiso, usu.ClasSubClasPermiso, usu.idudn, " +
            "rol.id as IdRol, rol.nombre AS NombreRol, " +
            "conf.id AS IdEmpresa, " +
            "ud.Telefono, ud.celular, ud.empresa, ud.direccion, ud.ciudad, ud.notas, ud.fisicamoral, ud.rfc, ud.internoexterno " +
            "FROM usuarios usu " +
            "INNER JOIN usuariosroles usurol ON usu.id = usurol.idusuario " +
            "INNER JOIN roles rol ON rol.id = usurol.idrol " +
            "INNER JOIN UsuarioConfiguracion uc ON uc.IdUsuario=usu.id " +
            "INNER JOIN Configuracion conf ON conf.Id=uc.IdConfiguracion " +
            "LEFT JOIN usuariosdetalle ud ON ud.idusuario=usu.id " +
            "WHERE usu.id=@id " +
            "AND usu.activo=1");
            b.AddParameter("@id", id, SqlDbType.Int);
            UsuariosRoles resultado = new UsuariosRoles();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Usuarios.Id               = int.Parse(reader["Id"].ToString());
                resultado.Usuarios.Nombre           = reader["Nombre"].ToString();
                resultado.Usuarios.ApellidoPaterno  = reader["ApellidoPaterno"].ToString();
                resultado.Usuarios.ApellidoMaterno  = reader["ApellidoMaterno"].ToString();
                resultado.Usuarios.Correo           = reader["Correo"].ToString();
                resultado.Usuarios.Clave            = reader["Clave"].ToString();
                resultado.Usuarios.Contraseña       = Utilerias.Cifrado.Desencriptar(reader["Contraseña"].ToString());
                resultado.Usuarios.Activo           = bool.Parse(reader["Activo"].ToString());
                resultado.Usuarios.Telefono         = reader["Telefono"].ToString();
                resultado.Usuarios.Celular          = reader["Celular"].ToString();
                resultado.Usuarios.Empresa          = reader["Empresa"].ToString() == "" ? 0 : int.Parse(reader["Empresa"].ToString());
                resultado.Usuarios.Direccion        = reader["Direccion"].ToString();
                resultado.Usuarios.Ciudad           = reader["Ciudad"].ToString();
                resultado.Usuarios.Notas            = reader["Notas"].ToString();
                resultado.Usuarios.FisicaMoral      = reader["fisicamoral"].ToString() == "" ? 0 : int.Parse(reader["fisicamoral"].ToString());
                resultado.Usuarios.RFC              = reader["rfc"].ToString();
                resultado.Usuarios.InternoExterno   = reader["internoexterno"].ToString() == "" ? 0 : int.Parse(reader["internoexterno"].ToString());
                resultado.Usuarios.ArchivosPermiso  = bool.Parse(reader["archivospermiso"].ToString());
                resultado.Usuarios.ClasSubClasPermiso = bool.Parse(reader["classubclaspermiso"].ToString());
                resultado.Usuarios.IdUDN            = reader["idudn"].ToString() == "" ? 0 : int.Parse(reader["idudn"].ToString());

                resultado.Roles.Id                  = int.Parse(reader["IdRol"].ToString());
                resultado.Roles.Nombre              = reader["NombreRol"].ToString();

                resultado.Configuracion.Id          = int.Parse(reader["IdEmpresa"].ToString());
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected UsuariosRoles SeleccionarPorClaveContraseña(string clave, string contraseña)
        {
            b.ExecuteCommandQuery("SELECT usu.id,usu.nombre, usu.apellidopaterno, ISNULL(usu.apellidomaterno, '') AS ApellidoMaterno, usu.correo, usu.clave, usu.contraseña, " +
            "usu.Activo, usu.ClasSubClasPermiso, " +
            "rol.id AS IdRol, rol.nombre AS NombreRol, rol.paginainicio, " +
            "conf.id AS IdConfiguracion, conf.logo, conf.Nombre AS Empresa, conf.TituloIntermedioPestaña, conf.ClaseLogo, conf.ClaseNavegacion " +
            "FROM usuarios usu " +
            "INNER JOIN usuariosroles usurol ON usu.id = usurol.idusuario " +
            "INNER JOIN roles rol ON rol.id = usurol.idrol " +
            "INNER JOIN UsuarioConfiguracion uc ON uc.IdUsuario=usu.id " +
            "INNER JOIN Configuracion conf ON conf.Id=uc.IdConfiguracion " +
            "WHERE usu.clave=@clave " +
            "AND usu.contraseña=@contraseña " +
            "AND usu.activo=1 " +
            "AND rol.activo=1");
            b.AddParameter("@clave", clave, SqlDbType.NVarChar);
            b.AddParameter("@contraseña", Utilerias.Cifrado.Encriptar(contraseña), SqlDbType.NVarChar);
            UsuariosRoles resultado = new UsuariosRoles();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Usuarios.Id                           = int.Parse(reader["Id"].ToString());
                resultado.Usuarios.Nombre                       = reader["Nombre"].ToString();
                resultado.Usuarios.ApellidoPaterno              = reader["ApellidoPaterno"].ToString();
                resultado.Usuarios.ApellidoMaterno              = reader["ApellidoMaterno"].ToString();
                resultado.Usuarios.Correo                       = reader["correo"].ToString();
                resultado.Usuarios.Clave                        = reader["Clave"].ToString();
                resultado.Usuarios.Contraseña                   = Utilerias.Cifrado.Encriptar(reader["Contraseña"].ToString());
                resultado.Usuarios.Activo                       = bool.Parse(reader["Activo"].ToString());
                resultado.Usuarios.ClasSubClasPermiso           = bool.Parse(reader["classubclaspermiso"].ToString());
                resultado.Roles.Id                              = int.Parse(reader["IdRol"].ToString());
                resultado.Roles.Nombre                          = reader["NombreRol"].ToString();
                resultado.Roles.PaginaInicio                    = reader["paginainicio"].ToString();
                resultado.Configuracion.Id                      = int.Parse(reader["IdConfiguracion"].ToString());
                resultado.Configuracion.Logo                    = reader["Logo"].ToString();
                resultado.Configuracion.Nombre                  = reader["Empresa"].ToString();
                resultado.Configuracion.TituloIntermedioPestaña = reader["TituloIntermedioPestaña"].ToString();
                resultado.Configuracion.ClaseLogo               = reader["ClaseLogo"].ToString();
                resultado.Configuracion.ClaseNavegacion         = reader["ClaseNavegacion"].ToString();
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected UsuariosRoles SeleccionarEmpresaPorUsuarioYaConectado(string idusuario, string idconfiguracion)
        {
            string consulta = "SELECT usu.id,usu.nombre, usu.apellidopaterno, ISNULL(usu.apellidomaterno, '') AS ApellidoMaterno, usu.correo, usu.clave, usu.contraseña, usu.Activo, " +
            "rol.id AS IdRol, rol.nombre AS NombreRol, " +
            "conf.id AS IdConfiguracion, conf.logo, conf.Nombre AS Empresa, conf.TituloIntermedioPestaña, conf.ClaseLogo, conf.ClaseNavegacion " +
            "FROM usuarios usu " +
            "INNER JOIN usuariosroles usurol ON usu.id = usurol.idusuario " +
            "INNER JOIN roles rol ON rol.id = usurol.idrol " +
            "INNER JOIN multi mu ON mu.idusuario = usu.id " +
            "INNER JOIN Configuracion conf ON conf.Id = mu.idconfiguracion " +
            "WHERE usu.id = @idusuario " +
            "AND usu.activo = 1 " +
            "AND mu.idconfiguracion = @idconfiguracion";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            UsuariosRoles resultado = new UsuariosRoles();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Usuarios.Id = int.Parse(reader["Id"].ToString());
                resultado.Usuarios.Nombre = reader["Nombre"].ToString();
                resultado.Usuarios.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                resultado.Usuarios.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                resultado.Usuarios.Correo = reader["correo"].ToString();
                resultado.Usuarios.Clave = reader["Clave"].ToString();
                resultado.Usuarios.Contraseña = Utilerias.Cifrado.Encriptar(reader["Contraseña"].ToString());
                resultado.Usuarios.Activo = bool.Parse(reader["Activo"].ToString());
                resultado.Roles.Id = int.Parse(reader["IdRol"].ToString());
                resultado.Roles.Nombre = reader["NombreRol"].ToString();
                resultado.Configuracion.Id = int.Parse(reader["IdConfiguracion"].ToString());
                resultado.Configuracion.Logo = reader["Logo"].ToString();
                resultado.Configuracion.Nombre = reader["Empresa"].ToString();
                resultado.Configuracion.TituloIntermedioPestaña = reader["TituloIntermedioPestaña"].ToString();
                resultado.Configuracion.ClaseLogo = reader["ClaseLogo"].ToString();
                resultado.Configuracion.ClaseNavegacion = reader["ClaseNavegacion"].ToString();
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected string SeleccionarSiPuedeBajarVerArchivos(string idusuario)
        {
            b.ExecuteCommandQuery("SELECT archivospermiso FROM usuarios WHERE id=@idusuario AND activo=1");
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.SelectString();
        }

        /// <summary>
        /// Valida si la contraseña difiere de la otorgada por default para que sea cambiada por una personalizada
        /// </summary>
        /// <param name="clave"></param>
        /// <param name="contraseña"></param>
        /// <returns></returns>
        protected int ContraseñaCambiada(string clave, string contraseña)
        {
            string consulta = "SELECT contraseña " +
            "FROM usuarios " +
            "WHERE clave=@clave " +
            "AND contraseña=@contraseña";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@clave", clave, SqlDbType.NVarChar);
            b.AddParameter("@contraseña", Cifrado.Encriptar(contraseña), SqlDbType.NVarChar);
            if (b.SelectString() == Cifrado.Encriptar(Funciones.ClaveSalida()))
                return 0;
            else
                return 1;
        }

        protected int Agregar(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Correo, string Clave, string Contraseña)
        {
            b.ExecuteCommandQuery("IF NOT EXISTS(SELECT nombre,apellidopaterno,apellidomaterno FROM usuarios WHERE nombre LIKE '%' + @nombre + '%' AND apellidopaterno LIKE '%' + @apaterno + '%' AND apellidomaterno LIKE '%' + @amaterno + '%') " +
            "BEGIN " +
            "INSERT INTO usuarios (nombre,apellidopaterno,apellidomaterno,correo,clave,contraseña,activo,archivospermiso,classubclaspermiso) " +
            "VALUES(@nombre,@apaterno,@amaterno,@correo,@clave,@contraseña,1,1,1); " +
            "SELECT @@IDENTITY " +
            "END " +
            "ELSE " +
            "SELECT 0");
            b.AddParameter("@nombre", Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apaterno", ApellidoPaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@amaterno", ApellidoMaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@correo", Correo, SqlDbType.NVarChar);
            b.AddParameter("@clave", Clave, SqlDbType.NVarChar);
            b.AddParameter("@contraseña", Cifrado.Encriptar(Contraseña), SqlDbType.NVarChar);
            //b.AddParameter("@activo", Activo == "on" ? true : false, SqlDbType.Bit);
            return b.SelectWithReturnValue();
        }

        protected int AgregarResponsable(string Nombre, string Correo)
        {
            b.ExecuteCommandQuery("" +
            "INSERT INTO usuarios (nombre,correo,clave,contraseña,activo) " +
            "VALUES(@nombre, @correo, @clave, @contraseña, 1); " +
            "SELECT @@IDENTITY");
            b.AddParameter("@nombre", Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@correo", Correo, SqlDbType.NVarChar);
            b.AddParameter("@clave", Correo, SqlDbType.NVarChar);
            b.AddParameter("@contraseña", Utilerias.Cifrado.Encriptar(Funciones.ClaveSalida()), SqlDbType.NVarChar);
            return  b.SelectWithReturnValue();
        }

        protected int AgregarDetalle(string IdUsuario, string Telefono, string Celular, string Empresa, string Direccion, string Ciudad, string Notas, string FisicaMoral, string RFC, string InternoExterno)
        {
            string consulta = "INSERT INTO usuariosdetalle (IdUsuario";
            if (!string.IsNullOrEmpty(Telefono))
                consulta += ",telefono ";
            if (!string.IsNullOrEmpty(Celular))
                consulta += ",celular ";
            if (!string.IsNullOrEmpty(Empresa))
                consulta += ",empresa ";
            if (!string.IsNullOrEmpty(Direccion))
                consulta += ",direccion ";
            if (!string.IsNullOrEmpty(Ciudad))
                consulta += ",ciudad ";
            if (!string.IsNullOrEmpty(Notas))
                consulta += ",notas ";
            if (!string.IsNullOrEmpty(FisicaMoral))
                consulta += ",fisicamoral ";
            if (!string.IsNullOrEmpty(RFC))
                consulta += ",rfc ";
            if (!string.IsNullOrEmpty(InternoExterno))
                consulta += ",internoexterno ";
            consulta += ") " +
                "VALUES(@idusuario";
            if (!string.IsNullOrEmpty(Telefono))
                consulta += ",@telefono ";
            if (!string.IsNullOrEmpty(Celular))
                consulta += ",@celular ";
            if (!string.IsNullOrEmpty(Empresa))
                consulta += ",@empresa ";
            if (!string.IsNullOrEmpty(Direccion))
                consulta += ",@direccion ";
            if (!string.IsNullOrEmpty(Ciudad))
                consulta += ",@ciudad ";
            if (!string.IsNullOrEmpty(Notas))
                consulta += ",@notas ";                
            if (!string.IsNullOrEmpty(FisicaMoral))
                consulta += ",@fisicamoral ";
            if (!string.IsNullOrEmpty(RFC))
                consulta += ",@rfc ";
            if(!string.IsNullOrEmpty(InternoExterno))
                consulta += ",@internoexterno ";
            consulta += ")";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", IdUsuario, SqlDbType.Int);
            if (!string.IsNullOrEmpty(Telefono))
                b.AddParameter("@telefono", Telefono, SqlDbType.NVarChar);
            if (!string.IsNullOrEmpty(Celular))
                b.AddParameter("@celular", Celular, SqlDbType.NVarChar);
            if (!string.IsNullOrEmpty(Empresa))
                b.AddParameter("@empresa", Empresa, SqlDbType.Int);
            if (!string.IsNullOrEmpty(Direccion))
                b.AddParameter("@direccion", Direccion, SqlDbType.NVarChar);
            if (!string.IsNullOrEmpty(Ciudad))
                b.AddParameter("@ciudad", Ciudad, SqlDbType.NVarChar);
            if (!string.IsNullOrEmpty(Notas))
                b.AddParameter("@notas", Notas, SqlDbType.NVarChar);
            if (!string.IsNullOrEmpty(FisicaMoral))
                b.AddParameter("@fisicamoral", FisicaMoral, SqlDbType.Int);
            if (!string.IsNullOrEmpty(RFC))
                b.AddParameter("@rfc", RFC, SqlDbType.NVarChar, 18);
            if (!string.IsNullOrEmpty(InternoExterno))
                b.AddParameter("@internoexterno", InternoExterno, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int ModificarDetalle(string Telefono, string Celular, string Empresa, string Direccion, string Ciudad, string Notas, string IdUsuario, string FisicaMoral, string RFC, string InternoExterno)
        {
            string consulta = "UPDATE usuariosDetalle SET telefono=@telefono, celular=@celular, empresa=@empresa, direccion=@direccion, ciudad=@ciudad, notas=@notas, fisicamoral=@fisicamoral, rfc=@rfc, internoexterno=@internoexterno WHERE idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@telefono", Telefono, SqlDbType.NVarChar);
            b.AddParameter("@celular", Celular, SqlDbType.NVarChar);
            b.AddParameter("@empresa", int.Parse(Empresa), SqlDbType.Int);
            b.AddParameter("@direccion", Direccion, SqlDbType.NVarChar);
            b.AddParameter("@ciudad", Ciudad, SqlDbType.NVarChar);
            b.AddParameter("@notas", Notas, SqlDbType.NVarChar);
            b.AddParameter("@fisicamoral", FisicaMoral, SqlDbType.Int);
            b.AddParameter("@idusuario", int.Parse(IdUsuario), SqlDbType.Int);
            b.AddParameter("@rfc", RFC, SqlDbType.NVarChar, 18);
            b.AddParameter("@internoexterno", InternoExterno, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string eNombre, string eApellidoPaterno, string eApellidoMaterno, string eCorreo, string eClave, string eContraseña, string eActivo, string eRol, string eId, string eArchivosPermiso, string eClasSubClasPermiso, string eIdUDN)
        {
            int regreso = 0;
            //Actualizar el detalle del usuario
            b.ExecuteCommandQuery("UPDATE usuarios SET nombre=@nombre, apellidopaterno=@apellidopaterno, apellidomaterno=@apellidomaterno, correo=@correo, clave=@clave, contraseña=@contraseña, activo=@activo, archivospermiso=@archivospermiso, classubclaspermiso=@classubclaspermiso,idudn=@udn WHERE id=@id");
            b.AddParameter("@nombre", eNombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidopaterno", eApellidoPaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidomaterno", eApellidoMaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@correo", eCorreo, SqlDbType.NVarChar);
            b.AddParameter("@clave", eClave, SqlDbType.NVarChar);
            b.AddParameter("@contraseña", Utilerias.Cifrado.Encriptar(eContraseña), SqlDbType.NVarChar);
            b.AddParameter("@activo", eActivo == "1" ? true : false, SqlDbType.Bit);
            b.AddParameter("@archivospermiso", eArchivosPermiso == "1" ? true : false, SqlDbType.Bit);
            b.AddParameter("@classubclaspermiso", eClasSubClasPermiso == "1" ? true : false, SqlDbType.Bit);
            b.AddParameter("@udn", eIdUDN, SqlDbType.Int);
            b.AddParameter("@id",eId, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
            {
                //Actualizar el rol, por si se ha cambiado
                b.ExecuteCommandQuery("UPDATE usuariosroles SET idrol=@idrol WHERE idusuario=@idusuario");
                b.AddParameter("@idusuario", eId, SqlDbType.Int);
                b.AddParameter("@idrol", eRol, SqlDbType.Int);
                regreso = b.InsertUpdateDelete();
            }
            return regreso;
        }

        protected int ModificarGerente(string eNombre, string eClave, string eActivo, string eId)
        {
            //Actualizar el detalle del Gerente
            b.ExecuteCommandQuery("UPDATE usuarios SET nombre=@nombre, clave=@clave, activo=@activo WHERE id=@id");
            b.AddParameter("@nombre", eNombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@clave", eClave, SqlDbType.NVarChar);
            b.AddParameter("@activo", eActivo == "1" ? true : false, SqlDbType.Bit);
            b.AddParameter("@id", eId, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int ModificarResponsable(string Nombre, string Correo, string IdUsuario)
        {
            int regreso = 0;
            //Actualizar el detalle del usuario
            b.ExecuteCommandQuery("UPDATE usuarios SET nombre=@nombre, correo=@correo WHERE id=@id");
            b.AddParameter("@nombre", Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@correo", Correo, SqlDbType.NVarChar);
            b.AddParameter("@id", IdUsuario, SqlDbType.Int);
            return b.InsertUpdateDelete();            
        }

        protected int ModificarContraseña(string Anterior, string Nueva, string Id)
        {
            b.ExecuteCommandQuery("IF EXISTS(SELECT 1 FROM USUARIOS WHERE contraseña=@anterior AND Id=@id) " +
            "BEGIN UPDATE usuarios SET contraseña=@nueva WHERE id=@id END " +
            "ELSE BEGIN SELECT 0 END");
            b.AddParameter("@anterior", Utilerias.Cifrado.Encriptar(Anterior), SqlDbType.NVarChar);
            b.AddParameter("@nueva", Utilerias.Cifrado.Encriptar(Nueva), SqlDbType.NVarChar);
            b.AddParameter("@Id", Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected string ValidarCorreoUsuario(string correo)
        {
            string consulta = "SELECT 1 from usuarios WHERE correo=@correo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@correo", correo, SqlDbType.NVarChar);
            return b.SelectString();
        }

        protected int ReiniciarCorreo(string correo)
        {
            string consulta = "UPDATE usuarios SET contraseña=@contraseña WHERE correo=@correo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@correo", correo, SqlDbType.NVarChar);
            b.AddParameter("@contraseña", Utilerias.Cifrado.Encriptar(Funciones.ClaveSalida()), SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        protected int CambiarCorreoResponsable(string idusuario, string correo)
        {
            b.ExecuteCommandQuery("UPDATE usuarios SET clave=@correo, correo=@correo, contraseña=@contraseña WHERE id=@id AND activo=1");
            b.AddParameter("@correo", correo, SqlDbType.NVarChar);
            b.AddParameter("@id", idusuario, SqlDbType.Int);
            b.AddParameter("@contraseña", Cifrado.Encriptar(Funciones.ClaveSalida()), SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        protected int AgregarCorreoAHistorial(string idusuario, string correo)
        {
            b.ExecuteCommandQuery("INSERT INTO otroscorreos (correo, idusuario) VALUES(@correo, @idusuario)");
            b.AddParameter("@correo", correo, SqlDbType.NVarChar);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
                

    }
}