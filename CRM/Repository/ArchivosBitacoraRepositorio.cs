using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class ArchivosBitacoraRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public List<ArchivosBitacora> SeleccionarPorIdBitacora(string id)
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre, IdBitacora FROM archivosbitacora WHERE idbitacora=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            List<ArchivosBitacora> resultado = new List<ArchivosBitacora>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ArchivosBitacora item = new ArchivosBitacora()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    IdBitacora = int.Parse(reader["IdBitacora"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Agregar(ArchivosBitacora items)
        {
            b.ExecuteCommandQuery("INSERT INTO archivosbitacora (nombre,idbitacora) " +
            "VALUES(@nombre,@idbitacora)");
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@idbitacora", items.IdBitacora, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

    }
}