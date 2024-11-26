using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Utilerias;
using CRM.Models;

namespace CRM.Repository
{
    public class TareasRepositorio
    {
        internal AccesoDatos_ b { get; set; } = new AccesoDatos_();

        public List<TareasUsuarios> Buscar(string asunto, string responsable, string inicio, string fin)
        {
            string consulta = "SELECT ta.Id, ta.asunto, us.Nombre, ta.inicio, ta.fin, ta.avance, ta.Prioridad " +
            "FROM Tareas ta " +
            "INNER JOIN TareasUsuariosContactosEmpresas tuce ON ta.Id = tuce.IdTarea " +
            "LEFT JOIN Usuarios us ON us.id = tuce.IdUsuario " +
            "WHERE 1=1 ";
            if (asunto != "")
                consulta += "AND ta.asunto LIKE @asunto ";
            if (responsable != "")
                consulta += "AND tuce.IdUsuario=@responsable ";
            if (inicio != "" && fin != "")
                consulta += "AND ta.inicio>=@inicio AND ta.fin<=@fin ";
            //consulta += "AND tuce.IdUsuario=@responsable ";
            b.ExecuteCommandQuery(consulta);
            if (asunto != "")
                b.AddParameter("@asunto", "%" + asunto + "%", SqlDbType.NVarChar);
            if (responsable != "")
                b.AddParameter("@responsable", responsable, SqlDbType.Int);
            if (inicio != "" && fin != "")
            {
                b.AddParameter("@inicio", inicio, SqlDbType.Date);
                b.AddParameter("@fin", fin, SqlDbType.Date);
            }
            List<TareasUsuarios> resultado = new List<TareasUsuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                TareasUsuarios item = new TareasUsuarios();
                item.Tareas.Id = int.Parse(reader["Id"].ToString());
                item.Tareas.Asunto = reader["Asunto"].ToString();
                item.Usuarios.Nombre = reader["Nombre"].ToString();
                item.Tareas.Inicio = DateTime.Parse(reader["Inicio"].ToString());
                item.Tareas.Fin = DateTime.Parse(reader["Fin"].ToString());
                item.Tareas.Avance = int.Parse(reader["Avance"].ToString());
                item.Tareas.Prioridad = int.Parse(reader["Prioridad"].ToString());
                
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public TareasUsuarios SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT ta.Id, ta.Asunto, ta.Inicio, ta.Fin, ta.HoraInicio, ta.HoraFin, ta.Actividad, ta.Estado, ta.Avance, ta.Prioridad, ta.Notas," +
            "us.Id AS IdUsuario, us.Nombre " +
            "FROM tareas ta " +
            "INNER JOIN TareasUsuariosContactosEmpresas tuce ON ta.Id = tuce.IdTarea " +
            "INNER JOIN Usuarios us ON tuce.IdUsuario = us.Id WHERE ta.id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            TareasUsuarios resultado = new TareasUsuarios();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Tareas.Id            = int.Parse(reader["Id"].ToString());
                resultado.Tareas.Asunto        = reader["Asunto"].ToString();
                resultado.Usuarios.Id          = int.Parse(reader["IdUsuario"].ToString());
                resultado.Tareas.Inicio        = DateTime.Parse(reader["Inicio"].ToString());
                resultado.Tareas.Fin           = DateTime.Parse(reader["Fin"].ToString());
                resultado.Tareas.HoraInicio    = reader["HoraInicio"].ToString();
                resultado.Tareas.HoraFin       = reader["HoraFin"].ToString();
                resultado.Tareas.Actividad     = int.Parse(reader["Actividad"].ToString());
                resultado.Tareas.Estado        = int.Parse(reader["Estado"].ToString());
                resultado.Tareas.Avance        = int.Parse(reader["Avance"].ToString());
                resultado.Tareas.Prioridad     = int.Parse(reader["Prioridad"].ToString());
                resultado.Tareas.Notas         = reader["Notas"].ToString();
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Agregar(Tareas items)
        {
            b.ExecuteCommandQuery("INSERT INTO tareas (asunto, inicio, fin,horainicio, horafin,actividad,estado,avance,prioridad,notas,alta) " +
            "VALUES(@asunto,@inicio,@fin,@horainicio,@horafin,@actividad,@estado,@avance,@prioridad,@notas,getdate()); " +
            "SELECT @@IDENTITY");
            b.AddParameter("@asunto", items.Asunto, SqlDbType.NVarChar);
            b.AddParameter("@inicio", Funciones_.PrepararFechaParaAgregar(items.Inicio), SqlDbType.Date);
            b.AddParameter("@fin", Funciones_.PrepararFechaParaAgregar(items.Fin), SqlDbType.Date);
            b.AddParameter("@horainicio", items.HoraInicio, SqlDbType.Time);
            b.AddParameter("@horafin", items.HoraFin, SqlDbType.Time);
            b.AddParameter("@actividad", items.Actividad, SqlDbType.Int);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@avance", items.Avance, SqlDbType.Int);
            b.AddParameter("@prioridad", items.Prioridad, SqlDbType.Int);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            return b.SelectWithReturnValue();
        }

        public bool Modificar(Tareas items)
        {
            b.ExecuteCommandQuery("UPDATE tareas SET asunto=@asunto,responsable=@responsable,inicio=@inicio,fin=@fin,horainicio=@horainicio,horafin=@horafin,actividad=@actividad,estado=@estado,avance=@avance,prioridad=@prioridad,notas=@notas " +
            "WHERE id=@id");
            b.AddParameter("@asunto", items.Asunto, SqlDbType.NVarChar);
            b.AddParameter("@responsable", items.Responsable, SqlDbType.Int);
            b.AddParameter("@inicio", items.Inicio, SqlDbType.Date);
            b.AddParameter("@fin", items.Fin, SqlDbType.Date);
            b.AddParameter("@horainicio", items.HoraInicio, SqlDbType.Time);
            b.AddParameter("@horafin", items.HoraFin, SqlDbType.Time);
            b.AddParameter("@actividad", items.Actividad, SqlDbType.Int);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@avance", items.Avance, SqlDbType.Int);
            b.AddParameter("@prioridad", items.Prioridad, SqlDbType.Int);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            int i = b.InsertUpdateDelete();
            if (i >= 1)
                return true;
            else
                return false;
        }



    }
}