using CRM.Models.Models;
using System.Data;
using System.Collections.Generic;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class TipoPersonaRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<TipoPersona> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT id, nombre FROM tipopersona");
            List<TipoPersona> resultado = new List<TipoPersona>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                TipoPersona item = new TipoPersona();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string nombre)
        { 
            string consulta = "INSERT INTO tipopersona (nombre) VALUES(@nombre)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", nombre, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }
    }
}
