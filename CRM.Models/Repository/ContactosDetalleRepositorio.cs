using CRM.Models.Models;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class ContactosDetalleRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected ContactosDetalle SeleccionarPorIdContacto(string idcontacto)
        {
            string consulta = "SELECT * FROM contactosdetalle WHERE idcontacto=@idcontacto";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            ContactosDetalle resultado = new ContactosDetalle();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.IdContacto = reader["idcontacto"].ToString() == "" ? 0 : int.Parse(reader["idcontacto"].ToString());
                resultado.Puesto = reader["idpuesto"].ToString() == "" ? 0 : int.Parse(reader["idpuesto"].ToString());
                resultado.Departamento = reader["iddepartamento"].ToString() == "" ? 0 : int.Parse(reader["iddepartamento"].ToString());
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int AgregarModificar(string idcontacto, string idpuesto, string iddepartamento)
        {
            string consulta = "IF EXISTS(SELECT * FROM contactosdetalle WHERE idcontacto=@idcontacto) " +
            "BEGIN ";
            if (!string.IsNullOrEmpty(idpuesto))
                consulta += "UPDATE contactosdetalle SET idpuesto=@idpuesto WHERE idcontacto=@idcontacto;";
            if (!string.IsNullOrEmpty(iddepartamento))
                consulta += "UPDATE contactosdetalle SET iddepartamento=@iddepartamento WHERE idcontacto=@idcontacto;";
            consulta += "END " +
            "ELSE " +
            "BEGIN " +
            "   INSERT INTO contactosdetalle (idcontacto";
            
            if (!string.IsNullOrEmpty(idpuesto))
                consulta += ",idpuesto";
            if (!string.IsNullOrEmpty(iddepartamento))
                consulta += ",iddepartamento";
            consulta += ") VALUES(@idcontacto";
            
            if (!string.IsNullOrEmpty(idpuesto))
                consulta += ",@idpuesto";
            if (!string.IsNullOrEmpty(iddepartamento))
                consulta += ",@iddepartamento";
            consulta += ");" +
            "END";
            
            b.ExecuteCommandQuery(consulta);
            if (!string.IsNullOrEmpty(idpuesto))
                b.AddParameter("@idpuesto", idpuesto, SqlDbType.Int);
            if (!string.IsNullOrEmpty(iddepartamento))
                b.AddParameter("@iddepartamento", iddepartamento, SqlDbType.Int);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            return b.InsertUpdateDelete();

        }
    }
}
