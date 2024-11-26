using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models.Models;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class EmpresasUsuariosRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Modelos> SeleccionarEmpresasPorUsuario(string idusuario)
        {
            string consulta = "SELECT eu.idempresa, emp.Nombre, eu.Activo " +
            "FROM EmpresasUsuarios eu " +
            "INNER JOIN empresas emp on emp.id = eu.IdEmpresa " +
            "WHERE eu.idusuario = @idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
               
                item.Empresas.Id = int.Parse(reader["IdEmpresa"].ToString());
                item.Empresas.Nombre = reader["Nombre"].ToString();
                item.Usuarios.Activo = bool.Parse(reader["Activo"].ToString());

                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<Usuarios> SeleccionarUsuariosExistentesRol2(string idconfiguracion)
        {
            string consulta = "SELECT DISTINCT eu.idusuario " +
            "FROM EmpresasUsuarios eu " +
            "INNER JOIN empresas emp ON emp.id = eu.idempresa " +
            "INNER JOIN Configuracion conf ON conf.Id = emp.IdConfiguracion " +
            "INNER JOIN UsuariosRoles ur ON ur.IdUsuario=eu.IdUsuario " +
            "WHERE conf.id = @idconfiguracion " +
            "AND ur.IdRol=2";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios()
                {
                    Id = int.Parse(reader["IdUsuario"].ToString()),
                };
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<Usuarios> SeleccionarUsuariosExistentesRol3(string idconfiguracion)
        {
            string consulta = "SELECT DISTINCT eu.idusuario " +
            "FROM EmpresasUsuarios eu " +
            "INNER JOIN empresas emp ON emp.id = eu.idempresa " +
            "INNER JOIN Configuracion conf ON conf.Id = emp.IdConfiguracion " +
            "INNER JOIN UsuariosRoles ur ON ur.IdUsuario=eu.IdUsuario " +
            "WHERE conf.id = @idconfiguracion " +
            "AND ur.IdRol=3";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios()
                {
                    Id = int.Parse(reader["IdUsuario"].ToString()),
                };
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected string SeleccionarValidarSiUsuarioPerteneceAEmpresa(string idusuario)
        {
            b.ExecuteCommandQuery("SELECT idempresa FROM empresasusuarios WHERE idusuario=@id");
            b.AddParameter("@id", idusuario, SqlDbType.Int);
            return b.SelectString();
        }

        protected int Agregar(string idempresa, string idusuario, string activo)
        {
            string consulta = "INSERT INTO empresasusuarios ";
            consulta += "(";
            if (!string.IsNullOrEmpty(idempresa))
                consulta += "idempresa,";
            consulta += "idusuario,activo) " +
                "VALUES(";
            if (!string.IsNullOrEmpty(idempresa))
                consulta += "@idempresa,";
            consulta += "@idusuario,@activo)";
            b.ExecuteCommandQuery(consulta);
            if (!string.IsNullOrEmpty(idempresa))
                b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@activo", activo, SqlDbType.Bit);
            return b.InsertUpdateDelete();
        }

        protected int ModificarEmpresaPorUsuario(string idusuario, string idempresa)
        {
            string consulta = "UPDATE empresasusuarios SET idempresa=@idempresa WHERE idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int ModificarEmpresasPorUsuario(string idusuario, string idempresa, string activo)
        {
            string consulta = "UPDATE empresasusuarios SET activo=@activo WHERE idempresa=@idempresa AND idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@activo", activo, SqlDbType.Bit);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
