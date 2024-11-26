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
    public class CodigoPostalRespositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<DireccionCompleta> SeleccionarCodigoPostal(string cp)
        {
            string consulta = "SELECT cp.CP, co.Colonia + ' ' + po.Poblacion + ' ' + edo.Estado AS DireccionCompleta " +
            "FROM cp " +
            "INNER JOIN colonias co ON co.IdCP = cp.Id " +
            "INNER JOIN poblaciones po ON po.id = cp.IdPoblacion " +
            "INNER JOIN estados edo ON edo.id = po.IdEstado " +
            "WHERE cp.CP = @cp";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@cp", cp, SqlDbType.Int);
            List<DireccionCompleta> resultado = new List<DireccionCompleta>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                DireccionCompleta item = new DireccionCompleta();
                item.CP = int.Parse(reader["CP"].ToString());
                item.Direccion = reader["DireccionCompleta"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected string SeleccionarEstadoPorCodigoPostal(string cp)
        { 
            string consulta = "SELECT edo.estado " +
            "FROM cp " +
            "INNER JOIN poblaciones pob ON pob.Id = cp.IdPoblacion " +
            "INNER JOIN estados edo ON edo.Id = pob.IdEstado " +
            "WHERE cp.CP=@cp";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@cp", cp, SqlDbType.Int);
            return b.SelectString();
        }
    }

    public class DireccionCompleta
    { 
        public int CP { get; set; }
        public string Direccion { get; set; }
    }
}
