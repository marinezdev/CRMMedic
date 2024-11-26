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
    public class AvisoRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Modelos> SeleccionarContactoPorIdOportunidad(string idoportunidad)
        {
            string consulta = "SELECT avi.Fecha, avi.IdUsuario, usu.Nombre + ' ' + usu.ApellidoPaterno + ' ' + usu.ApellidoMaterno AS Contacto " +
            "FROM aviso avi " +
            "LEFT JOIN Usuarios usu ON usu.id = avi.IdUsuario " +
            "WHERE avi.idoportunidad=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", idoportunidad, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Aviso.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Aviso.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
                item.Usuarios.Nombre = reader["Contacto"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<Usuarios> SelecccionarUsuariosParaEnviarAviso()
        {
            string consulta = "SELECT avi.idoportunidad,avi.idusuario,usu.Correo, opor.IdConfiguracion " +
            "FROM aviso avi " +
            "INNER JOIN usuarios usu ON usu.id = avi.IdUsuario " +
            "WHERE fecha = CONVERT(VARCHAR, GETDATE(), 23)";
            b.ExecuteCommandQuery(consulta);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios();
                item.Id = int.Parse(reader["idoportunidad"].ToString());
                item.IdUsuario = int.Parse(reader["idusuario"].ToString());
                item.Correo = reader["Correo"].ToString();
                item.Empresa = int.Parse(reader["idconfiguracion"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string idoportunidad, string fecha, string idusuariocontacto)
        {
            b.ExecuteCommandQuery("INSERT INTO aviso VALUES(@idoportunidad, @fecha, @idusuariocontacto)");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@fecha", fecha, SqlDbType.Date);
            b.AddParameter("@idusuariocontacto", idusuariocontacto, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Eliminar(string idoportunidad, string idusuario)
        { 
            string consulta = "DELETE FROM aviso WHERE idoportunidad=@idoportunidad AND idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
