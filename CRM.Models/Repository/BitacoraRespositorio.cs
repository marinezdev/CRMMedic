using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class BitacoraRespositorio 
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        /// <summary>
        /// Obtiene todas las oportuniaddes asignadas al usuario para darle seguimiento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected List<UsuariosEmpresasOportunidadesDetalle> SeleccionarPorUsuario(string id) //TODO CHECAR LA CONSULTA PARA CAMBIARLA
        {
            

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
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Obtiene bitacora por idresponsable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected List<ContactosActividades> SeleccionarPorId(string idcon, string idbit)
        {
            string anterior = "SELECT id, notas, fecha, estado FROM bitacora WHERE idresponsable=@id AND id=@idbit";

            string consulta = "SELECT ca.idbitacora, ca.idcontacto, ca.notas " +            
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
                //item.TipoActividad  = int.Parse(reader["TipoActividad"].ToString());
                //item.Fecha          = DateTime.Parse(reader["Fecha"].ToString());
                item.Notas          = reader["Notas"].ToString();
                //item.TipoActividadNombre = reader["TipoActividadNombre"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Obtiene bitacora por id de bitacora
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected Bitacora SeleccionarPorIdBitacora(string id)
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
            b.CloseConnection();
            return resultado;
        }

        protected List<Bitacoras> SeleccionarBitacoraPorOportunidadPorUsuario(string idusuario, string idoportunidad)
        {
            string consulta = "SELECT " +
            "bud.id, bud.idbitacora, bud.notas, bud.Creado, " +
            "usu.Nombre + ' ' + ISNULL(usu.ApellidoPaterno,'') + ' ' + ISNULL(usu.ApellidoMaterno,'') AS NombreUsuario " +
            "FROM bitacora bic " +
            "LEFT JOIN bitacorausuariosdetalle bud ON bud.idbitacora = bic.id " +
            "INNER JOIN usuarios usu ON usu.id=bic.IdResponsable " +
            "WHERE bud.IdUsuario = @idusuario " +
            "AND bic.IdOportunidad = @idoportunidad";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<Bitacoras> resultado = new List<Bitacoras>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Bitacoras item = new Bitacoras();
                item.BitacoraUsuariosDetalle.Id = int.Parse(reader["id"].ToString());
                item.BitacoraUsuariosDetalle.IdBitacora = int.Parse(reader["idbitacora"].ToString());
                item.BitacoraUsuariosDetalle.Notas = reader["notas"].ToString();
                item.BitacoraUsuariosDetalle.Creado = DateTime.Parse(reader["creado"].ToString());
                item.Usuarios.Nombre = reader["nombreusuario"].ToString();
                //item.Generico.Nombre = reader["tipoactividad"].ToString();

                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<Bitacoras> SeleccionarBitacoraPorOportunidad(string idoportunidad, string idconfiguracion)
        {
            string consulta = "SELECT " +
            "bud.id, bud.idbitacora, bud.notas, bud.Creado, " +
            "usu.Nombre + ' ' + ISNULL(usu.ApellidoPaterno,'') + ' ' + ISNULL(usu.ApellidoMaterno,'') AS NombreUsuario " +
            "FROM bitacora bic " +
            "LEFT JOIN bitacorausuariosdetalle bud ON bud.idbitacora = bic.id " +
            "INNER JOIN usuarios usu ON usu.id=bic.IdResponsable " +
            "WHERE bic.IdOportunidad = @idoportunidad " +
            "--AND bud.id IS NOT NULL";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<Bitacoras> resultado = new List<Bitacoras>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Bitacoras item = new Bitacoras();
                //item.BitacoraUsuariosDetalle.Id         = int.Parse(reader["id"].ToString());
                //item.BitacoraUsuariosDetalle.IdBitacora = int.Parse(reader["idbitacora"].ToString());
                item.BitacoraUsuariosDetalle.Notas      = reader["notas"].ToString();
                item.BitacoraUsuariosDetalle.Creado     = reader["creado"].ToString() != "" ? DateTime.Parse(reader["creado"].ToString()) : DateTime.Parse("01/01/1900");
                item.Usuarios.Nombre                    = reader["nombreusuario"].ToString();
                //item.Generico.Nombre = reader["tipoactividad"].ToString();

                resultado.Add(item);
            }

            if (idconfiguracion == "2")
            {
                string consulta2 = "SELECT usu.Nombre, usu.apellidopaterno, ISNULL(usu.ApellidoMaterno, '') AS ApellidoMaterno " +
                "FROM oportunidades opor " +
                "INNER JOIN oecu ON oecu.idoportunidad=opor.id " +
                "INNER JOIN usuarios usu ON usu.Id = oecu.IdUsuario " +
                "WHERE opor.id = @id";

                b.ExecuteCommandQuery(consulta2);
                b.AddParameter("@id", idoportunidad, SqlDbType.Int);
                var reader2 = b.ExecuteReader();
                while (reader2.Read())
                {
                    Bitacoras item2 = new Bitacoras();
                    //item2.BitacoraUsuariosDetalle.Id = 0;
                    //item2.BitacoraUsuariosDetalle.IdBitacora = 0;
                    //item2.BitacoraUsuariosDetalle.Notas = "";
                    item2.BitacoraUsuariosDetalle.Creado = DateTime.Parse("1900/01/01");
                    item2.Usuarios.Nombre = reader2["nombre"].ToString() + " " + reader2["apellidopaterno"].ToString() + " " + reader2["apellidomaterno"].ToString();

                    resultado.Add(item2);
                }
            }

            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int SeleccionarSiOportunidadTieneResponsablesAsignados(string idoportunidad)
        {
            string consulta = "SELECT IIF(count(id) > 0, '1','0') FROM BitacoraUsuarios WHERE idoportunidad=@idoportunidad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected List<ArchivosBitacora> SeleccionarArchivos(string id)
        {
            b.ExecuteCommandQuery("SELECT Nombre, Fecha FROM archivosbitacora WHERE idbitacora=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            List<ArchivosBitacora> resultado = new List<ArchivosBitacora>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ArchivosBitacora item = new ArchivosBitacora();
                item.Nombre = reader["Nombre"].ToString();
                item.Fecha = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900-01-01") : DateTime.Parse(reader["Fecha"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected string SeleccionarPendientesEstado1(string id)
        {
            b.ExecuteCommandQuery("SELECT COUNT(*) FROM bitacora WHERE estado=1 AND idresponsable=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.SelectString();
        }

        protected List<Usuarios> SeleccionarResponsablesPorOportunidad(string idoportunidad)
        {
            string consulta = "SELECT usu.id, usu.nombre + ' ' + ISNULL(usu.ApellidoPaterno,'') + ' ' + ISNULL(usu.ApellidoMaterno,'') AS Nombre " +
            "FROM bitacora bic " +
            "INNER JOIN usuarios usu ON usu.id = bic.IdResponsable " +
            "WHERE bic.IdOportunidad = @idoportunidad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(Bitacora items)
        {
            string consulta = "DECLARE @obtenido INT; " +
            "IF NOT EXISTS (SELECT id FROM bitacora WHERE IdResponsable=@idresponsable AND IdOportunidad=@idoportunidad) " +
            "   BEGIN " +
            "       INSERT INTO bitacora (IdResponsable,IdOportunidad) " +
            "       VALUES(@idresponsable,@idoportunidad); " +
            "       SELECT @@IDENTITY; " +
            "   END " +
            "ELSE " +
            "   BEGIN " +
            "       SELECT id FROM bitacora WHERE idresponsable=@idresponsable AND idoportunidad=@idoportunidad " +
            "   END";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idresponsable", items.IdResponsable, SqlDbType.Int);
            b.AddParameter("@idoportunidad", items.IdOportunidad, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }

        protected int Modificar(Bitacora items)
        {
            b.ExecuteCommandQuery("UPDATE bitacora SET Estado=@estado WHERE id=@id; " +
            "INSERT INTO etapasbitacora (idbitacora, estado) VALUES(@id, @estado);  ");
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int ModificarBitacoraEstado2(string idbitacora)
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

        protected int RevisoAsunto(string id)
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

        protected int EliminarUsuarioDeBitacora(string idusuario, string idoportunidad)
        {
            string consulta = "DELETE FROM bitacora WHERE idoportunidad=@idoportunidad and IdResponsable=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}