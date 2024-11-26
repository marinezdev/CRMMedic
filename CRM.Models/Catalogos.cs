using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models
{
    /// <summary>
    /// Clase estática de acceso rápido a catálogos
    /// </summary>
    public static class Catalogos
    {
        /// <summary>
        /// Genera una consulta para llenar una lista proporcionando el nombre de la tabla
        /// </summary>
        /// <param name="tabla"></param>
        /// <returns></returns>
        public static List<Models.Catalogos> Seleccionar(string tabla)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("SELECT * FROM {0} ORDER BY nombre", tabla);
            b.ExecuteCommandQuery(consulta);
            List<Models.Catalogos> resultado = new List<Models.Catalogos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Models.Catalogos item = new Models.Catalogos
                {
                    Id = int.Parse(reader[0].ToString()),
                    Nombre = reader[1].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Obtiene un listado de una tabla especificando una configuración
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="idconfiguracion"></param>
        /// <returns></returns>
        public static List<Models.Catalogos> Seleccionar(string tabla, string idconfiguracion)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("SELECT id,nombre FROM {0} WHERE idconfiguracion={1} ORDER BY nombre", tabla, idconfiguracion);
            b.ExecuteCommandQuery(consulta);
            List<Models.Catalogos> resultado = new List<Models.Catalogos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Models.Catalogos item = new Models.Catalogos
                {
                    Id = int.Parse(reader[0].ToString()),
                    Nombre = reader[1].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Obtiene los datos de una tabla para llenar una lista
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="nombre">Nombre del campo nombre</param>
        /// <param name="id">Nombre del campo id</param>
        /// <returns></returns>
        public static List<Models.Catalogos> Seleccionar(string tabla, string nombre, string id)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("SELECT {1}, {2} FROM {0} ORDER BY nombre", tabla, nombre, id);
            b.ExecuteCommandQuery(consulta);
            List<Models.Catalogos> resultado = new List<Models.Catalogos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Models.Catalogos item = new Models.Catalogos
                {
                    Id = int.Parse(reader[1].ToString()),
                    Nombre = reader[0].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Obtiene un registro de una fila de la consulta obtenida
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="idNombre">Nombre del campo Id</param>
        /// <param name="id">Valor numérico del campo id</param>
        /// <returns>Propiedad con los valores obtenidos</returns>
        public static Models.Catalogos SeleccionarPorId(string tabla, string idNombre, string id)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("SELECT * FROM {0} WHERE {1}=@id", tabla, idNombre);
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            Models.Catalogos resultado = new Models.Catalogos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader[0].ToString());
                resultado.Nombre = reader[1].ToString();
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Obtiene registros de una tabla de sub si encuentra datos de la tabla padre
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Models.Catalogos> SeleccionarSubConsulta(string tabla, string campopadre, string id)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("SELECT * FROM {0} WHERE {1} = {2} ORDER BY nombre", tabla, campopadre, id);
            b.ExecuteCommandQuery(consulta);
            List<Models.Catalogos> resultado = new List<Models.Catalogos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Models.Catalogos item = new Models.Catalogos
                {
                    Id = int.Parse(reader[0].ToString()),
                    Nombre = reader[1].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        public static string SeleccionarNombrePorId(string id, string tabla)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("SELECT nombre FROM {0} WHERE id=@id", tabla);
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.SelectString();
        }

        ///// <summary>
        ///// Guarda dos valores dos columnas sin especificación de identidad
        ///// </summary>
        ///// <param name="tabla">Nombre de la tabla</param>
        ///// <param name="columna1">Nombre de la columna 1</param>
        ///// <param name="columna2">Nombre de la columna 2</param>
        //public static void Guardar(string tabla, string valor1, string valor2)
        //{
        //    AccesoDatos b = new AccesoDatos();

        //    string consulta = string.Format("INSERT INTO {0} VALUES(@valor1, @valor2)", tabla);
        //    b.ExecuteCommandQuery(consulta);
        //    b.AddParameter("@valor1", valor1, SqlDbType.NVarChar);
        //    b.AddParameter("@valor2", valor2, SqlDbType.NVarChar);
        //    b.InsertUpdateDelete();
        //}

        /// <summary>
        /// Guarda un valor una columna con especificación de identidad
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="columna1">Nombre de la columna 1</param>
        /// <param name="valor1">Valor para el campo 1</param>
        public static int Guardar(string tabla, string columna1, string valor1)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("INSERT INTO {0} ({1}) VALUES(@valor1)", tabla, columna1);
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@valor1", valor1, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        /// <summary>
        /// Guarda dos valores dos columnas con especificación de identidad
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="columna1">Nombre de la columna 1</param>
        /// <param name="columna2">Nombre de la columna 2</param>
        /// <param name="valor1">Valor para el campo 1</param>
        /// <param name="valor2">Valor para el campo 2</param>
        public static void Guardar(string tabla, string columna1, string columna2, string valor1, string valor2)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("INSERT INTO {0} ({1}, {2}) VALUES(@valor1, @valor2)", tabla, columna1, columna2);
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@valor1", valor1, SqlDbType.NVarChar);
            b.AddParameter("@valor2", valor2, SqlDbType.NVarChar);
            b.InsertUpdateDelete();
        }

        /// <summary>
        /// Guarda tres valores tres columnas con especificación de identidad
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="columna1">Nombre de la columna 1</param>
        /// <param name="columna2">Nombre de la columna 2</param>
        /// <param name="columna3">Nombre de la columna 3</param>
        /// <param name="valor1">Valor para el campo 1</param>
        /// <param name="valor2">Valor para el campo 2</param>
        /// <param name="valor3">Valor para el campo 3</param>
        public static void Guardar(string tabla, string columna1, string columna2, string columna3, string valor1, string valor2, string valor3)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("INSERT INTO {0} ({1}, {2}, {3}) VALUES(@valor1, @valor2, @valor3)", tabla, columna1, columna2, columna3);
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@valor1", valor1, SqlDbType.NVarChar);
            b.AddParameter("@valor2", valor2, SqlDbType.NVarChar);
            b.AddParameter("@valor3", valor3, SqlDbType.NVarChar);
            b.InsertUpdateDelete();
        }

        public static void ModificarNombre(string tabla, string valor1, string valor2)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("UPDATE {0} SET Nombre=@valor2 WHERE id=@valor1", tabla);
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@valor1", valor1, SqlDbType.NVarChar);
            b.AddParameter("@valor2", valor2, SqlDbType.NVarChar);
            b.InsertUpdateDelete();
        }

        /// <summary>
        /// Modifica tabla con dos valores, uno para un valor, otro para un id
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="columnaid"></param>
        /// <param name="valorcolumnaid"></param>
        /// <param name="columnanombre"></param>
        public static void Modificar(string tabla, string columna1, string valor1, string valor2)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("UPDATE {0} SET Nombre=@valor2 WHERE {1}=@valor1", tabla, columna1);
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@valor1", valor1, SqlDbType.NVarChar);
            b.AddParameter("@valor2", valor2, SqlDbType.NVarChar);
            b.InsertUpdateDelete();
        }

        /// <summary>
        /// Modifica tabla con tres valores, dos para dos campos, uno para Id
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="columna1">Nombre del campo Id</param>
        /// <param name="valor1">Valor del campo Id</param>
        /// <param name="columna2">Nombre de la columna2</param>
        /// <param name="valor2">Valor de la columma2</param>
        /// <param name="columna3">Nombre de la columna3</param>
        /// <param name="valor3">Valor de la columna 3</param>
        public static void Modificar(string tabla, string columna1, string valor1, string columna2, string valor2, string columna3, string valor3)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("UPDATE {0} SET {2}=@valor2, {3}=@valor3 WHERE {1}=@valor1", tabla, columna1, columna2, columna3);
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@valor1", valor1, SqlDbType.NVarChar);
            b.AddParameter("@valor2", valor2, SqlDbType.NVarChar);
            b.AddParameter("@valor3", valor3, SqlDbType.NVarChar);
            b.InsertUpdateDelete();
        }

        /// <summary>
        /// Modifica tabla con tres valores, dos para campos, uno para Id
        /// </summary>
        /// <param name="prms">Todos los parámetros (cuidar el orden): nombre de la tabla, nombre del campo id, valor del campo id, nombre columna 2, valor columna 2
        /// nombre columna 3, valor columna 3</param>
        public static void Modificar(params string[] prms)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("UPDATE {0} SET {2}=@valor2, {3}=@valor3 WHERE {1}=@valor1", prms[0], prms[1], prms[3], prms[5]);
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@valor1", prms[2], SqlDbType.NVarChar);
            b.AddParameter("@valor2", prms[4], SqlDbType.NVarChar);
            b.AddParameter("@valor3", prms[6], SqlDbType.NVarChar);
            b.InsertUpdateDelete();
        }

        //Procesos dinámicos para crear los documentos *************************************************
        public static List<Models.DocumentosASAE> SeleccionarClasificacionesDocumentosASAE(string idclasificacion)
        {
            AccesoDatos b = new AccesoDatos();

            string consulta = string.Format("SELECT Id, Nombre, idusuario, fecha, clasificacion, subclasificacion, descripcion, version, vigencia, versionusuarios, fechaoficial " +
            "FROM documentosasae " +
            "WHERE clasificacion=@idclasificacion " +
            "AND SubClasificacion IS NULL");
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idclasificacion", idclasificacion, SqlDbType.Int);
            List<Models.DocumentosASAE> resultado = new List<Models.DocumentosASAE>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Models.DocumentosASAE item = new Models.DocumentosASAE
                {
                    Id = int.Parse(reader["id"].ToString()),
                    Nombre = reader["nombre"].ToString(),
                    IdUsuario = int.Parse(reader["idusuario"].ToString()),
                    Fecha = DateTime.Parse(reader["fecha"].ToString()),
                    Clasificacion = int.Parse(reader["clasificacion"].ToString()),
                    SubClasificacion = reader["subclasificacion"].ToString() == "" ? 0 : int.Parse(reader["subclasificacion"].ToString()),
                    Descripcion = reader["descripcion"].ToString(),
                    Version = reader["version"].ToString(),
                    Vigencia = bool.Parse(reader["vigencia"].ToString()),
                    VersionUsuarios = reader["versionusuarios"].ToString(),
                    FechaOficial = DateTime.Parse(reader["fechaoficial"].ToString())
                };
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        public static List<Models.DocumentosASAE> SeleccionarClasificacionSubClasificacionDocumentosASAE(string idclasificacion, string idsubclasificacion)
        {
            AccesoDatos b = new AccesoDatos();

            b.ExecuteCommandQuery("SELECT Id, Nombre, idusuario, fecha, clasificacion, subclasificacion, descripcion, version, vigencia, versionusuarios, fechaoficial " +
            "FROM documentosasae WHERE clasificacion=@idclasificacion ANd subclasificacion=@idsubclasificacion");
            b.AddParameter("@idclasificacion", idclasificacion, SqlDbType.Int);
            b.AddParameter("@idsubclasificacion", idsubclasificacion, SqlDbType.Int);
            List<Models.DocumentosASAE> resultado = new List<Models.DocumentosASAE>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Models.DocumentosASAE item = new Models.DocumentosASAE();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                item.IdUsuario = int.Parse(reader["idusuario"].ToString());
                item.Fecha = DateTime.Parse(reader["fecha"].ToString());
                item.Clasificacion = int.Parse(reader["clasificacion"].ToString());
                item.SubClasificacion = reader["subclasificacion"].ToString() == "" ? 0 : int.Parse(reader["subclasificacion"].ToString());
                item.Descripcion = reader["descripcion"].ToString();
                item.Version = reader["version"].ToString();
                item.Vigencia = bool.Parse(reader["vigencia"].ToString());
                item.VersionUsuarios = reader["versionusuarios"].ToString();
                item.FechaOficial = DateTime.Parse(reader["fechaoficial"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        //**********************************************************************************************
    }
}
