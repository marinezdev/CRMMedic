using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Models.Models;

namespace CRM.Models.Repository
{
    public class MarketingSeguimientoRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<MarketingSeguimiento> SeleccionarPorIdCampaña(string idcampaña)
        {
            b.ExecuteCommandQuery("SELECT id, idcampaña, fecha, seguimiento FROM marketingseguimiento WHERE idcampaña=@idcampaña");
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            List<MarketingSeguimiento> resultado = new List<MarketingSeguimiento>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                MarketingSeguimiento item = new MarketingSeguimiento();
                item.Id = int.Parse(reader["id"].ToString());
                item.IdCampaña = int.Parse(reader["idcampaña"].ToString());
                item.Fecha = DateTime.Parse(reader["fecha"].ToString());
                item.Seguimiento = reader["seguimiento"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected MarketingModelos SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT ms.id, ms.idcampaña, ms.seguimiento, mk.nombre " +
            "FROM marketingseguimiento ms " +
            "INNER JOIN marketing mk ON mk.id=ms.idcampaña " +
            "WHERE ms.id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            MarketingModelos resultado = new MarketingModelos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.MarketingSeguimiento.Id = int.Parse(reader["id"].ToString());
                resultado.MarketingSeguimiento.IdCampaña = int.Parse(reader["idcampaña"].ToString());
                resultado.MarketingSeguimiento.Seguimiento = reader["seguimiento"].ToString();
                resultado.Marketing.Nombre = reader["nombre"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string idcampaña, string seguimiento)
        {
            b.ExecuteCommandQuery("INSERT INTO marketingseguimiento (idcampaña, seguimiento) VALUES(@idcampaña, @seguimiento)");
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            b.AddParameter("@seguimiento", seguimiento, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string id, string seguimiento)
        {
            b.ExecuteCommandQuery("UPDATE marketingseguimiento SET seguimiento=@seguimiento WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            b.AddParameter("@seguimiento", seguimiento, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }
    }
}
