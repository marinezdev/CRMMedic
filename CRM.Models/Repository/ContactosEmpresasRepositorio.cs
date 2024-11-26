using CRM.Models.Models;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class ContactosEmpresasRepositorio 
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        /// <summary>
        /// Agregar Contacto, Empresa tabla pivote
        /// </summary>
        /// <param name="idcontacto"></param>
        /// <param name="idempresa"></param>
        /// <returns></returns>
        protected bool AgregarContactoEmpresa(string idcontacto, string idempresa)
        {
            b.ExecuteCommandQuery("INSERT INTO contactosempresas (idcontacto, idempresa) VALUES(@idcontacto,@idempresa)");
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Agrega un contacto a la tabla pivote
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected int AgregarSoloContacto(string id)
        {
            b.ExecuteCommandQuery("INSERT INTO contactosempresas (idcontacto) VALUES(@id)");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int ActualizarContactoEmpresa(string idcontacto, string idempresa)
        {
            b.ExecuteCommandQuery("UPDATE contactosempresas SET idempresa=@idempresa WHERE idcontacto=@idcontacto");
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected bool AgregarSoloEmpresa(string idempresa)
        {
            b.ExecuteCommandQuery("INSERT INTO contactosempresas (idempresa) VALUES(@idempresa)");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }
    }
}