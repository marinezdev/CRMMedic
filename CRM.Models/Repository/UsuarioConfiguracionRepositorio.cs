using CRM.Models.Models;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class UsuarioConfiguracionRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        /// <summary>
        /// Se une el usuario y la configuracion a la que pertenecerá
        /// </summary>
        /// <param name="idusuario"></param>
        /// <param name="idconfiguracion"></param>
        /// <returns></returns>
        protected int Agregar(string idusuario, string idconfiguracion)
        {
            b.ExecuteCommandQuery("INSERT INTO usuarioconfiguracion VALUES(@idusuario,@idconfiguracion)");
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string idusuario, string idconfiguracion)
        {
            string consulta = "UPDATE usuarioconfiguracion SET idconfiguracion=@idconfiguracion WHERE idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("idconfiguracion", idconfiguracion, SqlDbType.Int);
            b.AddParameter("idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}