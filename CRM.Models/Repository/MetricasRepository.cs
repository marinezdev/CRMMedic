using CRM.Models.Models;
using System.Data;

namespace CRM.Models.Repository
{
    public class MetricasRepository
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected Metricas SeleccionarPorIdOportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT id, metrica, comprador, criterios,proceso, necesidad, campeon, fulcro, otros, idoportunidad FROM metricas WHERE idoportunidad=@idoportunidad");
            b.AddParameter("@idoportunidad", id, SqlDbType.Int);
            Metricas resultado = new Metricas();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Metrica = reader["metrica"].ToString();
                resultado.Comprador = reader["comprador"].ToString();
                resultado.Criterios = reader["criterios"].ToString();
                resultado.Proceso = reader["proceso"].ToString();
                resultado.Necesidad = reader["necesidad"].ToString();
                resultado.Campeon = reader["campeon"].ToString();
                resultado.Fulcro = reader["fulcro"].ToString();
                resultado.Otros = reader["otros"].ToString();
                resultado.IdOportunidad = int.Parse(reader["idoportunidad"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(Metricas items)
        {
            b.ExecuteCommandQuery("INSERT INTO metricas (metrica, comprador, criterios,proceso, necesidad, campeon, fulcro, otros, idoportunidad) " +
            "VALUES(@metrica, @comprador, @criterios,@proceso, @necesidad, @campeon, @fulcro, @otros, @idoportunidad)");
            b.AddParameter("@metrica", items.Metrica, SqlDbType.NVarChar);
            b.AddParameter("@comprador", items.Comprador, SqlDbType.NVarChar);
            b.AddParameter("@criterios", items.Criterios, SqlDbType.NVarChar);
            b.AddParameter("@proceso", items.Proceso, SqlDbType.NVarChar);
            b.AddParameter("@necesidad", items.Necesidad, SqlDbType.NVarChar);
            b.AddParameter("@campeon", items.Campeon, SqlDbType.NVarChar);
            b.AddParameter("@fulcro", items.Fulcro, SqlDbType.NVarChar);
            b.AddParameter("@otros", items.Otros, SqlDbType.NVarChar);
            b.AddParameter("@idoportunidad", items.IdOportunidad, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(Metricas items)
        {
            b.ExecuteCommandQuery("UPDATE metricas SET metrica=@metrica, comprador=@comprador, criterios=@criterios,proceso=@proceso, necesidad=@necesidad, campeon=@campeon, fulcro=fulcro, " +
            "otros=@otros WHERE idoportunidad=@idoportunidad");
            b.AddParameter("@metrica", items.Metrica, SqlDbType.NVarChar);
            b.AddParameter("@comprador", items.Comprador, SqlDbType.NVarChar);
            b.AddParameter("@criterios", items.Criterios, SqlDbType.NVarChar);
            b.AddParameter("@proceso", items.Proceso, SqlDbType.NVarChar);
            b.AddParameter("@necesidad", items.Necesidad, SqlDbType.NVarChar);
            b.AddParameter("@campeon", items.Campeon, SqlDbType.NVarChar);
            b.AddParameter("@fulcro", items.Fulcro, SqlDbType.NVarChar);
            b.AddParameter("@otros", items.Otros, SqlDbType.NVarChar);
            b.AddParameter("@idoportunidad", items.IdOportunidad, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

    }
}
