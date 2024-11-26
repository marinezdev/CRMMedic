using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CRM.Models;

namespace CRM.Repository
{
    public class OportunidadesUsuariosRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        /// <summary>
        /// Guarda un usuario del sistema asignado a una oportunidad para que le dé seguimiento
        /// </summary>
        public int AgregarUsuario(string idoportunidad, string idasignado, string clasificacion, string subclasificacion)
        {
            b.ExecuteCommandQuery("IF NOT EXISTS(SELECT (1) FROM oportunidadesusuarios WHERE idoportunidad=@idoportunidad AND idasignado=@idasignado) " +
             "   BEGIN" +
             "       INSERT INTO oportunidadesusuarios (idoportunidad,idasignado,idclasificacionresponsable,idsubclasificacionresponsable) " +
             "       VALUES(@idoportunidad,@idasignado,@clasificacion,@subclasificacion)" +
             "   END");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idasignado", idasignado, SqlDbType.Int);
            b.AddParameter("@clasificacion", clasificacion, SqlDbType.Int);
            b.AddParameter("@subclasificacion", subclasificacion, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

    }
}