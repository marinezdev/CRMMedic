using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models.Models;

namespace CRM.Models.Repository
{
    public class MarketingContactosRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected DataSet ProspectosActividadesReporteData(int IdCampaña)
        {
            DataSet dsResultado = null;
            string consulta = "ProspectosActividades @IdCampaña";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@IdCampaña", IdCampaña, SqlDbType.Int);
            dsResultado = b.SelectExecuteFunctions();
            b.CloseConnection();

            if (dsResultado.Tables.Count > 0)
            {
                dsResultado.Tables[0].TableName = "Prospectos";
                dsResultado.Tables[1].TableName = "Actividades a Prospectos";
            }

            return dsResultado;
        }

        protected DataSet ProspectosActividadesResumenData(int IdCampaña)
        {
            DataSet dsResultado = null;
            string consulta = "ProspectosActividadesResumen @IdCampaña";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@IdCampaña", IdCampaña, SqlDbType.Int);
            dsResultado = b.SelectExecuteFunctions();
            b.CloseConnection();

            return dsResultado;
        }


        protected List<Modelos> SeleccionarPorIdCampaña(string idcampaña)
        {
            string consulta = "SELECT mkc.idcontacto, mkc.idcampaña, mkc.asistencia, con.nombre, con.apellidopaterno, ISNULL(con.apellidomaterno, '') AS apellidomaterno, con.correo, con.telefono, con.celular, emp.nombre AS empresa, " +
            "mkt.nombre AS Campaña, mkt.Inicio, mkt.fin, mkc.seguimiento, mkc.ingreso, mkc.fechaalta, con.TipoContacto, con.Cargo, (select Area.Nombre from Area where id = con.IdArea) as Area, udn.Nombre AS UDN " +
            "FROM MarketingContactos mkc " +
            "INNER JOIN contactos con ON con.id = mkc.idcontacto " +
            "LEFT JOIN ContactosEmpresas ce ON ce.idcontacto=con.id " +
            "INNER JOIN marketing mkt ON mkt.id=mkc.IdCampaña " +
            "LEFT JOIN empresas emp ON emp.id = ce.idempresa " +
            "LEFT JOIN udn ON udn.id=con.idudn " +
            "WHERE mkc.idcampaña = @idcampaña";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);

#if DEBUG
            System.Diagnostics.Debug.WriteLine("declare @idcampaña int;");
            System.Diagnostics.Debug.WriteLine("set @idcampaña = " + idcampaña.ToString() + ";");
            System.Diagnostics.Debug.WriteLine(consulta);
#endif

            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.MarketingContactos.IdContacto = int.Parse(reader["idcontacto"].ToString());
                item.MarketingContactos.IdCampaña = int.Parse(reader["idcampaña"].ToString());
                item.MarketingContactos.Seguimiento = reader["seguimiento"].ToString();
                item.MarketingContactos.Ingreso = reader["ingreso"].ToString() == "" ? 0 : int.Parse(reader["ingreso"].ToString());
                item.MarketingContactos.Asistencia = reader["asistencia"].ToString() == "" ? false : bool.Parse(reader["asistencia"].ToString());
                item.MarketingContactos.FechaAlta = reader["fechaalta"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["fechaalta"].ToString());
                item.Contactos.Nombre = reader["nombre"].ToString();
                item.Contactos.ApellidoPaterno = reader["apellidopaterno"].ToString();
                item.Contactos.ApellidoMaterno = reader["apellidomaterno"].ToString();
                item.Contactos.Correo = reader["correo"].ToString();
                item.Contactos.Telefono = reader["telefono"].ToString();
                item.Contactos.Celular = reader["celular"].ToString();
                item.Empresas.Nombre = reader["empresa"].ToString();
                item.Marketing.Nombre = reader["campaña"].ToString();
                item.Marketing.Inicio = reader["inicio"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["inicio"].ToString());
                item.Marketing.Fin = reader["fin"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["fin"].ToString());

                if (reader["TipoContacto"].ToString() == "1")
                {
                    item.Contactos.sTipoPersona = "Cliente";
                }
                else
                {
                    item.Contactos.sTipoPersona = "Prospecto";
                }

