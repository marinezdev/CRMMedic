using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class BitacoraRespositorio 
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        /// <summary>
        /// Obtiene todas las oportuniaddes asignadas al usuario para darle seguimiento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UsuariosEmpresasOportunidadesDetalle> SeleccionarPorUsuario(string id) //TODO CHECAR LA CONSULTA PARA CAMBIARLA
        {
            string consultaanterior = "SELECT bi.Id AS IdBitacora, " +
            "bi.Notas, bi.Estado, " +
            "CASE bi.Estado " +
            "   WHEN 1 THEN 'Asignado' " +
            "   WHEN 2 THEN 'Visto' " +
            "   WHEN 3 THEN 'En Proceso' " +
            "   WHEN 4 THEN 'Terminado' " +
            "   WHEN 5 THEN 'Cancelado' " +
            " END AS EstadoNombre, " +
            "opo.Id AS IdOportunidad, " +
            "opo.Nombre AS Oportunidad " +
            "FROM OportunidadesEmpresasContactosUsuarios oecu " +
            "INNER JOIN bitacora bi ON bi.IdOportunidad = oecu.IdOportunidad " +
            "INNER JOIN oportunidades opo ON opo.id = oecu.IdOportunidad " +
            "WHERE bi.idresponsable = @id";

            string consulta = "" +
            "SELECT opor.IdOportunidad, " +
            "opo.nombre, " +
            "CASE opo.Etapa " +
            "    WHEN 1 THEN 'Asignado' " +
            "    WHEN 2 THEN 'Visto' " +
            "    WHEN 3 THEN 'En Proceso' " +
            "    WHEN 4 THEN 'Terminado' " +
            "    WHEN 5 THEN 'Cancelado' " +
            "    END AS EtapaNombre, " +
            "opo.cierre, " +
            "bic.id AS IdBitacora, " +
            "opor.idasignado " +
            "FROM OportunidadesResponsables opor " +
            "INNER JOIN oportunidades opo ON opo.id=opor.IdOportunidad " +
            "LEFT JOIN bitacora bic ON bic.idresponsable=opor.IdAsignado " +
            "WHERE opor.IdAsignado IN (" +
            "        SELECT con.Id AS IdContacto " +
            "        FROM usuarios usu " +
            "        LEFT JOIN contactos con ON con.IdUsuarioResponsable=usu.id " +
            "        WHERE usu.id=@id)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            List<UsuariosEmpresasOportunidadesDetalle> resultado = new List<UsuariosEmpresasOportunidadesDetalle>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UsuariosEmpresasOportunidadesDetalle item = new UsuariosEmpresasOportunidadesDetalle();
                item.Oportunidades.Id                       = int.Parse(reader["IdOportunidad"].ToString());
                item.Oportunidades.Nombre                   = reader["Nombre"].ToString();
                item.Oportunidades.EtapaNombre              = reader["EtapaNombre"].ToString();
                item.Oportunidades.Cierre                   = DateTime.Parse(reader["Cierre"].ToString());
                item.Bitacora.Id                            = reader["IdBitacora"].ToString() == "" ? 0 : int.Parse(reader["IdBitacora"].ToString());
                item.OportunidadesResponsables.IdAsignado   = int.Parse(reader["IdAsignado"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        /// <summary>
        /// Obtiene bitacora por idresponsable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ContactosActividades> SeleccionarPorId(string idcon, string idbit)
        {
            string anterior = "SELECT id, notas, fecha, estado FROM bitacora WHERE idresponsable=@id AND id=@idbit";

            string consulta = "SELECT ca.idbitacora, ca.idcontacto,ca.tipoactividad, ca.fecha, ca.notas, " +
            "CASE " +
            "WHEN ca.tipoactividad=1 THEN 'Tarea' " +
            "WHEN ca.tipoactividad=2 THEN 'Llamada' " +
            "WHEN ca.tipoactividad=3 THEN 'Correo Electrónico' " +
            "WHEN ca.tipoactividad=4 THEN 'Carta' " +
            "WHEN ca.tipoactividad=5 THEN 'Cita' " +
            "END AS TipoActividadNombre " +
            "FROM bitacora bic " +
            "INNER JOIN contactosactividades ca ON ca.idbitacora = bic.id " +
            "WHERE bic.idresponsable = @id " +
            "AND bic.id = @idbit";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", idcon, SqlDbType.Int);
            b.AddParameter("@idbit", idbit, SqlDbType.Int);
            List<ContactosActividades> resultado = new List<ContactosActividades>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ContactosActividades item = new ContactosActividades();
                item.TipoActividad  = int.Parse(reader["TipoActividad"].ToString());
                item.Fecha          = DateTime.Parse(reader["Fecha"].ToString());
                item.Notas          = reader["Notas"].ToString();
                item.TipoActividadNombre = reader["TipoActividadNombre"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        /// <summary>
        /// Obtiene bitacora por id de bitacora
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Bitacora SeleccionarPorIdBitacora(string id)
        {
            b.ExecuteCommandQuery("SELECT id, notas, fecha, estado FROM bitacora WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            Bitacora resultado = new Bitacora();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Notas = reader["Notas"].ToString();
                resultado.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                resultado.Estado = int.Parse(reader["Estado"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }


        public List<ArchivosBitacora> SeleccionarArchivos(string id)
        {
            b.ExecuteCommandQuery("SELECT Nombre FROm archivosbitacora WHERE idbitacora=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            List<ArchivosBitacora> resultado = new List<ArchivosBitacora>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ArchivosBitacora item = new ArchivosBitacora();
                item.Nombre = reader["Nombre"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public string SeleccionarPendientesEstado1(string id)
        {
            b.ExecuteCommandQuery("SELECT COUNT(*) FROM bitacora WHERE estado=1 AND idresponsable=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.SelectString();
        }

        public int Agregar(Bitacora items)
        {
            b.ExecuteCommandQuery("" +
            "DECLARE @obtenido INT; " +
            "IF NOT EXISTS (SELECT id FROM bitacora WHERE IdResponsable=@idresponsable AND IdOportunidad=@idoportunidad) " +
            "   BEGIN " +
            "       INSERT INTO bitacora (Estado,IdResponsable,IdOportunidad) " +
            "       VALUES(1,@idresponsable,@idoportunidad); " +
            "       SET @obtenido=@@IDENTITY; " +
            "       INSERT INTO etapasbitacora (idbitacora, estado) VALUES(@obtenido, 1); " +
            "       SELECT @obtenido; " +
            "   END " +
            "ELSE " +
            "   SELECT 0");
            b.AddParameter("@idresponsable", items.IdResponsable, SqlDbType.Int);
            b.AddParameter("@idoportunidad", items.IdOportunidad, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }

        public int Modificar(Bitacora items)
        {
            b.ExecuteCommandQuery("UPDATE bitacora SET Estado=@estado WHERE id=@id; " +
            "INSERT INTO etapasbitacora (idbitacora, estado) VALUES(@id, @estado);  ");
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int ModificarBitacoraEstado2(string idbitacora)
        {
            string consulta = "IF NOT EXISTS(SELECT 1 FROM bitacora WHERE id=@id AND Estado=2) " +
                "BEGIN " +
                "UPDATE bitacora SET Estado=2 WHERE id=@id; " +
                "INSERT INTO etapasbitacora (idbitacora, estado) VALUES(@id, 2); " +
                "END ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", idbitacora, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int RevisoAsunto(string id)
        {
            b.ExecuteCommandQuery("IF EXISTS((SELECT Estado FROM bitacora WHERE id=@id AND estado=1 OR estado=0)) " +
            "BEGIN " +
            "   UPDATE bitacora SET Estado=2 WHERE id=@id; " +
            "END " +
            "ELSE " +
            "   SELECT '1'");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}