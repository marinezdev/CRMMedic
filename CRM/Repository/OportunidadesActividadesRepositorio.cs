using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class OportunidadesActividadesRepositorio 
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public OportunidadesActividades SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT Id, TipoActividad, Fecha, Notas, IdOportunidad FROM oportunidadesactividades WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            OportunidadesActividades resultado = new OportunidadesActividades();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.TipoActividad = int.Parse(reader["TipoActividad"].ToString());
                resultado.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                resultado.Notas = reader["Notas"].ToString();
                resultado.IdOportunidad = int.Parse(reader["IdOportunidad"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<OportunidadesActividades> SeleccionarPorIdOportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT Id, " +
            "CASE " +
            "WHEN tipoactividad=1 THEN 'Tarea' " +
            "WHEN tipoactividad=2 THEN 'Llamada de Teléfono' " +
            "WHEN tipoactividad=3 THEN 'Correo Electrónico' " +
            "WHEN tipoactividad=4 THEN 'Carta' " +
            "WHEN tipoactividad=5 THEN 'Cita' " +
            "END AS Actividad, " +
            "Fecha, Notas, Agregado, IdOportunidad FROM oportunidadesactividades WHERE idoportunidad=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            List<OportunidadesActividades> resultado = new List<OportunidadesActividades>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                OportunidadesActividades item = new OportunidadesActividades();

                item.Id = int.Parse(reader["Id"].ToString());
                item.ActividadNombre = reader["Actividad"].ToString();
                item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Notas = reader["Notas"].ToString();
                item.IdOportunidad = int.Parse(reader["IdOportunidad"].ToString());

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Agregar(OportunidadesActividades items)
        {
            b.ExecuteCommandQuery("INSERT INTO oportunidadesactividades (tipoactividad, fecha, notas, idoportunidad, idusuario) " +
            "VALUES(@tipoactividad, @fecha, @notas, @idoportunidad, @idusuario)");
            b.AddParameter("@tipoactividad", items.TipoActividad, SqlDbType.Int);
            b.AddParameter("@fecha", items.Fecha, SqlDbType.Date);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@idoportunidad", items.IdOportunidad, SqlDbType.Int);
            b.AddParameter("@idusuario", items.IdUsuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int Modificar(OportunidadesActividades items)
        {
            b.ExecuteCommandQuery("UPDATE oportunidadesactividades SET tipoactividad=@tipoactividad, fecha=@fecha,  " +
            "notas=@notas " +
            "WHERE id=@id");
            b.AddParameter("@tipoactividad", items.TipoActividad, SqlDbType.Int);
            b.AddParameter("@fecha", items.Fecha, SqlDbType.Date);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}