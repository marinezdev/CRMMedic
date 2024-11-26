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
    public class MonedaRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Moneda> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT id, nombre FROM moneda");
            List<Moneda> resultado = new List<Moneda>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Moneda item = new Moneda();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }
    }
}
