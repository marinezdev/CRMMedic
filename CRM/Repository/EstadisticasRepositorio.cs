using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    /// <summary>
    /// Lo que tiene que ver con obtener cómo se conectan los datos de un lado para otro y obtener informes
    /// </summary>
    public class EstadisticasRepositorio 
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public EstadisticasTablas TablasContenido(string idconfiguracion)
        {
            b.ExecuteCommandQuery("SELECT " +
            "(SELECT COUNT(con.Id) FROM contactos con INNER JOIN OportunidadesResponsables opor ON opor.IdAsignado = con.id INNER JOIN oportunidades opo ON opo.id = opor.IdOportunidad WHERE con.idconfiguracion=@idconfiguracion) AS Contactos, " +
            "(SELECT count(emp.id) FROM Oecu INNER JOIN oportunidades opo ON opo.id = oecu.IdOportunidad INNER JOIN Empresas emp ON emp.id = oecu.IdEmpresa WHERE emp.idconfiguracion=@idconfiguracion) AS Empresas, " +
            "(SELECT COUNT(1) FROM tareas) AS Tareas, " +
            "(SELECT COUNT(1) FROM oportunidades WHERE idconfiguracion=@idconfiguracion) AS Oportunidades, " +
            "(SELECT COUNT(1) FROM usuariosroles WHERE idrol=4) AS Comerciales");
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            EstadisticasTablas resultado = new EstadisticasTablas();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Contactos = int.Parse(reader["Contactos"].ToString());
                resultado.Empresas = int.Parse(reader["Empresas"].ToString());
                resultado.Tareas = int.Parse(reader["Tareas"].ToString());
                resultado.Oportunidades = int.Parse(reader["Oportunidades"].ToString());
                resultado.Comerciales = int.Parse(reader["Comerciales"].ToString());

            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarEmpresasContactosOportunidadesTareas()
        {
            string consulta = "SELECT emp.Nombre As Empresa, con.Nombre + ' ' + con.ApellidoPaterno AS Contacto, opo.Nombre AS Oportunidad, tar.Asunto AS Tarea " +
            "FROM oecu " +
            "LEFT JOIN tuce ON oecu.idempresa = tuce.idempresa " +
            "INNER JOIN contactos con ON oecu.IdContacto = con.id " +
            "INNER JOIN empresas emp ON oecu.IdEmpresa = emp.id " +
            "LEFT JOIN Oportunidades opo ON opo.id = oecu.idoportunidad AND opo.Etapa=1 " +
            "LEFT JOIN tareas tar ON tar.id = tuce.IdTarea AND tar.Estado=1";
            b.ExecuteCommandQuery(consulta);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Contactos.Nombre = reader["Contacto"].ToString();
                item.Oportunidades.Nombre = reader["Oportunidad"].ToString();
                item.Tareas.Asunto = reader["Tarea"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarContactosEmpresasOportunidadesTareas()
        {
            string consulta = "SELECT con.Nombre + ' ' + con.ApellidoPaterno AS Contacto, emp.Nombre As Empresa, opo.Nombre AS Oportunidad, tar.Asunto AS Tarea " +
            "FROM oecu " +
            "LEFT JOIN tuce ON oecu.idempresa = tuce.idempresa " +
            "INNER JOIN contactos con ON oecu.IdContacto = con.id " +
            "INNER JOIN empresas emp ON oecu.IdEmpresa = emp.id " +
            "LEFT JOIN Oportunidades opo ON opo.id = oecu.idoportunidad AND opo.Etapa = 1 " +
            "LEFT JOIN tareas tar ON tar.id = tuce.IdTarea AND tar.Estado = 1";
            b.ExecuteCommandQuery(consulta);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Contactos.Nombre = reader["Contacto"].ToString();
                item.Oportunidades.Nombre = reader["Oportunidad"].ToString();
                item.Tareas.Asunto = reader["Tarea"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarContactosEmpresasOportunidades()
        {
            string consulta = "SELECT opo.Id, emp.Nombre AS Empresa, con.Nombre + ' ' + con.ApellidoPaterno AS Contacto, opo.Nombre AS Oportunidad " +
            "FROM oecu " +
            "INNER JOIN contactos con ON oecu.IdContacto = con.id " +
            "INNER JOIN empresas emp ON oecu.IdEmpresa = emp.id " +
            "INNER JOIN Oportunidades opo ON opo.id = oecu.idoportunidad AND opo.Etapa = 1";
            b.ExecuteCommandQuery(consulta);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Contactos.Nombre = reader["Contacto"].ToString();
                item.Oportunidades.Nombre = reader["Oportunidad"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarContactosEmpresasTareas()
        {
            string consulta = "SELECT emp.Nombre As Empresa, con.Nombre + ' ' + con.ApellidoPaterno AS Contacto, tar.Asunto AS Tareas " +
            "FROM OportunidadesEmpresasContactosUsuarios oecu " +
            "LEFT JOIN TareasUsuariosContactosEmpresas tuce ON oecu.idempresa = tuce.idempresa " +
            "INNER JOIN contactos con ON oecu.IdContacto = con.id " +
            "INNER JOIN empresas emp ON oecu.IdEmpresa = emp.id " +
            "LEFT JOIN tareas tar ON tar.id = tuce.IdTarea AND tar.Estado = 1"; 
            b.ExecuteCommandQuery(consulta);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Contactos.Nombre = reader["Contacto"].ToString();
                item.Tareas.Asunto = reader["Tareas"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarUsuariosTareasOportunidades()
        {
            string consulta = "SELECT usu.Nombre AS Comercial, count(ope.Nombre) AS Operaciones, COUNT(tar.Asunto) AS Tareas " +
            "FROM usuarios usu " +
            "INNER JOIN UsuariosRoles usr ON usu.Id = usr.IdUsuario " +
            "INNER JOIN Roles rol ON usr.IdRol = rol.Id " +
            "LEFT JOIN OECU ON oecu.IdUsuario = usu.id " +
            "LEFT JOIN tuce ON tuce.IdUsuario = usu.id " +
            "LEFT JOIN oportunidades ope ON oecu.idoportunidad = ope.id " +
            "LEFT JOIN tareas tar ON tuce.IdTarea = tar.id " +
            "WHERE rol.id = 4 " +
            "GROUP BY usu.Nombre, rol.Nombre"; 
            b.ExecuteCommandQuery(consulta);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Usuarios.Nombre  = reader["Comercial"].ToString();
                item.Oportunidades.Importe = reader["Operaciones"].ToString();
                item.Tareas.Asunto = reader["Tareas"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        // Conteo para gráficos

        public List<Modelos> SeleccionarAsuntosPorEmpresaGeneral(string idconfiguracion)
        {
            string consulta = "SELECT emp.nombre, count(emp.nombre) AS Conteo " +
            "FROM oportunidades opo " +
            "INNER JOIN oecu ON oecu.IdOportunidad = opo.Id " +
            "INNER JOIN Empresas emp ON emp.Id = oecu.IdEmpresa " +
            "WHERE oecu.idconfiguracion=@idconfiguracion " +
            "GROUP BY emp.nombre";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Empresas.Nombre = reader["Nombre"].ToString();
                item.Empresas.Sector = int.Parse(reader["Conteo"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarAsuntosPorEmpresaPorEstado(string estado, string idconfiguracion)
        {
            string consulta = "SELECT emp.nombre, count(emp.nombre) AS Conteo " +
            "FROM oportunidades opo " +
            "LEFT JOIN oecu ON oecu.IdOportunidad = opo.Id " +
            "LEFT JOIN Empresas emp ON emp.Id = oecu.IdEmpresa " +
            "WHERE opo.Estado=@estado " +
            "AND oecu.idconfiguracion=@idconfiguracion " +
            "GROUP BY emp.nombre";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Empresas.Nombre = reader["Nombre"].ToString();
                item.Empresas.Sector = int.Parse(reader["Conteo"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarAsuntosPorEmpresaPorEstado1_1(string idconfiguracion)
        {
            string consulta = "SELECT emp.nombre, count(emp.nombre) AS Conteo " +
            "FROM oportunidades opo " +
            "INNER JOIN oecu ON oecu.IdOportunidad = opo.Id " +
            "INNER JOIN Empresas emp ON emp.Id = oecu.IdEmpresa " +
            "WHERE opo.Estado=1 " +
            "AND oecu.idconfiguracion=@idconfiguracion " +
            "AND DATEDIFF(DAY,opo.cierre,getdate()) <= -16  " +
            "GROUP BY emp.nombre";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Empresas.Nombre = reader["Nombre"].ToString();
                item.Empresas.Sector = int.Parse(reader["Conteo"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
         
        public List<Modelos> SeleccionarAsuntosPorEmpresaPorEstado1_2(string idconfiguracion)
        {
            string consulta = "SELECT emp.nombre, count(emp.nombre) AS Conteo " +
            "FROM oportunidades opo " +
            "INNER JOIN oecu ON oecu.IdOportunidad = opo.Id " +
            "INNER JOIN Empresas emp ON emp.Id = oecu.IdEmpresa " +
            "WHERE opo.Estado=1 " +
            "AND (DATEDIFF(DAY,opo.cierre,getdate()) IN (-59,-58,-57,-56,-55,-54,-53,-52,-51, -50,-49,-48,-47,-46,-45,-44,-43,-42,-41,-40,-39,-38,-37,-36,-35,-34,-33,-32,-31)) " + //a 60 días
            //"AND (DATEDIFF(DAY,opo.cierre,getdate()) IN (-15,-14,-13,-12,-11,-10,-8,-7)) " + //a 15 días
            "AND oecu.idconfiguracion=@idconfiguracion " +
            "GROUP BY emp.nombre";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Empresas.Nombre = reader["Nombre"].ToString();
                item.Empresas.Sector = int.Parse(reader["Conteo"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarAsuntosPorEmpresaPorEstado1_3(string idconfiguracion)
        {
            string consulta = "SELECT emp.nombre, count(emp.nombre) AS Conteo " +
            "FROM oportunidades opo " +
            "INNER JOIN oecu ON oecu.IdOportunidad = opo.Id " +
            "INNER JOIN Empresas emp ON emp.Id = oecu.IdEmpresa " +
            "WHERE opo.Estado=1 " +
            "AND (DATEDIFF(DAY,opo.cierre,getdate()) IN (-30,-29,-28,-27,-26,-25,-24,-23,-22,-21,-20,-19,-18,-17,-16,-15,-14,-13,-12,-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1))  " + //a 30 días
            //"AND (DATEDIFF(DAY,opo.cierre,getdate()) IN (-9,-5,-4,-3,-2,-1))  " + //a 7 días
            "AND oecu.idconfiguracion=@idconfiguracion " +
            "GROUP BY emp.nombre";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Empresas.Nombre = reader["Nombre"].ToString();
                item.Empresas.Sector = int.Parse(reader["Conteo"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarAsuntosPorEmpresaPorEstado1_4(string idconfiguracion)
        {
            string consulta = "SELECT emp.nombre, count(emp.nombre) AS Conteo " +
            "FROM oportunidades opo " +
            "INNER JOIN oecu ON oecu.IdOportunidad = opo.Id " +
            "INNER JOIN Empresas emp ON emp.Id = oecu.IdEmpresa " +
            "WHERE opo.Estado=1 " +
            "AND DATEDIFF(DAY,opo.cierre,getdate()) >= 0 " +
            "AND oecu.idconfiguracion=@idconfiguracion " +
            "GROUP BY emp.nombre";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Empresas.Nombre = reader["Nombre"].ToString();
                item.Empresas.Sector = int.Parse(reader["Conteo"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }


    }
}