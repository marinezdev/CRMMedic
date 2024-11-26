using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models.Models;

namespace CRM.Models.Repository
{
    public class DocumentosASAETipoArchivo
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Models.DocumentosASAETipoArchivo> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT id, extensiones, tammaxpermitido, permitir FROM documentosasaetipoarchivo");
            List<Models.DocumentosASAETipoArchivo> resultado = new List<Models.DocumentosASAETipoArchivo>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Models.DocumentosASAETipoArchivo item = new Models.DocumentosASAETipoArchivo()
                {
                    Id = int.Parse(reader["id"].ToString()),
                    Extensiones = reader["extensiones"].ToString(),
                    TamMaxPermitido = int.Parse(reader["tammaxpermitido"].ToString()),
                    Permitir = bool.Parse(reader["permitir"].ToString()),
                };
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected Models.DocumentosASAETipoArchivo SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, extensiones, tammaxpermitido, permitir FROM documentosasaetipoarchivo WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            Models.DocumentosASAETipoArchivo resultado = new Models.DocumentosASAETipoArchivo();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.Extensiones = reader["extensiones"].ToString();
                resultado.TamMaxPermitido = int.Parse(reader["tammaxpermitido"].ToString());
                resultado.Permitir = bool.Parse(reader["permitir"].ToString());

            }
            b.CloseConnection();
            return resultado;
        }

        protected int SeleccionarSiPermitido(string extension)
        {
            b.ExecuteCommandQuery("SELECT tammaxpermitido FROM documentosasaetipoarchivo WHERE extensiones LIKE @extension AND permitir = 1");
            b.AddParameter("@extension", "%" + extension + "%", SqlDbType.NVarChar, 150);
            return int.Parse(b.SelectString());
        }


        protected int Agregar(Models.DocumentosASAETipoArchivo items)
        {
            b.ExecuteCommandQuery("INSERT INTO documentosasaetipoarchivo (extensiones, tammaxpermitido, permitir) VALUES(@extensiones, @tammaxpermitido, @permitir)");
            b.AddParameter("@extensiones", items.Extensiones, SqlDbType.NVarChar, 150);
            b.AddParameter("tammaxpermitido", items.TamMaxPermitido, SqlDbType.Int);
            b.AddParameter("@permitir", items.Permitir, SqlDbType.Bit);
            return b.InsertUpdateDelete();
        }


        protected int Modificar(Models.DocumentosASAETipoArchivo items)
        {
            b.ExecuteCommandQuery("UPDATE documentosasaetipoarchivo SET extensiones=@extensiones, tammaxpermitido=@tammaxpermitido, permitir=@permitir WHERE id=@id");
            b.AddParameter("@extensiones", items.Extensiones, SqlDbType.NVarChar, 150);
            b.AddParameter("tammaxpermitido", items.TamMaxPermitido, SqlDbType.Int);
            b.AddParameter("@permitir", items.Permitir, SqlDbType.Bit);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }


    }
}
