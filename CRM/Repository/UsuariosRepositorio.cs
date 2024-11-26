using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class UsuariosRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public List<UsuariosRoles> Seleccionar()
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
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<UsuariosRoles> SeleccionarTodos()
        {
            b.ExecuteCommandQuery("SELECT usu.id, usu.Nombre, usu.ApellidoPaterno, ISNULL(usu.ApellidoMaterno, '') AS ApellidoMaterno, usu.Clave, usu.Activo, conf.Nombre AS Empresa " +
            "FROM usuarios usu " +
            "INNER JOIN UsuarioConfiguracion uc ON uc.IdUsuario=usu.id " +
            "INNER JOIN Configuracion conf ON conf.Id=uc.IdConfiguracion " +
            "ORDER BY usu.nombre");
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
                item.Usuarios.Activo            = bool.Parse(reader["Activo"].ToString());
                item.Configuracion.Nombre       = reader["Empresa"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<UsuariosRoles> SeleccionarPorRol(string idconfiguracion, string idrol, string idusuario)
        {
            string consulta = "SELECT usu.id, usu.Nombre, usu.ApellidoPaterno , usu.ApellidoMaterno " +
            "FROM usuarios usu " +
            "INNER JOIN UsuarioConfiguracion uc ON uc.IdUsuario = usu.id " +
            "INNER JOIN Configuracion conf ON conf.Id = uc.IdConfiguracion " +
            "INNER JOIN UsuariosRoles ur ON ur.IdUsuario = usu.Id " +
            "WHERE conf.id=@idconfiguracion ";
            if (!string.IsNullOrEmpty(idrol) && idrol=="2")
                consulta += "AND ur.IdRol IN (2, 3) ";
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
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<UsuariosRoles> SeleccionarPorIdOportunidad(string idoportunidad)
        {
            string consulta = "SELECT opor.idoportunidad, " +
            "usu.id AS IdUsuario, usu.Nombre, usu.apellidopaterno, ISNULL(usu.ApellidoMaterno, '') AS ApellidoMaterno, " +
            "bic.id AS idbitacora " +
            //"--SUBSTRING(bic.notas, 0, 50) AS Notas " +
            "FROM oportunidadesusuarios opor " +
            "INNER JOIN usuarios usu ON usu.id = opor.idasignado " +
            "INNER JOIN BitacoraUsuarios bic ON(bic.IdResponsable = usu.id AND opor.IdOportunidad = bic.IdOportunidad) " +
            "WHERE opor.idoportunidad = @id";
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
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Validar(string clave, string contraseña)
        {
            b.ExecuteCommandQuery("SELECT 1 FROM usuarios WHERE clave=@clave AND contraseña=@contraseña AND activo=1");
            b.AddParameter("@clave", clave, SqlDbType.NVarChar);
            b.AddParameter("@contraseña", Utilerias.Cifrado_.Encriptar(contraseña), SqlDbType.NVarChar);
            return int.Parse(b.SelectString());
        }

        public List<UsuariosRoles> Buscar(string nombre)
        {
            b.ExecuteCommandQuery("SELECT usu.id,usu.nombre, us.apellidopaterno, ISNULL(us.apellidomaterno, '') AS ApellidoMaterno, usu.clave, usu.contraseña, " +
            "rol.id as IdRol, rol.nombre AS NombreRol " +
            "FROM usuarios usu " +
            "INNER JOIN usuariosroles usurol ON usu.id = usurol.idusuario " +
            "INNER JOIN roles rol ON rol.id = usurol.idrol WHERE usu.Nombre LIKE @nombre");
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
                item.Usuarios.Contraseña        = Utilerias.Cifrado_.Encriptar(reader["Contraseña"].ToString());
                item.Usuarios.Activo            = bool.Parse(reader["Activo"].ToString());
                item.Roles.Id                   = int.Parse(reader["IdRol"].ToString());
                item.Roles.Nombre               = reader["NombreRol"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public UsuariosRoles SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT usu.id,usu.nombre, usu.apellidopaterno, ISNULL(usu.apellidomaterno, '') AS ApellidoMaterno, usu.clave, usu.contraseña, usu.Activo, " +
            "rol.id as IdRol, rol.nombre AS NombreRol, " +
            "conf.id AS IdEmpresa " +
            "FROM usuarios usu " +
            "INNER JOIN usuariosroles usurol ON usu.id = usurol.idusuario " +
            "INNER JOIN roles rol ON rol.id = usurol.idrol " +
            "INNER JOIN UsuarioConfiguracion uc ON uc.IdUsuario=usu.id " +
            "INNER JOIN Configuracion conf ON conf.Id=uc.IdConfiguracion " +
            "WHERE usu.id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            UsuariosRoles resultado = new UsuariosRoles();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Usuarios.Id               = int.Parse(reader["Id"].ToString());
                resultado.Usuarios.Nombre           = reader["Nombre"].ToString();
                resultado.Usuarios.ApellidoPaterno  = reader["ApellidoPaterno"].ToString();
                resultado.Usuarios.ApellidoMaterno  = reader["ApellidoMaterno"].ToString();
                resultado.Usuarios.Clave            = reader["Clave"].ToString();
                resultado.Usuarios.Contraseña       = Utilerias.Cifrado_.Desencriptar(reader["Contraseña"].ToString());
                resultado.Usuarios.Activo           = bool.Parse(reader["Activo"].ToString());
                resultado.Roles.Id                  = int.Parse(reader["IdRol"].ToString());
                resultado.Roles.Nombre              = reader["NombreRol"].ToString();
                resultado.Configuracion.Id          = int.Parse(reader["IdEmpresa"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public UsuariosRoles SeleccionarPorClaveContraseña(string clave, string contraseña)
        {
            b.ExecuteCommandQuery("SELECT usu.id,usu.nombre, usu.apellidopaterno, ISNULL(usu.apellidomaterno, '') AS ApellidoMaterno, usu.clave, usu.contraseña, usu.Activo, " +
            "rol.id AS IdRol, rol.nombre AS NombreRol, " +
            "conf.id AS IdConfiguracion, conf.logo, conf.Nombre AS Empresa, conf.TituloIntermedioPestaña, conf.ClaseLogo, conf.ClaseNavegacion " +
            "FROM usuarios usu " +
            "INNER JOIN usuariosroles usurol ON usu.id = usurol.idusuario " +
            "INNER JOIN roles rol ON rol.id = usurol.idrol " +
            "INNER JOIN UsuarioConfiguracion uc ON uc.IdUsuario=usu.id " +
            "INNER JOIN Configuracion conf ON conf.Id=uc.IdConfiguracion " +
            "WHERE usu.clave=@clave " +
            "AND usu.contraseña=@contraseña");
            b.AddParameter("@clave", clave, SqlDbType.NVarChar);
            b.AddParameter("@contraseña", Utilerias.Cifrado_.Encriptar(contraseña), SqlDbType.NVarChar);
            UsuariosRoles resultado = new UsuariosRoles();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Usuarios.Id                           = int.Parse(reader["Id"].ToString());
                resultado.Usuarios.Nombre                       = reader["Nombre"].ToString();
                resultado.Usuarios.ApellidoPaterno              = reader["ApellidoPaterno"].ToString();
                resultado.Usuarios.ApellidoMaterno              = reader["ApellidoMaterno"].ToString();
                resultado.Usuarios.Clave                        = reader["Clave"].ToString();
                resultado.Usuarios.Contraseña                   = Utilerias.Cifrado_.Encriptar(reader["Contraseña"].ToString());
                resultado.Usuarios.Activo                       = bool.Parse(reader["Activo"].ToString());
                resultado.Roles.Id                              = int.Parse(reader["IdRol"].ToString());
                resultado.Roles.Nombre                          = reader["NombreRol"].ToString();
                resultado.Configuracion.Id                      = int.Parse(reader["IdConfiguracion"].ToString());
                resultado.Configuracion.Logo                    = reader["Logo"].ToString();
                resultado.Configuracion.Nombre                  = reader["Empresa"].ToString();
                resultado.Configuracion.TituloIntermedioPestaña = reader["TituloIntermedioPestaña"].ToString();
                resultado.Configuracion.ClaseLogo               = reader["ClaseLogo"].ToString();
                resultado.Configuracion.ClaseNavegacion         = reader["ClaseNavegacion"].ToString();
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Agregar(string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Clave, string Contraseña, string Activo)
        {
            b.ExecuteCommandQuery("IF NOT EXISTS(SELECT nombre,apellidopaterno,apellidomaterno FROM usuarios WHERE nombre LIKE '%' + @nombre + '%' AND apellidopaterno LIKE '%' + @apaterno + '%' AND apellidomaterno LIKE '%' + @amaterno + '%') " +
            "BEGIN " +
            "INSERT INTO usuarios (nombre, apellidopaterno, apellidomaterno, clave, contraseña, activo) " +
            "VALUES(@nombre, @apaterno, @amaterno, @clave, @contraseña, @activo); " +
            "SELECT @@IDENTITY " +
            "END " +
            "ELSE " +
            "SELECT 0");
            b.AddParameter("@nombre", Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apaterno", ApellidoPaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@amaterno", ApellidoMaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@clave", Clave, SqlDbType.NVarChar);
            b.AddParameter("@contraseña", Utilerias.Cifrado_.Encriptar(Contraseña), SqlDbType.NVarChar);
            b.AddParameter("@activo", Activo == "on" ? true : false, SqlDbType.Bit);
            return b.SelectWithReturnValue();
        }

        public int Modificar(string eNombre, string eApellidoPaterno, string eApellidoMaterno, string eClave, string eContraseña, string eActivo, string eRol, string eId)
        {
            int regreso = 0;
            //Actualizar el detalle del usuario
            b.ExecuteCommandQuery("UPDATE usuarios SET nombre=@nombre, apellidopaterno=@apellidopaterno, apellidomaterno=@apellidomaterno, clave=@clave, contraseña=@contraseña, activo=@activo WHERE id=@id");
            b.AddParameter("@nombre", eNombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidopaterno", eApellidoPaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidomaterno", eApellidoMaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@clave", eClave, SqlDbType.NVarChar);
            b.AddParameter("@contraseña", Utilerias.Cifrado_.Encriptar(eContraseña), SqlDbType.NVarChar);
            b.AddParameter("@activo", eActivo == "1" ? true : false, SqlDbType.Bit);
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

        public int ModificarContraseña(string Anterior, string Nueva, string Id)
        {
            b.ExecuteCommandQuery("IF EXISTS(SELECT 1 FROM USUARIOS WHERE contraseña=@anterior AND Id=@id) " +
            "BEGIN UPDATE usuarios SET contraseña=@nueva WHERE id=@id END " +
            "ELSE BEGIN SELECT 0 END");
            b.AddParameter("@anterior", Anterior, SqlDbType.NVarChar);
            b.AddParameter("@nueva", Utilerias.Cifrado_.Encriptar(Nueva), SqlDbType.NVarChar);
            b.AddParameter("@Id", Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }




    }
}