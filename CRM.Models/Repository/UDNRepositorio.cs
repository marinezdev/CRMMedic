
using CRM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Clase CRUD
    /// </summary>
    public class UDNRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<UDN> Seleccion()
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre, activo FROM udn");
            List<UDN> resultado = new List<UDN>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDN item = new UDN();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                item.Activo = bool.Parse(reader["activo"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<UDN> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre, activo FROM udn WHERE activo=1");
            List<UDN> resultado = new List<UDN>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDN item = new UDN();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected UDN SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT Id,Nombre,activo FROM udn WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            UDN resultado = new UDN();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["Id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.Activo = bool.Parse(reader["activo"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        #region Estadísticas

        //Proyectos
        protected List<UDNEstadisticas> SeleccionarProyectosPorUDN()
        {
            string consulta = "SELECT ISNULL(COUNT(opor.IdUDN),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT join oportunidades opor on opor.IdUDN = udn.Id AND MONTH(opor.fecha) = CONVERT(VARCHAR, MONTH(GETDATE()), 23) " +
            "AND opor.estado=1 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";
            
            b.ExecuteCommandQuery(consulta);            
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UDNEstadisticas> SeleccionarProyectosAcumuladosAlMesPorUDN(string estado)
        {
            string consulta = "SELECT ISNULL(COUNT(opor.IdUDN),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.cierre) IN (1,2,3,4,5,6,7,8,9,10,11,12) " +
            "AND YEAR(opor.cierre) = YEAR(GETDATE()) " +
            "AND opor.estado = @estado " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UDNEstadisticas> SeleccionarProyectosPorUDNCerrados()
        {
            string consulta = "SELECT ISNULL(COUNT(opor.IdUDN),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT join oportunidades opor on opor.IdUDN = udn.Id AND MONTH(opor.fecha) = CONVERT(VARCHAR, MONTH(GETDATE()), 23) " +
            "AND opor.estado=2 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UDNEstadisticas> SeleccionarProyectosAcumuladosAlMesPorUDNCerrados()
        {
            string consulta = "SELECT ISNULL(COUNT(opor.IdUDN),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.fecha) IN (1,2,3,4,5,6,7,8,9,10,11,12) " +
            "AND opor.estado=2 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /* Proyectos e importes por mes acumulado y por mes en curso obtenidos por estado ************************/

        protected List<UDNEstadisticas> SeleccionarProyectosAcumuladosAlMesPorUDNPorEstado(string estado)
        {
            string consulta = "SELECT ISNULL(COUNT(opor.IdUDN),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor on opor.IdUDN = udn.Id " +
            //"AND MONTH(opor.fecha) IN (1,2,3,4,5,6,7,8,9,10,11,12) " +
            //"AND YEAR(opor.fecha)=YEAR(GETDATE()) " +
            "AND opor.estado=@estado " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }
        
        protected List<UDNEstadisticas> SeleccionarProyectosPorUDNEnElMesPorEstado(string estado)
        {
            string consulta = "SELECT ISNULL(COUNT(opor.IdUDN),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.fecha) = MONTH(GETDATE()) " +
            "AND YEAR(opor.fecha)=YEAR(GETDATE()) " +
            "AND opor.estado=@estado " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id"; 
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Selecciona los proyectos en el mes sólo para el estado 3 ó 4
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        protected List<UDNEstadisticas> SeleccionarProyectosEnElMesPorUDNPorEstado34(string estado)
        {
            string consulta1 = "SELECT ISNULL(COUNT(opor.id), 0) AS proyectos, udn.nombre AS UDN " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor ON opor.idudn = udn.id AND opor.estado = @estado " +
            "LEFT JOIN estadooportunidad eo ON eo.idoportunidad = opor.id OR MONTH(eo.fecha) = CONVERT(VARCHAR, MONTH(GETDATE()), 23) AND eo.estado = @estado " +
            "GROUP BY udn.nombre, udn.id";

            string consulta = "SELECT " + 	 
	        "    ( " +
		    "        SELECT COUNT(Oportunidades.Id) " +
		    "        FROM Oportunidades " +
		    "        INNER JOIN estadooportunidad ON Oportunidades.id = EstadoOportunidad.IdOportunidad " +
		    "        WHERE Oportunidades.IdUDN = UDN.Id " +
			"            AND EstadoOportunidad.Estado = @estado " +
			"            AND MONTH(oportunidades.cierre) = MONTH(GETDATE()) " +
            "            AND YEAR(oportunidades.cierre)=YEAR(GETDATE()) " +
	        "    ) AS Proyectos, " +
	        "    UDN.Nombre AS UDN " +
            "FROM UDN";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UDNEstadisticas> SeleccionarImportesPorUDNAlMesPorEstado(string estado)
        {
            string consulta = "SELECT ISNULL(SUM(opor.Importe),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor on opor.IdUDN = udn.Id " +
            //"AND MONTH(opor.fecha) IN(1,2,3,4,5,6,7,8,9,10,11,12) " +
            //"AND YEAR(opor.fecha)=YEAR(GETDATE()) " +
            "AND opor.estado=@estado " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UDNEstadisticas> SeleccionarImportesPorUDNEnElMesPorEstado(string estado)
        {
            string consulta = "SELECT ISNULL(SUM(opor.Importe),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.fecha) = CONVERT(VARCHAR, MONTH(GETDATE()), 23) " +
            "AND YEAR(opor.fecha)=YEAR(GETDATE()) " +
            "AND opor.estado=@estado " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Selecciona los importes en el mes sólo para el estado 3 ó 4
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        protected List<UDNEstadisticas> SeleccionarImportesPorUDNEnElMesPorEstado34(string estado)
        {
            string consulta1 = "SELECT ISNULL(SUM(opor.importe), 0) AS proyectos, udn.nombre AS UDN " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor ON opor.idudn = udn.id AND opor.estado = @estado " +
            "LEFT JOIN estadooportunidad eo ON eo.idoportunidad = opor.id OR MONTH(eo.fecha) = CONVERT(VARCHAR, MONTH(GETDATE()), 23) AND eo.estado = @estado " +
            "GROUP BY udn.nombre, udn.id";

            string consulta = "SELECT " +
            "    ( " +
            "        SELECT ISNULL(SUM(oportunidades.importe), 0) AS Proyectos " +
            "        FROM Oportunidades " +
            "        INNER JOIN estadooportunidad ON Oportunidades.id = EstadoOportunidad.IdOportunidad " +
            "        WHERE Oportunidades.IdUDN = UDN.Id " +
            "            AND EstadoOportunidad.Estado = @estado " +
            "            AND MONTH(oportunidades.cierre) = MONTH(GETDATE()) " +
            "            AND YEAR(oportunidades.cierre)=YEAR(GETDATE()) " +
            "    ) AS Proyectos, " +
            "    UDN.Nombre AS UDN " +
            "FROM UDN";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        /*********************************************************************************************************/

        /* Proyectos e importes por mes acumulado y por mes en curso obtenidos por estado para gráficos **********/

        protected List<Modelos> SeleccionarProyectosImportesAcumuladoPorEstado(string estado, string idudn)
        {
            string consulta = "SELECT udn.nombre AS udn, emp.nombre AS empresa, opor.nombre AS oportunidad, opor.importe AS monto, " +
            "clas.nombre AS clasificacion, sclas.nombre AS subclasificacion, " +
            "usu.nombre + ' ' + usu.apellidopaterno + ' ' + usu.apellidomaterno AS creador," +
            "opor.fecha AS creado, opor.cierre, " +
            "CASE " +
            "   WHEN opor.Estado = 1 AND(DATEDIFF(DAY, opor.cierre, getdate()) <= -16) THEN 'EnProceso1' " +
            "   WHEN opor.Estado = 1 AND(DATEDIFF(DAY, opor.cierre, getdate()) IN(-15, -14, -13, -12, -11, -10, -9, -8, -7)) THEN 'EnProceso2' " +
            "   WHEN opor.Estado = 1 AND(DATEDIFF(DAY, opor.cierre, getdate()) IN(-6, -5, -4, -3, -2, -1)) THEN 'EnProceso3' " +
            "   WHEN opor.Estado = 1 AND(DATEDIFF(DAY, opor.cierre, getdate()) >= 0) THEN 'EnProceso4' " +
            "   WHEN opor.Estado = 3 THEN 'CerradoPerdido' " +
            "   WHEN opor.Estado = 4 THEN 'CerradoGanado' " +
            "   WHEN opor.Estado = 5 THEN 'Terminado' " +
            "   WHEN opor.Estado = 6 THEN 'Cancelado' " +
            "   WHEN opor.Estado = 7 THEN 'Suspendido' " +
            "   WHEN opor.Estado = 8 THEN 'Reasignar' " +
            "   END AS estadoactual, " +
            "opor.id " +
            "FROM oportunidades opor " +
            "INNER JOIN udn ON udn.id = opor.idudn " +
            "INNER JOIN oecu ON oecu.idoportunidad = opor.id " +
            "LEFT JOIN empresas emp ON emp.id = oecu.idempresa " +
            "LEFT JOIN clasificacion clas ON clas.id = opor.idclasificacion " +
            "LEFT JOIN subclasificacion sclas ON sclas.id = opor.idsubclasificacion " +
            "INNER JOIN usuarios usu ON usu.Id=oecu.IdUsuario " +
            "WHERE opor.estado = @estado " +
            "AND opor.idconfiguracion = 2 " +
            "AND MONTH(opor.cierre) IN(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12) " +
            "AND YEAR(opor.cierre)=YEAR(GETDATE()) " +
            "AND opor.idudn = @idudn";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            b.AddParameter("@idudn", idudn, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.UDN.Nombre =  reader["udn"].ToString();
                item.Empresas.Nombre = reader["empresa"].ToString();
                item.Oportunidades.Nombre = reader["oportunidad"].ToString();
                item.Oportunidades.Importe = decimal.Parse(reader["monto"].ToString());
                item.Clasificacion.Nombre = reader["clasificacion"].ToString();
                item.Subclasificacion.Nombre = reader["subclasificacion"].ToString();
                item.Usuarios.Nombre = reader["creador"].ToString();
                item.Oportunidades.Fecha = DateTime.Parse(reader["creado"].ToString());
                item.Oportunidades.Cierre = DateTime.Parse(reader["cierre"].ToString());
                item.Actividades.Nombre = reader["estadoactual"].ToString();
                item.Oportunidades.Id = int.Parse(reader["id"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<Modelos> SeleccionarProyectosImportesEnElMesPorEstado(string estado, string idudn)
        {
            string consulta = "SELECT udn.nombre AS udn, emp.nombre AS empresa, opor.nombre AS oportunidad, opor.importe AS monto, " +
            "clas.nombre AS clasificacion, sclas.nombre AS subclasificacion, " +
            "usu.nombre + ' ' + usu.apellidopaterno + ' ' + usu.apellidomaterno AS creador," +
            "opor.fecha AS creado, opor.cierre, " +
            "CASE " +
            "   WHEN opor.Estado = 1 AND(DATEDIFF(DAY, opor.cierre, getdate()) <= -16) THEN 'EnProceso1' " +
            "   WHEN opor.Estado = 1 AND(DATEDIFF(DAY, opor.cierre, getdate()) IN(-15, -14, -13, -12, -11, -10, -9, -8, -7)) THEN 'EnProceso2' " +
            "   WHEN opor.Estado = 1 AND(DATEDIFF(DAY, opor.cierre, getdate()) IN(-6, -5, -4, -3, -2, -1)) THEN 'EnProceso3' " +
            "   WHEN opor.Estado = 1 AND(DATEDIFF(DAY, opor.cierre, getdate()) >= 0) THEN 'EnProceso4' " +
            "   WHEN opor.Estado = 3 THEN 'CerradoPerdido' " +
            "   WHEN opor.Estado = 4 THEN 'CerradoGanado' " +
            "   WHEN opor.Estado = 5 THEN 'Terminado' " +
            "   WHEN opor.Estado = 6 THEN 'Cancelado' " +
            "   WHEN opor.Estado = 7 THEN 'Suspendido' " +
            "   WHEN opor.Estado = 8 THEN 'Reasignar' " +
            "   END AS estadoactual, " +
            "opor.id " +
            "FROM oportunidades opor " +
            "INNER JOIN udn ON udn.id = opor.idudn " +
            "INNER JOIN oecu ON oecu.idoportunidad = opor.id " +
            "LEFT JOIN empresas emp ON emp.id = oecu.idempresa " +
            "LEFT JOIN clasificacion clas ON clas.id = opor.idclasificacion " +
            "LEFT JOIN subclasificacion sclas ON sclas.id = opor.idsubclasificacion " +
            "INNER JOIN usuarios usu ON usu.Id=oecu.IdUsuario " +
            "WHERE opor.estado = @estado " +
            "AND opor.idconfiguracion = 2 " +
            "AND MONTH(opor.fecha) = MONTH(GETDATE()) " +
            "AND YEAR(opor.fecha)=YEAR(GETDATE()) " +
            "AND opor.idudn=@idudn";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            b.AddParameter("@idudn", idudn, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.UDN.Nombre = reader["udn"].ToString();
                item.Empresas.Nombre = reader["empresa"].ToString();
                item.Oportunidades.Nombre = reader["oportunidad"].ToString();
                item.Oportunidades.Importe = decimal.Parse(reader["monto"].ToString());
                item.Clasificacion.Nombre = reader["clasificacion"].ToString();
                item.Subclasificacion.Nombre = reader["subclasificacion"].ToString();
                item.Usuarios.Nombre = reader["creador"].ToString();
                item.Oportunidades.Fecha = DateTime.Parse(reader["creado"].ToString());
                item.Oportunidades.Cierre = DateTime.Parse(reader["cierre"].ToString());
                item.Actividades.Nombre = reader["estadoactual"].ToString();
                item.Oportunidades.Id = int.Parse(reader["id"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        /*********************************************************************************************************/

        protected List<UDNEstadisticas> SeleccionarProyectosAcumuladosCerradosPerdidosAlMesPorUDN()
        {
            string consulta = "SELECT ISNULL(COUNT(opor.IdUDN),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.cierre) IN (1,2,3,4,5,6,7,8,9,10,11,12) " +
            "AND YEAR(opor.cierre)=YEAR(GETDATE()) " +
            "AND opor.estado=3 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UDNEstadisticas> SeleccionarProyectosAcumuladosCerradosGanadosAlMesPorUDN()
        {
            string consulta = "SELECT ISNULL(COUNT(opor.IdUDN),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.cierre) IN (1,2,3,4,5,6,7,8,9,10,11,12) " +
            "AND YEAR(opor.cierre)=YEAR(GETDATE()) " +
            "AND opor.estado=4 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UDNEstadisticas> SeleccionarProyectosPorUDNCerradosPerdidosEnElMes()
        {
            string consulta = "SELECT ISNULL(COUNT(opor.IdUDN),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT join oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.fecha) = CONVERT(VARCHAR, MONTH(GETDATE()), 23) " +
            "AND opor.estado=3 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UDNEstadisticas> SeleccionarProyectosPorUDNCerradosGanadosEnElMes()
        {
            string consulta = "SELECT ISNULL(COUNT(opor.IdUDN),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT join oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.fecha) = CONVERT(VARCHAR, MONTH(GETDATE()), 23) " +
            "AND opor.estado=4 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        //Importes
        protected List<UDNEstadisticas> SeleccionarImportesPorUDN()
        {
            string consulta = "SELECT ISNULL(SUM(opor.Importe),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT join oportunidades opor on opor.IdUDN = udn.Id AND MONTH(opor.fecha) = CONVERT(VARCHAR, MONTH(GETDATE()), 23) AND opor.estado=1 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UDNEstadisticas> SeleccionarImportesAcumuladosAlMesPorUDN()
        {
            string consulta = "SELECT ISNULL(SUM(opor.Importe),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.cierre) IN(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12) " +
            "AND YEAR(opor.cierre)=YEAR(GETDATE()) " +
            "AND opor.estado=1 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UDNEstadisticas> SeleccionarImportesPorUDNCerrados()
        {
            string consulta = "SELECT ISNULL(SUM(opor.Importe),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT join oportunidades opor on opor.IdUDN = udn.Id AND MONTH(opor.fecha) = CONVERT(VARCHAR, MONTH(GETDATE()), 23) AND opor.estado=2 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        //Montos cerrados perdidos en el mes
        protected List<UDNEstadisticas> SeleccionarImportesPorUDNCerradosPerdidos()
        {
            string consulta = "SELECT ISNULL(SUM(opor.Importe),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT join oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.fecha) = CONVERT(VARCHAR, MONTH(GETDATE()), 23) " +
            "AND opor.estado=3 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }
        //Montos cerrados ganados en el mes
        protected List<UDNEstadisticas> SeleccionarImportesPorUDNCerradosGanados()
        {
            string consulta = "SELECT ISNULL(SUM(opor.Importe),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT join oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.fecha) = CONVERT(VARCHAR, MONTH(GETDATE()), 23) " +
            "AND opor.estado=4 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        //Montos cerrados perdidos al mes
        protected List<UDNEstadisticas> SeleccionarImportesAcumuladosAlMesPorUDNCerradosPerdidos()
        {
            string consulta = "SELECT ISNULL(SUM(opor.Importe),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.cierre) IN(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12) " +
            "AND YEAR(opor.cierre)=YEAR(GETDATE()) " +
            "AND opor.estado=3 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }
        //Montos cerrados ganados al mes
        protected List<UDNEstadisticas> SeleccionarImportesAcumuladosAlMesPorUDNCerradosGanados()
        {
            string consulta = "SELECT ISNULL(SUM(opor.Importe),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT JOIN oportunidades opor on opor.IdUDN = udn.Id " +
            "AND MONTH(opor.cierre) IN(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12) " +
            "AND YEAR(opor.cierre)=YEAR(GETDATE()) " +
            "AND opor.estado=4 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        //----------

        protected List<UDNEstadisticas> SeleccionarImportesAcumuladosAlMesPorUDNCerrados()
        {
            string consulta = "SELECT ISNULL(SUM(opor.Importe),0) AS Proyectos, udn.Nombre AS udn " +
            "FROM udn " +
            "LEFT join oportunidades opor on opor.IdUDN = udn.Id AND MONTH(opor.fecha) IN(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12) " +
            "AND opor.estado=2 " +
            "GROUP BY udn.nombre, udn.id " +
            "ORDER BY udn.id";

            b.ExecuteCommandQuery(consulta);
            List<UDNEstadisticas> resultado = new List<UDNEstadisticas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNEstadisticas item = new UDNEstadisticas();
                item.Proyectos = int.Parse(reader["Proyectos"].ToString());
                item.UDN = reader["UDN"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected List<UDNXMes> SeleccionarProyectosPorMes()
        {
            b.ExecuteCommandQuery("SELECT * FROM ProyectosPorMes");
            List<UDNXMes> resultado = new List<UDNXMes>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNXMes item = new UDNXMes();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Ene = decimal.Parse(reader["Ene"].ToString());
                item.Feb = decimal.Parse(reader["Feb"].ToString());
                item.Mar = decimal.Parse(reader["Mar"].ToString());
                item.Abr = decimal.Parse(reader["Abr"].ToString());
                item.May = decimal.Parse(reader["May"].ToString());
                item.Jun = decimal.Parse(reader["Jun"].ToString());
                item.Jul = decimal.Parse(reader["Jul"].ToString());
                item.Ago = decimal.Parse(reader["Ago"].ToString());
                item.Sep = decimal.Parse(reader["Sep"].ToString());
                item.Oct = decimal.Parse(reader["Oct"].ToString());
                item.Nov = decimal.Parse(reader["Nov"].ToString());
                item.Dic = decimal.Parse(reader["Dic"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        public List<ProyectosPorMesPorAño> SeleccionarProyectosPorMesPorAño()
        {
            b.ExecuteCommandQuery("SELECT * FROM ProyectosPorMesPorAño");
            List<ProyectosPorMesPorAño> resultado = new List<ProyectosPorMesPorAño>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ProyectosPorMesPorAño item = new ProyectosPorMesPorAño();
                item.Id = int.Parse(reader["id"].ToString());
                item.Ene = int.Parse(reader["ene"].ToString());
                item.Feb = int.Parse(reader["feb"].ToString());
                item.Mar = int.Parse(reader["mar"].ToString());
                item.Abr = int.Parse(reader["abr"].ToString());
                item.May = int.Parse(reader["may"].ToString());
                item.Jun = int.Parse(reader["jun"].ToString());
                item.Jul = int.Parse(reader["jul"].ToString());
                item.Ago = int.Parse(reader["ago"].ToString());
                item.Sep = int.Parse(reader["sep"].ToString());
                item.Oct = int.Parse(reader["oct"].ToString());
                item.Nov = int.Parse(reader["nov"].ToString());
                item.Dic = int.Parse(reader["dic"].ToString());
                item.Año = int.Parse(reader["año"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<UDNXMes> SeleccionarImportesPorMes()
        {
            b.ExecuteCommandQuery("SELECT * FROM ImportesPorMes");
            List<UDNXMes> resultado = new List<UDNXMes>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                UDNXMes item = new UDNXMes();
                item.Id = int.Parse(reader["Id"].ToString());
                item.Ene = decimal.Parse(reader["Ene"].ToString());
                item.Feb = decimal.Parse(reader["Feb"].ToString());
                item.Mar = decimal.Parse(reader["Mar"].ToString());
                item.Abr = decimal.Parse(reader["Abr"].ToString());
                item.May = decimal.Parse(reader["May"].ToString());
                item.Jun = decimal.Parse(reader["Jun"].ToString());
                item.Jul = decimal.Parse(reader["Jul"].ToString());
                item.Ago = decimal.Parse(reader["Ago"].ToString());
                item.Sep = decimal.Parse(reader["Sep"].ToString());
                item.Oct = decimal.Parse(reader["Oct"].ToString());
                item.Nov = decimal.Parse(reader["Nov"].ToString());
                item.Dic = decimal.Parse(reader["Dic"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }
        public List<ImportesPorMesPorAño> SeleccionarImportesPorMesPorAño()
        {
            b.ExecuteCommandQuery("SELECT * FROM ImportesPorMesPorAño");
            List<ImportesPorMesPorAño> resultado = new List<ImportesPorMesPorAño>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ImportesPorMesPorAño item = new ImportesPorMesPorAño();
                item.Id = int.Parse(reader["id"].ToString());
                item.Ene = decimal.Parse(reader["ene"].ToString());
                item.Feb = decimal.Parse(reader["feb"].ToString());
                item.Mar = decimal.Parse(reader["mar"].ToString());
                item.Abr = decimal.Parse(reader["abr"].ToString());
                item.May = decimal.Parse(reader["may"].ToString());
                item.Jun = decimal.Parse(reader["jun"].ToString());
                item.Jul = decimal.Parse(reader["jul"].ToString());
                item.Ago = decimal.Parse(reader["ago"].ToString());
                item.Sep = decimal.Parse(reader["sep"].ToString());
                item.Oct = decimal.Parse(reader["oct"].ToString());
                item.Nov = decimal.Parse(reader["nov"].ToString());
                item.Dic = decimal.Parse(reader["dic"].ToString());
                item.Año = int.Parse(reader["año"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }



        protected void ActualizarProyectosImportes()
        {
            b.ExecuteCommandSP("EstadisticasProyectosImportes_Actualizar");
            b.Select();
        }

        #endregion Estadísticas

        protected int Agregar(UDN items)
        {
            string consulta = "INSERT INTO udn (nombre) VALUES(@nombre)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(string nombre, string activo, string id)
        {
            string consulta = "IF(NOT EXISTS(SELECT idudn FROM contactos WHERE idudn = @id) OR NOT EXISTS(SELECT idudn FROM empresas WHERE idudn = @id) OR NOT EXISTS(SELECT idudn FROM oportunidades WHERE idudn = @id)) " +
            " BEGIN UPDATE udn SET nombre=@nombre, activo=@activo WHERE id=@id END ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@activo", activo == "1" ? true : false, SqlDbType.Bit);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        /// <summary>
        /// Elimina la unidad de negocio siempre y cuando no tenga registros asociados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected int Eliminar(string id)
        {
            b.ExecuteCommandQuery("DELETE FROM udn WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
