using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models.Models;
using System.Data;
using CRM.Utilerias;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class EstadoOportunidadRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected EstadoOportunidad SeleccionarPorId(string id)
        {
            string consulta = "SELECT id, idoportunidad, estado, comentarios, fecha FROM estadooportunidad WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            EstadoOportunidad resultado = new EstadoOportunidad();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.IdOportunidad = int.Parse(reader["idoportunidad"].ToString());
                resultado.Estado = int.Parse(reader["estado"].ToString());
                resultado.Comentarios = reader["comentarios"].ToString();
                resultado.Fecha = DateTime.Parse(reader["fecha"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<EstadoOportunidad> SeleccionarPorIdOportunidad(string idoportunidad)
        {
            b.ExecuteCommandQuery("SELECT id, idoportunidad, estado, comentarios, fecha FROM estadooportunidad WHERE idoportunidad=@id");
            b.AddParameter("@id", idoportunidad, SqlDbType.Int);
            List<EstadoOportunidad> resultado = new List<EstadoOportunidad>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                EstadoOportunidad item = new EstadoOportunidad();
                item.Id             = int.Parse(reader["id"].ToString());
                item.IdOportunidad  = int.Parse(reader["idoportunidad"].ToString());
                item.Estado         = int.Parse(reader["estado"].ToString());
                item.EstadoNombre   = Funciones.EstadoNombre(reader["estado"].ToString());
                item.Comentarios    = reader["comentarios"].ToString();
                item.Fecha          = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900-01-01") : DateTime.Parse(reader["Fecha"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string idoportunidad, string estado, string comentarios)
        {
            b.ExecuteCommandQuery("INSERT INTO estadooportunidad (idoportunidad, estado, comentarios) VALUES(@idoportunidad, @estado, @comentarios)");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            b.AddParameter("@comentarios", comentarios, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string idestado, string comentarios, string fecha, string id)
        {
            b.ExecuteCommandQuery("UPDATE estadooportunidad SET estado=@estado, comentarios=@comentarios, fecha=@fecha WHERE id=@id");
            b.AddParameter("@estado", idestado, SqlDbType.Int);
            b.AddParameter("@comentarios", comentarios, SqlDbType.NVarChar);
            b.AddParameter("@fecha", fecha, SqlDbType.DateTime);
            b.AddParameter("@id", id, SqlDbType.Int);            
            return b.InsertUpdateDelete();
        }




    }
}
