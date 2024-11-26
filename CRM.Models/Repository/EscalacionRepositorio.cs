using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class EscalacionRepositorio 
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();


        protected List<Escalacion> SeleccionarPorIdOportunidad(string idoportunidad)
        {
            b.ExecuteCommandQuery("SELECT * FROm escalacion WHERE idoportunidad=@id");
            b.AddParameter("@id", idoportunidad, SqlDbType.Int);
            List<Escalacion> resultado = new List<Escalacion>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Escalacion item = new Escalacion();
                item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.IdUsuarioContacto = int.Parse(reader["IdUsuarioContacto"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<Modelos> SeleccionarUsuariosContactosPorIdOportunidad(string idoportunidad)
        {
            string consulta = "SELECT esc.Fecha, esc.IdUsuarioContacto, usu.Nombre + ' ' + ISNULL(usu.ApellidoPaterno, '') + ' ' + ISNULL(usu.ApellidoMaterno,'') AS Contacto " +
            "FROM escalacion esc " +
            "LEFT JOIN Usuarios usu ON usu.id = esc.IdUsuarioContacto " +
            "WHERE esc.idoportunidad=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", idoportunidad, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Escalacion.Fecha             = DateTime.Parse(reader["Fecha"].ToString());
                item.Escalacion.IdUsuarioContacto = int.Parse(reader["IdUsuarioContacto"].ToString());
                item.Usuarios.Nombre              = reader["Contacto"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string idoportunidad, string fecha, string idusuariocontacto)
        {
            b.ExecuteCommandQuery("INSERT INTO escalacion VALUES(@idoportunidad, @fecha, @idusuariocontacto)");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@fecha", fecha, SqlDbType.Date);
            b.AddParameter("@idusuariocontacto", idusuariocontacto, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Eliminar(string idoportunidad, string idusuario)
        {
            string consulta = "DELETE FROM escalacion WHERE idoportunidad=@idoportunidad AND idusuariocontacto=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}