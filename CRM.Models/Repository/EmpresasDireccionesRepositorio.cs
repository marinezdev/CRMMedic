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
    public class EmpresasDireccionesRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected EmpresasDirecciones SeleccionarDireccionAdicionalPorId(string iddireccionadicional)
        {
            string consulta = "SELECT * FROM empresasdirecciones WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", iddireccionadicional, SqlDbType.Int);
            EmpresasDirecciones resultado = new EmpresasDirecciones();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.IdEmpresa = int.Parse(reader["Idempresa"].ToString());
                resultado.IdTipoDireccion = int.Parse(reader["Idtipodireccion"].ToString());
                resultado.Direccion = reader["direccion"].ToString();
                resultado.CP = reader["cp"].ToString();
                resultado.Ciudad = reader["ciudad"].ToString();
                resultado.IdPais = int.Parse(reader["idpais"].ToString());
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<EmpresasDirecciones> SeleccionarDireccionesAdicionalesPorIdEmpresa(string idempresa)
        {
            string consulta = "SELECT * FROM empresasdirecciones WHERE idempresa=@idempresa";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            List<EmpresasDirecciones> resultado = new List<EmpresasDirecciones>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                EmpresasDirecciones item = new EmpresasDirecciones();
                item.Id                 = int.Parse(reader["Id"].ToString());
                item.IdEmpresa          = int.Parse(reader["Idempresa"].ToString());
                item.IdTipoDireccion    = int.Parse(reader["Idtipodireccion"].ToString());
                item.Direccion          = reader["direccion"].ToString();
                item.CP                 = reader["cp"].ToString();
                item.Ciudad             = reader["ciudad"].ToString();
                item.IdPais             = int.Parse(reader["idpais"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<EmpresasDirecciones> SeleccionarDireccionesAdicionalesConNombreDePaisPorIdEmpresa(string idempresa)
        {
            string consulta = "SELECT * FROM empresasdirecciones WHERE idempresa=@idempresa";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            List<EmpresasDirecciones> resultado = new List<EmpresasDirecciones>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                EmpresasDirecciones item = new EmpresasDirecciones();
                item.Id = int.Parse(reader["Id"].ToString());
                item.IdEmpresa = int.Parse(reader["Idempresa"].ToString());
                item.IdTipoDireccion = int.Parse(reader["Idtipodireccion"].ToString());
                item.Direccion = reader["direccion"].ToString();
                item.CP = reader["cp"].ToString();
                item.Ciudad = reader["ciudad"].ToString();
                item.NombrePais = Catalogos.SeleccionarNombrePorId(reader["idpais"].ToString(), "Paises");
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(string idempresa, string idtipodireccion, string direccion, string cp, string ciudad, string idpais)
        {
            string consulta = "INSERT INTO empresasdirecciones (idempresa,idtipodireccion,direccion,cp,ciudad,idpais) " +
            "VALUES(@idempresa,@idtipodireccion,@direccion,@cp,@ciudad,@idpais)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idtipodireccion", idtipodireccion, SqlDbType.Int);
            b.AddParameter("@direccion", direccion, SqlDbType.NVarChar);
            b.AddParameter("@cp", cp, SqlDbType.NVarChar );
            b.AddParameter("@ciudad", ciudad, SqlDbType.NVarChar);
            b.AddParameter("@idpais", idpais, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string idtipodireccion, string direccion, string cp, string ciudad, string idpais, string iddireccionadicional)
        {
            string consulta = "UPDATE empresasdirecciones SET idtipodireccion=@idtipodireccion,direccion=@direccion,cp=@cp,ciudad=@ciudad,idpais=@idpais " +
            "WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idtipodireccion", idtipodireccion, SqlDbType.Int);
            b.AddParameter("@direccion", direccion, SqlDbType.NVarChar);
            b.AddParameter("@cp", cp, SqlDbType.NVarChar);
            b.AddParameter("@ciudad", ciudad, SqlDbType.NVarChar);
            b.AddParameter("@idpais", idpais, SqlDbType.Int);
            b.AddParameter("@id", iddireccionadicional, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }



    }
}
