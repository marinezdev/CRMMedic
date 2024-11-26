using CRM.Models.Models;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class ContactoRolRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<ContactoRol> SeleccionarPorClasificacionRolesContacto(string id)
        {
            string consulta = "SELECT Id, Nombre FROM contactorol WHERE idclasificacionrolescontactos=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            List<ContactoRol> resultado = new List<ContactoRol>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ContactoRol item = new ContactoRol();
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