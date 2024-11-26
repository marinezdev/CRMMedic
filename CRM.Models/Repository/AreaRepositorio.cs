using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class AreaRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Area> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT id, nombre FROM area");
            List<Area> resultado = new List<Area>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Area item = new Area();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string nombre)
        {
            string consulta = "INSERT INTO area (nombre) VALUES(@nombre)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", nombre, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        public Area Area_Agregar(Area area)
        {
            b.ExecuteCommandSP("Area_Agregar");
            b.AddParameter("@Nombre", area.Nombre, SqlDbType.NVarChar);
            Area resultado = new Area();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = Convert.ToInt32(reader["Id"].ToString());
                resultado.Resultado = reader["Resultado"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        public List<Area> Area_Seleccionar()
        {
            b.ExecuteCommandSP("Area_Seleccionar");
            List<Area> resultado = new List<Area>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Area item = new Area()
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                };
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }
    }
}
