using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models.Models;
using CRM.Utilerias;
using System.Data;

namespace CRM.Models.Repository
{
    public class MarketingRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Marketing> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT id, consecutivo, nombre, nuevaanterior, inicio, fin, objetivo, medio, estado, usuario, correo, facebook, linkedin, llamada, paginaasae, envios, idudn FROM marketing");
            List<Marketing> resultado = new List<Marketing>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Marketing item = new Marketing();
                item.Id = int.Parse(reader["id"].ToString());
                item.Consecutivo = reader["consecutivo"].ToString();
                item.Nombre = reader["nombre"].ToString();
                item.NuevaAnterior = int.Parse(reader["nuevaanterior"].ToString());
                item.Inicio = reader["inicio"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["inicio"].ToString());
                item.Fin = reader["fin"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["fin"].ToString());
                item.Objetivo = reader["objetivo"].ToString();
                item.Medio = reader["medio"].ToString() == "" ? 0 : int.Parse(reader["medio"].ToString());
                item.Estado = int.Parse(reader["estado"].ToString());
                item.Correo = reader["correo"].ToString() == "" ? 0 : int.Parse(reader["correo"].ToString());
                item.Facebook = reader["facebook"].ToString() == "" ? 0 : int.Parse(reader["facebook"].ToString());
                item.Linkedin = reader["linkedin"].ToString() == "" ? 0 : int.Parse(reader["linkedin"].ToString());
                item.Llamada = reader["llamada"].ToString() == "" ? 0 : int.Parse(reader["llamada"].ToString());
                item.ASAE = reader["paginaasae"].ToString() == "" ? 0 : int.Parse(reader["paginaasae"].ToString());
                item.Envios = reader["envios"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["envios"].ToString());
                item.IdUDN = reader["idudn"].ToString() == "" ? 0 : int.Parse(reader["idudn"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected Marketing SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, consecutivo, nombre, nuevaanterior, inicio, fin, objetivo, medio, estado, correo, facebook, linkedin, llamada, paginaasae, " +
            "envios, inicioevento, finevento, horainicio, horafin, ubicacion, descripcion,idudn FROM marketing WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            Marketing resultado = new Marketing();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Consecutivo = reader["consecutivo"].ToString();
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.NuevaAnterior = int.Parse(reader["nuevaanterior"].ToString());
                resultado.Inicio = reader["inicio"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["inicio"].ToString());
                resultado.Fin = reader["fin"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["fin"].ToString());
                resultado.Objetivo = reader["objetivo"].ToString();
                resultado.Medio = reader["medio"].ToString() == "" ? 0 : int.Parse(reader["medio"].ToString());
                resultado.Estado = int.Parse(reader["estado"].ToString());
                resultado.Correo = reader["correo"].ToString() == "" ? 0 : int.Parse(reader["correo"].ToString());
                resultado.Facebook = reader["facebook"].ToString() == "" ? 0 : int.Parse(reader["facebook"].ToString());
                resultado.Linkedin = reader["linkedin"].ToString() == "" ? 0 : int.Parse(reader["linkedin"].ToString());
                resultado.Llamada = reader["llamada"].ToString() == "" ? 0 : int.Parse(reader["llamada"].ToString());
                resultado.ASAE = reader["paginaasae"].ToString() == "" ? 0 : int.Parse(reader["paginaasae"].ToString());
                resultado.Envios = reader["envios"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["envios"].ToString());
                resultado.InicioEvento = reader["inicioevento"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["inicioevento"].ToString());
                resultado.FinEvento = reader["finevento"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["finevento"].ToString());
                resultado.HoraInicio = reader["horainicio"].ToString();
                resultado.HoraFin = reader["horafin"].ToString();
                resultado.Ubicacion = reader["ubicacion"].ToString();
                resultado.Descripcion = reader["descripcion"].ToString();
                resultado.IdUDN = reader["idudn"].ToString() == "" ? 0 : int.Parse(reader["idudn"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected int SeleccionarEstadoCampaña(string idcampaña)
        {
            b.ExecuteCommandQuery("SELECT estado FROM marketing WHERE id=@idcampaña");
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }
        
        protected int Agregar(Marketing items)
        {
            b.ExecuteCommandQuery("IF NOT EXISTS(SELECT consecutivo FROM marketing WHERE consecutivo=@consecutivo) " +
            "BEGIN " +
            "DECLARE @idmarketingobtenido INT; " +
            "INSERT INTO marketing (consecutivo,nombre, nuevaanterior, inicio, fin, objetivo, medio, estado, usuario, correo, facebook, linkedin, llamada, paginaasae, envios, inicioevento, finevento, horainicio, horafin, ubicacion,descripcion,idudn) " +
            "VALUES(@consecutivo,@nombre, @nuevaanterior, @inicio, @fin, @objetivo, @medio, @estado, @usuario, @correo, @facebook, @linkedin, @llamada,@paginaasae,@envios, @inicioevento, @finevento, @horainicio, @horafin, @ubicacion,@descripcion,@idudn); " +
            "SET @idmarketingobtenido=@@IDENTITY; " +
            "INSERT INTO marketingseguimiento (idcampaña) VALUES(@idmarketingobtenido); " +
            "INSERT INTO marketingcorreo (idcampaña) VALUES (@idmarketingobtenido); " +
            "SELECT @idmarketingobtenido; " +
            "END " +
            "ELSE " +
            "BEGIN SELECT -2 END");
            b.AddParameter("@consecutivo", items.Consecutivo, SqlDbType.NVarChar);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar, 150);
            b.AddParameter("@nuevaanterior", items.NuevaAnterior, SqlDbType.Int);
            if (items.Inicio != DateTime.Parse("1900/01/01"))
                b.AddParameter("@inicio", items.Inicio, SqlDbType.DateTime);
            else
                b.AddParameter("@inicio");
            if (items.Fin != DateTime.Parse("1900/01/01"))
                b.AddParameter("@fin", items.Fin, SqlDbType.DateTime);
            else
                b.AddParameter("@fin");
            b.AddParameter("@objetivo", items.Objetivo, SqlDbType.NVarChar);
            b.AddParameter("@medio", items.Medio, SqlDbType.Int);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@usuario", items.Usuario, SqlDbType.Int);
            b.AddParameter("@correo", items.Correo, SqlDbType.Int);
            b.AddParameter("@facebook", items.Facebook, SqlDbType.Int);
            b.AddParameter("@linkedin", items.Linkedin, SqlDbType.Int);
            b.AddParameter("@llamada", items.Llamada, SqlDbType.Int);
            b.AddParameter("@paginaasae", items.ASAE, SqlDbType.Int);
            if (items.Envios != DateTime.Parse("1900/01/01"))
                b.AddParameter("@envios", items.Envios, SqlDbType.DateTime);
            else
                b.AddParameter("@envios");
            if (items.InicioEvento != DateTime.Parse("1900/01/01"))
                b.AddParameter("@inicioevento", items.InicioEvento, SqlDbType.Date);
            else
                b.AddParameter("@inicioevento");
            if(items.FinEvento != DateTime.Parse("1900/01/01"))
                b.AddParameter("@finevento", items.FinEvento, SqlDbType.Date);
            else
                b.AddParameter("@finevento");
            b.AddParameter("@horainicio", items.HoraInicio, SqlDbType.NVarChar, 8);
            b.AddParameter("@horafin", items.HoraFin, SqlDbType.NVarChar, 8);
            b.AddParameter("@ubicacion", items.Ubicacion.ToUpper(), SqlDbType.NVarChar, 50);
            b.AddParameter("@descripcion", items.Descripcion, SqlDbType.NVarChar, 150);
            if (items.IdUDN >= 0)
                b.AddParameter("@idudn", items.IdUDN, SqlDbType.Int);
            else
                b.AddParameter("@idudn");
            return b.SelectWithReturnValue();
        }

        protected int Modificar(Marketing items)
        {
            b.ExecuteCommandQuery("UPDATE marketing SET consecutivo=@consecutivo, nombre=@nombre, nuevaanterior=@nuevaanterior, inicio=@inicio, fin=@fin, objetivo=@objetivo, medio=@medio, estado=@estado, usuario=@usuario " +
            ",correo=@correo, facebook=@facebook, linkedin=@linkedin, llamada=@llamada, paginaasae=@paginaasae, envios=@envios, inicioevento=@inicioevento, finevento=@finevento, " +
            "horainicio=@horainicio, horafin=@horafin, ubicacion=@ubicacion, descripcion=@descripcion, idudn=@idudn " +
            "WHERE id=@id");
            b.AddParameter("@consecutivo", items.Consecutivo, SqlDbType.NVarChar);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar, 150);
            b.AddParameter("@nuevaanterior", items.NuevaAnterior, SqlDbType.Int);
            if (!items.Inicio.ToString().Contains("1900"))
                b.AddParameter("@inicio", items.Inicio, SqlDbType.DateTime);
            else
                b.AddParameter("@inicio");
            if (!items.Fin.ToString().Contains("1900"))
                b.AddParameter("@fin", items.Fin, SqlDbType.DateTime);
            else
                b.AddParameter("@fin");
            b.AddParameter("@objetivo", items.Objetivo, SqlDbType.NVarChar);
            b.AddParameter("@medio", items.Medio, SqlDbType.Int);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@usuario", items.Usuario, SqlDbType.Int);
            b.AddParameter("@correo", items.Correo, SqlDbType.Int);
            b.AddParameter("@facebook", items.Facebook, SqlDbType.Int);
            b.AddParameter("@linkedin", items.Linkedin, SqlDbType.Int);
            b.AddParameter("@llamada", items.Llamada, SqlDbType.Int);
            b.AddParameter("@paginaasae", items.ASAE, SqlDbType.Int);
            if (!items.Envios.ToString().Contains("1900"))
                b.AddParameter("@envios", items.Envios, SqlDbType.DateTime);
            else
                b.AddParameter("@envios");
            if (!items.InicioEvento.ToString().Contains("1900"))
                b.AddParameter("@inicioevento", items.InicioEvento, SqlDbType.Date);
            else
                b.AddParameter("@inicioevento");
            if (!items.FinEvento.ToString().Contains("1900"))
                b.AddParameter("@finevento", items.FinEvento, SqlDbType.Date);
            else
                b.AddParameter("@finevento");
            b.AddParameter("@horainicio", items.HoraInicio, SqlDbType.NVarChar, 8);
            b.AddParameter("@horafin", items.HoraFin, SqlDbType.NVarChar, 9);
            b.AddParameter("@ubicacion", items.Ubicacion.ToUpper(), SqlDbType.NVarChar, 50);
            b.AddParameter("@descripcion", items.Descripcion, SqlDbType.NVarChar, 150);
            b.AddParameter("@idudn", items.IdUDN, SqlDbType.Int);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
