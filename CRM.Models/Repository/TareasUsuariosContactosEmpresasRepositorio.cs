using CRM.Models.Models;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class TareasUsuariosContactosEmpresasRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        /// <summary>
        /// Guarda un nuevo registro para la tabla pivote tareasusuarioscontactosempresas
        /// </summary>
        /// <param name="idtarea"></param>
        /// <param name="idusuario"></param>
        /// <param name="idcontacto"></param>
        /// <param name="idempresa"></param>
        /// <returns></returns>
        protected bool Agregar(string idtarea, string idusuario, string idcontacto, string idempresa)
        {
            string consulta = string.Empty;
            if (idcontacto != null && idempresa == null)
                consulta = "INSERT INTO tareasusuarioscontactosempresas (idtarea,idusuario,idcontacto) VALUES(@idtarea,@idusuario,@idcontacto)";
            else if (idempresa != null && idcontacto != null)
                consulta = "INSERT INTO tareasusuarioscontactosempresas (idtarea,idusuario,idcontacto,idempresa) VALUES(@idtarea,@idusuario,@idcontacto,@idempresa)";
            else
                consulta = "INSERT INTO tareasusuarioscontactosempresas (idtarea,idusuario) VALUES(@idtarea,@idusuario)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idtarea", idtarea, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            if (idcontacto != null && idempresa==null)
                b.AddParameter("@idcontacto", idcontacto == null ? null : idcontacto, SqlDbType.Int);
            if (idempresa != null && idcontacto != null)
            {
                b.AddParameter("@idcontacto", idcontacto == null ? null : idcontacto, SqlDbType.Int);
                b.AddParameter("@idempresa", idempresa == null ? null : idempresa, SqlDbType.Int);
            }
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }
    }
}