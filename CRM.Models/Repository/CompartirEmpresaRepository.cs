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
    public class CompartirEmpresaRepository
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<CompartirUsuarios> SeleccionarUsuariosCompartidos(string idempresa)
        {
            string consulta = "SELECT ce.id, ce.idempresa, usu.id AS IdUsuario, usu.nombre + ' ' + ISNULL(usu.apellidopaterno,'') + ' ' + ISNULL(usu.apellidomaterno,'') AS nombre " +
            "FROM compartirempresas ce " +
            "INNER JOIN usuarios usu ON usu.id=ce.idusuario " +
            "WHERE ce.idempresa=@idempresa";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            List<CompartirUsuarios> resultado = new List<CompartirUsuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                CompartirUsuarios item = new CompartirUsuarios();
                item.CompartirEmpresas.Id = int.Parse(reader["Id"].ToString());
                item.CompartirEmpresas.IdEmpresa = int.Parse(reader["Idempresa"].ToString());
                item.Usuarios.Nombre = reader["nombre"].ToString();
                item.Usuarios.Id = int.Parse(reader["idusuario"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected bool ValidarSiUsuarioPuedeModificar(string idempresa, string idusuario)
        {
            //consulta por si no se debe compartir en escalera
            string consulta1 = "SELECT 1 FROM compartirempresas ce INNER JOIN empresas emp ON emp.id=ce.IdEmpresa WHERE ce.idempresa=@idempresa AND ce.idusuario=@idusuario AND emp.idusuario=@idusuario";
            string consulta = "SELECT 1 FROM compartirempresas WHERE idempresa=@idempresa AND idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            if (b.SelectString() == "1")
                return true;
            else
                return false;
        }

        protected bool ValidarEmpresaUsuarioPermiso(string idempresa, string idusuario)
        {
            b.ExecuteCommandSP("Empresas_Validar_CreadorPermiso");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            if (b.SelectWithReturnValue() == 1)
                return true;
            else
                return false;
        }

        protected int Agregar(string idempresa, string idusuario)
        {
            string consulta = "IF NOT EXISTS(SELECT 1 FROM compartirempresas WHERE idempresa=@idempresa ANd idusuario=@idusuario) " +
                "BEGIN " +
                "INSERT INTO compartirempresas(idempresa, idusuario) VALUES(@idempresa, @idusuario) " +
                "END ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int EliminarUsuarioCompartido(string idempresa, string idusuario)
        {
            string consulta = "DELETE FROM compartirempresas WHERE idempresa=@idempresa AND idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
