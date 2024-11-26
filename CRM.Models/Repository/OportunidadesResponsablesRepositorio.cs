using CRM.Models.Models;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class OportunidadesResponsablesRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<UsuariosEmpresasOportunidadesDetalle> SeleccionarResponsablesPorOportunidad(string id, string idconfiguracion)
        {
            string consultaanterior = "SELECT opor.idoportunidad, " +
            "usu.id AS IdContacto, usu.Nombre, usu.apellidopaterno, ISNULL(usu.ApellidoMaterno, '') AS ApellidoMaterno, " +
            "emp.Nombre AS Empresa,  " +
            "bic.id AS idbitacora, " +
            "SUBSTRING(bic.notas, 0, 50) AS Notas, " +
            "con.Telefono " +
            "FROM oportunidadesresponsables opor " +
            "INNER JOIN usuarios usu ON usu.id = opor.IdAsignado " +
            "LEFT JOIN contactos con ON con.IdUsuarioResponsable=usu.id " +
            "INNER JOIN ContactosEmpresas ce ON ce.IdContacto = usu.id " +
            "INNER JOIN empresas emp ON ce.IdEmpresa = emp.id " +
            "LEFT JOIN clasificacionrolescontactos crc ON crc.id = opor.idclasificacionresponsable " +
            "LEFT JOIN contactorol cr ON cr.id = opor.idsubclasificacionresponsable " +
            "LEFT JOIN Bitacora bic ON(bic.IdResponsable = usu.id AND opor.IdOportunidad = bic.IdOportunidad) " +
            "WHERE opor.idoportunidad = @id";

            string consultaant = "SELECT opor.id, usu.id AS IdUsuario, usu.Nombre, usu.apellidopaterno, ISNULL(usu.ApellidoMaterno, '') AS ApellidoMaterno, emp.Nombre AS Empresa, bic.id AS idbitacora " +
            "FROM oportunidades opor " +
            "INNER JOIN OportunidadesResponsables oporr ON oporr.IdOportunidad = opor.id " +
            "INNER JOIN usuarios usu ON usu.Id = oporr.IdAsignado " +
            "INNER JOIN EmpresasUsuarios eu ON eu.IdUsuario = usu.id " +
            "INNER JOIN empresas emp ON emp.id = eu.IdEmpresa " +
            "LEFT JOIN Bitacora bic on bic.IdOportunidad=opor.id " +
            "WHERE opor.id = @id";

            string consulta = "SELECT opor.id, usu.id AS IdUsuario, usu.Nombre, usu.apellidopaterno, ISNULL(usu.ApellidoMaterno, '') AS ApellidoMaterno, bic.id AS idbitacora, ur.IdRol " +
            "FROM oportunidades opor " +
            "INNER JOIN Bitacora bic on bic.IdOportunidad = opor.id " +
            "INNER JOIN usuarios usu ON usu.Id = bic.IdResponsable " +
            "INNER JOIN UsuariosRoles ur ON ur.IdUsuario=usu.id " +
            "WHERE opor.id = @id";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            List<UsuariosEmpresasOportunidadesDetalle> resultado = new List<UsuariosEmpresasOportunidadesDetalle>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UsuariosEmpresasOportunidadesDetalle item = new UsuariosEmpresasOportunidadesDetalle();
                //item.Usuarios.Id = reader["idoportunidad"].ToString() == "" ? 0 : int.Parse(reader["idoportunidad"].ToString());
                //item.Usuarios.Nombre = reader["Nombre"].ToString();
                //item.Usuarios.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                //item.Usuarios.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                //item.Contactos.Telefono = reader["Telefono"].ToString();
                //item.Empresas.Nombre = reader["Empresa"].ToString();
                //item.Bitacora.Id = reader["idbitacora"].ToString() == "" ? 0 : int.Parse(reader["idbitacora"].ToString());
                //item.Bitacora.Notas = reader["Notas"].ToString();

                item.Oportunidades.Id = reader["id"].ToString() == "" ? 0 : int.Parse(reader["id"].ToString());
                item.Usuarios.Id = reader["idusuario"].ToString() == "" ? 0 : int.Parse(reader["idusuario"].ToString());
                item.Usuarios.Nombre = reader["Nombre"].ToString();
                item.Usuarios.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.Usuarios.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                //item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Bitacora.Id = reader["idbitacora"].ToString() == "" ? 0 : int.Parse(reader["idbitacora"].ToString());
                item.UsuariosRoles.Roles.Id = reader["idrol"].ToString() == "" ? 0 : int.Parse(reader["idrol"].ToString());

                resultado.Add(item);
            }

            if (idconfiguracion == "2")
            {
                string consulta2 = "SELECT opor.id, usu.id AS IdUsuario, usu.Nombre, usu.apellidopaterno, ISNULL(usu.ApellidoMaterno, '') AS ApellidoMaterno " +
                "FROM oportunidades opor " +
                "INNER JOIN oecu ON oecu.idoportunidad=opor.id " +
                "INNER JOIN usuarios usu ON usu.Id = oecu.IdUsuario " +
                "WHERE opor.id = @id";

                b.ExecuteCommandQuery(consulta2);
                b.AddParameter("@id", id, SqlDbType.Int);
                List<UsuariosEmpresasOportunidadesDetalle> resultado2 = new List<UsuariosEmpresasOportunidadesDetalle>();
                var reader2 = b.ExecuteReader();
                while (reader2.Read())
                {
                    UsuariosEmpresasOportunidadesDetalle item = new UsuariosEmpresasOportunidadesDetalle();

                    item.Oportunidades.Id = reader2["id"].ToString() == "" ? 0 : int.Parse(reader2["id"].ToString());
                    item.Usuarios.Id = reader2["idusuario"].ToString() == "" ? 0 : int.Parse(reader2["idusuario"].ToString());
                    item.Usuarios.Nombre = reader2["Nombre"].ToString();
                    item.Usuarios.ApellidoPaterno = reader2["ApellidoPaterno"].ToString();
                    item.Usuarios.ApellidoMaterno = reader2["ApellidoMaterno"].ToString();
                    item.Bitacora.Id = 0;

                    resultado.Add(item);
                }
            }
            reader = null;

            b.CloseConnection();
            return resultado;
        }


        protected string ContarResponsablesPorIdOportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT COUNT(1) " +
            "FROM Oportunidadesresponsables " +
            "WHERE idoportunidad=@id " +
            "GROUP BY idoportunidad");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.SelectString();
        }

        protected int SeleccionarSiOportunidadTieneResponsablesAsignados(string idoportunidad)
        {
            string consulta = "SELECT IIF(count(idoportunidad) > 0, '1','0') FROM oportunidadesresponsables WHERE idoportunidad=@idoportunidad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected List<Modelos> UsuariosQuehanVistoBitacora()
        {
            b.ExecuteCommandQuery("SELECT opor.IdOportunidad, con.Nombre, con.ApellidoPaterno, ISNULL(con.ApellidoMaterno, '') AS ApellidoMaterno, ISNULL(bic.Id, '0') AS IdBitacora " +
            "FROM OportunidadesResponsables opor " +
            "LEFT JOIN bitacora bic ON bic.IdResponsable = opor.IdAsignado " +
            "LEFT JOIN contactos con ON con.id = opor.IdAsignado");
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Oportunidades.Id = reader["IdOportunidad"].ToString() == "" ? 0 : int.Parse(reader["idoportunidad"].ToString());
                item.Contactos.Nombre = reader["ApellidoPaterno"].ToString() + " " + reader["ApellidoMaterno"].ToString() + " " + reader["Nombre"].ToString();
                item.Bitacora.Id = reader["IdBitacora"].ToString() == "" ? 0 : int.Parse(reader["IdBitacora"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int SeleccionarSiResponsableYaEstaAsignadoAOportunidad(string idoportunidad, string idasignado)
        {
            string consulta = "SELECT 1 FROM oportunidadesresponsables WHERE idoportunidad=@idoportunidad AND idasignado=@idasignado";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idasignado", idasignado, SqlDbType.Int);
            return int.Parse(b.SelectString());

        }

        /// <summary>
        /// Guardar cada responsable asignado a la oportunidad
        /// </summary>
        protected int Agregar(string idoportunidad, string idasignado, string clasificacion, string subclasificacion)
        {
            string consulta = "INSERT INTO oportunidadesresponsables (idoportunidad,idasignado";
            if (!string.IsNullOrEmpty(clasificacion))
                consulta += ",idclasificacionresponsable";
            if (!string.IsNullOrEmpty(subclasificacion))
                consulta += ",idsubclasificacionresponsable";
            consulta += ") " +
            "       VALUES(@idoportunidad,@idasignado";
            if (!string.IsNullOrEmpty(clasificacion))
                consulta += ",@clasificacion";
            if (!string.IsNullOrEmpty(subclasificacion))
                consulta += ",@subclasificacion";
            consulta += ") ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idasignado", idasignado, SqlDbType.Int);
            if (!string.IsNullOrEmpty(clasificacion))
                b.AddParameter("@clasificacion", clasificacion, SqlDbType.Int);
            if (!string.IsNullOrEmpty(subclasificacion))
                b.AddParameter("@subclasificacion", subclasificacion, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Eliminar(string idoportunidad, string idusuario)
        {
            string consulta = "DELETE FROM oportunidadesresponsables WHERE idoportunidad=@idoportunidad AND idasignado=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

    }
}