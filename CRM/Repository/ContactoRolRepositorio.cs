using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class ContactoRolRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public List<ContactoRol> SeleccionarPorClasificacionRolesContacto(string id)
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
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}