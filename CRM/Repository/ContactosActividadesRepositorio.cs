using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;
using System.Web.UI.WebControls;

namespace CRM.Repository
{
    public class ContactosActividadesRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public List<ContactosActividades> SeleccionarPorIdBitacoraIdContacto(string idbitacora, string idcontacto)
        {
            b.ExecuteCommandQuery("SELECT id, idbitacora, idcontacto, tipoactividad, fecha, notas, creado " +
            "FROM contactosactividades " +
            "WHERE idbitacora=@idbitacora " +
            "AND idcontacto=@idcontacto");
            b.AddParameter("@idbitacora", idbitacora, SqlDbType.Int);
            b.AddParameter("@idcontacto", idcontacto, SqlDbType.Int);
            List<ContactosActividades> resultado = new List<ContactosActividades>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ContactosActividades item = new ContactosActividades();
                item.Id = int.Parse(reader["Id"].ToString());
                item.IdBitacora = int.Parse(reader["IdBitacora"].ToString());
                item.IdContacto = int.Parse(reader["IdContacto"].ToString());
                item.TipoActividad = int.Parse(reader["TipoActividad"].ToString());
                item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Notas = reader["Notas"].ToString();
                item.Creado = DateTime.Parse(reader["Fecha"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public ContactosActividades SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, idbitacora, idcontacto, tipoactividad, fecha, notas, creado " +
            "FROM contactosactividades " +
            "WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            ContactosActividades resultado = new ContactosActividades();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.IdBitacora = int.Parse(reader["IdBitacora"].ToString());
                resultado.IdContacto = int.Parse(reader["IdContacto"].ToString());
                resultado.TipoActividad = int.Parse(reader["TipoActividad"].ToString());
                resultado.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                resultado.Notas = reader["Notas"].ToString();
                resultado.Creado = DateTime.Parse(reader["Fecha"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarContactosConActividades(string idoportunidad)
        {           
            string consulta = "SELECT bic.id, con.Nombre + ' ' + con.ApellidoPaterno + ' ' + con.ApellidoMaterno AS Contacto, ca.TipoActividad, " +
            "CASE " +
            "WHEN ca.tipoactividad=1 THEN 'Tarea' " +
            "WHEN ca.tipoactividad=2 THEN 'Llamada' " +
            "WHEN ca.tipoactividad=3 THEN 'Correo Electrónico' " +
            "WHEN ca.tipoactividad=4 THEN 'Carta' " +
            "WHEN ca.tipoactividad=5 THEN 'Cita' " +
            "END AS TipoActividadNombre, " + 
            "ca.Fecha, ca.notas " +
            "FROM oportunidadesresponsables opor " +
            "INNER JOIN contactos con ON con.id = opor.idasignado " +
            "INNER JOIN contactosactividades ca ON ca.IdContacto = con.IdUsuarioResponsable " +
            "LEFT JOIN Bitacora bic ON(bic.IdResponsable = con.idusuarioresponsable AND opor.IdOportunidad = bic.IdOportunidad) " +
            "WHERE opor.idoportunidad=@idoportunidad " +
            "ORDER BY Contacto";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.NVarChar);            
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Contactos.Nombre = reader["Contacto"].ToString();
                //item.ContactosActividades.TipoActividad = int.Parse(reader["TipoActividad"].ToString());
                item.ContactosActividades.TipoActividadNombre = CRM.Utilerias.Funciones.TipoActividadNombre(reader["TipoActividad"].ToString());
                item.ContactosActividades.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.ContactosActividades.Notas = reader["Notas"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }


        public int AgregarModificar(ContactosActividades items)
        {
            string consulta = "";
            if (items.Id < 1)
            {
                consulta = "INSERT INTO contactosactividades (idbitacora, idcontacto, tipoactividad, fecha, notas)  " +
                " VALUES(@idbitacora, @idcontacto, @tipoactividad, @fecha, @notas)";
                b.ExecuteCommandQuery(consulta);
            }
            else if (items.Id > 0)
            {
                consulta = "UPDATE contactosactividades SET idbitacora=@idbitacora, idcontacto=@idcontacto, tipoactividad=@tipoactividad, fecha=@fecha, notas=@notas  " +
                "WHERE id=@id";
                b.ExecuteCommandQuery(consulta);
            }
            b.AddParameter("@idbitacora", items.IdBitacora, SqlDbType.Int);
            b.AddParameter("@idcontacto", items.IdContacto, SqlDbType.Int);
            b.AddParameter("@tipoactividad", items.TipoActividad, SqlDbType.Int);
            b.AddParameter("@fecha", items.Fecha, SqlDbType.Date);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int Modificar(ContactosActividades items)
        {
            string consulta = "UPDATE contactosactividades SET idbitacora=@idbitacora, idcontacto=@idcontacto, tipoactividad=@tipoactividad, fecha=@fecha, notas=@notas  " +
            " WHERE id=@id";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idbitacora", items.IdBitacora, SqlDbType.Int);
            b.AddParameter("@idcontacto", items.IdContacto, SqlDbType.Int);
            b.AddParameter("@tipoactividad", items.TipoActividad, SqlDbType.Int);
            b.AddParameter("@fecha", items.Fecha, SqlDbType.Date);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }


    }
}