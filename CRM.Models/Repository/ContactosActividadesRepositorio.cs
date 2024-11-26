using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class ContactosActividadesRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<ContactosActividades> SeleccionarPorIdBitacoraIdContacto(string idbitacora, string idcontacto)
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
                item.TipoActividad = reader["TipoActividad"].ToString() == "" ? 0 : int.Parse(reader["TipoActividad"].ToString());
                item.Fecha = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["Fecha"].ToString());
                item.Notas = reader["Notas"].ToString();
                item.Creado = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["Fecha"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected ContactosActividades SeleccionarPorId(string id)
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
                resultado.TipoActividad = reader["TipoActividad"].ToString() == "" ? 0 : int.Parse(reader["TipoActividad"].ToString());
                resultado.Fecha = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900-01-01") : DateTime.Parse(reader["Fecha"].ToString());
                resultado.Notas = reader["Notas"].ToString();
                resultado.Creado = reader["Fecha"].ToString() == "" ? DateTime.Parse("1900-01-01") : DateTime.Parse(reader["Fecha"].ToString());
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Esta consulta es para contactos pero se usa para usuarios responsables
        /// </summary>
        /// <param name="idoportunidad"></param>
        /// <returns></returns>
        protected List<Modelos> SeleccionarContactosConActividades(string idoportunidad)
        {           
            string consultaant = "SELECT bic.id, usu.Nombre + ' ' + usu.ApellidoPaterno + ' ' + usu.ApellidoMaterno AS Contacto, ca.TipoActividad,  " +
            "CASE " +
            "WHEN ca.tipoactividad = 1 THEN 'Tarea' " +
            "WHEN ca.tipoactividad = 2 THEN 'Llamada' " +
            "WHEN ca.tipoactividad = 3 THEN 'Correo Electrónico' " +
            "WHEN ca.tipoactividad = 4 THEN 'Carta' " +
            "WHEN ca.tipoactividad = 5 THEN 'Cita' " +
            "END AS TipoActividadNombre, " +
            "ca.Fecha, ca.notas, ca.creado " +
            "FROM bitacora bic " +
            "INNER JOIN ContactosActividades ca ON ca.IdBitacora = bic.id " +
            "INNER JOIN usuarios usu ON usu.Id = bic.IdResponsable  " +
            "WHERE bic.IdOportunidad = @idoportunidad " +
            "ORDER BY Contacto, ca.tipoactividad";

            string consulta = "SELECT bic.id, usu.Nombre, " +
            "ca.notas " +
            "FROM bitacora bic " +
            "INNER JOIN usuarios usu ON usu.Id = bic.IdResponsable " +
            "INNER JOIN ContactosActividades ca ON ca.IdBitacora = bic.id AND ca.IdContacto = usu.id " +
            "WHERE bic.IdOportunidad = @idoportunidad ";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", idoportunidad, SqlDbType.NVarChar);            
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Contactos.Nombre                           = reader["Nombre"].ToString();
                //item.ContactosActividades.TipoActividad        = int.Parse(reader["TipoActividad"].ToString());
                //item.ContactosActividades.TipoActividadNombre   = reader["TipoActividad"].ToString() == "" ? "" : CRM.Utilerias.Funciones.TipoActividadNombre(reader["TipoActividad"].ToString());
                //item.ContactosActividades.Fecha                 = reader["Fecha"].ToString() == "" ? DateTime.Parse(reader["Creado"].ToString()) : DateTime.Parse(reader["Fecha"].ToString());
                item.ContactosActividades.Notas                 = reader["Notas"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }


        protected int AgregarModificar(ContactosActividades items)
        {
            string consulta = "";
            if (items.Id < 1)
            {
                consulta = "INSERT INTO contactosactividades (idbitacora, idcontacto, ";
                if (items.TipoActividad>0)
                    consulta += "tipoactividad, ";
                if (items.Fecha > DateTime.Parse("1900/01/01"))
                    consulta += "fecha, ";
                consulta += "notas) " +
                "VALUES(@idbitacora, @idcontacto, ";
                if (items.TipoActividad > 0)
                    consulta += "@tipoactividad, ";
                if (items.Fecha > DateTime.Parse("1900/01/01"))
                    consulta += "@fecha, ";
                consulta += "@notas)";
                b.ExecuteCommandQuery(consulta);
            }
            else if (items.Id > 0)
            {
                consulta = "UPDATE contactosactividades SET idbitacora=@idbitacora, " +
                    "idcontacto=@idcontacto,";
                if (items.TipoActividad > 0)
                    consulta += "tipoactividad=@tipoactividad,";
                if (items.Fecha > DateTime.Parse("1900/01/01"))
                    consulta += "fecha=@fecha,";
                consulta += "notas=@notas  " +
                "WHERE id=@id";
                b.ExecuteCommandQuery(consulta);
            }
            b.AddParameter("@idbitacora", items.IdBitacora, SqlDbType.Int);
            b.AddParameter("@idcontacto", items.IdContacto, SqlDbType.Int);
            if (items.TipoActividad > 0)
                b.AddParameter("@tipoactividad", items.TipoActividad, SqlDbType.Int);
            if (items.Fecha > DateTime.Parse("1900/01/01"))
                b.AddParameter("@fecha", items.Fecha, SqlDbType.Date);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(ContactosActividades items)
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