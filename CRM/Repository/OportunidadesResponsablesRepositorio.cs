using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class OportunidadesResponsablesRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public List<UsuariosEmpresasOportunidadesDetalle> SeleccionarResponsablesPorOportunidad(string id)
        {
            string consulta = "SELECT opor.idoportunidad, " +
            "con.id AS IdContacto, con.Nombre, con.apellidopaterno, ISNULL(con.ApellidoMaterno, '') AS ApellidoMaterno, " +
            "emp.Nombre AS Empresa, con.cargo, con.Telefono, " +
            "con.ContactoUsuario, " +
            "bic.id AS idbitacora, " +
            "SUBSTRING(bic.notas, 0, 50) AS Notas, " +
            "con.Idusuarioresponsable " +
            "FROM oportunidadesresponsables opor " +
            "INNER JOIN contactos con ON con.id = opor.idasignado " +
            "INNER JOIN ContactosEmpresas ce ON ce.IdContacto = con.id " +
            "INNER JOIN empresas emp ON ce.IdEmpresa = emp.id " +
            "INNER JOIN clasificacionrolescontactos crc ON crc.id = opor.idclasificacionresponsable " +
            "INNER JOIN contactorol cr ON cr.id = opor.idsubclasificacionresponsable " +
            "LEFT JOIN Bitacora bic ON (bic.IdResponsable=con.idusuarioresponsable AND opor.IdOportunidad=bic.IdOportunidad) " + 
            "WHERE opor.idoportunidad=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            List<UsuariosEmpresasOportunidadesDetalle> resultado = new List<UsuariosEmpresasOportunidadesDetalle>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UsuariosEmpresasOportunidadesDetalle item = new UsuariosEmpresasOportunidadesDetalle();
                item.Oportunidades.Id = reader["idoportunidad"].ToString() == "" ? 0 : int.Parse(reader["idoportunidad"].ToString());
                item.Contactos.Id = reader["idcontacto"].ToString() == "" ? 0 : int.Parse(reader["idcontacto"].ToString());
                item.Contactos.Nombre = reader["Nombre"].ToString();
                item.Contactos.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.Contactos.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Contactos.Cargo = reader["cargo"].ToString();
                item.Contactos.Telefono = reader["Telefono"].ToString();
                item.Contactos.ContactoUsuario = int.Parse(reader["ContactoUsuario"].ToString());
                item.Bitacora.Id = reader["idbitacora"].ToString() == "" ? 0 : int.Parse(reader["idbitacora"].ToString());
                item.Bitacora.Notas = reader["Notas"].ToString();
                item.Contactos.IdUsuarioResponsable = reader["idusuarioresponsable"].ToString() == "" ? 0 : int.Parse(reader["idusuarioresponsable"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public string ContarResponsablesPorIdOportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT COUNT(1) " +
            "FROM Oportunidadesresponsables " +
            "WHERE idoportunidad=@id " +
            "GROUP BY idoportunidad");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.SelectString();
        }

        public List<Modelos> UsuariosQuehanVistoBitacora()
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
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        /// <summary>
        /// Guardar cada responsable asignado a la oportunidad
        /// </summary>
        public int Agregar(string idoportunidad, string idasignado, string clasificacion, string subclasificacion)
        {
            b.ExecuteCommandQuery("IF NOT EXISTS(SELECT (1) FROM oportunidadesresponsables WHERE idoportunidad=@idoportunidad AND idasignado=@idasignado) " +
            "   BEGIN" +
            "       INSERT INTO oportunidadesresponsables (idoportunidad,idasignado,idclasificacionresponsable,idsubclasificacionresponsable) " +
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