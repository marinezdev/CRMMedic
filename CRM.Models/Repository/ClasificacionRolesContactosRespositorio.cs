using CRM.Models.Models;
using System.Collections.Generic;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class ClasificacionRolesContactosRespositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<ClasificacionRolesContactos> Seleccionar()
        {
            string consulta = "SELECT Id, Nombre FROM ClasificacionRolesContactos";
            b.ExecuteCommandQuery(consulta);
            List<ClasificacionRolesContactos> resultado = new List<ClasificacionRolesContactos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ClasificacionRolesContactos item = new ClasificacionRolesContactos();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }
    }
}