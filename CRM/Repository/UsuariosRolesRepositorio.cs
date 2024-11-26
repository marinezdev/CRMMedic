using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CRM.Repository
{
    public class UsuariosRolesRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        /// <summary>
        /// Agrega un nuevo usuario al sistema
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <param name="IdRol"></param>
        /// <returns></returns>
        public bool Agregar(string IdUsuario, string IdRol)
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