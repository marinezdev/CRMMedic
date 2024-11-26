using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class BitacoraUsuariosRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public int SeleccionarValidarSiUsuarioTieneBitacora(string idoportunidad, string idusuario)
        {
            b.ExecuteCommandQuery("SELECT Id FROM bitacorausuarios WHERE idoportunidad=@idoportunidad AND idresponsable=@idusuario");
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            if (!string.IsNullOrEmpty(b.SelectString()))
                return int.Parse(b.SelectString());
            else
                return 0;
        }

        public int Agregar(BitacoraUsuarios items)
        {
            b.ExecuteCommandQuery("" +
            "DECLARE @obtenido INT; " +
            "IF NOT EXISTS (SELECT id FROM bitacorausuarios WHERE IdResponsable=@idresponsable AND IdOportunidad=@idoportunidad) " +
            "   BEGIN " +
            "       INSERT INTO bitacorausuarios (Estado,IdResponsable,IdOportunidad) " +
            "       VALUES(1,@idresponsable,@idoportunidad); " +
            "       SET @obtenido=@@IDENTITY; " +
            "       INSERT INTO etapasbitacorausuarios (idbitacora, estado) VALUES(@obtenido, 1); " +
            "       SELECT @obtenido; " +
            "   END " +
            "ELSE " +
            "   SELECT 0");
            b.AddParameter("@idresponsable", items.IdResponsable, SqlDbType.Int);
            b.AddParameter("@idoportunidad", items.IdOportunidad, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }

        
    }
}