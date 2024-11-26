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
    public class CompartirOportunidadesRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<CompartirUsuarios> SeleccionarUsuariosCompartidos(string idoportunidad)
        {
            string consulta = "SELECT co.id, co.idoportunidad, usu.id AS IdUsuario, usu.nombre + ' ' + ISNULL(usu.apellidopaterno,'') + ' ' + ISNULL(usu.apellidomaterno,'') AS nombre " +
            "FROM compartiroportunidades co " +
            "INNER JOIN usuarios usu ON usu.id=co.idusuario " +
            "WHERE co.idoportunidad=@idoportunidad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            List<CompartirUsuarios> resultado = new List<CompartirUsuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                CompartirUsuarios item = new CompartirUsuarios();
                item.CompartirOportunidades.Id            = int.Parse(reader["Id"].ToString());
                item.CompartirOportunidades.IdOportunidad = int.Parse(reader["Idoportunidad"].ToString());
                item.Usuarios.Nombre                      = reader["nombre"].ToString();
                item.Usuarios.Id                          = int.Parse(reader["idusuario"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected bool ValidarSiUsuarioPuedeModificar(string idoportunidad, string idusuario)
        {
            //consulta por si no se debe compartir en escalera
            string consulta1 = "SELECT 1 FROM compartiroportunidades co INNER JOIN oecu ON oecu.idoportunidad = co.Idoportunidad WHERE co.idoportunidad=@idoportunidad AND co.idusuario=@idusuario AND oecu.idusuario=@idusuario";
            string consulta = "SELECT 1 FROM compartiroportunidades WHERE idoportunidad=@idoportunidad AND idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            if (b.SelectString() == "1")
                return true;
            else
                return false;
        }

        protected bool ValidarOportunidadUsuarioPermiso(string idoportunidad, string idusuario)
        {
            b.ExecuteCommandSP("Oportunidades_Validar_CreadorPermiso");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            if (b.SelectWithReturnValue() == 1)
                return true;
            else
                return false;
        }

        protected int Agregar(string idoportunidad, string idusuario)
        {
            string consulta = "IF NOT EXISTS(SELECT 1 FROM compartiroportunidades WHERE idoportunidad=@idoportunidad ANd idusuario=@idusuario) " +
                "BEGIN " +
                "INSERT INTO compartiroportunidades (idoportunidad, idusuario) VALUES(@idoportunidad, @idusuario) " +
                "END ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int EliminarUsuarioCompartido(string idoportunidad, string idusuario)
        {
            string consulta = "DELETE FROM compartiroportunidades WHERE idoportunidad=@idempresa AND idusuario=@idusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
