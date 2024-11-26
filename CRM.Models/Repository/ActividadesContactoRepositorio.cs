using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Models.Models;
using System.Security.Cryptography.X509Certificates;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class ActividadesContactoRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected int Agregar(ActividadesContacto items)
        {
            string consulta = "IF NOT EXISTS(SELECT id FROM actividadescontacto WHERE idusuario=@idusuario AND idcontacto=@idcontacto) " +
            "BEGIN " +
            "   INSERT INTO actividadescontacto  (idusuario, idcontacto) VALUES (@idusuario, @idcontacto) " +
            "   SELECT @@IDENTITY " +
            "END " +
            "ELSE " +
            "BEGIN " +
            "   SELECT id FROM actividadescontacto WHERE idusuario=@idusuario AND idcontacto=@idcontacto " +
            "END";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", items.IdUsuario, SqlDbType.Int);
            b.AddParameter("@idcontacto", items.IdContacto, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }
    }
}
