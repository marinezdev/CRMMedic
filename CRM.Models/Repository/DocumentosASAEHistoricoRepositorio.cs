using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Models.Models;

namespace CRM.Models.Repository
{
    public class DocumentosASAEHistoricoRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Modelos> SeleccionarPorIdDocumento(string iddocumento)
        {
            string consulta = "SELECT dah.iddocumento, dah.nombre AS nombrearchivo, dah.version, dah.fecha, dah.comentarios " +
            ", us.nombre + ' ' + us.apellidopaterno + ' ' + ISNULL(us.apellidomaterno, '') AS usuario " +
            ",daup.nombre AS posicion " +
            "FROM DocumentosASAEHistorico dah " +
            "INNER JOIN usuarios us ON us.id = dah.IdUsuario " +
            "INNER JOIN DocumentosASAEUsuarioPosicion daup ON daup.id = dah.IdUsuarioPosicion " +
            "WHERE dah.iddocumento = @iddocumento " +
            "ORDER BY dah.version";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@iddocumento", iddocumento, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.DocumentosASAEHistorico.Nombre  = reader["nombrearchivo"].ToString();
                item.DocumentosASAEHistorico.Version = int.Parse(reader["version"].ToString());
                item.DocumentosASAEHistorico.Fecha   = DateTime.Parse(reader["fecha"].ToString());
                item.DocumentosASAEHistorico.Comentarios = reader["comentarios"].ToString();
                item.Usuarios.Nombre                 = reader["usuario"].ToString();
                item.DocumentosASAE.Descripcion      = reader["posicion"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;

        }

        protected int SeleccionarUltimaVersionDocumento(string iddocumento)
        {
            string consulta = "" +
            "SELECT MAX(version) FROM documentosasaehistorico WHERE iddocumento=@iddocumento; ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@iddocumento", iddocumento, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected DocumentosASAEHistorico SeleccionarDetalleUltimaVersionDocumento(string iddocumento)
        {
            b.ExecuteCommandQuery("SELECT TOP 1 id, iddocumento, nombre, version FROM documentosasaehistorico WHERE iddocumento=@iddocumento ORDER BY version DESC");
            b.AddParameter("@iddocumento", iddocumento, SqlDbType.Int);
            DocumentosASAEHistorico resultado = new DocumentosASAEHistorico();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id  = int.Parse(reader["id"].ToString());
                resultado.IdDocumento = int.Parse(reader["iddocumento"].ToString());
                resultado.Nombre = reader["nombre"].ToString();
                resultado.Version = int.Parse(reader["version"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(DocumentosASAEHistorico items)
        {
            b.ExecuteCommandQuery("DECLARE @ultimaversion INT; " +
            "IF NOT EXISTS (SELECT version FROM documentosasaehistorico WHERE iddocumento=@iddocumento) " +
            "BEGIN " +
            "INSERT INTO documentosasaehistorico (iddocumento, nombre, version, idusuario, idusuarioposicion,comentarios) " +
            "VALUES(@iddocumento, @nombre, 1, @idusuario, @idusuarioposicion,@comentarios); " +
            "END " +
            "ELSE " +
            "BEGIN " +
            "SELECT @ultimaversion=MAX(version) FROM documentosasaehistorico WHERE iddocumento=@iddocumento; " +
            "INSERT INTO documentosasaehistorico (iddocumento, nombre, version, idusuario, idusuarioposicion,comentarios) " +
            "VALUES(@iddocumento, @nombre, @ultimaversion+1, @idusuario, @idusuarioposicion,@comentarios); " +
            "END ");
            b.AddParameter("@iddocumento", items.IdDocumento, SqlDbType.Int);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar, 150);
            b.AddParameter("@idusuario", items.IdUsuario, SqlDbType.Int);
            b.AddParameter("@idusuarioposicion", items.IdUsuarioPosicion, SqlDbType.Int);
            b.AddParameter("@comentarios", items.Comentarios, SqlDbType.NVarChar, 350);
            return b.InsertUpdateDelete();
        }

        protected int AgregarAutorizacion(DocumentosASAEHistorico items)
        {
            b.ExecuteCommandQuery("INSERT INTO documentosasaehistorico (iddocumento, nombre, version, idusuario, idusuarioposicion, comentarios) " +
            "VALUES(@iddocumento, @nombre, @version, @idusuario, @idusuarioposicion, @comentarios);");
            b.AddParameter("@iddocumento", items.IdDocumento, SqlDbType.Int);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar, 150);
            b.AddParameter("@version", items.Version, SqlDbType.Int);
            b.AddParameter("@idusuario", items.IdUsuario, SqlDbType.Int);
            b.AddParameter("@idusuarioposicion", items.IdUsuarioPosicion, SqlDbType.Int);            
            b.AddParameter("@comentarios", items.Comentarios, SqlDbType.NVarChar, 350);
            return b.InsertUpdateDelete();

        }

        protected int Modificar(DocumentosASAEHistorico items)
        {
            string consulta = "UPDATE documentosasaehistorico SET nombre=@nombre WHERE version=@version AND iddocumento=@iddocumento";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@iddocumento", items.IdDocumento, SqlDbType.Int);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar, 150);
            b.AddParameter("@version", items.Version, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int ModificarAnteriorAlaUltimaVersion(DocumentosASAEHistorico items)
        {
            b.ExecuteCommandQuery("UPDATE documentosasaehistorico SET nombre=@nombre WHERE iddocumento=@iddocumento AND version=@version");
            b.AddParameter("@iddocumento", items.IdDocumento, SqlDbType.Int);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar, 150);
            b.AddParameter("@version", items.Version, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Eliminar(string iddocumento)
        {
            b.ExecuteCommandQuery("DELETE FROM documentosasaehistorico WHERE iddocumento = @iddocumento;");
            b.AddParameter("@iddocumento", iddocumento, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
