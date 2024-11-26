using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Repository
{
    public class PoblacionesRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();
        public List<Poblaciones> Poblaciones_Seleccionar(Estados estados)
        {
            b.ExecuteCommandSP("Poblaciones_Seleccionar");
            b.AddParameter("@IdEstado", estados.Id, SqlDbType.Int);
            List<Poblaciones> resultado = new List<Poblaciones>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Poblaciones item = new Poblaciones()
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Poblacion = reader["Poblacion"].ToString(),
                };
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }
    }
}
