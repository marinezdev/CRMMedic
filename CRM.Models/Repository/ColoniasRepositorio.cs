using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Repository
{
    public class ColoniasRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();
        public List<Colonias> Colonias_Seleccionar(Poblaciones poblaciones)
        {
            b.ExecuteCommandSP("Colonias_Seleccionar");
            b.AddParameter("@IdPolacion", poblaciones.Id, SqlDbType.Int);
            List<Colonias> resultado = new List<Colonias>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Colonias item = new Colonias()
                {
                    IdCP = Convert.ToInt32(reader["IdCP"].ToString()),
                    Colonia = reader["Colonia"].ToString(),
                };
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

    }
}
