using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models.Models;
using System.Data;

namespace CRM.Models.Repository
{
    public class MarketingCorreoPlantillasRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<MarketingCorreoPlantillas> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT id, nombre, codigo FROM marketingcorreoplantillas");
            List<MarketingCorreoPlantillas> resultado = new List<MarketingCorreoPlantillas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                MarketingCorreoPlantillas item = new MarketingCorreoPlantillas();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                item.Codigo = reader["codigo"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected MarketingCorreoPlantillas SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, nombre, codigo FROM marketingcorreoplantillas WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            MarketingCorreoPlantillas resultado = new MarketingCorreoPlantillas();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.Codigo = reader["codigo"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }
    }
}
