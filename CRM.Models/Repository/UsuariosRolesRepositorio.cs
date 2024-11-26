using CRM.Models.Models;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class UsuariosRolesRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected string SeleccionarRolUsuario(string idusuario)
        {
            string consulta = "SELECT idrol FROm usuariosroles WHERE idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.SelectString();
        }

        /// <summary>
        /// Agrega un nuevo usuario al sistema asignándole el rol correspondiente
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <param name="IdRol"></param>
        /// <returns></returns>
        protected bool Agregar(string IdUsuario, string IdRol)
        {
            b.ExecuteCommandQuery("INSERT INTO usuariosroles (idusuario, idrol) VALUES(@idusuario, @idrol)");
            b.AddParameter("@idusuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@idrol", IdRol, SqlDbType.Int);
            if (b.InsertUpdateDelete() >= 1)
                return true;
            else
                return false;
        }
    }
}