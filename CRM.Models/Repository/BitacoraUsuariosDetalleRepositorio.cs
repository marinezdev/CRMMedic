using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class BitacoraUsuariosDetalleRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected BitacoraUsuariosDetalle SeleccionarPorId(string id)
        {
            string consulta = "SELECT id, notas, creado, idtipoactividad " +
                "FROM bitacorausuariosdetalle " +
                "WHERE id=@id";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            BitacoraUsuariosDetalle resultado = new BitacoraUsuariosDetalle();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = reader["Id"].ToString() == "" ? 0 : int.Parse(reader["Id"].ToString());
                resultado.Creado = DateTime.Parse(reader["creado"].ToString());
                resultado.Notas = reader["Notas"].ToString();
                //resultado.IdTipoActividad = int.Parse(reader["IdTipoActividad"].ToString());
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<BitacoraUsuariosDetalle> SeleccionarPorIdOportunidad(string idoportunidad)
        {
            string consulta = "SELECT " +
            "bud.id, bud.idbitacora, bud.idusuario, bud.fecha, bud.notas, bud.creado,  " +
            "usu.Nombre, usu.ApellidoPaterno, usu.ApellidoMaterno " +
            "FROM bitacora bic " +
            "LEFT JOIN bitacorausuariosdetalle bud ON bud.idbitacora = bic.id " +
            "INNER JOIN usuarios usu ON usu.Id = bic.IdResponsable " +
            "WHERE bic.idoportunidad = @idoportunidad";

            string consulta1 = "SELECT * FROM BitacoraUsuariosDetalle WHERE IdBitacora IN (SELECT idbitacora FROM bitacora WHERE idoportunidad=@idoportunidad)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<BitacoraUsuariosDetalle> resultado = new List<BitacoraUsuariosDetalle>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                BitacoraUsuariosDetalle item = new BitacoraUsuariosDetalle();
                item.Id                     = reader["Id"].ToString() == "" ? 0 : int.Parse(reader["Id"].ToString());
                item.IdBitacora             = reader["IdBitacora"].ToString() == "" ? 0 : int.Parse(reader["IdBitacora"].ToString());
                item.IdUsuario              = reader["IdUsuario"].ToString() == "" ? 0 :int.Parse(reader["IdUsuario"].ToString());
                //item.IdTipoActividad        = reader["IdTipoActividad"].ToString() == "" ? 0 : int.Parse(reader["IdTipoActividad"].ToString());
                //item.TipoActividadNombre    = reader["TipoActividadNombre"].ToString();
                item.Fecha                  = reader["Creado"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["Creado"].ToString());
                item.Notas                  = reader["Notas"].ToString();
                item.Nombre                 = reader["Nombre"].ToString() + " " + reader["ApellidoPaterno"].ToString() + " " + reader["ApellidoMaterno"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<BitacoraUsuariosDetalle> SeleccionarPorIdUsuarioIdOportunidad(string idusuario, string idoportunidad)
        {
            string consulta = "SELECT " +
            "bud.id, bud.idbitacora, bud.idusuario, bud.notas, bud.creado, usu.nombre + ' ' + usu.apellidopaterno + ' ' + usu.apellidomaterno AS nombre  " +
            "FROM bitacora bic " +
            "LEFT JOIN bitacorausuariosdetalle bud ON bud.idbitacora = bic.id " +
            "INNER JOIN usuarios usu ON usu.id=bic.idresponsable " + 
            "WHERE bud.idusuario = @idusuario " +
            "AND bic.idoportunidad = @idoportunidad";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<BitacoraUsuariosDetalle> resultado = new List<BitacoraUsuariosDetalle>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                BitacoraUsuariosDetalle item = new BitacoraUsuariosDetalle();
                item.Id                  = reader["Id"].ToString() == "" ? 0 : int.Parse(reader["Id"].ToString());
                item.IdBitacora          = reader["IdBitacora"].ToString() == "" ? 0 : int.Parse(reader["IdBitacora"].ToString());
                item.IdUsuario           = reader["IdUsuario"].ToString() == "" ? 0 : int.Parse(reader["IdUsuario"].ToString());
                //item.IdTipoActividad     = reader["IdTipoActividad"].ToString() == "" ? 0 : int.Parse(reader["IdTipoActividad"].ToString());
                item.TipoActividadNombre = reader["nombre"].ToString();
                item.Fecha               = reader["Creado"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["Creado"].ToString());
                item.Notas               = reader["Notas"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int SeleccionarSiOportunidadTieneUsuariosResponsablesAsignados(string idoportunidad)
        {
            string consulta = "SELECT IIF(count(bi.id) > 0, '1','0') " +
                "FROM bitacorausuarios bi " +
                //"INNER JOIN ContactosActividades ca ON ca.IdBitacora = bi.id " +
                "WHERE bi.IdOportunidad = @idoportunidad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected int Agregar(BitacoraUsuariosDetalle items)
        {
            string consulta = "INSERT INTO bitacorausuariosdetalle (idbitacora,idusuario,notas) " +
            "VALUES(@idbitacora,@idusuario,@notas)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", items.IdUsuario, SqlDbType.Int);
            b.AddParameter("@idbitacora", items.IdBitacora, SqlDbType.Int);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string id, string idtipoactividad, string notas)
        {
            string consulta = "UPDATE bitacorausuariosdetalle SET ";
            //if (idtipoactividad != "0")
            //    consulta += "idtipoactividad=@idtipoactividad, ";
            consulta += " notas=@notas WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            //if (idtipoactividad != "0")
            //    b.AddParameter("@idtipoactividad", idtipoactividad, SqlDbType.Int);
            b.AddParameter("@notas", notas, SqlDbType.NVarChar);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}