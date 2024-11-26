using CRM.Models.Models;
using System.Data;
using System.Runtime.InteropServices.ComTypes;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class OportunidadesEmpresasContactosUsuariosRepositorio 
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected string SeleccionarEmpresaPorOportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT IdEmpresa FROM OportunidadesEmpresasContactosUsuarios WHERE IdOportunidad=@idoportunidad");
            b.AddParameter("@idoportunidad", id, SqlDbType.Int);
            return b.SelectString();
        }

        protected string SeleccionarIdResponsableCreadorOportunidad(string idoportunidad)
        {
            string consulta = "SELECT idusuario FROM oecu WHERE idoportunidad=@idoportunidad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            return b.SelectString();
        }

        protected string SeleccionarNombreCreadorOportunidad(string idoportunidad)
        {
            string consulta = "SELECT usu.nombre + ' ' + ISNULL(apellidopaterno, '') + ' ' + ISNULL(apellidomaterno,'') AS mombre FROM usuarios usu INNER JOIN oecu ON oecu.idusuario=usu.id WHERE oecu.idoportunidad=@idoportunidad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            return b.SelectString();
        }

        /// <summary>
        /// Guarda la nueva oportunidad en la tabla indice
        /// </summary>
        /// <param name="idoportunidad"></param>
        /// <param name="idempresa"></param>
        /// <param name="idcontacto"></param>
        /// <param name="idusuario"></param>
        /// <returns></returns>
        protected bool Agregar(string idoportunidad, string idempresa, string idcontacto, string idusuario)
        {
            b.ExecuteCommandQuery("INSERT INTO OportunidadesEmpresasContactosUsuarios (idoportunidad, idempresa, idcontacto, idusuario) " +
            "VALUES(@idoportunidad, @idempresa, @idcontacto, @idusuario)");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        protected int AgregarSoloEmpresa(string idempresa, string idoportunidad)
        {
            b.ExecuteCommandQuery("UPDATE OportunidadesEmpresasContactosUsuarios SET idempresa=@idempresa WHERE idoportunidad=@idoportunidad ");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int AgregarSolocontacto(string idcontacto, string idoportunidad)
        {
            b.ExecuteCommandQuery("UPDATE OportunidadesEmpresasContactosUsuarios SET idcontacto=@idcontacto WHERE idoportunidad=@idoportunidad ");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        /// <summary>
        /// Agrega la oportunidad a la tabla pivote
        /// </summary>
        /// <param name="idoportunidad"></param>
        /// <param name="idusuario"></param>
        /// <returns></returns>
        protected int AgregarSoloOportunidad(string idoportunidad, string idusuario, string idconfiguracion)
        {
            b.ExecuteCommandQuery("INSERT INTO OportunidadesEmpresasContactosUsuarios (idoportunidad, idusuario, idconfiguracion) " +
            "VALUES(@idoportunidad, @idusuario,@idconfiguracion)");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int AgregarBitacora(string idoportunidad, string idempresa, string idusuario, string idbitacora)
        {
            b.ExecuteCommandQuery("UPDATE OportunidadesEmpresasContactosUsuarios SET IdBitacora=@idbitacora " +
            "WHERE idoportunidad=@idoportunidad AND idempresa=@idempresa)");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idbitacora", idbitacora, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }



    }
}