using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Utilerias;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class PaisesRepository
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();
        protected List<Generico> SeleccionarPaises()
        {
            b.ExecuteCommandQuery("SELECT * FROM paises");
            List<Generico> resultado = new List<Generico>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Generico item = new Generico();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }
    }
}
