using CRM.Models.Models;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class ClassSubClassRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected ClassSubClass SeleccionarPorIdOportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT * FROM classsubclass WHERE idoportunidad=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            ClassSubClass resultado = new ClassSubClass();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.IdOportunidad = int.Parse(reader["IdOportunidad"].ToString());
                resultado.Campo1 = reader["Campo1"].ToString();
                resultado.Campo2 = reader["Campo2"].ToString();
                resultado.Campo3 = reader["Campo3"].ToString();
                resultado.Campo4 = reader["Campo4"].ToString();
                resultado.IdClasificacion = int.Parse(reader["IdClasificacion"].ToString());
                resultado.IdSubClasificacion = int.Parse(reader["IdSubClasificacion"].ToString());
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int AgregarModificar(ClassSubClass items)
        {
            string consulta = "" +
            "IF EXISTS(SELECT idoportunidad FROM classsubclass WHERE idoportunidad=@idoportunidad) " +
            "   BEGIN " +
            "       UPDATE classsubclass SET campo1=@campo1, campo2=@campo2, campo3=@campo3, campo4=@campo4 WHERE idoportunidad=@idoportunidad; " +
            "   END " +
            "ELSE " +
            "   INSERT INTO classsubclass VALUES(@idoportunidad, @campo1, @campo2, @campo3, @campo4, @idclass, @idsubclass)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", items.IdOportunidad, SqlDbType.Int);
            b.AddParameter("@campo1", items.Campo1, SqlDbType.NVarChar);
            b.AddParameter("@campo2", items.Campo2, SqlDbType.NVarChar);
            b.AddParameter("@campo3", items.Campo3, SqlDbType.NVarChar);
            b.AddParameter("@campo4", items.Campo4, SqlDbType.NVarChar);
            b.AddParameter("@idclass", items.IdClasificacion, SqlDbType.Int);
            b.AddParameter("@idsubclass", items.IdSubClasificacion, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }


    }
}