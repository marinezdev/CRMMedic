using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class ActividadesRepositorio 
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public List<Actividades> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT * FROM actividades");
            List<Actividades> resultado = new List<Actividades>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Actividades item = new Actividades()
                {
                    Id     = int.Parse(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public bool Agregar(Actividades items)
        {
            b.ExecuteCommandQuery("INSERT INTO actividades (nombre) VALUES(@nombre)");
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar);
            int i = b.InsertUpdateDelete();
            if (i >= 1)
                return true;
            else
                return false;
        }


    }
}