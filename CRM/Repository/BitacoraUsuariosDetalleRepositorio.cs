using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class BitacoraUsuariosDetalleRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public List<BitacoraUsuariosDetalle> SeleccionarPorIdOportunidad(string idoportunidad)
        {
            string consulta = "SELECT " +
            "bud.id, bud.idbitacora, bud.idusuario, bud.idtipoactividad, bud.fecha, bud.notas, " +
            "usu.Nombre, usu.ApellidoPaterno, usu.ApellidoMaterno " +
            "FROM bitacorausuarios bu " +
            "LEFT JOIN bitacorausuariosdetalle bud ON bud.idbitacora = bu.id " +
            "INNER JOIN usuarios usu ON usu.Id=bu.IdResponsable " +
            "WHERE bu.idoportunidad = @idoportunidad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<BitacoraUsuariosDetalle> resultado = new List<BitacoraUsuariosDetalle>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                BitacoraUsuariosDetalle item = new BitacoraUsuariosDetalle();
                item.Id = int.Parse(reader["Id"].ToString());
                item.IdBitacora = int.Parse(reader["IdBitacora"].ToString());
                item.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
                item.IdTipoActividad = int.Parse(reader["IdTipoActividad"].ToString());
                item.TipoActividadNombre = Utilerias.Funciones.TipoActividadNombre(reader["IdTipoActividad"].ToString());
                item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Notas = reader["Notas"].ToString();
                item.Nombre = reader["Nombre"].ToString() + " " + reader["ApellidoPaterno"].ToString() + " " + reader["ApellidoMaterno"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<BitacoraUsuariosDetalle> SeleccionarPorIdUsuarioIdOportunidad(string idusuario, string idoportunidad)
        {
            string consulta = "SELECT " +
            "bud.id, bud.idbitacora, bud.idusuario, bud.idtipoactividad, bud.fecha, bud.notas " +
            "FROM bitacorausuarios bu " +
            "LEFT JOIN bitacorausuariosdetalle bud ON bud.idbitacora = bu.id " +
            "WHERE bu.idresponsable = @idusuario AND bu.idoportunidad = @idoportunidad";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<BitacoraUsuariosDetalle> resultado = new List<BitacoraUsuariosDetalle>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                BitacoraUsuariosDetalle item = new BitacoraUsuariosDetalle();
                item.Id = int.Parse(reader["Id"].ToString());
                item.IdBitacora = int.Parse(reader["IdBitacora"].ToString());
                item.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
                item.IdTipoActividad = int.Parse(reader["IdTipoActividad"].ToString());
                item.TipoActividadNombre = Utilerias.Funciones.TipoActividadNombre(reader["IdTipoActividad"].ToString());
                item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Notas = reader["Notas"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Agregar(BitacoraUsuariosDetalle items)
        {
            string consulta = "DECLARE @idbitacora INT; " +
            "SELECT @idbitacora=Id FROM bitacorausuarios WHERE idresponsable = @idusuario; " + 
            "INSERT INTO bitacorausuariosdetalle (idbitacora, idusuario, idtipoactividad, fecha, notas) " +
            "VALUES(@idbitacora, @idusuario, @idtipoactividad, @fecha, @notas)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", items.IdUsuario, SqlDbType.Int);
            b.AddParameter("@idtipoactividad", items.IdTipoActividad, SqlDbType.Int);
            b.AddParameter("@fecha", items.Fecha, SqlDbType.DateTime);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }
    }
}