using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class EscalacionRepositorio 
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();


        public List<Escalacion> SeleccionarPorIdOportunidad(string idoportunidad)
        {
            b.ExecuteCommandQuery("SELECT * FROm escalacion WHERE idoportunidad=@id");
            b.AddParameter("@id", idoportunidad, SqlDbType.Int);
            List<Escalacion> resultado = new List<Escalacion>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Escalacion item = new Escalacion();
                item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Agregar(string idoportunidad, string fecha)
        {
            b.ExecuteCommandQuery("INSERT INTO escalacion VALUES(@idoportunidad, @fecha)");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@fecha", fecha, SqlDbType.Date);
            return b.InsertUpdateDelete();
        }

    }
}