                item.Contactos.Cargo = reader["Cargo"].ToString();
                item.Contactos.Area = reader["Area"].ToString();
                item.UDN.Nombre = reader["UDN"].ToString();

                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<Modelos> SeleccionarPorIdCampañaFiltrado(string idcampaña, string filtro)
        {
            string consulta = "SELECT mkc.idcontacto, mkc.idcampaña, mkc.asistencia, con.nombre, con.apellidopaterno, ISNULL(con.apellidomaterno,'') AS apellidomaterno, con.correo, emp.nombre AS empresa, " +
            "mkt.nombre AS Campaña, mkt.Inicio, mkt.fin, mkc.seguimiento, mkc.ingreso " +
            "FROM MarketingContactos mkc " +
            "INNER JOIN contactos con ON con.id = mkc.idcontacto " +
            "LEFT JOIN ContactosEmpresas ce ON ce.idcontacto=con.id " +
            "INNER JOIN marketing mkt ON mkt.id=mkc.IdCampaña " +
            "LEFT JOIN empresas emp ON emp.id = ce.idempresa " +
            "WHERE mkc.idcampaña = @idcampaña ";
            if (filtro == "2")
                consulta += "AND mkc.confirmacion=1 "; //confirmados
            else if (filtro == "3")
                consulta += "AND ISNULL(mkc.confirmacion, 0) "; //no confirmados
            else if (filtro == "4")
                consulta += "AND mkc.asistencia=1 "; //asistieron
            else if (filtro == "5")
                consulta += "AND mkc.asistencia=0 "; //no asistieron
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.MarketingContactos.IdContacto = int.Parse(reader["idcontacto"].ToString());
                item.MarketingContactos.IdCampaña = int.Parse(reader["idcampaña"].ToString());
                item.MarketingContactos.Seguimiento = reader["seguimiento"].ToString();
                item.MarketingContactos.Ingreso = reader["ingreso"].ToString() == "" ? 0 : int.Parse(reader["ingreso"].ToString());
                item.MarketingContactos.Asistencia = reader["asistencia"].ToString() == "" ? false : bool.Parse(reader["asistencia"].ToString());
                item.Contactos.Nombre = reader["nombre"].ToString();
                item.Contactos.ApellidoPaterno = reader["apellidopaterno"].ToString();
                item.Contactos.ApellidoMaterno = reader["apellidomaterno"].ToString();
                item.Contactos.Correo = reader["correo"].ToString();
                item.Empresas.Nombre = reader["empresa"].ToString();
                item.Marketing.Nombre = reader["campaña"].ToString();
                item.Marketing.Inicio = DateTime.Parse(reader["inicio"].ToString());
                item.Marketing.Fin = DateTime.Parse(reader["fin"].ToString());
                resultado.Add(item);
                }
            b.CloseConnection();
            return resultado;
        }

        protected Modelos SeleccionarPorIdCampañaIdContacto(string idcampaña, string idcontacto)
        {
            string consulta = "SELECT mkc.idcontacto, mkc.idcampaña, mkc.seguimiento, mkc.ingreso, mkc.asistencia, con.nombre, con.apellidopaterno, con.apellidomaterno " +
            "FROM MarketingContactos mkc " +
            "INNER JOIN contactos con ON con.id = mkc.idcontacto " +
            "WHERE mkc.idcampaña = @idcampaña " +
            "AND mkc.idcontacto = @idcontacto";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            Modelos resultado = new Modelos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.MarketingContactos.IdContacto = int.Parse(reader["idcontacto"].ToString());
                resultado.MarketingContactos.IdCampaña = int.Parse(reader["idcampaña"].ToString());
                resultado.MarketingContactos.Seguimiento = reader["seguimiento"].ToString();
                resultado.MarketingContactos.Ingreso = int.Parse(reader["ingreso"].ToString());
                resultado.MarketingContactos.Asistencia = reader["asistencia"].ToString() != "" ? true : false;
                resultado.Contactos.Nombre = reader["nombre"].ToString();
                resultado.Contactos.ApellidoPaterno = reader["apellidopaterno"].ToString();
                resultado.Contactos.ApellidoMaterno = reader["apellidomaterno"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<Modelos> SeleccionarPorIdContacto(string idcontacto)
        {
            b.ExecuteCommandQuery("SELECT mkc.idcontacto, mkc.idcampaña, con.nombre, con.apellidopaterno, con.apellidomaterno, mk.nombre AS campaña, mkc.seguimiento, mkc.ingreso " +
            "FROM MarketingContactos mkc " +
            "INNER JOIN contactos con ON con.id = mkc.idcontacto " +
            "INNER JOIN marketing mk ON mk.id = mkc.idcampaña " +
            "WHERE mkc.idcontacto=@idcontacto");
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.MarketingContactos.IdContacto = int.Parse(reader["idcontacto"].ToString());
                item.MarketingContactos.IdCampaña = int.Parse(reader["idcampaña"].ToString());
                item.MarketingContactos.Seguimiento = reader["seguimiento"].ToString();
                item.MarketingContactos.Ingreso = int.Parse(reader["ingreso"].ToString());
                item.Contactos.Nombre = reader["nombre"].ToString();
                item.Contactos.ApellidoPaterno = reader["apellidopaterno"].ToString();
                item.Contactos.ApellidoMaterno = reader["apellidomaterno"].ToString();
                item.Marketing.Nombre = reader["campaña"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<Modelos> SeleccionarPorIdCampañaEnvioCorreos(string idcampaña)
        {
            string consulta = "SELECT con.nombre + ' ' + con.apellidopaterno + ' ' + con.apellidomaterno AS nombre, con.Correo " +
            "FROM marketingcontactos mk " +
            "INNER JOIN contactos con ON con.id = mk.IdContacto " +
            "WHERE idcampaña=@idcampaña";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Contactos.Nombre           = reader["nombre"].ToString();
                item.Contactos.ApellidoPaterno  = reader["apellidopaterno"].ToString();
                item.Contactos.ApellidoMaterno  = reader["apellidomaterno"].ToString();
                item.Contactos.Correo           = reader["correo"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<Modelos> SeleccionarListado01PorIdCampaña(string idcampaña)
        {
            string consulta = "SELECT con.nombre + ' ' + con.apellidopaterno + ' ' + ISNULL(con.apellidomaterno,'') AS nombre, con.Correo " +
            "FROM marketingcontactos mk " +
            "INNER JOIN contactos con ON con.id = mk.IdContacto " +
            "WHERE idcampaña=@idcampaña";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Contactos.Nombre = reader["nombre"].ToString();
                //item.Contactos.ApellidoPaterno = reader["apellidopaterno"].ToString();
                //item.Contactos.ApellidoMaterno = reader["apellidomaterno"].ToString();
                item.Contactos.Correo = reader["correo"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<Modelos> SeleccionarListado02PorIdCampaña(string idcampaña)
        {
            string consulta = "SELECT con.nombre + ' ' + con.apellidopaterno + ' ' + ISNULL(con.apellidomaterno,'') AS nombre, con.Correo " +
            "FROM marketingcontactos mk " +
            "INNER JOIN contactos con ON con.id = mk.IdContacto " +
            "WHERE mk.ingreso > 1 " +
            "AND mk.idcampaña=@idcampaña";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Contactos.Nombre = reader["nombre"].ToString();
                //item.Contactos.ApellidoPaterno = reader["apellidopaterno"].ToString();
                //item.Contactos.ApellidoMaterno = reader["apellidomaterno"].ToString();
                item.Contactos.Correo = reader["correo"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<Modelos> SeleccionarListado03PorIdCampaña(string idcampaña)
        {
            string consulta = "SELECT con.nombre + ' ' + con.apellidopaterno + ' ' + ISNULL(con.apellidomaterno,'') AS nombre, con.Correo " +
            "FROM marketingcontactos mk " +
            "INNER JOIN contactos con ON con.id = mk.IdContacto " +
            "WHERE mk.confirmacion = 1 " +
            "AND mk.idcampaña=@idcampaña";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Contactos.Nombre = reader["nombre"].ToString();
                //item.Contactos.ApellidoPaterno = reader["apellidopaterno"].ToString();
                //item.Contactos.ApellidoMaterno = reader["apellidomaterno"].ToString();
                item.Contactos.Correo = reader["correo"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<Modelos> SeleccionarListado04PorIdCampaña(string idcampaña)
        {
            string consulta = "SELECT con.nombre + ' ' + con.apellidopaterno + ' ' + ISNULL(con.apellidomaterno,'') AS nombre, con.Correo " +
            "FROM marketingcontactos mk " +
            "INNER JOIN contactos con ON con.id = mk.IdContacto " +
            "WHERE mk.asistencia = 1 " +
            "AND mk.idcampaña=@idcampaña";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Contactos.Nombre = reader["nombre"].ToString();
                //item.Contactos.ApellidoPaterno = reader["apellidopaterno"].ToString();
                //item.Contactos.ApellidoMaterno = reader["apellidomaterno"].ToString();
                item.Contactos.Correo = reader["correo"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected MarketingContactos SeleccionarEstadisticasContactosPorIdEvento(string idevento)
        {            
            string consulta = "" +
            "DECLARE @ideventoo INT; " + 
            "DECLARE @clavecampaña NVARCHAR(50); " +
            "SELECT @clavecampaña = consecutivo FROM marketing WHERE id = @idcampaña; " +
            "SELECT @ideventoo = id FROM asaewebeventos WHERE claveevento = @clavecampaña; " +
            "SELECT " +
            "(SELECT COUNT(id) FROM MarketingContactos WHERE idcampaña = @idcampaña AND ingreso = 1) AS totales, " +
            //"(SELECT COUNT(id) FROM asaewebusuarioseventos WHERE idevento = @ideventoo) AS registrados, " +
            "(SELECT COUNT(id) FROM marketingcontactos WHERE idcampaña = @idcampaña AND ingreso>1) AS registrados, " +
            "(SELECT COUNT(id) FROM marketingcontactos WHERE confirmacion = 1 AND idcampaña = @idcampaña) AS confirmados, " + 
            "(SELECT COUNT(id) FROM marketingcontactos WHERE asistencia = 1 AND idcampaña = @idcampaña) AS asistieron "; 
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idevento, SqlDbType.Int);
            MarketingContactos resultado = new MarketingContactos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Totales = int.Parse(reader["totales"].ToString());
                resultado.Registrados = int.Parse(reader["registrados"].ToString());
                resultado.Confirmados = int.Parse(reader["confirmados"].ToString());
                resultado.Asistieron = int.Parse(reader["asistieron"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected MarketingContactos SeleccionarEstadisticasPorTipoIngreso(string idcampaña)
        { 
            string consulta = "SELECT " +
            "(SELECT COUNT(id) FROM marketingcontactos WHERE idcampaña = @idcampaña AND ingreso = 1) AS crm, " +
            "(SELECT COUNT(id) FROM marketingcontactos WHERE idcampaña = @idcampaña AND ingreso = 2) AS facebook, " +
            "(SELECT COUNT(id) FROM marketingcontactos WHERE idcampaña = @idcampaña AND ingreso = 3) AS web, " +
            "(SELECT COUNT(id) FROM marketingcontactos WHERE idcampaña = @idcampaña AND ingreso = 4) AS otros";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            MarketingContactos resultado = new MarketingContactos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.CRM = int.Parse(reader["crm"].ToString());
                resultado.Facebook = int.Parse(reader["facebook"].ToString());
                resultado.Web = int.Parse(reader["web"].ToString());
                resultado.Otros = int.Parse(reader["otros"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string idcampaña, string idcontacto)
        {
            b.ExecuteCommandQuery("IF NOT EXISTS(SELECT 1 FROM marketingcontactos WHERE idcampaña=@idcampaña AND idcontacto=@idcontacto)  " +
            "INSERT INTO marketingcontactos (idcampaña, idcontacto, ingreso) VALUES(@idcampaña, @idcontacto, 1)");
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string idcampaña, string idcontacto, string seguimiento, string asistencia)
        {
            b.ExecuteCommandQuery("UPDATE marketingcontactos SET seguimiento=@seguimiento WHERE idcampaña=@idcampaña AND idcontacto=@idcontacto");
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            b.AddParameter("@seguimiento", seguimiento, SqlDbType.NVarChar, 500);
            //b.AddParameter("@asistencia", asistencia == "1" ? true : false, SqlDbType.Bit);
            return b.InsertUpdateDelete();
        }

        protected int ModificarAsistencia(string idcontacto, string idcampaña, string estado)
        {
            b.ExecuteCommandQuery("UPDATE marketingcontactos SET asistencia=@estado WHERE idcontacto=@idcontacto AND idcampaña=@idcampaña");
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            b.AddParameter("@estado", estado == "1" ? true : false, SqlDbType.Bit);
            return b.InsertUpdateDelete();
        }

        protected int ModificarConfirmacion(string idcontacto, string idcampaña)
        {
            b.ExecuteCommandQuery("UPDATE marketingcontactos SET confirmacion=1 WHERE idcontacto=@idcontacto AND idcampaña=@idcampaña");
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Eliminar(string idcampaña, string idcontacto)
        {
            b.ExecuteCommandQuery("DELETE FROM marketingcontactos WHERE idcampaña=@idcampaña AND idcontacto=@idcontacto");
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
