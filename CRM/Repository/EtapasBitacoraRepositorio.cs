using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class EtapasBitacoraRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        /// <summary>
        /// Otiene las etapas por las que pasa la bitacora del responsable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<EtapasBitacora> SeleccionarPorIdOportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT eb.estado, eb.fecha " +
            "FROM EtapasBitacora eb " +
            "INNER JOIN bitacora bic ON eb.IdBitacora = bic.id " +
            "INNER JOIN Oportunidades opo ON opo.id = bic.IdOportunidad " +
            "WHERE opo.id=@id " +
            "ORDER BY eb.fecha");
            b.AddParameter("@id", id, SqlDbType.Int);
            List<EtapasBitacora> resultado = new List<EtapasBitacora>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                EtapasBitacora item = new EtapasBitacora();
                item.Estado = int.Parse(reader["Estado"].ToString());
                item.Fecha = DateTime.Parse(reader["Fecha"].ToString());

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarResponsablesHanLeido(string IdOportunidad)
        {
            string consultaanterios = "SELECT " +
            "con.Nombre + ' ' + con.ApellidoPaterno + ' ' + con.ApellidoMaterno AS Contacto, " +
            "CASE " +
            "   WHEN eb.Estado='1' THEN '1' " +
            "   ELSE '0' " +
            "   END AS Estado " +
            "FROM OportunidadesResponsables opor " +
            "LEFT JOIN bitacora bic ON bic.IdResponsable=opor.IdAsignado " +
            "LEFT JOIN EtapasBitacora eb ON eb.IdBitacora=bic.Id " +
            "LEFT JOIN contactos con ON con.id=opor.IdAsignado " +
            "WHERE eb.Estado=1 " +
            "AND opor.IdOportunidad=@id";
            
            
            b.ExecuteCommandQuery("SELECT " +
            "con.Nombre + ' ' + con.ApellidoPaterno + ' ' + con.ApellidoMaterno AS Contacto, " +
            "CASE " +
            "   WHEN eb.Estado = '1' THEN '1' " +
            "   ELSE '0' " +
            "   END AS Estado " +
            "FROM bitacora bic " +
            "LEFT JOIN EtapasBitacora eb ON eb.IdBitacora = bic.Id " +
            "INNER JOIN contactos con ON con.id = bic.IdResponsable " +
            "WHERE eb.Estado = 1 " +
            "AND bic.IdOportunidad=@id");
            b.AddParameter("@id", IdOportunidad, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Contactos.Nombre = reader["Contacto"].ToString();
                item.Bitacora.Estado = int.Parse(reader["Estado"].ToString());

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;

        }

        public List<Modelos> SeleccionarResponsablesEnProceso(string IdOportunidad)
        {
            string consultaanterior = "SELECT " +
            "con.Nombre + ' ' + con.ApellidoPaterno + ' ' + con.ApellidoMaterno AS Contacto, " +
            "CASE " +
            "   WHEN eb.Estado='2' THEN '1' " +
            "   ELSE '0' " +
            "   END AS Estado " +
            "FROM OportunidadesResponsables opor " +
            "LEFT JOIN bitacora bic ON bic.IdResponsable=opor.IdAsignado " +
            "LEFT JOIN EtapasBitacora eb ON eb.IdBitacora=bic.Id " +
            "LEFT JOIN contactos con ON con.id=opor.IdAsignado " +
            "WHERE eb.Estado=2 " +
            "AND opor.IdOportunidad=@id";

            string consulta = "SELECT " + 
            "con.Nombre + ' ' + con.ApellidoPaterno + ' ' + con.ApellidoMaterno AS Contacto, " +
            "CASE " +
            "   WHEN eb.Estado = '2' THEN '1' " +
            "   ELSE '0' " +
            "   END AS Estado " +
            "FROM bitacora bic " +
            "LEFT JOIN EtapasBitacora eb ON eb.IdBitacora = bic.Id " +
            "LEFT JOIN contactos con ON con.id = bic.IdResponsable " +
            "WHERE eb.Estado = 2 " +
            "AND bic.IdOportunidad=@id";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", IdOportunidad, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Contactos.Nombre = reader["Contacto"].ToString();
                item.Bitacora.Estado = int.Parse(reader["Estado"].ToString());

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;

        }

        public List<Modelos> SeleccionarResponsablesTerminado(string IdOportunidad)
        {
            b.ExecuteCommandQuery("SELECT " +
            "con.Nombre + ' ' + con.ApellidoPaterno + ' ' + con.ApellidoMaterno AS Contacto, " +
            "CASE " +
            "   WHEN eb.Estado='3' THEN '1' " +
            "   ELSE '0' " +
            "   END AS Estado " +
            "FROM OportunidadesResponsables opor " +
            "LEFT JOIN contactos con ON con.id=opor.IdAsignado " +
            "LEFT JOIN bitacora bic ON (bic.IdResponsable=con.Id AND bic.IdOportunidad=opor.IdOportunidad) " +
            "LEFT JOIN EtapasBitacora eb ON eb.IdBitacora = bic.Id " +
            "WHERE eb.Estado=3 " +
            "AND opor.IdOportunidad=@id");
            b.AddParameter("@id", IdOportunidad, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Contactos.Nombre = reader["Contacto"].ToString();
                item.Bitacora.Estado = int.Parse(reader["Estado"].ToString());

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;

        }

        /// <summary>
        /// Devuelve la cuenta de cuántos responsables están asignados a la oportunidad indicada
        /// </summary>
        /// <param name="IdOportunidad"></param>
        /// <returns></returns>
        public string SeleccionarResponsablesPorOportunidad(string IdOportunidad)
        {
            b.ExecuteCommandQuery("SELECT COUNT(1) FROM oportunidadesresponsables WHERE IdOportunidad=@id");
            b.AddParameter("@id", IdOportunidad, SqlDbType.Int);
            return b.SelectString();
        }

    }
}