using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class UsuarioConfiguracionRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public int Agregar(string idusuario, string idconfiguracion)
        {
            b.ExecuteCommandQuery("INSERT INTO usuarioconfiguracion VALUES(@idusuario,@idconfiguracion)");
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}