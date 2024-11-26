using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Repository
{
    public class MarketingRecordatoriosRepository
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<mod.MarketingRecordatorios> SeleccionarPorIdCampaña(string idcampaña)
        {
            b.ExecuteCommandQuery("SELECT id, idcampaña, asunto, cuerpo, fechaenvio, fecharegistro, enviara, agenda FROM marketingrecordatorios WHERE idcampaña=@idcampaña");
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            List<mod.MarketingRecordatorios> resultado = new List<mod.MarketingRecordatorios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.MarketingRecordatorios item = new mod.MarketingRecordatorios();
                item.Id = int.Parse(reader["id"].ToString());
                item.IdCampaña = int.Parse(reader["idcampaña"].ToString());
                item.Asunto = reader["asunto"].ToString();
                item.Cuerpo = reader["cuerpo"].ToString();
                item.Envio = DateTime.Parse(reader["fechaenvio"].ToString());
                item.EnviarA = int.Parse(reader["enviara"].ToString());
                item.Agenda = int.Parse((reader["agenda"].ToString() == null || reader["agenda"].ToString() == String.Empty) ? "0" : reader["agenda"].ToString()); 
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.MarketingRecordatorios SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, idcampaña, asunto, cuerpo, fechaenvio, fecharegistro, enviara, agenda " + //, inicioevento, finevento, horainicio, horafin, ubicacion, descripcion " +
            "FROM marketingrecordatorios WHERE id=@id;");
            b.AddParameter("@id", id, SqlDbType.Int);
            mod.MarketingRecordatorios resultado = new mod.MarketingRecordatorios();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.IdCampaña = int.Parse(reader["idcampaña"].ToString());
                resultado.Asunto = reader["asunto"].ToString();
                resultado.Cuerpo = reader["cuerpo"].ToString();
                resultado.Envio = DateTime.Parse(reader["fechaenvio"].ToString());
                resultado.EnviarA = int.Parse(reader["enviara"].ToString());
                resultado.Agenda = reader["agenda"].ToString() == "" ? 0 : int.Parse(reader["agenda"].ToString());
                //resultado.InicioEvento = reader["inicioevento"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["inicioevento"].ToString());
                //resultado.FinEvento = reader["finevento"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["finevento"].ToString());
                //resultado.HoraInicio = reader["horainicio"].ToString();
                //resultado.HoraFin = reader["horafin"].ToString();
                //resultado.Ubicacion = reader["ubicacion"].ToString();
                //resultado.Descripcion = reader["descripcion"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(mod.MarketingRecordatorios items)
        {
            b.ExecuteCommandQuery(" " +
            "INSERT INTO marketingrecordatorios (idcampaña, asunto, cuerpo, fechaenvio, enviara, agenda) " + //, inicioevento, finevento, horainicio, horafin, ubicacion, descripcion) " +
            "VALUES(@idcampaña, @asunto, @cuerpo, @fechaenvio, @enviara, @agenda); " +   //, @inicioevento, @finevento, @horainicio, @horafin, @ubicacion, @descripcion); ; " +
            "SELECT @@IDENTITY");
            b.AddParameter("@idcampaña", items.IdCampaña, SqlDbType.Int);
            b.AddParameter("@asunto", items.Asunto, SqlDbType.NVarChar, 150);
            b.AddParameter("@cuerpo", items.Cuerpo, SqlDbType.NVarChar);
            b.AddParameter("@fechaenvio", items.Envio, SqlDbType.DateTime);
            b.AddParameter("@enviara", items.EnviarA, SqlDbType.Int);
            if (items.Agenda == 1)
            {
                b.AddParameter("@agenda", items.Agenda, SqlDbType.Int);
                //b.AddParameter("@inicioevento", items.InicioEvento, SqlDbType.Date);
                //b.AddParameter("@finevento", items.FinEvento, SqlDbType.Date);
                //b.AddParameter("@horainicio", items.HoraInicio, SqlDbType.NVarChar, 8);
                //b.AddParameter("@horafin", items.HoraFin, SqlDbType.NVarChar, 8);
                //b.AddParameter("@ubicacion", items.Ubicacion, SqlDbType.NVarChar, 50);
                //b.AddParameter("@descripcion", items.Descripcion, SqlDbType.NVarChar, 150);
            }
            else
            {
                b.AddParameter("@agenda", 0, SqlDbType.Int);
                //b.AddParameter("@inicioevento");
                //b.AddParameter("@finevento");
                //b.AddParameter("@horainicio");
                //b.AddParameter("@horafin");
                //b.AddParameter("@ubicacion");
                //b.AddParameter("@descripcion");
            }
            return int.Parse(b.SelectString());
        }

        protected int Modificar(mod.MarketingRecordatorios items)
        {
            b.ExecuteCommandQuery("UPDATE marketingrecordatorios SET asunto=@asunto, cuerpo=@cuerpo, fechaenvio=@fechaenvio, enviara=@enviara, " +
            "agenda=@agenda, inicioevento=@inicioevento, finevento=@finevento, horainicio=@horainicio, horafin=@horafin, ubicacion=@ubicacion, descripcion=@descripcion WHERE id=@id");
            b.AddParameter("@asunto", items.Asunto, SqlDbType.NVarChar, 150);
            b.AddParameter("@cuerpo", items.Cuerpo, SqlDbType.NVarChar);
            b.AddParameter("@fechaenvio", items.Envio, SqlDbType.DateTime);
            b.AddParameter("@enviara", items.EnviarA, SqlDbType.Int);
            if (items.Agenda == 1)
            {
                b.AddParameter("@agenda", items.Agenda, SqlDbType.Int);
                b.AddParameter("@inicioevento", items.InicioEvento, SqlDbType.Date);
                b.AddParameter("@finevento", items.FinEvento, SqlDbType.Date);
                b.AddParameter("@horainicio", items.HoraInicio, SqlDbType.NVarChar, 8);
                b.AddParameter("@horafin", items.HoraFin, SqlDbType.NVarChar, 8);
                b.AddParameter("@ubicacion", items.Ubicacion, SqlDbType.NVarChar, 50);
                b.AddParameter("@descripcion", items.Descripcion, SqlDbType.NVarChar, 150);
            }
            else
            {
                b.AddParameter("@agenda", 0, SqlDbType.Int);
                b.AddParameter("@inicioevento");
                b.AddParameter("@finevento");
                b.AddParameter("@horainicio");
                b.AddParameter("@horafin");
                b.AddParameter("@ubicacion");
                b.AddParameter("@descripcion");
            }
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }


    }
}
