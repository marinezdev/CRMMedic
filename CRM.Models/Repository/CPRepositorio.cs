using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Repository
{
    public class CPRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();


        public List<CP> CP_Seleccionar(CP cP)
        {
            b.ExecuteCommandSP("CP_Seleccionar");
            b.AddParameter("@Id", cP.Id, SqlDbType.Int);
            List<CP> resultado = new List<CP>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                CP item = new CP()
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    _CP = reader["CP"].ToString(),
                };
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        public List<Direccion> CP_Busqueda(CP cP)
        {
            b.ExecuteCommandSP("CP_Busqueda");
            b.AddParameter("@CP", cP._CP, SqlDbType.NVarChar);
            List<Direccion> resultado = new List<Direccion>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Direccion item = new Direccion()
                {
                    IdEstado = Convert.ToInt32(reader["IdEstado"].ToString()),
                    Estado = reader["Estado"].ToString(),
                    IdPoblacion = Convert.ToInt32(reader["IdPoblacion"].ToString()),
                    Poblacion = reader["Poblacion"].ToString(),
                    IdColonia = Convert.ToInt32(reader["IdColonia"].ToString()),
                    Colonia = reader["Colonia"].ToString(),
                    IdCP = Convert.ToInt32(reader["IdCP"].ToString()),
                    CP = reader["CP"].ToString()
                };
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        
    }
}
