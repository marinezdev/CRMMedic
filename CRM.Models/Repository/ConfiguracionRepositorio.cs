using CRM.Models.Models;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class ConfiguracionRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        /// <summary>
        /// Listado para llenar un select
        /// </summary>
        /// <returns></returns>
        protected List<Configuracion> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT id, nombre FROM configuracion ORDER BY Nombre");
            List<Configuracion> resultado = new List<Configuracion>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Configuracion item = new Configuracion()
                {
                    Id = int.Parse(reader["id"].ToString()),
                    Nombre = reader["nombre"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected Configuracion SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, nombre, logo, titulointermediopestaña,claselogo, clasenavegacion FROM configuracion WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            Configuracion resultado = new Configuracion();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.Logo = reader["Logo"].ToString();
                resultado.TituloIntermedioPestaña = reader["TituloIntermedioPestaña"].ToString();
                resultado.ClaseLogo = reader["ClaseLogo"].ToString();
                resultado.ClaseNavegacion = reader["ClaseNavegacion"].ToString();
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Listado para llenar una tabla
        /// </summary>
        /// <returns></returns>
        protected List<Configuracion> SeleccionarTodo()
        {
            b.ExecuteCommandQuery("SELECT id, nombre, logo, titulointermediopestaña FROM configuracion");
            List<Configuracion> resultado = new List<Configuracion>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Configuracion item = new Configuracion();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                item.Logo = reader["logo"].ToString();
                item.TituloIntermedioPestaña = reader["titulointermediopestaña"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string nombre, string logo, string titulointermediopestaña)
        {
            b.ExecuteCommandQuery("INSERT INTO configuracion (nombre, logo, titulointermediopestaña) VALUES(@nombre, @logo, @titulointermediopestaña)");
            b.AddParameter("@nombre", nombre, SqlDbType.NVarChar);
            b.AddParameter("@logo", logo, SqlDbType.NVarChar);
            b.AddParameter("@titulointermediopestaña", titulointermediopestaña, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string nombre, string logo, string titulointermediopestaña, string id)
        {
            b.ExecuteCommandQuery("UPDATE configuracion SET nombre=@nombre, logo=@logo, titulointermediopestaña=@titulointermediopestaña WHERE id=@id");
            b.AddParameter("@nombre", nombre, SqlDbType.NVarChar);
            b.AddParameter("@logo", logo, SqlDbType.NVarChar);
            b.AddParameter("@titulointermediopestaña", titulointermediopestaña, SqlDbType.NVarChar);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

    }
}