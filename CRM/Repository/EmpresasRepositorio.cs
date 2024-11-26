using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class EmpresasRepositorio 
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        /// <summary>
        /// Obtener todas las empresas
        /// </summary>
        /// <returns></returns>
        public List<Empresas> Seleccionar(string idconfiguracion)
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre FROM empresas WHERE idconfiguracion=@idconfiguracion ORDER BY nombre");
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Empresas> resultado = new List<Empresas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Empresas item = new Empresas()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        /// <summary>
        /// Obtiene todas las empresas que pertenezcana una configuración en particular
        /// </summary>
        /// <param name="idconfiguracion"></param>
        /// <returns></returns>
        public List<Empresas> SeleccionarTodas(string idconfiguracion)
        {
            b.ExecuteCommandQuery("SELECT emp.Id,emp.Nombre,emp.Telefono,emp.PaginaWeb,emp.Estado,emp.RFC,emp.Sector, emp.InternaExterna " +
            "FROM empresas emp " +
            "INNER JOIN configuracion conf ON conf.id=emp.idconfiguracion " +
            "WHERE emp.idconfiguracion=@idconfiguracion " +
            "ORDER BY nombre");
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Empresas> resultado = new List<Empresas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Empresas item = new Empresas()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Telefono = reader["Telefono"].ToString(),
                    PaginaWeb = reader["PaginaWeb"].ToString(),
                    Estado = reader["Estado"].ToString() == "" ? 0 : int.Parse(reader["Estado"].ToString()),
                    RFC = reader["RFC"].ToString(),
                    Sector = reader["Sector"].ToString() == "" ? 0 : int.Parse(reader["Sector"].ToString()),
                    InternaExterna = int.Parse(reader["InternaExterna"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Empresas> Buscar(string nombre)
        {
            b.ExecuteCommandQuery("SELECT * FROM empresas WHERE nombre LIKE @nombre");
            b.AddParameter("@nombre", "%" + nombre + "%", SqlDbType.NVarChar);
            List<Empresas> resultado = new List<Empresas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Empresas item = new Empresas()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Telefono = reader["Telefono"].ToString(),
                    Correo = reader["Correo"].ToString(),
                    PaginaWeb = reader["PaginaWeb"].ToString(),
                    Direccion = reader["Direccion"].ToString(),
                    Ciudad = reader["Ciudad"].ToString(),
                    Estado = int.Parse(reader["Estado"].ToString()),
                    Prospecto = bool.Parse(reader["Prospecto"].ToString()),
                    Cliente = bool.Parse(reader["Cliente"].ToString()),
                    Competidor = bool.Parse(reader["Competidor"].ToString()),
                    Sector = int.Parse(reader["Sector"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public Empresas SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT * FROM empresas WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            Empresas resultado = new Empresas();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id            = int.Parse(reader["Id"].ToString());
                resultado.Nombre        = reader["Nombre"].ToString();
                resultado.Telefono      = reader["Telefono"].ToString();
                resultado.Correo        = reader["Correo"].ToString();
                resultado.PaginaWeb     = reader["PaginaWeb"].ToString();
                resultado.Direccion     = reader["Direccion"].ToString();
                resultado.Ciudad        = reader["Ciudad"].ToString();
                resultado.Estado        = reader["Estado"].ToString() == "" ? 0 : int.Parse(reader["Estado"].ToString());
                resultado.Prospecto     = reader["Prospecto"].ToString() == "" ? false : bool.Parse(reader["Prospecto"].ToString());
                resultado.Cliente       = reader["Cliente"].ToString() == "" ? false : bool.Parse(reader["Cliente"].ToString());
                resultado.Competidor    = reader["Competidor"].ToString() == "" ? false : bool.Parse(reader["Competidor"].ToString());
                resultado.Sector        = reader["Sector"].ToString() == "" ? 0 : int.Parse(reader["Sector"].ToString());
                resultado.RFC           = reader["RFC"].ToString();
                resultado.InternaExterna = int.Parse(reader["InternaExterna"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Usuarios> SeleccionarusuariosPorEmpresaId(string id)
        {
            b.ExecuteCommandQuery("SELECT usu.Id, usu.Nombre " +
            "FROM empresasusuarios eu " +
            "INNER JOIN empresas emp ON emp.id = eu.IdEmpresa " +
            "INNER JOIN usuarios usu ON usu.id = eu.IdUsuario " +
            "WHERE emp.id =@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            List<Usuarios> resultado = new List<Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Usuarios item = new Usuarios()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public Empresas SeleccionarDireccionEmpresa(string id)
        {
            b.ExecuteCommandQuery("SELECT Id, Direccion, ciudad, estado FROM empresas WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            Empresas resultado = new Empresas();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Direccion = reader["Direccion"].ToString();
                resultado.Ciudad = reader["Ciudad"].ToString();
                resultado.Estado = reader["Estado"].ToString() == "" ? 0 : int.Parse(reader["Estado"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Empresas> SeleccionarParecidas(string nombre)
        {
            string consulta = "SELECT  id, nombre FROM empresas WHERE nombre like '%' + @nombre + '%'";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", nombre, SqlDbType.NVarChar);
            List<Empresas> resultado = new List<Empresas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Empresas item = new Empresas()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarEmpresasEnTemasEnProceso(string idconfiguracion)
        {
            string consulta = "SELECT opor.Id, emp.Nombre AS Empresa, opor.Nombre, opor.Estado, cla.Nombre AS Clasificacion, scla.Nombre AS SubClasificacion, opor.Fecha, opor.Cierre, " +
            "DATEDIFF(DAY,opor.Cierre, getdate()) DiasTranscurridos, " +
            "CASE " +
            "WHEN opor.Estado=1 AND DATEDIFF(DAY,opor.cierre,getdate()) <= -60 THEN 'EnProceso1' " +
            "WHEN opor.estado=1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-59,-58,-57,-56,-55,-54,-53,-52,-51, -50,-49,-48,-47,-46,-45,-44,-43,-42,-41,-40,-39,-38,-37,-36,-35,-34,-33,-32,-31)) THEN 'EnProceso2' " +
            "WHEN opor.estado=1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-30,-29,-28,-27,-26,-25,-24,-23,-22,-21,-20,-19,-18,-17,-16,-15,-14,-13,-12,-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1)) THEN 'EnProceso3' " +
            "WHEN opor.estado=1 AND DATEDIFF(DAY, opor.cierre, getdate()) >= 0 THEN 'EnProceso4' " +
            "WHEN opor.estado=2 THEN 'Cerrado' " +
            "WHEN opor.estado=5 THEN 'Terminado' " +
            "WHEN opor.estado=6 THEN 'Cancelado' " +
            "WHEN opor.estado=7 THEN 'Suspendido' " +
            "WHEN opor.estado=8 THEN 'Reasignar' " +
            "END AS EstadoActual  " +
            "FROM oportunidades opor " +
            "INNER JOIN Clasificacion cla ON cla.Id=opor.IdClasificacion " +
            "INNER JOIN SubClasificacion scla ON scla.Id=opor.IdSubClasificacion " +
            "LEFT JOIN oecu ON oecu.IdOportunidad = opor.id " +
            "LEFT JOIN Empresas emp ON (emp.id=oecu.IdEmpresa AND oecu.IdOportunidad = opor.id) " +
            "WHERE oecu.idconfiguracion=@idconfiguracion";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Oportunidades.Nombre = reader["Nombre"].ToString();
                item.Oportunidades.Estado = int.Parse(reader["Estado"].ToString());
                item.Oportunidades.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Oportunidades.Cierre = DateTime.Parse(reader["Cierre"].ToString());
                item.Clasificacion.Nombre = reader["Clasificacion"].ToString();
                item.Subclasificacion.Nombre = reader["SubClasificacion"].ToString();

                item.Bitacora.NombreEstado = reader["EstadoActual"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarEmpresasTemasDetalle1(string idconfiguracion)
        {
            string consulta = "SELECT opor.Id, emp.Nombre AS Empresa, opor.Nombre, opor.Estado, cla.Nombre AS Clasificacion, scla.Nombre AS SubClasificacion, opor.Fecha, opor.Cierre, " +
            "DATEDIFF(DAY,opor.Cierre, getdate()) DiasTranscurridos, " +
            "CASE " +
            "WHEN opor.Estado=1 AND DATEDIFF(DAY,opor.cierre,getdate()) <= -60 THEN 'EnProceso1' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-59,-58,-57,-56,-55,-54,-53,-52,-51, -50,-49,-48,-47,-46,-45,-44,-43,-42,-41,-40,-39,-38,-37,-36,-35,-34,-33,-32,-31)) THEN 'EnProceso2' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-30,-29,-28,-27,-26,-25,-24,-23,-22,-21,-20,-19,-18,-17,-16,-15,-14,-13,-12,-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1)) THEN 'EnProceso3' " +
            "WHEN opor.estado = 1 AND DATEDIFF(DAY, opor.cierre, getdate()) >= 0 THEN 'EnProceso4' " +
            "WHEN opor.estado = 2 THEN 'Cerrado' " +
            "WHEN opor.estado = 5 THEN 'Terminado' " +
            "WHEN opor.estado = 6 THEN 'Cancelado' " +
            "WHEN opor.estado = 7 THEN 'Suspendido' " +
            "WHEN opor.estado = 8 THEN 'Reasignar' " +
            "END AS EstadoActual  " +
            "FROM oportunidades opor " +
            "INNER JOIN Clasificacion cla ON cla.Id=opor.IdClasificacion " +
            "INNER JOIN SubClasificacion scla ON scla.Id=opor.IdSubClasificacion " +
            "LEFT JOIN oecu ON oecu.IdOportunidad = opor.id " +
            "LEFT JOIN Empresas emp ON (emp.id=oecu.IdEmpresa AND oecu.IdOportunidad = opor.id) " +
            "WHERE opor.Estado=1 AND DATEDIFF(DAY,opor.cierre,getdate()) <= -16 " +
            "AND oecu.idconfiguracion=@idconfiguracion";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Oportunidades.Nombre = reader["Nombre"].ToString();
                item.Oportunidades.Estado = int.Parse(reader["Estado"].ToString());
                item.Oportunidades.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Oportunidades.Cierre = DateTime.Parse(reader["Cierre"].ToString());
                item.Clasificacion.Nombre = reader["Clasificacion"].ToString();
                item.Subclasificacion.Nombre = reader["SubClasificacion"].ToString();

                item.Bitacora.NombreEstado = reader["EstadoActual"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarEmpresasTemasDetalle12(string idconfiguracion)
        {
            string consulta = "SELECT opor.Id, emp.Nombre AS Empresa, opor.Nombre, opor.Estado, cla.Nombre AS Clasificacion, scla.Nombre AS SubClasificacion, opor.Fecha, opor.Cierre, " +
            "DATEDIFF(DAY,opor.Cierre, getdate()) DiasTranscurridos, " +
            "CASE " +
            "WHEN opor.Estado=1 AND DATEDIFF(DAY,opor.cierre,getdate()) <= -60 THEN 'EnProceso1' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-59,-58,-57,-56,-55,-54,-53,-52,-51, -50,-49,-48,-47,-46,-45,-44,-43,-42,-41,-40,-39,-38,-37,-36,-35,-34,-33,-32,-31)) THEN 'EnProceso2' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-30,-29,-28,-27,-26,-25,-24,-23,-22,-21,-20,-19,-18,-17,-16,-15,-14,-13,-12,-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1)) THEN 'EnProceso3' " +
            "WHEN opor.estado = 1 AND DATEDIFF(DAY, opor.cierre, getdate()) >= 0 THEN 'EnProceso4' " +
            "WHEN opor.estado = 2 THEN 'Cerrado' " +
            "WHEN opor.estado = 5 THEN 'Terminado' " +
            "WHEN opor.estado = 6 THEN 'Cancelado' " +
            "WHEN opor.estado = 7 THEN 'Suspendido' " +
            "WHEN opor.estado = 8 THEN 'Reasignar' " +
            "END AS EstadoActual  " +
            "FROM oportunidades opor " +
            "INNER JOIN Clasificacion cla ON cla.Id=opor.IdClasificacion " +
            "INNER JOIN SubClasificacion scla ON scla.Id=opor.IdSubClasificacion " +
            "LEFT JOIN oecu ON oecu.IdOportunidad = opor.id " +
            "LEFT JOIN Empresas emp ON (emp.id=oecu.IdEmpresa AND oecu.IdOportunidad = opor.id) " +
            "WHERE opor.Estado=1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-15,-14,-13,-12,-11,-10,-9,-8,-7)) " +
            "AND oecu.idconfiguracion=@idconfiguracion";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Oportunidades.Nombre = reader["Nombre"].ToString();
                item.Oportunidades.Estado = int.Parse(reader["Estado"].ToString());
                item.Oportunidades.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Oportunidades.Cierre = DateTime.Parse(reader["Cierre"].ToString());
                item.Clasificacion.Nombre = reader["Clasificacion"].ToString();
                item.Subclasificacion.Nombre = reader["SubClasificacion"].ToString();

                item.Bitacora.NombreEstado = reader["EstadoActual"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarEmpresasTemasDetalle13(string idconfiguracion)
        {
            string consulta = "SELECT opor.Id, emp.Nombre AS Empresa, opor.Nombre, opor.Estado, cla.Nombre AS Clasificacion, scla.Nombre AS SubClasificacion, opor.Fecha, opor.Cierre, " +
            "DATEDIFF(DAY,opor.Cierre, getdate()) DiasTranscurridos, " +
            "CASE " +
            "WHEN opor.Estado=1 AND DATEDIFF(DAY,opor.cierre,getdate()) <= -60 THEN 'EnProceso1' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-59,-58,-57,-56,-55,-54,-53,-52,-51, -50,-49,-48,-47,-46,-45,-44,-43,-42,-41,-40,-39,-38,-37,-36,-35,-34,-33,-32,-31)) THEN 'EnProceso2' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-30,-29,-28,-27,-26,-25,-24,-23,-22,-21,-20,-19,-18,-17,-16,-15,-14,-13,-12,-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1)) THEN 'EnProceso3' " +
            "WHEN opor.estado = 1 AND DATEDIFF(DAY, opor.cierre, getdate()) >= 0 THEN 'EnProceso4' " +
            "WHEN opor.estado = 2 THEN 'Cerrado' " +
            "WHEN opor.estado = 5 THEN 'Terminado' " +
            "WHEN opor.estado = 6 THEN 'Cancelado' " +
            "WHEN opor.estado = 7 THEN 'Suspendido' " +
            "WHEN opor.estado = 8 THEN 'Reasignar' " +
            "END AS EstadoActual  " +
            "FROM oportunidades opor " +
            "INNER JOIN Clasificacion cla ON cla.Id=opor.IdClasificacion " +
            "INNER JOIN SubClasificacion scla ON scla.Id=opor.IdSubClasificacion " +
            "LEFT JOIN oecu ON oecu.IdOportunidad = opor.id " +
            "LEFT JOIN Empresas emp ON (emp.id=oecu.IdEmpresa AND oecu.IdOportunidad = opor.id) " +
            "WHERE opor.Estado=1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-6,-5,-4,-3,-2,-1)) " +
            "AND oecu.idconfiguracion=@idconfiguracion";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Oportunidades.Nombre = reader["Nombre"].ToString();
                item.Oportunidades.Estado = int.Parse(reader["Estado"].ToString());
                item.Oportunidades.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Oportunidades.Cierre = DateTime.Parse(reader["Cierre"].ToString());
                item.Clasificacion.Nombre = reader["Clasificacion"].ToString();
                item.Subclasificacion.Nombre = reader["SubClasificacion"].ToString();

                item.Bitacora.NombreEstado = reader["EstadoActual"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarEmpresasTemasDetalle14(string idconfiguracion)
        {
            string consulta = "SELECT opor.Id, emp.Nombre AS Empresa, opor.Nombre, opor.Estado, cla.Nombre AS Clasificacion, scla.Nombre AS SubClasificacion, opor.Fecha, opor.Cierre, " +
            "DATEDIFF(DAY,opor.Cierre, getdate()) DiasTranscurridos, " +
            "CASE " +
            "WHEN opor.Estado=1 AND DATEDIFF(DAY,opor.cierre,getdate()) <= -60 THEN 'EnProceso1' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-59,-58,-57,-56,-55,-54,-53,-52,-51, -50,-49,-48,-47,-46,-45,-44,-43,-42,-41,-40,-39,-38,-37,-36,-35,-34,-33,-32,-31)) THEN 'EnProceso2' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-30,-29,-28,-27,-26,-25,-24,-23,-22,-21,-20,-19,-18,-17,-16,-15,-14,-13,-12,-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1)) THEN 'EnProceso3' " +
            "WHEN opor.estado = 1 AND DATEDIFF(DAY, opor.cierre, getdate()) >= 0 THEN 'EnProceso4' " +
            "WHEN opor.estado = 2 THEN 'Cerrado' " +
            "WHEN opor.estado = 5 THEN 'Terminado' " +
            "WHEN opor.estado = 6 THEN 'Cancelado' " +
            "WHEN opor.estado = 7 THEN 'Suspendido' " +
            "WHEN opor.estado = 8 THEN 'Reasignar' " +
            "END AS EstadoActual  " +
            "FROM oportunidades opor " +
            "INNER JOIN Clasificacion cla ON cla.Id=opor.IdClasificacion " +
            "INNER JOIN SubClasificacion scla ON scla.Id=opor.IdSubClasificacion " +
            "LEFT JOIN oecu ON oecu.IdOportunidad = opor.id " +
            "LEFT JOIN Empresas emp ON (emp.id=oecu.IdEmpresa AND oecu.IdOportunidad = opor.id) " +
            "WHERE opor.Estado=1 AND DATEDIFF(DAY, opor.cierre, getdate()) >= 0 " +
            "AND oecu.idconfiguracion=@idconfiguracion";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Oportunidades.Nombre = reader["Nombre"].ToString();
                item.Oportunidades.Estado = int.Parse(reader["Estado"].ToString());
                item.Oportunidades.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Oportunidades.Cierre = DateTime.Parse(reader["Cierre"].ToString());
                item.Clasificacion.Nombre = reader["Clasificacion"].ToString();
                item.Subclasificacion.Nombre = reader["SubClasificacion"].ToString();

                item.Bitacora.NombreEstado = reader["EstadoActual"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarEmpresasTemasDetalle2(string idconfiguracion)
        {
            string consulta = "SELECT opor.Id, emp.Nombre AS Empresa, opor.Nombre, opor.Estado, cla.Nombre AS Clasificacion, scla.Nombre AS SubClasificacion, opor.Fecha, opor.Cierre, " +
            "DATEDIFF(DAY,opor.Cierre, getdate()) DiasTranscurridos, " +
            "CASE " +
            "WHEN opor.Estado=1 AND DATEDIFF(DAY,opor.cierre,getdate()) <= -60 THEN 'EnProceso1' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-59,-58,-57,-56,-55,-54,-53,-52,-51, -50,-49,-48,-47,-46,-45,-44,-43,-42,-41,-40,-39,-38,-37,-36,-35,-34,-33,-32,-31)) THEN 'EnProceso2' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-30,-29,-28,-27,-26,-25,-24,-23,-22,-21,-20,-19,-18,-17,-16,-15,-14,-13,-12,-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1)) THEN 'EnProceso3' " +
            "WHEN opor.estado = 1 AND DATEDIFF(DAY, opor.cierre, getdate()) >= 0 THEN 'EnProceso4' " +
            "WHEN opor.estado = 2 THEN 'Cerrado' " +
            "WHEN opor.estado = 5 THEN 'Terminado' " +
            "WHEN opor.estado = 6 THEN 'Cancelado' " +
            "WHEN opor.estado = 7 THEN 'Suspendido' " +
            "WHEN opor.estado = 8 THEN 'Reasignar' " +
            "END AS EstadoActual  " +
            "FROM oportunidades opor " +
            "INNER JOIN Clasificacion cla ON cla.Id=opor.IdClasificacion " +
            "INNER JOIN SubClasificacion scla ON scla.Id=opor.IdSubClasificacion " +
            "LEFT JOIN oecu ON oecu.IdOportunidad = opor.id " +
            "LEFT JOIN Empresas emp ON (emp.id=oecu.IdEmpresa AND oecu.IdOportunidad = opor.id) " +
            "WHERE opor.Estado=2 " +
            "AND oecu.idconfiguracion=@idconfiguracion";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Oportunidades.Nombre = reader["Nombre"].ToString();
                item.Oportunidades.Estado = int.Parse(reader["Estado"].ToString());
                item.Oportunidades.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Oportunidades.Cierre = DateTime.Parse(reader["Cierre"].ToString());
                item.Clasificacion.Nombre = reader["Clasificacion"].ToString();
                item.Subclasificacion.Nombre = reader["SubClasificacion"].ToString();

                item.Bitacora.NombreEstado = reader["EstadoActual"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarEmpresasTemasDetalle5(string idconfiguracion)
        {
            string consulta = "SELECT opor.Id, emp.Nombre AS Empresa, opor.Nombre, opor.Estado, cla.Nombre AS Clasificacion, scla.Nombre AS SubClasificacion, opor.Fecha, opor.Cierre, " +
            "DATEDIFF(DAY,opor.Cierre, getdate()) DiasTranscurridos, " +
            "CASE " +
            "WHEN opor.Estado=1 AND DATEDIFF(DAY,opor.cierre,getdate()) <= -60 THEN 'EnProceso1' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-59,-58,-57,-56,-55,-54,-53,-52,-51, -50,-49,-48,-47,-46,-45,-44,-43,-42,-41,-40,-39,-38,-37,-36,-35,-34,-33,-32,-31)) THEN 'EnProceso2' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-30,-29,-28,-27,-26,-25,-24,-23,-22,-21,-20,-19,-18,-17,-16,-15,-14,-13,-12,-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1)) THEN 'EnProceso3' " +
            "WHEN opor.estado = 1 AND DATEDIFF(DAY, opor.cierre, getdate()) >= 0 THEN 'EnProceso4' " +
            "WHEN opor.estado = 2 THEN 'Cerrado' " +
            "WHEN opor.estado = 5 THEN 'Terminado' " +
            "WHEN opor.estado = 6 THEN 'Cancelado' " +
            "WHEN opor.estado = 7 THEN 'Suspendido' " +
            "WHEN opor.estado = 8 THEN 'Reasignar' " +
            "END AS EstadoActual  " +
            "FROM oportunidades opor " +
            "INNER JOIN Clasificacion cla ON cla.Id=opor.IdClasificacion " +
            "INNER JOIN SubClasificacion scla ON scla.Id=opor.IdSubClasificacion " +
            "LEFT JOIN oecu ON oecu.IdOportunidad = opor.id " +
            "LEFT JOIN Empresas emp ON (emp.id=oecu.IdEmpresa AND oecu.IdOportunidad = opor.id) " +
            "WHERE opor.Estado=5 " +
            "AND oecu.idconfiguracion=@idconfiguracion";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Oportunidades.Nombre = reader["Nombre"].ToString();
                item.Oportunidades.Estado = int.Parse(reader["Estado"].ToString());
                item.Oportunidades.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Oportunidades.Cierre = DateTime.Parse(reader["Cierre"].ToString());
                item.Clasificacion.Nombre = reader["Clasificacion"].ToString();
                item.Subclasificacion.Nombre = reader["SubClasificacion"].ToString();

                item.Bitacora.NombreEstado = reader["EstadoActual"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarEmpresasTemasDetalle6(string idconfiguracion)
        {
            string consulta = "SELECT opor.Id, emp.Nombre AS Empresa, opor.Nombre, opor.Estado, cla.Nombre AS Clasificacion, scla.Nombre AS SubClasificacion, opor.Fecha, opor.Cierre, " +
            "DATEDIFF(DAY,opor.Cierre, getdate()) DiasTranscurridos, " +
            "CASE " +
            "WHEN opor.Estado=1 AND DATEDIFF(DAY,opor.cierre,getdate()) <= -60 THEN 'EnProceso1' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-59,-58,-57,-56,-55,-54,-53,-52,-51, -50,-49,-48,-47,-46,-45,-44,-43,-42,-41,-40,-39,-38,-37,-36,-35,-34,-33,-32,-31)) THEN 'EnProceso2' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-30,-29,-28,-27,-26,-25,-24,-23,-22,-21,-20,-19,-18,-17,-16,-15,-14,-13,-12,-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1)) THEN 'EnProceso3' " +
            "WHEN opor.estado = 1 AND DATEDIFF(DAY, opor.cierre, getdate()) >= 0 THEN 'EnProceso4' " +
            "WHEN opor.estado = 2 THEN 'Cerrado' " +
            "WHEN opor.estado = 5 THEN 'Terminado' " +
            "WHEN opor.estado = 6 THEN 'Cancelado' " +
            "WHEN opor.estado = 7 THEN 'Suspendido' " +
            "WHEN opor.estado = 8 THEN 'Reasignar' " +
            "END AS EstadoActual  " +
            "FROM oportunidades opor " +
            "INNER JOIN Clasificacion cla ON cla.Id=opor.IdClasificacion " +
            "INNER JOIN SubClasificacion scla ON scla.Id=opor.IdSubClasificacion " +
            "LEFT JOIN oecu ON oecu.IdOportunidad = opor.id " +
            "LEFT JOIN Empresas emp ON (emp.id=oecu.IdEmpresa AND oecu.IdOportunidad = opor.id) " +
            "WHERE opor.Estado=6 " +
            "AND oecu.idconfiguracion=@idconfiguracion";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Oportunidades.Nombre = reader["Nombre"].ToString();
                item.Oportunidades.Estado = int.Parse(reader["Estado"].ToString());
                item.Oportunidades.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Oportunidades.Cierre = DateTime.Parse(reader["Cierre"].ToString());
                item.Clasificacion.Nombre = reader["Clasificacion"].ToString();
                item.Subclasificacion.Nombre = reader["SubClasificacion"].ToString();

                item.Bitacora.NombreEstado = reader["EstadoActual"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarEmpresasTemasDetalle7(string idconfiguracion)
        {
            string consulta = "SELECT opor.Id, emp.Nombre AS Empresa, opor.Nombre, opor.Estado, cla.Nombre AS Clasificacion, scla.Nombre AS SubClasificacion, opor.Fecha, opor.Cierre, " +
            "DATEDIFF(DAY,opor.Cierre, getdate()) DiasTranscurridos, " +
            "CASE " +
            "WHEN opor.Estado=1 AND DATEDIFF(DAY,opor.cierre,getdate()) <= -60 THEN 'EnProceso1' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-59,-58,-57,-56,-55,-54,-53,-52,-51, -50,-49,-48,-47,-46,-45,-44,-43,-42,-41,-40,-39,-38,-37,-36,-35,-34,-33,-32,-31)) THEN 'EnProceso2' " +
            "WHEN opor.estado = 1 AND (DATEDIFF(DAY, opor.cierre, getdate()) IN (-30,-29,-28,-27,-26,-25,-24,-23,-22,-21,-20,-19,-18,-17,-16,-15,-14,-13,-12,-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1)) THEN 'EnProceso3' " +
            "WHEN opor.estado = 1 AND DATEDIFF(DAY, opor.cierre, getdate()) >= 0 THEN 'EnProceso4' " +
            "WHEN opor.estado = 2 THEN 'Cerrado' " +
            "WHEN opor.estado = 5 THEN 'Terminado' " +
            "WHEN opor.estado = 6 THEN 'Cancelado' " +
            "WHEN opor.estado = 7 THEN 'Suspendido' " +
            "WHEN opor.estado = 8 THEN 'Reasignar' " +
            "END AS EstadoActual  " +
            "FROM oportunidades opor " +
            "INNER JOIN Clasificacion cla ON cla.Id=opor.IdClasificacion " +
            "INNER JOIN SubClasificacion scla ON scla.Id=opor.IdSubClasificacion " +
            "LEFT JOIN oecu ON oecu.IdOportunidad = opor.id " +
            "LEFT JOIN Empresas emp ON (emp.id=oecu.IdEmpresa AND oecu.IdOportunidad = opor.id) " +
            "WHERE opor.Estado=7 " +
            "AND oecu.idconfiguracion=@idconfiguracion";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Oportunidades.Nombre = reader["Nombre"].ToString();
                item.Oportunidades.Estado = int.Parse(reader["Estado"].ToString());
                item.Oportunidades.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                item.Oportunidades.Cierre = DateTime.Parse(reader["Cierre"].ToString());
                item.Clasificacion.Nombre = reader["Clasificacion"].ToString();
                item.Subclasificacion.Nombre = reader["SubClasificacion"].ToString();

                item.Bitacora.NombreEstado = reader["EstadoActual"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        /// <summary>
        /// Agregar un nueva empresa
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int Agregar(Empresas items)
        {
            string consulta = "IF EXISTS(SELECT rfc FROM empresas WHERE idconfiguracion=@idconfiguracion AND rfc=@rfc) " +
            "BEGIN " +
            "   SELECT 0 " +
            "END " +
            "ELSE " +
            "   BEGIN " +
            "       INSERT INTO empresas (nombre, rfc, internaexterna,idconfiguracion) VALUES(@nombre,@rfc,@internaexterna,@idconfiguracion); " +
            "       SELECT @@IDENTITY; " +
            "   END";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@rfc", items.RFC.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@internaexterna", items.InternaExterna, SqlDbType.Int);
            b.AddParameter("@idconfiguracion", items.IdConfiguracion, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }

        public int AgregarCompleto(Empresas items)
        {
            string consulta = "IF EXISTS(SELECT rfc FROM empresas WHERE idconfiguracion=@idconfiguracion AND rfc=@rfc) " +
            "BEGIN " +
            "   SELECT 0 " +
            "END " +
            "ELSE " +
            "   BEGIN " +
            "       INSERT INTO empresas (nombre, rfc, internaexterna,direccion,ciudad,estado,sector,telefono,paginaweb,idconfiguracion) " +
            "       VALUES(@nombre,@rfc,@internaexterna,@direccion,@ciudad,@estado,@sector,@telefono,@paginaweb,@idconfiguracion); " +
            "       SELECT @@IDENTITY; " +
            "   END";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@rfc", items.RFC.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@internaexterna", items.InternaExterna, SqlDbType.Int);
            b.AddParameter("@direccion", items.Ciudad, SqlDbType.NVarChar);
            b.AddParameter("@ciudad", items.Ciudad, SqlDbType.NVarChar);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@sector", items.Sector, SqlDbType.Int);
            b.AddParameter("@telefono", items.Telefono, SqlDbType.NVarChar);
            b.AddParameter("@paginaweb", items.PaginaWeb, SqlDbType.NVarChar);
            b.AddParameter("@idconfiguracion", items.IdConfiguracion, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }

        public int AgregarYContinuar(Empresas items)
        {
            b.ExecuteCommandQuery("INSERT INTO empresas (nombre, rfc,idconfiguracion) " +
            "VALUES(@nombre,@rfc,@idconfiguracion); " +
            "SELECT @@IDENTITY");
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@rfc", items.RFC, SqlDbType.NVarChar);
            b.AddParameter("@idconfiguracion", items.IdConfiguracion, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }

        public bool Modificar(Empresas items)
        {
            b.ExecuteCommandQuery("UPDATE empresas SET nombre=@nombre," +
            "telefono=@telefono," +
            //"correo=@correo," +
            "paginaweb=@paginaweb," +
            "direccion=@direccion," +
            "ciudad=@ciudad," +
            "estado=@estado," +
            //"prospecto=@prospecto,cliente=@cliente,competidor=@competidor," +
            "sector=@sector, " +
            "rfc=@rfc, " +
            "internaexterna=@internaexterna " +            
            "WHERE id=@id");
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@telefono", items.Telefono, SqlDbType.NVarChar);
            //b.AddParameter("@correo", items.Correo, SqlDbType.NVarChar);
            b.AddParameter("@paginaweb", items.PaginaWeb, SqlDbType.NVarChar);
            b.AddParameter("@direccion", items.Direccion, SqlDbType.NVarChar);
            b.AddParameter("@ciudad", items.Ciudad, SqlDbType.NVarChar);
            b.AddParameter("@estado", items.Estado == 0 ? 33 : items.Estado, SqlDbType.Int);
            //b.AddParameter("@prospecto", items.Prospecto, SqlDbType.Bit);
            //b.AddParameter("@cliente", items.Cliente, SqlDbType.Bit);
            //b.AddParameter("@competidor", items.Competidor, SqlDbType.Bit);
            b.AddParameter("@sector", items.Sector == 0 ? 11 : items.Sector, SqlDbType.Int);
            b.AddParameter("@rfc", items.RFC, SqlDbType.NVarChar);
            b.AddParameter("@internaexterna", items.InternaExterna, SqlDbType.Int);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            if (b.InsertUpdateDelete() >= 1)
                return true;
            else
                return false;
        }
    }
}