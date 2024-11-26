using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Models.Models;

namespace CRM.Models.Repository
{
    public class DocumentosASAEUsuariosRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Modelos> SeleccionarUsuariosAsignados(string iddocumento)
        {
            b.ExecuteCommandQuery("SELECT usu.nombre + ' ' + usu.apellidopaterno + ' ' + ISNULL(usu.apellidomaterno, '') AS usuario, " +
            "daup.Nombre AS posicion " +
            "FROM documentosasaeusuarios dau " +
            "INNER JOIN usuarios usu on usu.id = dau.idusuario " +
            "INNER JOIN documentosasaeusuarioposicion daup ON daup.id = dau.idclasificacion " +
            "WHERE dau.iddocumento = @iddocumento");
            b.AddParameter("@iddocumento", iddocumento, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Usuarios.Nombre = reader["usuario"].ToString();
                item.DocumentosASAE.Nombre = reader["posicion"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected int SeleccionarSiUsuarioEstaAsignadoADocumento(string iddocumento, string idusuario)
        {
            b.ExecuteCommandQuery("SELECT idusuario FROM documentosasaeusuarios WHERE iddocumento=@iddocumento AND idusuario=@idusuario");
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@iddocumento", iddocumento, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected int SeleccionarPosicion(string idusuario, string iddocumento)
        {
            b.ExecuteCommandQuery("SELECT idclasificacion FROM documentosasaeusuarios WHERE idusuario=@idusuario AND iddocumento=@iddocumento");
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@iddocumento", iddocumento, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected int Agregar(DocumentosASAEUsuarios items)
        {
            b.ExecuteCommandQuery("INSERT INTO documentosasaeusuarios (idusuario, iddocumento,idclasificacion) VALUES(@idusuario, @iddocumento,@idclasificacion)");
            b.AddParameter("@idusuario", items.IdUsuario, SqlDbType.Int);
            b.AddParameter("@iddocumento", items.IdDocumento, SqlDbType.Int);
            b.AddParameter("@idclasificacion", items.IdClasificacion, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Eliminar(string iddocumento)
        {
            b.ExecuteCommandQuery("DELETE FROM documentosasaeusuarios WHERE iddocumento=@iddocumento;");
            b.AddParameter("@iddocumento", iddocumento, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
