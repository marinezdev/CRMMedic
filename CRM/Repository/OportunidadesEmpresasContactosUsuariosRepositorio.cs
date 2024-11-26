using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class OportunidadesEmpresasContactosUsuariosRepositorio 
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public string SeleccionarEmpresaPorOportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT IdEmpresa FROM OportunidadesEmpresasContactosUsuarios WHERE IdOportunidad=@idoportunidad");
            b.AddParameter("@idoportunidad", id, SqlDbType.Int);
            return b.SelectString();
        }

        /// <summary>
        /// Guarda la nueva oportunidad en la tabla indice
        /// </summary>
        /// <param name="idoportunidad"></param>
        /// <param name="idempresa"></param>
        /// <param name="idcontacto"></param>
        /// <param name="idusuario"></param>
        /// <returns></returns>
        public bool Agregar(string idoportunidad, string idempresa, string idcontacto, string idusuario)
        {
            b.ExecuteCommandQuery("INSERT INTO OportunidadesEmpresasContactosUsuarios (idoportunidad, idempresa, idcontacto, idusuario) " +
            "VALUES(@idoportunidad, @idempresa, @idcontacto, @idusuario)");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        public int AgregarSoloEmpresa(string idempresa, string idoportunidad)
        {
            b.ExecuteCommandQuery("UPDATE OportunidadesEmpresasContactosUsuarios SET idempresa=@idempresa WHERE idoportunidad=@idoportunidad ");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int AgregarSolocontacto(string idcontacto, string idoportunidad)
        {
            b.ExecuteCommandQuery("UPDATE OportunidadesEmpresasContactosUsuarios SET idcontacto=@idcontacto WHERE idoportunidad=@idoportunidad ");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        /// <summary>
        /// Agrega la oportunidad a la tabla pivote
        /// </summary>
        /// <param name="idoportunidad"></param>
        /// <param name="idusuario"></param>
        /// <returns></returns>
        public int AgregarSoloOportunidad(string idoportunidad, string idusuario, string idconfiguracion)
        {
            b.ExecuteCommandQuery("INSERT INTO OportunidadesEmpresasContactosUsuarios (idoportunidad, idusuario, idconfiguracion) " +
            "VALUES(@idoportunidad, @idusuario,@idconfiguracion)");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int AgregarBitacora(string idoportunidad, string idempresa, string idusuario, string idbitacora)
        {
            b.ExecuteCommandQuery("UPDATE OportunidadesEmpresasContactosUsuarios SET IdBitacora=@idbitacora " +
            "WHERE idoportunidad=@idoportunidad AND idempresa=@idempresa)");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idbitacora", idbitacora, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }



    }
}