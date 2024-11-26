using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class ArchivosRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public List<Archivos> SeleccionarPorIdOportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre, IdOportunidad FROM archivos WHERE idoportunidad=@idopo");
            b.AddParameter("@idopo", id, SqlDbType.Int);
            List<Archivos> resultado = new List<Archivos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Archivos item = new Archivos()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    IdOportunidad = int.Parse(reader["IdOportunidad"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public bool Agregar(Archivos items)
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
    }
}