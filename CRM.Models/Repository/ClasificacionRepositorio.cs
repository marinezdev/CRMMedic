using CRM.Models.Models;
using System.Collections.Generic;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class ClasificacionRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Clasificacion> Seleccionar(string idconfiguracion)
        {
            b.ExecuteCommandQuery("SELECT * FROM clasificacion WHERE idconfiguracion=@idconfiguracion ORDER BY Nombre");
            b.AddParameter("@idconfiguracion", idconfiguracion, System.Data.SqlDbType.Int);
            List<Clasificacion> resultado = new List<Clasificacion>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Clasificacion item = new Clasificacion()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected Clasificacion SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, nombre, idconfiguracion FROM clasificacion WHERE id=@id");
            b.AddParameter("@id", id, System.Data.SqlDbType.Int);
            Clasificacion resultado = new Clasificacion();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.IdConfiguracion = int.Parse(reader["IdConfiguracion"].ToString());
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UsuariosRoles> SeleccionarParaAdministracion()
        {
            b.ExecuteCommandQuery("SELECT cla.Id, cla.Nombre, conf.Nombre AS Empresa " +
            "FROM clasificacion cla " +
            "INNER JOIN configuracion conf ON conf.id=cla.IdConfiguracion " +
            "WHERE cla.idconfiguracion>1 " +
            "ORDER BY cla.Nombre");
            List<UsuariosRoles> resultado = new List<UsuariosRoles>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UsuariosRoles item = new UsuariosRoles();
                item.Clasificacion.Id = int.Parse(reader["Id"].ToString());
                item.Clasificacion.Nombre = reader["Nombre"].ToString();
                item.Configuracion.Nombre = reader["Empresa"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<Clasificacion> SeleccionarTodo()
        {
            b.ExecuteCommandQuery("SELECT * FROM clasificacion ORDER BY Nombre");
            List<Clasificacion> resultado = new List<Clasificacion>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Clasificacion item = new Clasificacion()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    IdConfiguracion = int.Parse(reader["IdConfiguracion"].ToString())

                };
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Obtiene la suma de todas las oportunidades clasificadas
        /// </summary>
        /// <returns></returns>
        protected List<Clasificacion> SeleccionarConteo(string idconfiguracion)
        {
            b.ExecuteCommandQuery("SELECT SUM(opo.IdClasificacion) AS Conteo, cla.Nombre " +
            "FROM oportunidades opo " +
            "INNER JOIN clasificacion cla ON cla.id = opo.IdClasificacion " +
            "WHERE opo.idconfiguracion=@idconfiguracion " +
            "GROUP BY opo.IdClasificacion, cla.nombre " +
            "ORDER BY opo.IdClasificacion DESC");
            b.AddParameter("@idconfiguracion", idconfiguracion, System.Data.SqlDbType.Int);
            List<Clasificacion> resultado = new List<Clasificacion>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Clasificacion items = new Clasificacion()
                {
                    Id = reader["Conteo"].ToString() == "" ? 0 : int.Parse(reader["Conteo"].ToString()),
                    Nombre = reader["Nombre"].ToString()
                };
                resultado.Add(items);
            }
            reader = null;
            b.CloseConnection();
            return resultado;

        }

        protected int Agregar(string Nombre, string IdConfiguracion)
        {
            b.ExecuteCommandQuery("INSERT INTO clasificacion (nombre, idconfiguracion) VALUES(@nombre, @idconfiguracion)");
            b.AddParameter("@nombre", Nombre, System.Data.SqlDbType.NVarChar);
            b.AddParameter("@idconfiguracion", IdConfiguracion, System.Data.SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string Nombre, string Id)
        {
            b.ExecuteCommandQuery("UPDATE clasificacion SET nombre=@nombre WHERE id=@id");
            b.AddParameter("@nombre", Nombre, System.Data.SqlDbType.NVarChar);
            b.AddParameter("@id", Id, System.Data.SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}