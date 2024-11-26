using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models.Models;
using System.Data;

namespace CRM.Models.Repository
{
    public class EmpresasProveedoresRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<EmpresasProveedores> Seleccionar()
        {
            string consulta = "SELECT id, nombre, rfc, telefono FROM empresasproveedores";
            b.ExecuteCommandQuery(consulta);
            List<EmpresasProveedores> resultado = new List<EmpresasProveedores>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                EmpresasProveedores item = new EmpresasProveedores();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                item.RFC = reader["rfc"].ToString();
                item.Telefono = reader["telefono"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected EmpresasProveedores SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, nombre, rfc, telefono FROM empresasproveedores WHERE id=@id");
            b.AddParameter("@id", id, System.Data.SqlDbType.Int);
            EmpresasProveedores resultado = new EmpresasProveedores();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.RFC = reader["rfc"].ToString();
                resultado.Telefono = reader["telefono"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(EmpresasProveedores items)
        {
            string consulta = "" +
            "IF NOT EXISTS(SELECT rfc FROM empresasproveedores WHERE rfc=@rfc) " +
            "   INSERT INTO empresasproveedores (nombre, rfc, telefono) VALUES (@nombre, @rfc, @telefono) ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar, 150);
            b.AddParameter("@rfc", items.RFC.ToUpper(), SqlDbType.NChar, 13);
            b.AddParameter("@telefono", items.Telefono, SqlDbType.NVarChar, 50);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(EmpresasProveedores items)
        {
            string consulta = "UPDATE empresasproveedores SET nombre=@nombre, rfc=@rfc, telefono=@telefono WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar, 150);
            b.AddParameter("@rfc", items.RFC.ToUpper(), SqlDbType.NChar, 13);
            b.AddParameter("@telefono", items.Telefono, SqlDbType.NVarChar, 50);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
