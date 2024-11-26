using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class ContactosEmpresasRepositorio 
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        /// <summary>
        /// Agregar Contacto, Empresa tabla pivote
        /// </summary>
        /// <param name="idcontacto"></param>
        /// <param name="idempresa"></param>
        /// <returns></returns>
        public bool AgregarContactoEmpresa(string idcontacto, string idempresa)
        {
            b.ExecuteCommandQuery("INSERT INTO contactosempresas (idcontacto, idempresa) VALUES(@idcontacto,@idempresa)");
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Agrega un contacto a la tabla pivote
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AgregarSoloContacto(string id)
        {
            b.ExecuteCommandQuery("INSERT INTO contactosempresas (idcontacto) VALUES(@id)");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int ActualizarContactoEmpresa(string idcontacto, string idempresa)
        {
            b.ExecuteCommandQuery("UPDATE contactosempresas SET idempresa=@idempresa WHERE idcontacto=@idcontacto");
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public bool AgregarSoloEmpresa(string idempresa)
        {
            b.ExecuteCommandQuery("INSERT INTO contactosempresas (idempresa) VALUES(@idempresa)");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }
    }
}