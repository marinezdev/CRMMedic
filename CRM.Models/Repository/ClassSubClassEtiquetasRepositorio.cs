using CRM.Models.Models;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class ClassSubClassEtiquetasRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected ClassSubClassEtiquetas SeleccionarPorIdClasificacionIdSubClasificacion(string idC, string idSC)
        {
            b.ExecuteCommandQuery("SELECT * FROM classsubclassetiquetas WHERE idclasificacion=@idc AND idsubclasificacion=@idsc");
            b.AddParameter("@idc", idC, SqlDbType.Int);
            b.AddParameter("@idsc", idSC, SqlDbType.Int);
            ClassSubClassEtiquetas resultado = new ClassSubClassEtiquetas();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Etiqueta1 = reader["Etiqueta1"].ToString();
                resultado.Etiqueta2 = reader["Etiqueta2"].ToString();
                resultado.Etiqueta3 = reader["Etiqueta3"].ToString();
                resultado.Etiqueta4 = reader["Etiqueta4"].ToString();
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected UsuariosRoles SeleccionarPorIdClasificacionIdSubClasificacionParaEdicion(string idC, string idSC)
        {
            b.ExecuteCommandQuery("SELECT csce.etiqueta1,csce.etiqueta2,csce.etiqueta3,csce.etiqueta4,cla.id AS IdClasificacion, cla.nombre AS Clasificacion, " +
            "scla.Id AS IdSubClasificacion, scla.Nombre AS SubClasificacion " +
            "FROM classsubclassetiquetas csce " +
            "INNER JOIN clasificacion cla ON cla.id=csce.IdClasificacion " +
            "INNER JOIN SubClasificacion scla ON scla.id=csce.IdSubClasificacion " +
            "WHERE csce.idclasificacion=@idc AND csce.idsubclasificacion=@idsc");
            b.AddParameter("@idc", idC, SqlDbType.Int);
            b.AddParameter("@idsc", idSC, SqlDbType.Int);
            UsuariosRoles resultado = new UsuariosRoles();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.ClassSubClassEtiquetas.Etiqueta1 = reader["Etiqueta1"].ToString();
                resultado.ClassSubClassEtiquetas.Etiqueta2 = reader["Etiqueta2"].ToString();
                resultado.ClassSubClassEtiquetas.Etiqueta3 = reader["Etiqueta3"].ToString();
                resultado.ClassSubClassEtiquetas.Etiqueta4 = reader["Etiqueta4"].ToString();
                resultado.Clasificacion.Id = int.Parse(reader["IdClasificacion"].ToString());
                resultado.Clasificacion.Nombre = reader["Clasificacion"].ToString();
                resultado.SubClasificacion.Id = int.Parse(reader["IdSubClasificacion"].ToString());
                resultado.SubClasificacion.Nombre = reader["SubClasificacion"].ToString();
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UsuariosRoles> SeleccionarParaAdministracion()
        {
            b.ExecuteCommandQuery("SELECT csce.Etiqueta1, csce.Etiqueta2, csce.Etiqueta3, csce.Etiqueta4," +
            "cla.Nombre AS Clasificacion, scla.nombre AS SubClasificacion, cla.id AS IdClasificacion, scla.id AS IdSubClasificacion " +
            "FROM ClassSubClassetiquetas csce " +
            "INNER JOIN Clasificacion cla ON cla.id = csce.IdClasificacion " +
            "INNER JOIN SubClasificacion scla ON scla.id = csce.IdSubClasificacion");
            List<UsuariosRoles> resultado = new List<UsuariosRoles>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UsuariosRoles items = new UsuariosRoles();
                items.ClassSubClassEtiquetas.Etiqueta1 = reader["Etiqueta1"].ToString();
                items.ClassSubClassEtiquetas.Etiqueta2 = reader["Etiqueta2"].ToString();
                items.ClassSubClassEtiquetas.Etiqueta3 = reader["Etiqueta3"].ToString();
                items.ClassSubClassEtiquetas.Etiqueta4 = reader["Etiqueta4"].ToString();
                items.Clasificacion.Id = int.Parse(reader["IdClasificacion"].ToString());
                items.Clasificacion.Nombre = reader["Clasificacion"].ToString();
                items.SubClasificacion.Id = int.Parse(reader["IdSubClasificacion"].ToString());
                items.SubClasificacion.Nombre = reader["SubClasificacion"].ToString();
                resultado.Add(items);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int Modificar(string etiqueta1, string etiqueta2, string etiqueta3, string etiqueta4, string idclasificacion, string idsubclasificacion)
        {
            b.ExecuteCommandQuery("UPDATE ClassSubClassEtiquetas SET etiqueta1=@etiqueta1,etiqueta2=@etiqueta2,etiqueta3=@etiqueta3,etiqueta4=@etiqueta4 " +
            "WHERE idclasificacion=@idclasificacion AND idsubclasificacion=@idsubclasificacion");
            b.AddParameter("@etiqueta1", etiqueta1, SqlDbType.NVarChar);
            b.AddParameter("@etiqueta2", etiqueta2, SqlDbType.NVarChar);
            b.AddParameter("@etiqueta3", etiqueta3, SqlDbType.NVarChar);
            b.AddParameter("@etiqueta4", etiqueta4, SqlDbType.NVarChar);
            b.AddParameter("@idclasificacion", idclasificacion, SqlDbType.Int);
            b.AddParameter("@idsubclasificacion", idsubclasificacion, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}