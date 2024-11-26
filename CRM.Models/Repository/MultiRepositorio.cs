using CRM.Models.Models;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    public class MultiRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Multi2> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT usu.nombre + ' ' + usu.ApellidoPaterno + ' ' + usu.ApellidoMaterno AS Usuario, conf.Nombre AS Empresa, mu.IdUsuario, mu.IdConfiguracion " +
            "FROM multi mu " +
            "INNER JOIN usuarios usu ON usu.Id = mu.IdUsuario " +
            "INNER JOIN configuracion conf ON conf.Id = mu.IdConfiguracion");
            List<Multi2> resultado = new List<Multi2>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Multi2 item = new Multi2();
                item.Multi.IdConfiguracion      = int.Parse(reader["idconfiguracion"].ToString());
                item.Multi.IdUsuario            = int.Parse(reader["idusuario"].ToString());
                item.Usuarios.Nombre      = reader["Usuario"].ToString();
                item.Configuracion.Nombre = reader["empresa"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int Eliminar(string idconfiguracion, string idusuario)
        {
            b.ExecuteCommandQuery("DELETE FROM multi WHERE idconfiguracion=@idconfiguracion AND idusuario=@idusuario");
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
