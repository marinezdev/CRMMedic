using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class OportunidadesActividadesRepositorio 
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected OportunidadesActividades SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT Id, TipoActividad, Fecha, Notas, IdOportunidad FROM oportunidadesactividades WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            OportunidadesActividades resultado = new OportunidadesActividades();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.TipoActividad = reader["TipoActividad"].ToString() == "" ? 0 : int.Parse(reader["TipoActividad"].ToString());
                resultado.Fecha = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900-01-01") : DateTime.Parse(reader["Fecha"].ToString());
                resultado.Notas = reader["Notas"].ToString();
                resultado.IdOportunidad = int.Parse(reader["IdOportunidad"].ToString());
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<OportunidadesActividades> SeleccionarPorIdOportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT oa.Id, oa.tipoactividad, ta.Nombre AS TipoActividadNombre, oa.Fecha, oa.Notas, oa.Agregado, oa.IdOportunidad, usu.nombre + ' ' + ISNULL(usu.ApellidoPaterno,'') + ' ' + ISNULL(usu.apellidomaterno,'') AS Responsable, oa.idusuarioasignado " +
            "FROM oportunidadesactividades oa " +
            "INNER JOIN Usuarios usu ON usu.id = oa.IdUsuario " +
            "INNER JOIN TipoActividad ta ON ta.Id=oa.TipoActividad " +
            "WHERE oa.idoportunidad = @id"); 
            b.AddParameter("@id", id, SqlDbType.Int);
            List<OportunidadesActividades> resultado = new List<OportunidadesActividades>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                OportunidadesActividades item = new OportunidadesActividades();

                item.Id                 = int.Parse(reader["Id"].ToString());
                item.TipoActividad      = reader["TipoActividad"].ToString() == "" ? 0 : int.Parse(reader["TipoActividad"].ToString());
                item.ActividadNombre    = reader["tipoactividadnombre"].ToString();
                item.Fecha              = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["Fecha"].ToString());
                item.Notas              = reader["Notas"].ToString();
                item.IdOportunidad      = int.Parse(reader["IdOportunidad"].ToString());
                item.NombreUsuario      = reader["Responsable"].ToString();
                item.IdUsuarioAsignado  = reader["idusuarioasignado"].ToString() == "" ? 0 : int.Parse(reader["idusuarioasignado"].ToString());

                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<OportunidadesActividades> SeleccionarPorIdUsuario(string idusuario, string idoportunidad)
        {
            b.ExecuteCommandQuery("SELECT oa.Id, oa.tipoactividad, oa.Fecha, oa.Notas, oa.Agregado, oa.IdOportunidad, usu.nombre + ' ' + ISNULL(usu.ApellidoPaterno,'') + ' ' + ISNULL(usu.apellidomaterno,'') AS Responsable " +
            "FROM oportunidadesactividades oa " +
            "INNER JOIN Usuarios usu ON usu.id = oa.IdUsuario " +
            "WHERE oa.idusuario = @idusuario " +
            "AND oa.idoportunidad=@idoportunidad");
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<OportunidadesActividades> resultado = new List<OportunidadesActividades>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                OportunidadesActividades item = new OportunidadesActividades();

                item.Id = int.Parse(reader["Id"].ToString());
                item.TipoActividad = int.Parse(reader["TipoActividad"].ToString());
                item.ActividadNombre = Utilerias.Funciones.TipoActividadNombre(reader["tipoactividad"].ToString());
                item.Fecha = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["Fecha"].ToString());
                item.Notas = reader["Notas"].ToString();
                item.IdOportunidad = int.Parse(reader["IdOportunidad"].ToString());
                item.NombreUsuario = reader["Responsable"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int SeleccionarCuantasActividadesTieneOportunidad(string idoportunidad)
        {
            b.ExecuteCommandQuery("SELECT COUNT(Id) FROM oportunidadesactividades WHERE idoportunidad=@id");
            b.AddParameter("@id", idoportunidad, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected int SeleccionarSiUsuarioEstaAsignadoComoResponsableAActividad(string idoportunidad, string idusuario)
        {
            string consulta = "SELECT COUNT(1) FROM oportunidadesactividades WHERE idoportunidad=@idoportunidad AND idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected int Agregar(OportunidadesActividades items)
        {
            string consulta = "INSERT INTO oportunidadesactividades (";
            if (items.TipoActividad > 0)
                consulta += "tipoactividad, ";
            if (items.Fecha > DateTime.Parse("1900-01-01"))
                consulta += "fecha, ";
            if (items.IdUsuarioAsignado > 0)
                consulta += "idusuarioasignado, ";
            consulta += "notas, idoportunidad, idusuario) " +
            "VALUES(";
            if (items.TipoActividad > 0)
                consulta += "@tipoactividad, ";
            if (items.Fecha > DateTime.Parse("1900-01-01"))
                consulta += "@fecha, ";
            if (items.IdUsuarioAsignado > 0)
                consulta += "@idusuarioasignado, ";
            consulta += "@notas, @idoportunidad, @idusuario)";
            b.ExecuteCommandQuery(consulta);
            if (items.TipoActividad > 0)
                b.AddParameter("@tipoactividad", items.TipoActividad, SqlDbType.Int);
            if (items.Fecha > DateTime.Parse("1900-01-01"))
                b.AddParameter("@fecha", items.Fecha, SqlDbType.Date);
            if (items.IdUsuarioAsignado > 0)
                b.AddParameter("@idusuarioasignado", items.IdUsuarioAsignado, SqlDbType.Int);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@idoportunidad", items.IdOportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", items.IdUsuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(OportunidadesActividades items)
        {
            string consulta = "UPDATE oportunidadesactividades SET ";
            if (items.TipoActividad > 0)
                consulta += "tipoactividad=@tipoactividad, ";
            if (items.Fecha > DateTime.Parse("1900-01-01"))
                consulta += "fecha=@fecha,  ";
            consulta += "notas=@notas " +
            "WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            if (items.TipoActividad > 0)
                b.AddParameter("@tipoactividad", items.TipoActividad, SqlDbType.Int);
            if (items.Fecha > DateTime.Parse("1900-01-01"))
                b.AddParameter("@fecha", items.Fecha, SqlDbType.Date);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Eliminar(string idactividad)
        {
            string consulta = "DELETE FROM oportunidadesactividades WHERE id=@idactividad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idactividad", idactividad, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}