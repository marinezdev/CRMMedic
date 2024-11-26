using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Repository
{
    public class AsaeWebEventosRepository
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<mod.AsaeWebEventos> Seleccionar()
        {
            string consulta = "SELECT id, nombre, fechainicio, fechafin, clave, password, descripcion, activo, fecharegistro FROM asaewebeventos";
            b.ExecuteCommandQuery(consulta);
            List<mod.AsaeWebEventos> resultado = new List<mod.AsaeWebEventos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.AsaeWebEventos item = new mod.AsaeWebEventos();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                item.FechaInicio = DateTime.Parse(reader["fechainicio"].ToString());
                item.FechaFin = DateTime.Parse(reader["fechafin"].ToString());
                item.Clave = reader["clave"].ToString();
                item.Password = reader["password"].ToString();
                item.Descripcion = reader["descripcion"].ToString();
                item.Activo = bool.Parse(reader["activo"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.AsaeWebEventos SeleccionarPorId(string id)
        {
            string consulta = "SELECT id, nombre, fechainicio, fechafin, clave, password, descripcion, activo, fecharegistro FROM asaewebeventos WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            mod.AsaeWebEventos resultado = new mod.AsaeWebEventos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.FechaInicio = DateTime.Parse(reader["fechainicio"].ToString());
                resultado.FechaFin = DateTime.Parse(reader["fechafin"].ToString());
                resultado.Clave = reader["clave"].ToString();
                resultado.Password = reader["password"].ToString();
                resultado.Descripcion = reader["descripcion"].ToString();
                resultado.Activo = bool.Parse(reader["activo"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }


    }
}
