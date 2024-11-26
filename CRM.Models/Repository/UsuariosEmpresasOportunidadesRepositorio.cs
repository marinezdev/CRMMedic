using CRM.Models.Models;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class UsuariosEmpresasOportunidadesRepositorio //TODO ESTA CLASE DEBE DESAPARECER, TRALADAR SUS METODOS Y REFACTORIZARLOS
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<UsuariosEmpresasOportunidadesDetalle> SeleccionarResponsablesPorOportunidad(string id)
        {
            string consulta1 = "SELECT usu.Id AS IdUsuario, usu.Nombre AS NombreUsuario, bic.Id AS IdBitacora, bic.Estado," +
            "CASE bic.Estado " +
            "   WHEN 1 THEN 'Asignado' " +
            "   WHEN 2 THEN 'Visto' " +
            "   WHEN 3 THEN 'En Proceso' " +
            "   WHEN 4 THEN 'Terminado' " +
            "   WHEN 5 THEN 'Cancelado' " +
            " END AS EstadoNombre " +
            "FROM OportunidadesEmpresasContactosUsuarios oecu " +
            "INNER JOIN Oportunidades opo ON opo.Id = oecu.IdOportunidad " +
            "INNER JOIN Empresas emp ON emp.id = oecu.IdEmpresa " +
            "LEFT JOIN Bitacora bic ON bic.IdOportunidad = oecu.IdOportunidad  " +
            "LEFT JOIN Usuarios usu ON usu.id = bic.IdResponsable  " +
            "WHERE oecu.IdOportunidad=@id";
            string consulta = "SELECT con.Id AS IdUsuario, con.Nombre + ' ' + con.ApellidoPaterno + ' ' + con.ApellidoMaterno AS NombreUsuario, bic.Id AS IdBitacora, " +
            "bic.Estado, " +
            "CASE bic.Estado " +
            "    WHEN 1 THEN 'Asignado' " +
            "    WHEN 2 THEN 'Visto' " +
            "    WHEN 3 THEN 'En Proceso' " +
            "    WHEN 4 THEN 'Terminado' " +
            "    WHEN 5 THEN 'Cancelado' " +
            "    END AS EstadoNombre, " +
            "crc.Nombre AS Clasificacion, " +
            "cr.Nombre AS SubClasificacion " +
            "FROM OportunidadesEmpresasContactosUsuarios oecu " +
            "INNER JOIN Oportunidades opo ON opo.Id = oecu.IdOportunidad " +
            "INNER JOIN Empresas emp ON emp.id = oecu.IdEmpresa " +
            "LEFT JOIN Bitacora bic ON bic.IdOportunidad = oecu.IdOportunidad " +
            "LEFT JOIN Contactos con ON con.Id = bic.IdResponsable " +
            "INNER JOIN clasificacionrolescontactos crc ON crc.id = bic.idclasificacionresponsable " +
            "INNER JOIN contactorol cr ON cr.id = bic.idsubclasificacionresponsable " +
            "WHERE oecu.IdOportunidad = @id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            List<UsuariosEmpresasOportunidadesDetalle> resultado = new List<UsuariosEmpresasOportunidadesDetalle>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UsuariosEmpresasOportunidadesDetalle item = new UsuariosEmpresasOportunidadesDetalle();
                item.Usuarios.Id            = reader["IdUsuario"].ToString() == "" ? 0 : int.Parse(reader["IdUsuario"].ToString());
                item.Usuarios.Nombre        = reader["NombreUsuario"].ToString();
                item.Bitacora.Id            = reader["IdBitacora"].ToString() == "" ? 0 : int.Parse(reader["IdBitacora"].ToString());
                item.Bitacora.Estado        = reader["Estado"].ToString() == "" ? 0 : int.Parse(reader["Estado"].ToString());
                item.Bitacora.NombreEstado  = reader["EstadoNombre"].ToString();
                item.ClasificacionRolesContactos.Nombre = reader["Clasificacion"].ToString();
                item.ContactoRol.Nombre = reader["SubClasificacion"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Obtiene los responsables que no tengan una asignacion para la bitacora que esté usando
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected List<Usuarios> SeleccionarResponsablesPorEmpresa(string id)
        {
            b.ExecuteCommandQuery("SELECT usu.Id, usu.Nombre " +
            "FROM EmpresasUsuarios eu " +
            "INNER JOIN Empresas emp ON emp.id = eu.IdEmpresa " +
            "INNER JOIN Usuarios usu ON usu.id = eu.IdUsuario " +
            "WHERE emp.Id=@id ");
            //"AND usu.id NOT IN (SELECT idresponsable FROM bitacora WHERE estado=1 OR Estado=2)");
            b.AddParameter("@id", id, SqlDbType.Int);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nombre= reader["Nombre"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(UsuariosEmpresasOportunidades items)
        {
            b.ExecuteCommandQuery("INSERT INTO usuariosempresasoportunidades (idusuario,idempresa, idoportunidad, idbitacora) " +
            "VALUES(@idusuario,@idempresa,@idoportunidad,@idbitacora)");
            b.AddParameter("@idusuario", items.IdUsuario, SqlDbType.Int);
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@idoportunidad", items.Idoportunidad, SqlDbType.Int);
            b.AddParameter("@idbitacora", items.IdBitacora, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

    }
}