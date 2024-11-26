using CRM.Models.Models;
using System.Collections.Generic;
using System.Data;
using System;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class BitacoraUsuariosRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected BitacoraUsuarios SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, notas, fecha, idresponsable, idoportunidad, idtipoactividad FROM bitacorausuarios WHERE id=@id");
            b.AddParameter("@id", id, System.Data.SqlDbType.Int);
            BitacoraUsuarios resultado = new BitacoraUsuarios();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = reader["Id"].ToString() == "" ? 0 : int.Parse(reader["Id"].ToString());
                resultado.IdResponsable = reader["idresponsable"].ToString() == "" ? 0 : int.Parse(reader["idresponsable"].ToString());
                resultado.Fecha = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["Fecha"].ToString());
                resultado.Notas = reader["Notas"].ToString();
                resultado.IdTipoActividad = reader["IdTipoActividad"].ToString() == "" ? 0 : int.Parse(reader["IdTipoActividad"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<BitacoraUsuarios> SeleccionarIdOportunidad(string idoportunidad)
        {
            string consulta = "SELECT bu.id, bu.idtipoactividad, ta.Nombre AS TipoActividad, bu.notas, bu.fecha, bu.idresponsable, usu.Nombre + ' ' + ISNULL(usu.apellidopaterno,'') + ' ' + ISNULL(usu.apellidomaterno,'') AS Responsable " + 
            "FROM bitacora bu " +
            "INNER JOIN usuarios usu ON usu.id=bu.idresponsable " +
            "INNER JOIN tipoactividad ta ON ta.id=bu.idtipoactividad " +
            "WHERE bu.idoportunidad = @idoportunidad";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<BitacoraUsuarios> resultado = new List<BitacoraUsuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                BitacoraUsuarios item = new BitacoraUsuarios();
                item.Id             = reader["Id"].ToString() == "" ? 0 : int.Parse(reader["Id"].ToString());
                item.IdResponsable  = reader["idresponsable"].ToString() == "" ? 0 : int.Parse(reader["idresponsable"].ToString());
                item.Fecha          = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["Fecha"].ToString());
                item.Notas          = reader["Notas"].ToString();
                item.Responsable    = reader["Responsable"].ToString();
                item.IdTipoActividad = reader["IdTipoActividad"].ToString() == "" ? 0 : int.Parse(reader["IdTipoActividad"].ToString());
                item.TipoActividad = reader["TipoActividad"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<BitacoraUsuarios> SeleccionarIdOportunidad(string idusuario, string idoportunidad)
        {
            string consulta = "SELECT bu.id, bu.idtipoactividad, ta.Nombre AS TipoActividad, bu.notas, bu.fecha, bu.idresponsable, usu.Nombre + ' ' + ISNULL(usu.apellidopaterno,'') + ' ' + ISNULL(usu.apellidomaterno,'') AS Responsable " +
            "FROM bitacorausuarios bu " +
            "INNER JOIN usuarios usu ON usu.id=bu.idresponsable " +
            "INNER JOIN tipoactividad ta ON ta.id=bu.idtipoactividad " +
            "WHERE bu.idoportunidad = @idoportunidad AND bu.idresponsable=@idusuario";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            List<BitacoraUsuarios> resultado = new List<BitacoraUsuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                BitacoraUsuarios item = new BitacoraUsuarios();
                item.Id = reader["Id"].ToString() == "" ? 0 : int.Parse(reader["Id"].ToString());
                item.IdResponsable = reader["idresponsable"].ToString() == "" ? 0 : int.Parse(reader["idresponsable"].ToString());
                item.Fecha = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["Fecha"].ToString());
                item.Notas = reader["Notas"].ToString();
                item.Responsable = reader["Responsable"].ToString();
                item.IdTipoActividad = reader["IdTipoActividad"].ToString() == "" ? 0 : int.Parse(reader["IdTipoActividad"].ToString());
                item.TipoActividad = reader["TipoActividad"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<BitacoraUsuarios> SeleccionarBitacoraResponsablesPorIdOportunidad(string idoportunidad)
        {
            string consulta = "SELECT bic.idresponsable " +
            "FROM bitacora bic " +
            "INNER JOIN usuarios usu ON usu.id=bic.idresponsable " +
            "WHERE bic.idoportunidad=@idoportunidad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<BitacoraUsuarios> resultado = new List<BitacoraUsuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                BitacoraUsuarios item = new BitacoraUsuarios();
                item.IdResponsable = reader["idresponsable"].ToString() == "" ? 0 : int.Parse(reader["idresponsable"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int SeleccionarValidarSiUsuarioTieneBitacora(string idoportunidad, string idusuario)
        {
            b.ExecuteCommandQuery("SELECT Id FROM bitacorausuarios WHERE idoportunidad=@idoportunidad AND idresponsable=@idusuario");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            if (!string.IsNullOrEmpty(b.SelectString()))
                return int.Parse(b.SelectString());
            else
                return 0;
        }

        /// <summary>
        /// Verifica si hay usuarios asignados a una oportunidad (modificado quitando la tabla bitacorausuarios a sólo bitacora)
        /// </summary>
        /// <param name="idoportunidad"></param>
        /// <returns></returns>
        protected int SeleccionarSiOportunidadTieneResponsablesAsignados(string idoportunidad)
        {
            string consulta = "SELECT IIF(count(idoportunidad) > 0, '1','0') FROM bitacora WHERE idoportunidad=@idoportunidad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected int Agregar(BitacoraUsuarios items)
        {
            b.ExecuteCommandQuery("INSERT INTO bitacorausuarios (notas, fecha, idresponsable, idoportunidad, idtipoactividad) " +
            "VALUES(@notas, @fecha, @idresponsable, @idoportunidad, @idtipoactividad)");
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@fecha", items.Fecha, SqlDbType.DateTime);
            b.AddParameter("@idresponsable", items.IdResponsable, SqlDbType.Int);
            b.AddParameter("@idoportunidad", items.IdOportunidad, SqlDbType.Int);
            b.AddParameter("@idtipoactividad", items.IdTipoActividad, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }

        protected int Modificar(string Id, string TipoActividad, string Fecha, string Notas)
        {
            b.ExecuteCommandQuery("UPDATE bitacorausuarios SET idtipoactividad=@idtipoactividad, fecha=@fecha, notas=@notas WHERE id = @id");
            b.AddParameter("@notas", Notas, SqlDbType.NVarChar);
            b.AddParameter("@fecha", Fecha, SqlDbType.DateTime);
            b.AddParameter("@idtipoactividad", TipoActividad, SqlDbType.Int);
            b.AddParameter("@id", Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        
    }
}