using CRM.Models.Models;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class RolesRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Roles> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT id,nombre,observaciones,activo,paginainicio FROM roles");
            List<Roles> resultado = new List<Roles>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Roles item = new Roles();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                item.Observaciones = reader["Observaciones"].ToString();
                item.Activo = bool.Parse(reader["Activo"].ToString());
                item.PaginaInicio = reader["paginainicio"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected Roles SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, nombre, observaciones, activo,paginainicio FROM roles WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            Roles resultado = new Roles();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.Observaciones = reader["observaciones"].ToString();
                resultado.Activo = bool.Parse(reader["activo"].ToString());
                resultado.PaginaInicio = reader["paginainicio"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string Nombre, string Observaciones, string Activo, string PaginaInicio)
        {
            b.ExecuteCommandQuery("INSERT INTO roles (nombre,activo) " +
            "VALUES(@nombre,@activo)");
            b.AddParameter("@nombre", Nombre, SqlDbType.NVarChar);
            b.AddParameter("@activo", Activo == "on" ? true : false, SqlDbType.Bit);
            b.AddParameter("@paginainicio", PaginaInicio, SqlDbType.NVarChar,150);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string nombre, string observaciones, string activo, string paginainicio, string id)
        {
            b.ExecuteCommandQuery("UPDATE roles SET nombre=@nombre,observaciones=@observaciones,activo=@activo,paginainicio=@paginainicio WHERE id=@id");
            b.AddParameter("@nombre", nombre, SqlDbType.NVarChar);
            b.AddParameter("@observaciones", observaciones, SqlDbType.NVarChar);
            b.AddParameter("@activo", activo == "on" ? true : false, SqlDbType.Bit);
            b.AddParameter("@paginainicio", paginainicio, SqlDbType.NVarChar, 150);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}