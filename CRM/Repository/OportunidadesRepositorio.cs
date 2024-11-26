using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Repository
{
    public class OportunidadesRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public List<Modelos> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT opo.Id, opo.Nombre, ISNULL(emp.Nombre, 'No Asignada') AS Empresa," +
            "opo.Estado," +
            "opo.Fecha " +
            "FROM oportunidades opo " +
            "INNER JOIN oecu ON oecu.IdOportunidad=opo.id " +
            "LEFt JOIN empresas emp ON oecu.IdEmpresa=emp.id");

            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                item.Oportunidades.Nombre = reader["Nombre"].ToString();
                item.Empresas.Nombre = reader["Empresa"].ToString();
                item.Oportunidades.Estado = int.Parse(reader["Estado"].ToString());
                item.Oportunidades.Fecha = DateTime.Parse(reader["Fecha"].ToString());

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        /// <summary>
        /// Obtiene los últimos 5 registros agregados
        /// </summary>
        /// <returns></returns>
        public List<Modelos> SeleccionarTop5(string IdConfiguracion, string IdRol)
        {
            string consulta = "SELECT TOP 5 opo.Id, opo.Nombre, emp.Nombre AS Empresa " +
            "FROM oportunidades opo " +
            "INNER JOIN OECU ON oecu.IdOportunidad=opo.id " +
            "INNER JOIN Empresas emp ON emp.Id = oecu.IdEmpresa " +
            "INNER JOIN UsuariosRoles ur ON ur.IdUsuario=oecu.IdUsuario " +
            "WHERE opo.Etapa=1 " +
            "AND opo.idconfiguracion=@idconfiguracion ";
            if (!string.IsNullOrEmpty(IdRol) && IdRol=="3")
                consulta += "AND ur.IdRol = @idrol ORDER BY opo.Id DESC";
            else
                consulta += "ORDER BY opo.Id DESC";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", IdConfiguracion, SqlDbType.Int);
            if (!string.IsNullOrEmpty(IdRol) && IdRol == "3")
                b.AddParameter("@idrol", IdRol, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                item.Oportunidades.Nombre = reader["Nombre"].ToString();
                item.Empresas.Nombre = reader["Empresa"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<OportunidadesBuscar> Buscar(string Nombre, string Empresa, string Inicio, string Fin, string Clasificacion, string SubClasificacion)
        {
            string consulta = "SELECT opo.Id AS IdOportunidad, opo.Nombre, opo.Importe, opo.Cierre, opo.Asignado, opo.Probabilidad, opo.Etapa, opo.Avance, opo.Notas, opo.IdUsuario, opo.Contraparte, " +
            "emp.Nombre AS EmpresaNombre, " +
            "con.Nombre + ' ' + con.ApellidoPaterno + ' ' + con.ApellidoMaterno AS ContactoNombre " +
            "FROM oportunidades opo " +
            "INNER JOIN OportunidadesEmpresasContactosUsuarios OPOECU ON opo.Id = OPOECU.IdOportunidad " +
            "INNER JOIN Empresas emp ON emp.Id = OPOECU.IdEmpresa " +
            "INNER JOIN Contactos con ON con.Id = OPOECU.IdContacto " +
            "INNER JOIN Usuarios usu ON usu.Id = OPOECU.IdUsuario " +
            "WHERE 1 = 1 ";

            if (Nombre != "")
                consulta += "AND opo.Nombre LIKE @nombre ";
            if (Empresa != "")
                consulta += "OR emp.Nombre LIKE @nombre ";
            if (Clasificacion != "")
                consulta += "AND opo.IdClasificacion = @clasificacion ";
            if (SubClasificacion != "")
                consulta += "AND opo.IdSubClasificacion=@subclasificacion";

            b.ExecuteCommandQuery(consulta);

            if (Nombre != "" || Empresa != "")
                b.AddParameter("@nombre", "%" + Nombre + "%", SqlDbType.NVarChar);
            if (Clasificacion != "")
                b.AddParameter("@clasificacion", Clasificacion, SqlDbType.Int);
            if (SubClasificacion != "")
                b.AddParameter("@subclasificacion", SubClasificacion, SqlDbType.Int);

            List<OportunidadesBuscar> resultado = new List<OportunidadesBuscar>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                OportunidadesBuscar item = new OportunidadesBuscar();

                item.Oportunidades.Id = int.Parse(reader["IdOportunidad"].ToString());
                item.Oportunidades.Nombre = reader["Nombre"].ToString();
                item.Oportunidades.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
                item.Empresas.Nombre = reader["EmpresaNombre"].ToString();
                
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public OportunidadesECU SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT opo.id, opo.Nombre, opo.Importe, opo.Cierre, opo.asignado, opo.probabilidad, opo.Etapa, " +
            "opo.avance, opo.notas,opo.IdClasificacion, opo.Contraparte, cla.Nombre AS ClaNombre, opo.IdSubClasificacion, scla.Nombre AS SClaNombre, opo.PeriodoAtencion," +
            "opo.Aviso, opo.Estado, opo.IdUsuario, opo.comentariosfinales, " +
            "oecu.idempresa, oecu.idcontacto, " +
            "csc.campo1, csc.campo2, csc.campo3, csc.campo4, " +
            "(SELECT TOP 1 Fecha FROM escalacion WHERE idoportunidad=@id ORDER BY fecha desc) AS UltimaFechaEscalacion " +
            "FROM oportunidades opo " +
            "LEFT JOIN OportunidadesEmpresasContactosUsuarios oecu ON opo.Id = oecu.IdOportunidad " +
            "LEFT JOIN Clasificacion cla ON opo.IdClasificacion=cla.Id " +
            "LEFT JOIN SubClasificacion scla ON opo.IdSubClasificacion = scla.Id " +
            "LEFT JOIN ClassSubClass csc ON csc.idoportunidad=opo.id " +
            "WHERE opo.id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            OportunidadesECU resultado = new OportunidadesECU();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Oportunidades.Id                  = int.Parse(reader["Id"].ToString());
                resultado.Oportunidades.Nombre              = reader["Nombre"].ToString();
                resultado.Oportunidades.Importe             = reader["Importe"].ToString() == "" ? "0" : reader["Importe"].ToString();
                resultado.Oportunidades.Cierre              = reader["Cierre"].ToString() == "" ? DateTime.Now : DateTime.Parse(reader["Cierre"].ToString());
                resultado.Oportunidades.Asignado            = reader["Asignado"].ToString() == "" ? 0 : int.Parse(reader["Asignado"].ToString());
                resultado.Oportunidades.Probabilidad        = reader["Probabilidad"].ToString() == "" ? 0 : int.Parse(reader["Probabilidad"].ToString());
                resultado.Oportunidades.Etapa               = reader["Etapa"].ToString() == "" ? 0 : int.Parse(reader["Etapa"].ToString());
                resultado.Oportunidades.Avance              = reader["Avance"].ToString() == "" ? 0 : int.Parse(reader["Avance"].ToString());
                resultado.Oportunidades.Notas               = reader["Notas"].ToString();
                resultado.Oportunidades.IdClasificacion     = reader["IdClasificacion"].ToString() == "" ? 0 : int.Parse(reader["IdClasificacion"].ToString());
                resultado.Oportunidades.IdSubClasificacion  = reader["IdSubClasificacion"].ToString() == "" ? 0 : int.Parse(reader["IdSubClasificacion"].ToString());
                resultado.Oportunidades.PeriodoAtencion     = reader["PeriodoAtencion"].ToString() == "" ? 0 : int.Parse(reader["PeriodoAtencion"].ToString());
                resultado.Oportunidades.PeriodoAtencionNombre = Utilerias.Funciones.PeriodoAtencionNombre(resultado.Oportunidades.PeriodoAtencion.ToString());
                resultado.Oportunidades.Aviso               = reader["Aviso"].ToString() == "" ? DateTime.Now : DateTime.Parse(reader["Aviso"].ToString());
                resultado.Oportunidades.Estado              = reader["Estado"].ToString() == "" ? 0 : int.Parse(reader["Estado"].ToString());
                resultado.Oportunidades.IdUsuario           = reader["IdUsuario"].ToString() == "" ? 0 : int.Parse(reader["IdUsuario"].ToString());
                resultado.Oportunidades.ComentariosFinales  = reader["ComentariosFinales"].ToString();
                resultado.Oportunidades.Contraparte         = reader["Contraparte"].ToString();
                resultado.OECU.IdEmpresa                    = reader["IdEmpresa"].ToString() == "" ? 0 : int.Parse(reader["IdEmpresa"].ToString());
                resultado.OECU.IdContacto                   = reader["IdContacto"].ToString() == "" ? 0 : int.Parse(reader["IdContacto"].ToString());
                resultado.Clasificacion.Nombre              = reader["ClaNombre"].ToString();
                resultado.SubClasificacion.Nombre           = reader["SCLaNombre"].ToString();
                resultado.ClassSubClass.Campo1              = reader["Campo1"].ToString();
                resultado.ClassSubClass.Campo2              = reader["Campo2"].ToString();
                resultado.ClassSubClass.Campo3              = reader["Campo3"].ToString();
                resultado.ClassSubClass.Campo4              = reader["Campo4"].ToString();
                resultado.Escalacion.Fecha                  = reader["UltimaFechaEscalacion"].ToString() == "" ? DateTime.Now : DateTime.Parse(reader["UltimaFechaEscalacion"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarOportunidadesPorIdResponsable(string id)
        {
            string consulta = "DECLARE @idcon INT; " +
            "SELECT @idcon = Id FROM contactos WHERE idusuarioresponsable = @id; " +
            "SELECT con.id AS IdContacto,usu.id AS IdUsuario,opo.Id AS IdOportunidad,opo.Nombre,opo.cierre,bic.Id AS IdBitacora" +
            ",CASE " + 
            "WHEN opo.Estado = 1 AND DATEDIFF(DAY, opo.cierre, getdate()) <= -60 THEN 'EnProceso1' " +
            "WHEN opo.Estado = 1 AND(DATEDIFF(DAY, opo.cierre, getdate()) IN(-59,-58,-57,-56,-55,-54,-53,-52,-51, -50,-49,-48,-47,-46,-45,-44,-43,-42,-41,-40,-39,-38,-37,-36,-35,-34,-33,-32,-31)) THEN 'EnProceso2' " +
            "WHEN opo.Estado = 1 AND(DATEDIFF(DAY, opo.cierre, getdate()) IN(-30,-29,-28,-27,-26,-25,-24,-23,-22,-21,-20,-19,-18,-17,-16,-15,-14,-13,-12,-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1)) THEN 'EnProceso3' " +
            "WHEN opo.Estado = 1 AND DATEDIFF(DAY, opo.cierre, getdate()) >= 0 THEN 'EnProceso4' " +
            "WHEN opo.Estado = 2 THEN 'Cerrado' " +
            "WHEN opo.Estado = 5 THEN 'Terminado' " +
            "WHEN opo.Estado = 6 THEN 'Cancelado' " +
            "WHEN opo.Estado = 7 THEN 'Suspendido' " +
            "WHEN opo.Estado = 8 THEN 'Reasignar' " +
            "END AS EstadoActual " +
            ",con.idusuarioresponsable " +
            "FROM contactos con " +
            "INNER JOIN usuarios usu ON usu.Id = con.IdUsuarioResponsable " +
            "INNER JOIN OportunidadesResponsables opor ON opor.IdAsignado = con.id " +
            "INNER JOIN Oportunidades opo ON opo.id = opor.IdOportunidad " +
            "LEFT JOIN Bitacora bic ON( " +
            "    bic.idresponsable = usu.Id " +
            "    AND " +
            "    opo.id = bic.IdOportunidad " +
            ") " +
            "WHERE con.Id = @idcon";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Contactos.Id                   = int.Parse(reader["IdContacto"].ToString());
                item.Usuarios.Id                    = int.Parse(reader["IdUsuario"].ToString());
                item.Oportunidades.Id               = int.Parse(reader["IdOportunidad"].ToString());
                item.Oportunidades.Nombre           = reader["Nombre"].ToString();
                item.Oportunidades.Cierre           = DateTime.Parse(reader["Cierre"].ToString());
                item.Bitacora.Id                    = reader["IdBitacora"].ToString() == "" ? 0 : int.Parse(reader["IdBitacora"].ToString());
                //item.Oportunidades.Estado           = reader["Estado"].ToString() == "" ? 0 : int.Parse(reader["Estado"].ToString());
                item.Clasificacion.Nombre           = reader["EstadoActual"].ToString();
                item.Contactos.IdUsuarioResponsable = reader["IdUsuarioResponsable"].ToString() == "" ? 0 : int.Parse(reader["IdUsuarioResponsable"].ToString());

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public string SeleccionarOportunidadesPorIdResponsableEstado1(string id)
        {
            b.ExecuteCommandQuery("SELECT COUNT(1) " +
            "FROM contactos con " +
            "INNER JOIN usuarios usu ON usu.id = con.idusuarioresponsable " +
            "INNER JOIN OportunidadesResponsables opor ON opor.IdAsignado = con.Id " +
            "INNER JOIN oportunidades opo ON opo.id = opor.IdOportunidad " +
            "LEFT JOIN bitacora bic ON bic.IdResponsable=usu.id " +
            "WHERE con.IdUsuarioResponsable=@id " +
            "AND bic.idresponsable=@id " +
            "AND bic.id=null");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.SelectString();
        }

        public string SeleccionarNombreTemaPorIdoportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT nombre FROM oportunidades WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.SelectString();
        }

        public List<Modelos> SeleccionarFechasOportunidad(string id)
        { 
            b.ExecuteCommandQuery("SELECT opor.fecha, opor.cierre, opor.aviso, esc.Fecha AS Escalacion  " +
            "FROM Oportunidades opor " +
            "LEFT JOIN escalacion esc ON esc.IdOportunidad = opor.Id " +
            "WHERE opor.id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();
                item.Oportunidades.Fecha    = DateTime.Parse(reader["Fecha"].ToString());
                item.Oportunidades.Cierre   = reader["Cierre"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["Cierre"].ToString());
                item.Oportunidades.Aviso    = reader["Aviso"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["Aviso"].ToString());
                item.Escalacion.Fecha       = reader["Escalacion"].ToString() == "" ? DateTime.Parse("1900/01/01") : DateTime.Parse(reader["Escalacion"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public Modelos Seleccionar1()
        {
            Modelos resultado = new Modelos();
            resultado.OECU.IdContacto = 1;
            resultado.OECU.IdEmpresa = 1;
            resultado.Roles.Nombre = "Juan";
            return resultado;
        }

        public List<Modelos> Seleccionar2()
        {
            List<Modelos> resultado = new List<Modelos>();
            for (int i = 0; i < 10; i++)
            {
                Modelos item = new Modelos();
                item.Contactos.Id = 1;
                item.Oportunidades.Nombre = "Oportunidad";
                resultado.Add(item);
                i += 1;
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene los procesos que están en proceso o terminados para saber su estado actual respecto a las asignaciones a los responsables
        /// </summary>
        /// <returns></returns>
        public List<Modelos> SeleccionarTemasEnProceso(string idconfiguracion, string idrol)
        {
            string consulta = "SELECT opor.Id, opor.Nombre, emp.Nombre AS Empresa, opor.Importe, opor.Estado, cla.Nombre AS Clasificacion, scla.Nombre AS SubClasificacion, opor.Fecha, opor.Cierre, " +
            "CASE " +
            "WHEN opor.Estado=1 AND DATEDIFF(DAY,opor.cierre,getdate()) <=-60 THEN 'EnProceso1' " +
            "WHEN opor.Estado=1 AND(DATEDIFF(DAY, opor.cierre, getdate()) IN (-59,-58,-57,-56,-55,-54,-53,-52,-51, -50,-49,-48,-47,-46,-45,-44,-43,-42,-41,-40,-39,-38,-37,-36,-35,-34,-33,-32,-31)) THEN 'EnProceso2' " +
            "WHEN opor.Estado=1 AND(DATEDIFF(DAY, opor.cierre, getdate()) IN (-30,-29,-28,-27,-26,-25,-24,-23,-22,-21,-20,-19,-18,-17,-16,-15,-14,-13,-12,-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1)) THEN 'EnProceso3' " +
            "WHEN opor.Estado=1 AND DATEDIFF(DAY, opor.cierre, getdate()) >=0 THEN 'EnProceso4' " +
            "WHEN opor.Estado=2 THEN 'Cerrado' " +
            "WHEN opor.Estado=5 THEN 'Terminado' " +
            "WHEN opor.Estado=6 THEN 'Cancelado' " +
            "WHEN opor.Estado=7 THEN 'Suspendido' " +
            "WHEN opor.Estado=8 THEN 'Reasignar' " +
            "END AS EstadoActual  " +
            "FROM oportunidades opor " +
            "LEFT JOIN Clasificacion cla ON cla.Id=opor.IdClasificacion " +
            "LEFT JOIN SubClasificacion scla ON scla.Id=opor.IdSubClasificacion " +
            "INNER JOIN oecu ON oecu.IdOportunidad=opor.Id " +
            "LEFT JOIN empresas emp ON emp.id = oecu.IdEmpresa " +
            "INNER JOIN usuarios usu ON usu.Id=oecu.IdUsuario " +
            "INNER JOIN UsuariosRoles ur ON ur.IdUsuario=oecu.IdUsuario " +
            "WHERE oecu.idconfiguracion=@idconfiguracion ";            
            if (!string.IsNullOrEmpty(idrol) && idrol == "3")
                consulta += "AND ur.IdRol=3 ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            if (!string.IsNullOrEmpty(idrol))
                b.AddParameter("@idrol", idrol, SqlDbType.Int);
            List <Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Oportunidades.Id        = int.Parse(reader["Id"].ToString());
                item.Empresas.Nombre         = reader["Empresa"].ToString();
                item.Oportunidades.Importe   = reader["Importe"].ToString() == "" ? "0" : reader["Importe"].ToString();
                item.Oportunidades.Nombre    = reader["Nombre"].ToString();
                item.Oportunidades.Estado    = int.Parse(reader["Estado"].ToString());
                item.Oportunidades.Fecha     = DateTime.Parse(reader["Fecha"].ToString());
                item.Oportunidades.Cierre    = reader["Cierre"].ToString() == "" ? DateTime.Parse("1900-01-01") : DateTime.Parse(reader["Cierre"].ToString());
                item.Clasificacion.Nombre    = reader["Clasificacion"].ToString();
                item.Subclasificacion.Nombre = reader["SubClasificacion"].ToString();

                item.Bitacora.NombreEstado = reader["EstadoActual"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarTemasEnProcesoParaUsuarios(string idusuario)
        {
            string consulta = "SELECT opor.Id, opor.Nombre, emp.Nombre AS Empresa, opor.Estado, cla.Nombre AS Clasificacion, scla.Nombre AS SubClasificacion, opor.Fecha, opor.Cierre, " +
            "CASE " +
            "WHEN opor.Estado = 1 AND DATEDIFF(DAY, opor.cierre, getdate()) <= -16 THEN 'EnProceso1' " +
            "WHEN opor.Estado = 1 AND(DATEDIFF(DAY, opor.cierre, getdate()) IN(-15, -14, -13, -12, -11, -10, -9, -8, -7)) THEN 'EnProceso2' " +
            "WHEN opor.Estado = 1 AND(DATEDIFF(DAY, opor.cierre, getdate()) IN(-6, -5, -4, -3, -2, -1)) THEN 'EnProceso3' " +
            "WHEN opor.Estado = 1 AND DATEDIFF(DAY, opor.cierre, getdate()) >= 0 THEN 'EnProceso4' " +
            "WHEN opor.Estado = 2 THEN 'Cerrado' " +
            "WHEN opor.Estado = 5 THEN 'Terminado' " +
            "WHEN opor.Estado = 6 THEN 'Cancelado' " +
            "WHEN opor.Estado = 7 THEN 'Suspendido' " +
            "WHEN opor.Estado = 8 THEN 'Reasignar' " +
            "END AS EstadoActual " +
            "FROM Oportunidades opor " +
            "INNER JOIN OportunidadesUsuarios ou ON ou.IdOportunidad = opor.id " +
            "INNER JOIN oecu ON oecu.IdOportunidad = opor.Id " +
            "LEFT JOIN empresas emp ON emp.id = oecu.IdEmpresa " +
            "LEFT JOIN Clasificacion cla ON cla.Id = opor.IdClasificacion " +
            "LEFT JOIN SubClasificacion scla ON scla.Id = opor.IdSubClasificacion " +
            "WHERE ou.IdAsignado = @idusuario ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
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
                item.Oportunidades.Cierre = reader["Cierre"].ToString() == "" ? DateTime.Parse("1900-01-01") : DateTime.Parse(reader["Cierre"].ToString());
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
        /// Agrega una nueva oportunidad y agrega la primera etapa del proceso
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int Agregar(Oportunidades items)
        {
            b.ExecuteCommandQuery("DECLARE @creado INT; " +
            "INSERT INTO oportunidades (nombre, etapa, idconfiguracion) VALUES(@nombre,1,@idconfiguracion); " +
            "SET @creado=@@IDENTITY; " +
            "INSERT INTO etapasoportunidad (idoportunidad, etapa) VALUES(@creado, 1); " +
            "SELECT @creado");
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@idconfiguracion", items.IdConfiguracion, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }

        public int AgregarCompleto(Oportunidades items)
        {
            b.ExecuteCommandQuery("DECLARE @creado INT; " +
            "INSERT INTO oportunidades (nombre,etapa,idclasificacion,idsubclasificacion,importe,periodoatencion,cierre,notas,idconfiguracion) " +
            "VALUES(@nombre,1,@clasificacion,@subclasificacion,@importe,@atencion,@cierre,@notas,@idconfiguracion); " +
            "SET @creado=@@IDENTITY; " +
            "INSERT INTO etapasoportunidad (idoportunidad, etapa) VALUES(@creado, 1); " +
            "SELECT @creado");
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@clasificacion", items.IdClasificacion, SqlDbType.Int);
            b.AddParameter("@subclasificacion", items.IdSubClasificacion, SqlDbType.Int);
            b.AddParameter("@importe", items.Importe, SqlDbType.NVarChar);
            b.AddParameter("@atencion", items.PeriodoAtencion, SqlDbType.Int);
            b.AddParameter("@cierre", items.Cierre, SqlDbType.DateTime);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@idconfiguracion", items.IdConfiguracion, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }

        public int Modificar(Oportunidades items)
        {
            string consulta = "UPDATE oportunidades SET nombre=@nombre,importe=@importe, cierre=@cierre, periodoatencion=@atencion, notas=@notas, contraparte=@contraparte ";
            if (items.IdClasificacion > 0)
                consulta += ",idclasificacion=@clasificacion ";
            if (items.IdSubClasificacion > 0)
                consulta += ",idsubclasificacion=@subclasificacion ";
            if (items.Aviso > DateTime.Parse("1900-01-01"))
                consulta += ",aviso=@aviso ";
            consulta += "WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@importe", items.Importe, SqlDbType.NVarChar);
            b.AddParameter("@cierre", items.Cierre, SqlDbType.Date);
            b.AddParameter("@etapa", items.Etapa, SqlDbType.Int);
            b.AddParameter("@atencion", items.PeriodoAtencion, SqlDbType.Int);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@contraparte", string.IsNullOrEmpty(items.Contraparte) ? "" : items.Contraparte, SqlDbType.NVarChar);
            if (items.IdClasificacion > 0)
                b.AddParameter("@clasificacion", items.IdClasificacion, SqlDbType.Int);
            if (items.IdSubClasificacion > 0)
                b.AddParameter("@subclasificacion", items.IdSubClasificacion, SqlDbType.Int);
            if (items.Aviso > DateTime.Parse("1900-01-01"))
                b.AddParameter("@aviso", items.Aviso, SqlDbType.Date);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int ModificarEstadoOportunidad(string id, string estado) 
        {
            b.ExecuteCommandQuery("UPDATE oportunidades SET estado=@estado WHERE id=@id");
            b.AddParameter("@estado", estado, SqlDbType.Int);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int ModificarEstadoOportunidadMasComentarios(string id, string estado, string comentarios)
        {
            b.ExecuteCommandQuery("UPDATE oportunidades SET estado=@estado, comentariosfinales=@comentarios WHERE id=@id");
            b.AddParameter("@estado", estado, SqlDbType.Int);
            b.AddParameter("@comentarios", comentarios, SqlDbType.NVarChar);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}