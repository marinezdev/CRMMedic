using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Models.Models;

namespace CRM.Models.Repository
{
    public class DocumentosASAERepository
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<DocumentosASAE> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre, idusuario, fecha, clasificacion, subclasificacion, descripcion, version, vigencia, versionusuarios, fechaoficial FROM documentosasae");
            List<DocumentosASAE> resultado = new List<DocumentosASAE>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                DocumentosASAE item = new DocumentosASAE();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                item.IdUsuario = int.Parse(reader["idusuario"].ToString());
                item.Fecha = DateTime.Parse(reader["fecha"].ToString());
                item.Clasificacion = int.Parse(reader["clasificacion"].ToString());
                item.SubClasificacion = reader["subclasificacion"].ToString() == "" ? 0 : int.Parse(reader["subclasificacion"].ToString());
                item.Descripcion = reader["descripcion"].ToString();
                item.Version = reader["version"].ToString();
                item.Vigencia = bool.Parse(reader["vigencia"].ToString());
                item.VersionUsuarios = reader["versionusuarios"].ToString();
                item.FechaOficial = DateTime.Parse(reader["fechaoficial"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<DocumentosASAE> SeleccionarPorClasificacion(string idclasificacion)
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre, idusuario, fecha, clasificacion, subclasificacion, descripcion, version, vigencia, versionusuarios, fechaoficial FROM documentosasae WHERE clasificacion=@idclasificacion");
            b.AddParameter("@idclasificacion", idclasificacion, SqlDbType.Int);
            List<DocumentosASAE> resultado = new List<DocumentosASAE>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                DocumentosASAE item = new DocumentosASAE();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                item.IdUsuario = int.Parse(reader["idusuario"].ToString());
                item.Fecha = DateTime.Parse(reader["fecha"].ToString());
                item.Clasificacion = int.Parse(reader["clasificacion"].ToString());
                item.SubClasificacion = reader["subclasificacion"].ToString() == "" ? 0 : int.Parse(reader["subclasificacion"].ToString());
                item.Descripcion = reader["descripcion"].ToString();
                item.Version = reader["version"].ToString();
                item.Vigencia = bool.Parse(reader["vigencia"].ToString());
                item.VersionUsuarios = reader["versionusuarios"].ToString();
                item.FechaOficial = DateTime.Parse(reader["fechaoficial"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<DocumentosASAE> SeleccionarPorClasificacionSubClasificacion(string idclasificacion, string idsubclasificacion)
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre, idusuario, fecha, clasificacion, subclasificacion, descripcion, version, vigencia, versionusuarios, fechaoficial " +
            "FROM documentosasae WHERE clasificacion=@idclasificacion ANd subclasificacion=@idsubclasificacion");
            b.AddParameter("@idclasificacion", idclasificacion, SqlDbType.Int);
            b.AddParameter("@idsubclasificacion", idsubclasificacion, SqlDbType.Int);
            List<DocumentosASAE> resultado = new List<DocumentosASAE>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                DocumentosASAE item = new DocumentosASAE();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                item.IdUsuario = int.Parse(reader["idusuario"].ToString());
                item.Fecha = DateTime.Parse(reader["fecha"].ToString());
                item.Clasificacion = int.Parse(reader["clasificacion"].ToString());
                item.SubClasificacion = reader["subclasificacion"].ToString() == "" ? 0 : int.Parse(reader["subclasificacion"].ToString());
                item.Descripcion = reader["descripcion"].ToString();
                item.Version = reader["version"].ToString();
                item.Vigencia = bool.Parse(reader["vigencia"].ToString());
                item.VersionUsuarios = reader["versionusuarios"].ToString();
                item.FechaOficial = DateTime.Parse(reader["fechaoficial"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected DocumentosASAE SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, nombre, idusuario, fecha, clasificacion, subclasificacion, descripcion, version, vigencia, versionusuarios, fechaoficial FROM documentosasae WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            DocumentosASAE resultado = new DocumentosASAE();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.Nombre = reader["nombre"].ToString();
                resultado.IdUsuario = int.Parse(reader["idusuario"].ToString());
                resultado.Fecha = DateTime.Parse(reader["fecha"].ToString());
                resultado.Clasificacion = int.Parse(reader["clasificacion"].ToString());
                resultado.SubClasificacion = reader["subclasificacion"].ToString() == "" ? 0 : int.Parse(reader["subclasificacion"].ToString());
                resultado.Descripcion = reader["descripcion"].ToString();
                resultado.Version = reader["version"].ToString();
                resultado.Vigencia = bool.Parse(reader["vigencia"].ToString());
                resultado.VersionUsuarios = reader["versionusuarios"].ToString();
                resultado.FechaOficial = DateTime.Parse(reader["fechaoficial"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Busca si el usuario esta asignado en la modificación del documento
        /// </summary>
        /// <param name="iddocumento"></param>
        /// <param name="idusuario"></param>
        /// <returns></returns>
        protected int SeleccionarSiUsuarioEsCreadorDocumento(string iddocumento, string idusuario)
        {
            string consulta = "SELECT idusuario FROM documentosasae WHERE id=@iddocumento AND idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@iddocumento", iddocumento, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected List<DocumentosASAE> SeleccionarPadresDisponibles()
        {
            b.ExecuteCommandQuery("SELECT DISTINCT dac.id, dac.Nombre AS clasificacion FROM documentosasae da INNER JOIN documentosasaeclasificacion dac ON dac.id = da.clasificacion " +
            "ORDER BY dac.id");
            List<DocumentosASAE> resultado = new List<DocumentosASAE>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                DocumentosASAE items = new DocumentosASAE();
                items.Id = int.Parse(reader["id"].ToString());
                items.Nombre = reader["clasificacion"].ToString();
                resultado.Add(items);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<DocumentosASAE> SeleccionarHijos()
        {
            b.ExecuteCommandQuery("SELECT dac.id AS idclasificacion,da.id, da.nombre " +
            "FROM documentosasae da " +
            "INNER JOIN documentosasaeclasificacion dac ON dac.id = da.clasificacion " +
            "ORDER BY dac.id");
            List<DocumentosASAE> resultado = new List<DocumentosASAE>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                DocumentosASAE item = new DocumentosASAE();
                item.IdUsuario = int.Parse(reader["idclasificacion"].ToString());
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();                
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<DocumentosASAE> SeleccionarBusquedaPorNombre(string nombre)
        {
            string consulta = "SELECT da.id, da.Nombre, usu.Nombre + ' ' + usu.apellidopaterno + ' ' + ISNULL(usu.apellidomaterno,'') AS SubidoPor, Fecha " +
            "FROM documentosasae da " +
            "INNER JOIN usuarios usu ON usu.id = da.idusuario " +
            "WHERE da.nombre LIKE @nombre " +
            "OR usu.nombre LIKE @nombre ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", "%" + nombre + "%", SqlDbType.NVarChar);
            List<DocumentosASAE> resultado = new List<DocumentosASAE>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                DocumentosASAE item = new DocumentosASAE();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                item.Fecha = DateTime.Parse(reader["fecha"].ToString());
                item.Descripcion = reader["subidopor"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;


        }


        protected int Agregar(DocumentosASAE items)
        {
            string consulta = "INSERT INTO documentosasae (nombre, idusuario,clasificacion, descripcion, version, vigencia, versionusuarios, fechaoficial ";
            if (items.SubClasificacion > 0)
                consulta += ",subclasificacion";
            consulta += ") " +
            "VALUES(@nombre, @idusuario,@clasificacion, @descripcion, @version, @vigencia,@versionusuarios, @fechaoficial";
            if (items.SubClasificacion > 0)
                consulta += ",@subclasificacion";
            consulta += ");" +
            "SELECT @@IDENTITY";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar, 250);
            b.AddParameter("@idusuario", items.IdUsuario, SqlDbType.Int);
            b.AddParameter("@clasificacion", items.Clasificacion, SqlDbType.Int);
            b.AddParameter("@descripcion", items.Descripcion, SqlDbType.NVarChar, 500);
            b.AddParameter("@version", items.Version, SqlDbType.NVarChar, 50);
            b.AddParameter("@vigencia", items.Vigencia, SqlDbType.Bit);
            b.AddParameter("@versionusuarios", items.VersionUsuarios, SqlDbType.NVarChar, 50);
            b.AddParameter("@fechaoficial", items.FechaOficial, SqlDbType.DateTime);
            if (items.SubClasificacion > 0)
                b.AddParameter("@subclasificacion", items.SubClasificacion, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }

        protected int Modificar(DocumentosASAE items)
        {
            b.ExecuteCommandQuery("UPDATE documentosasae SET clasificacion=@clasificacion, descripcion=@descripcion, version=@version, vigencia=@vigencia, versionusuarios=@versionusuarios, fechaoficial=@fechaoficial, subclasificacion=@subclasificacion WHERE id=@id");
            b.AddParameter("@clasificacion", items.Clasificacion, SqlDbType.Int);
            b.AddParameter("@descripcion", items.Descripcion, SqlDbType.NVarChar, 500);
            b.AddParameter("@version", items.Version, SqlDbType.NVarChar, 50);
            b.AddParameter("@vigencia", items.Vigencia, SqlDbType.Bit);
            b.AddParameter("@versionusuarios", items.VersionUsuarios, SqlDbType.NVarChar, 50);
            b.AddParameter("@fechaoficial", items.FechaOficial, SqlDbType.DateTime);
            b.AddParameter("@subclasificacion", items.SubClasificacion, SqlDbType.Int);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int ModificarVersion(DocumentosASAE items)
        {
            b.ExecuteCommandQuery("UPDATE documentosasae SET version=@version, fecha=getdate() WHERE id=@iddocumento");
            b.AddParameter("@iddocumento", items.Id, SqlDbType.Int);
            b.AddParameter("@version", items.Version, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Eliminar(string idarchivo)
        {
            b.ExecuteCommandQuery("DELETE FROM documentosasae WHERE id=@iddocumento; ");
            b.AddParameter("@iddocumento", idarchivo, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
