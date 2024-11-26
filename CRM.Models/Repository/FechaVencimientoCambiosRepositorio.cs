using CRM.Models.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class FechaVencimientoCambiosRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();


        protected List<FechaVencimientoCambios> Seleccionar(string idoportunidad)
        {
            b.ExecuteCommandQuery("SELECT * FROM fechavencimientocambios WHERE idoportunidad=@idoportunidad");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<FechaVencimientoCambios> resultado = new List<FechaVencimientoCambios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                FechaVencimientoCambios item = new FechaVencimientoCambios();
                item.Id = int.Parse(reader["Id"].ToString());
                item.FechaVencimientoAnterior = DateTime.Parse(reader["fechavencimientoanterior"].ToString());
                item.IdOportunidad = int.Parse(reader["idoportunidad"].ToString());
                item.IdUsuario = int.Parse(reader["idusuario"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string fecha, string idoportunidad, string idusuario)
        {
            string consulta =
            "UPDATE oportunidades SET cierre=@fechavencimientoanterior WHERE id=@idoportunidad; " +
            "INSERT INTO fechavencimientocambios (fechavencimientoanterior, idoportunidad, idusuario) " +
            "VALUES(@fechavencimientoanterior, @idoportunidad, @idusuario)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@fechavencimientoanterior", fecha, SqlDbType.DateTime);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

    }
}
