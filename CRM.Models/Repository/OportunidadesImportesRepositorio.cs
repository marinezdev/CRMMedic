using CRM.Models.Models;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class OportunidadesImportesRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<OportunidadesImportes> SeleccionarImportesPorIdOportunidad(string idoportunidad)
        {
            string consulta = "SELECT oi.id, oi.idoportunidad, oi.importe, oi.moneda, oi.tipocambio, oi.rubro, oi.observaciones, mon.nombre AS MonedaNombre, rub.Nombre AS RubroNombre FROM oportunidadesimportes oi " +
            "INNER JOIN moneda mon ON mon.id=oi.moneda " +
            "INNER JOIN rubros rub on rub.id=oi.rubro " +
            "WHERE idoportunidad=@idoportunidad " +
            "ORDER BY oi.id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<OportunidadesImportes> resultado = new List<OportunidadesImportes>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                OportunidadesImportes item = new OportunidadesImportes();
                item.Id = int.Parse(reader["Id"].ToString());
                item.IdOportunidad = int.Parse(reader["IdOportunidad"].ToString());
                item.Importe = float.Parse(reader["importe"].ToString());
                item.Moneda = int.Parse(reader["moneda"].ToString());
                item.TipoCambio = float.Parse(reader["tipocambio"].ToString());
                item.Rubro = int.Parse(reader["rubro"].ToString());
                item.Observaciones = reader["observaciones"].ToString();
                item.MonedaNombre = reader["monedanombre"].ToString();
                item.RubroNombre = reader["rubronombre"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected float SeleccionarSumaImportesNacionalPorIdOportunidad(string idoportunidad)
        {
            string consulta = "" +
            //"UPDATE oportunidades SET importe=(SELECT SUM(importe) from oportunidadesimportes WHERE idoportunidad=@idoportunidad AND moneda=5) WHERE id=@idoportunidad; " +
            "SELECT SUM(importe) FROM oportunidadesimportes WHERE idoportunidad=@idoportunidad AND moneda=5 ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            if (b.SelectString() == "")
                return 0;
            else
                return float.Parse(b.SelectString());
        }

        protected float SeleccionarSumaImportesInternacionalPorIdOportunidad(string idoportunidad)
        {
            string consulta = "" +
            //"UPDATE oportunidades SET importe=(SELECT SUM(importe*tipocambio) from oportunidadesimportes WHERE idoportunidad=@idoportunidad AND moneda <> 5) WHERE id=@idoportunidad; " +
            "SELECT SUM(importe*tipocambio) FROM oportunidadesimportes WHERE idoportunidad=@idoportunidad AND moneda<>5 ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            if (b.SelectString() == "")
                return 0;
            else
                return float.Parse(b.SelectString());
        }

        protected OportunidadesImportes SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT * FROM oportunidadesimportes WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            OportunidadesImportes resultado = new OportunidadesImportes();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id            = int.Parse(reader["Id"].ToString());
                resultado.IdOportunidad = int.Parse(reader["IdOportunidad"].ToString());
                resultado.Importe       = float.Parse(reader["importe"].ToString());
                resultado.Moneda        = int.Parse(reader["moneda"].ToString());
                resultado.TipoCambio    = float.Parse(reader["tipocambio"].ToString());
                resultado.Rubro         = int.Parse(reader["rubro"].ToString());
                resultado.Observaciones = reader["observaciones"].ToString(); 
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string idoportunidad, string importe, string moneda, string tipocambio, string rubro, string observaciones)
        {
            //Calcular el importe si la moneda no es nacional            
            float imp = string.IsNullOrEmpty(importe) ? 0 : float.Parse(importe);
            float tc = string.IsNullOrEmpty(tipocambio) ? 0 : float.Parse(tipocambio);
            int mon = string.IsNullOrEmpty(moneda) ? 5 : int.Parse(moneda);
            int rub = string.IsNullOrEmpty(rubro) ? 1 : int.Parse(rubro);
            float imptotal = mon == 5 ? imp : imp;
            string consulta = "INSERT INTO oportunidadesimportes (idoportunidad,importe,moneda,tipocambio,rubro,observaciones) " +
            "VALUES(@idoportunidad,@importe,@moneda,@tipocambio,@rubro,@observaciones)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@importe", imptotal, SqlDbType.Float);
            b.AddParameter("@moneda", mon, SqlDbType.Int);
            b.AddParameter("@tipocambio", tc, SqlDbType.Float);
            b.AddParameter("@rubro", 1, SqlDbType.Int);
            b.AddParameter("@observaciones", observaciones, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string importe, string moneda, string tipocambio, string rubro, string observaciones, string id)
        {
            float imp = string.IsNullOrEmpty(importe) ? 0 : float.Parse(importe);
            float tc = string.IsNullOrEmpty(tipocambio) ? 0 : float.Parse(tipocambio);
            int mon = string.IsNullOrEmpty(moneda) ? 5 : int.Parse(moneda);
            int rub = string.IsNullOrEmpty(rubro) ? 1 : int.Parse(rubro);
            float imptotal = mon == 5 ? imp : imp * tc;
            string consulta = "UPDATE oportunidadesimportes SET importe=@importe,moneda=@moneda,tipocambio=@tipocambio,rubro=@rubro," +
            "observaciones=@observaciones WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@importe", imp, SqlDbType.Float);
            b.AddParameter("@moneda", mon, SqlDbType.Int);
            b.AddParameter("@tipocambio", tc, SqlDbType.Float);
            b.AddParameter("@rubro", rub, SqlDbType.Int);
            b.AddParameter("@observaciones", observaciones, SqlDbType.NVarChar);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int ModificarImporteTotalOportunidad(string idoportunidad, string importe)
        {
            string consulta = "UPDATE oportunidades SET importe=@importe WHERE id=@idoportunidad; ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@importe", importe, SqlDbType.Float);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Eliminar(string id)
        {
            string consulta = "DELETE FROM oportunidadesimportes WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
