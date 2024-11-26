using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Models.Models;

namespace CRM.Models.Repository
{
    public class DocumentosASAESubClasificacionesRepository
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Subclasificacion> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT id, nombre, idclasificacion FROM documentosasaesubclasificacion");
            List<Subclasificacion> resultado = new List<Subclasificacion>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Subclasificacion item = new Subclasificacion();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                item.IdClasificacion = int.Parse(reader["idclasificacion"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected Subclasificacion SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, nombre, idclasificacion FROM documentosasaesubclasificacion WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            Subclasificacion resultado = new Subclasificacion();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.IdClasificacion = int.Parse(reader["IdClasificacion"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<Subclasificacion> SeleccionarSubClasificacionesPorClasificacion(string idclasificacion)
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre FROM documentosasaesubclasificacion WHERE idclasificacion=@idclasificacion");
            b.AddParameter("@idclasificacion", idclasificacion, SqlDbType.Int);
            List<Subclasificacion> resultado = new List<Subclasificacion>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Subclasificacion item = new Subclasificacion();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(Subclasificacion items)
        {
            string consulta = "INSERT INTO documentosasaesubclasificacion (nombre,idclasificacion) VALUES(@nombre,@idclasificacion)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@idclasificacion", items.IdClasificacion, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(Subclasificacion items)
        {
            string consulta = "UPDATE documentosasaesubclasificacion SET nombre=@nombre, idclasificacion=@idclasificacion WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@idclasificacion", items.IdClasificacion, SqlDbType.Int);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Eliminar(string id)
        {
            string consulta = "IF NOT EXISTS(SELECT subclasificacion FROM documentosasae WHERE subclasificacion=@id) " +
            "DELETE FROM documentosasaesubclasificacion WHERE id = @id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int EliminarClasificacion(string id)
        {
            string consulta = "IF NOT EXISTS(SELECT clasificacion FROM documentosasae WHERE clasificacion=@id) OR NOT EXISTS(SELECT idclasificacion FROM documentosasaesubclasificacion WHERE idclasificacion=@id) " +
            "BEGIN " +
            "   DELETE FROM documentosasaeclasificacion WHERE id = @id; " +
            "   DELETE FROM documentosasaesubclasificacion WHERE idclasificacion=@id; " +
            "END " +
            "ELSE " +
            "   BEGIN " +
            "   SELECT 0 " +
            "END ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }




    }
}
