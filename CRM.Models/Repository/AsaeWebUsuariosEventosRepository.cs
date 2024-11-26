using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Repository
{
    public class AsaeWebUsuariosEventosRepository
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<mod.AsaeWebUsuariosEventos> SeleccionarPorIdEvento(string idevento)
        {
            string consulta = "SELECT id, idevento, nombre, apellidopaterno, apellidomaterno, nombreempresa, correoempresa, correopersonal, telefonomovil, telefonolocal, confirmado, activo, fecharegistro FROM asaewebusuarioseventos";
            b.ExecuteCommandQuery(consulta);
            List<mod.AsaeWebUsuariosEventos> resultado = new List<mod.AsaeWebUsuariosEventos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.AsaeWebUsuariosEventos item = new mod.AsaeWebUsuariosEventos();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                item.ApellidoPaterno = reader["apellidopaterno"].ToString();
                item.ApellidoMaterno = reader["apellidomaterno"].ToString();
                item.NombreEmpresa = reader["nombreempresa"].ToString();
                item.CorreoEmpresa = reader["correoempresa"].ToString();
                item.CorreoPersonal = reader["correopersonal"].ToString();
                item.TelefonoMovil = reader["telefonomovil"].ToString();
                item.TelefonoLocal = reader["telefonolocal"].ToString();
                item.Confirmado = bool.Parse(reader["confirmado"].ToString());
                item.Activo = bool.Parse(reader["activo"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.AsaeWebUsuariosEventos SeleccionarEstadisticasContactosPorIdEvento(string idevento)
        {
            string consulta = "SELECT " +
            "(SELECT COUNT(idevento) FROM AsaeWebUsuariosEventos WHERE idevento=@idevento) AS ContactosTotales, " +
            "(SELECT COUNT(idevento) FROM AsaeWebUsuariosEventos WHERE activo=1 AND idevento=@idevento) AS Activos, " +
            "(SELECT COUNT(idevento) FROM AsaeWebUsuariosEventos WHERE activo=0 AND idevento=@idevento) AS Inactivos, " +
            "(SELECT COUNT(idevento) FROM AsaeWebUsuariosEventos WHERE confirmado=1 AND idevento=@idevento) AS Confirmados " +
            "FROM AsaeWebEventos";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idevento", idevento, SqlDbType.Int);
            mod.AsaeWebUsuariosEventos resultado = new mod.AsaeWebUsuariosEventos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Totales = int.Parse(reader["contactostotales"].ToString());
                resultado.Activos = int.Parse(reader["activos"].ToString());
                resultado.Inactivos = int.Parse(reader["inactivos"].ToString());
                resultado.Confirmados = int.Parse(reader["confirmados"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }


    }
}
