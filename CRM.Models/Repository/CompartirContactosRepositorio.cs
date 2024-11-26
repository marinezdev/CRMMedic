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
    public class CompartirContactosRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<CompartirUsuarios> SeleccionarUsuariosCompartidos(string idcontacto)
        {
            string consulta = "SELECT cc.id, cc.idcontacto, usu.id AS IdUsuario, usu.nombre + ' ' + ISNULL(usu.apellidopaterno,'') + ' ' + ISNULL(usu.apellidomaterno,'') AS nombre " +
            "FROM compartircontactos cc " +
            "INNER JOIN usuarios usu ON usu.id=cc.idusuario " +
            "WHERE cc.idcontacto=@idcontacto";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            List<CompartirUsuarios> resultado = new List<CompartirUsuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                CompartirUsuarios item = new CompartirUsuarios();
                item.CompartirContactos.Id = int.Parse(reader["Id"].ToString());
                item.CompartirContactos.IdContacto = int.Parse(reader["Idcontacto"].ToString());
                item.Usuarios.Nombre = reader["nombre"].ToString();
                item.Usuarios.Id = int.Parse(reader["idusuario"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected bool ValidarSiUsuarioPuedeModificar(string idcontacto, string idusuario)
        {
            //consulta por si no se debe compartir en escalera
            string consulta1 = "SELECT 1 FROM compartircontactos cc INNER JOIN contactos con ON con.id=cc.IdContacto WHERE cc.idcontacto=@idcontacto AND cc.idusuario=@idusuario AND cc.idusuario=@idusuario";
            string consulta = "SELECT 1 FROM compartircontactos WHERE idcontacto=@idcontacto AND idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            if (b.SelectString() == "1")
                return true;
            else
                return false;
        }

        protected int Agregar(string idcontacto, string idusuario)
        {            
            string consulta = "IF NOT EXISTS(SELECT 1 FROM compartircontactos WHERE idcontacto=@idcontacto ANd idusuario=@idusuario) " +
            "BEGIN " +
            "INSERT INTO compartircontactos(idcontacto, idusuario) VALUES(@idcontacto, @idusuario) " +
            "END ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int EliminarUsuarioCompartido(string idcontacto, string idusuario)
        {
            string consulta = "DELETE FROM compartircontactos WHERE idcontacto=@idcontacto AND idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
