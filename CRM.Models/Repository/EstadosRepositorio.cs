using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CRM.Models.Repository
{
    public class EstadosRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();
        public List<Estados> Estados_Seleccionar()
        {
            b.ExecuteCommandSP("Estados_Seleccionar");
            List<Estados> resultado = new List<Estados>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Estados item = new Estados()
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Estado = reader["Estado"].ToString(),
                };
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }
    }
}
