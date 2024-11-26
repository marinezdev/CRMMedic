using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models.Models;

namespace CRM.Models.Repository
{
    public class MarketingCorreoRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected MarketingCorreo SeleccionarPorIdCampaña(string idcampaña)
        {
            b.ExecuteCommandQuery("SELECT id, idcampaña, asunto, cuerpo FROM marketingcorreo WHERE idcampaña=@idcampaña");
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            MarketingCorreo resultado = new MarketingCorreo();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.IdCampaña = int.Parse(reader["idcampaña"].ToString());
                resultado.Asunto = reader["asunto"].ToString();
                resultado.Cuerpo = reader["cuerpo"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string idcampaña, string asunto, string cuerpo)
        {
            b.ExecuteCommandQuery("INSERT INTO marketingcorreo (idcampaña, asunto, cuerpo) VALUES(@idcampaña, @asunto, @cuerpo)");
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            b.AddParameter("@asunto", asunto, SqlDbType.NVarChar, 200);
            b.AddParameter("@cuerpo", cuerpo, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string idcampaña, string asunto, string cuerpo)
        {
            b.ExecuteCommandQuery("UPDATE marketingcorreo SET asunto=@asunto, cuerpo=@cuerpo WHERE idcampaña=@idcampaña");
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            b.AddParameter("@asunto", asunto, SqlDbType.NVarChar, 200);
            b.AddParameter("@cuerpo", cuerpo, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }


    }
}
