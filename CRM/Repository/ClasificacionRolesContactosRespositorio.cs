using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class ClasificacionRolesContactosRespositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public List<ClasificacionRolesContactos> Seleccionar()
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
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}