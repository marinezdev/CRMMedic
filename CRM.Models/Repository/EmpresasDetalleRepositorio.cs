using CRM.Models.Models;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Dynamic;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class EmpresasDetalleRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected EmpresasDetalle SeleccionarPorIdEmpresa(string idempresa)
        {
            string consulta = "SELECT * FROM empresasdetalle WHERE idempresa=@idempresa";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            EmpresasDetalle resultado = new EmpresasDetalle();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.IdEmpresa     = reader["idempresa"].ToString() == "" ? 0 : int.Parse(reader["idempresa"].ToString());
                resultado.IdFuente      = reader["idfuente"].ToString() == "" ? 0 : int.Parse(reader["idfuente"].ToString());
                resultado.IdTipo        = reader["idtipo"].ToString() == "" ? 0 : int.Parse(reader["idtipo"].ToString());
                resultado.IdIndustria   = reader["idindustria"].ToString() == "" ? 0 : int.Parse(reader["idindustria"].ToString());
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int AgregarModificar(string idempresa, string idfuente, string idtipo, string idindustria)
        {
            string consulta = "IF EXISTS(SELECT * FROM empresasdetalle WHERE idempresa=@idempresa) " +
            "BEGIN ";            
            if (!string.IsNullOrEmpty(idfuente))
                consulta += "UPDATE empresasdetalle SET idfuente=@idfuente WHERE idempresa=@idempresa;";
            if (!string.IsNullOrEmpty(idtipo))
                consulta += "UPDATE empresasdetalle SET idtipo=@idtipo WHERE idempresa=@idempresa;";
            if (!string.IsNullOrEmpty(idindustria))
                consulta += "UPDATE empresasdetalle SET idindustria=@idindustria WHERE idempresa=@idempresa;";
            consulta += "END " +
            "ELSE " +
            "BEGIN " +
            "   INSERT INTO empresasdetalle (idempresa";
            if (!string.IsNullOrEmpty(idfuente))
                consulta += ",idfuente,";
            if (!string.IsNullOrEmpty(idtipo))
                consulta += "idtipo,";
            if (!string.IsNullOrEmpty(idindustria))
                consulta += "idindustria";
            consulta += ") VALUES(@idempresa";
            if (!string.IsNullOrEmpty(idfuente))
                consulta += ",@idfuente";
            if (!string.IsNullOrEmpty(idtipo))
                consulta += ",@idtipo";
            if (!string.IsNullOrEmpty(idindustria))
                consulta += ",@idindustria";
            consulta +=");" +
            "END";
            b.ExecuteCommandQuery(consulta);
            if (!string.IsNullOrEmpty(idfuente))
                b.AddParameter("@idfuente", idfuente, SqlDbType.Int);
            if (!string.IsNullOrEmpty(idtipo))
                b.AddParameter("@idtipo", idtipo, SqlDbType.Int);
            if (!string.IsNullOrEmpty(idindustria))
                b.AddParameter("@idindustria", idindustria, SqlDbType.Int);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            return b.InsertUpdateDelete();

        }

    }
}
