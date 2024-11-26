using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class ArchivosRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected Archivos SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre, Fecha, IdOportunidad, Notas, version FROM archivos WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            Archivos resultado = new Archivos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.Notas = reader["Notas"].ToString();
                resultado.Version = reader["version"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected string SeleccionarPorNombre(string id)
        {
            b.ExecuteCommandQuery("SELECT nombre FROM archivos WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.SelectString();
        }

        protected int CuantosArchivosTieneOportunidad(string idoportunidad)
        {
            string consulta = "SELECT count(id) FROM archivos WHERE idoportunidad=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", idoportunidad, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected List<Archivos> SeleccionarPorIdOportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre, Fecha, IdOportunidad, Notas, version FROM archivos WHERE idoportunidad=@idopo");
            b.AddParameter("@idopo", id, SqlDbType.Int);
            List<Archivos> resultado = new List<Archivos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Archivos item = new Archivos()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Fecha = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900-01-01") : DateTime.Parse(reader["Fecha"].ToString()),
                    IdOportunidad = int.Parse(reader["IdOportunidad"].ToString()),
                    Notas = reader["Notas"].ToString(),
                    Version = reader["version"].ToString()
                };
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected bool Agregar(Archivos items)
        {
            b.ExecuteCommandQuery("INSERT INTO archivos (nombre, idoportunidad) VALUES(@nombre, @idoportunidad)");
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@idoportunidad", items.IdOportunidad, SqlDbType.Int);
            int i = b.InsertUpdateDelete();
            if (i >= 1)
                return true;
            else
                return false;
        }

        protected int Modificar(string Notas, string Version, string Id)
        {
            b.ExecuteCommandQuery("UPDATE archivos set notas=@notas, version=@version WHERE id=@id");
            b.AddParameter("@notas", Notas, SqlDbType.NVarChar);
            b.AddParameter("@version", Version, SqlDbType.NVarChar);
            b.AddParameter("@id", Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Eliminar(string id)
        {
            b.ExecuteCommandQuery("DELETE FROM archivos WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}