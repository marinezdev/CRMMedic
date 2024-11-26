using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class ArchivosBitacoraRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected ArchivosBitacora SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre, Fecha, Notas, version, idbitacora FROM archivosbitacora WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            ArchivosBitacora resultado = new ArchivosBitacora();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.Fecha = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900-01-01") : DateTime.Parse(reader["Fecha"].ToString());
                resultado.Notas = reader["Notas"].ToString();
                resultado.Version = reader["version"].ToString();
                resultado.IdBitacora = int.Parse(reader["IdBitacora"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<ArchivosBitacora> SeleccionarPorIdBitacora(string id)
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre, Fecha, Notas, Version, IdBitacora FROM archivosbitacora WHERE idbitacora=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            List<ArchivosBitacora> resultado = new List<ArchivosBitacora>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ArchivosBitacora item = new ArchivosBitacora()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Fecha = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900-01-01") : DateTime.Parse(reader["Fecha"].ToString()),
                    IdBitacora = int.Parse(reader["IdBitacora"].ToString()),
                    Notas = reader["Notas"].ToString(),
                    Version = reader["Version"].ToString()
                };
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected string SeleccionarPorNombre(string id)
        {
            b.ExecuteCommandQuery("SELECT nombre FROM archivosbitacora WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.SelectString();
        }

        protected int Agregar(ArchivosBitacora items)
        {
            b.ExecuteCommandQuery("INSERT INTO archivosbitacora (nombre,idbitacora) " +
            "VALUES(@nombre,@idbitacora)");
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@idbitacora", items.IdBitacora, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string Notas, string Version, string Id)
        {
            b.ExecuteCommandQuery("UPDATE archivosbitacora set notas=@notas, version=@version WHERE id=@id");
            b.AddParameter("@notas", Notas, SqlDbType.NVarChar);
            b.AddParameter("@version", Version, SqlDbType.NVarChar);
            b.AddParameter("@id", Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Eliminar(string id)
        {
            b.ExecuteCommandQuery("DELETE FROM archivosbitacora WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

    }
}