using CRM.Models.Models;
using System.Data;
using System.Collections.Generic;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class OportunidadesUsuariosRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<UsuariosRoles> SeleccionarPorIdOportunidad(string idoportunidad)
        {
            string consulta = "SELECT opor.idoportunidad, " +
            "usu.id AS IdUsuario, usu.Nombre, usu.apellidopaterno, ISNULL(usu.ApellidoMaterno, '') AS ApellidoMaterno, " +
            "bic.id AS idbitacora " +
            "FROM oportunidadesusuarios opor " +
            "INNER JOIN usuarios usu ON usu.id = opor.idasignado " +
            "LEFT JOIN Bitacora bic ON(bic.IdResponsable = usu.id AND bic.IdOportunidad = opor.IdOportunidad) " +
            "WHERE opor.idoportunidad = @id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", idoportunidad, SqlDbType.Int);
            List<UsuariosRoles> resultado = new List<UsuariosRoles>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UsuariosRoles item = new UsuariosRoles();
                item.Usuarios.Id = int.Parse(reader["IdUsuario"].ToString());
                item.Usuarios.Nombre = reader["Nombre"].ToString();
                item.Usuarios.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.Usuarios.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }
        protected int SeleccionarSiUsuarioYaEstaAsignadoAOportunidad(string idoportunidad, string idasignado)
        {
            string consulta = "SELECT 1 FROM oportunidadesusuarios WHERE idoportunidad=@idoportunidad AND idasignado=@idasignado";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idasignado", idasignado, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        /// <summary>
        /// Guarda un usuario del sistema asignado a una oportunidad para que le dé seguimiento
        /// </summary>
        protected int AgregarUsuario(string idoportunidad, string idasignado, string clasificacion, string subclasificacion)
        {
            string consulta = "INSERT INTO oportunidadesusuarios (idoportunidad,idasignado";
            if (!string.IsNullOrEmpty(clasificacion))
                consulta += ",idclasificacionresponsable";
            if (!string.IsNullOrEmpty(subclasificacion))
                consulta += ",idsubclasificacionresponsable";
            consulta += ") ";
            consulta += "       VALUES(@idoportunidad,@idasignado";
            if (!string.IsNullOrEmpty(clasificacion))
                consulta += ",@clasificacion";
            if (!string.IsNullOrEmpty(subclasificacion))
                consulta += ",@subclasificacion";
             consulta += ")";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idasignado", idasignado, SqlDbType.Int);
            if (!string.IsNullOrEmpty(clasificacion))
                b.AddParameter("@clasificacion", clasificacion, SqlDbType.Int);
            if (!string.IsNullOrEmpty(subclasificacion))
                b.AddParameter("@subclasificacion", subclasificacion, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int EliminarUsuarioResponsableOportunidad(string idusuario, string idoportunidad)
        {
            string consulta = "DELETE FROM oportunidadesusuarios WHERE idoportunidad=@idoportunidad AND idasignado=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

    }
}