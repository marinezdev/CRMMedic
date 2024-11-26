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
    public class ContactosRepositorio
    {
        internal Utilerias.AccesoDatos_ b { get; set; } = new Utilerias.AccesoDatos_();

        public List<Contactos> Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT Id, Nombre, ApellidoPaterno, ISNULL(ApellidoMaterno, '') AS ApellidoMaterno FROM contactos");
            List<Contactos> resultado = new List<Contactos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Contactos item       = new Contactos();
                item.Id              = int.Parse(reader["Id"].ToString());
                item.Nombre          = reader["Nombre"].ToString();
                item.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<ContactosEmpresas> SeleccionarTodos(string idconfiguracion)
        {
            b.ExecuteCommandQuery("SELECT con.Id, con.Nombre, ISNULL(con.ApellidoPaterno, '') AS ApellidoPaterno, ISNULL(con.ApellidoMaterno,'') AS ApellidoMaterno, emp.Nombre AS Empresa, con.Correo, con.Telefono, con.Celular, con.FechaCumpleaños " +
            "FROM contactos con " +
            "LEFT JOIN ContactosEmpresas coem ON coem.IdContacto = con.Id " +
            "LEFT JOIN Empresas emp ON coem.IdEmpresa = emp.Id " +
            "INNER JOIN Configuracion conf ON conf.id=con.idconfiguracion " +
            "WHERE con.idconfiguracion=@idconfiguracion ");
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<ContactosEmpresas> resultado = new List<ContactosEmpresas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ContactosEmpresas item         = new ContactosEmpresas();
                item.Contactos.Id              = int.Parse(reader["Id"].ToString());
                item.Contactos.Nombre          = reader["Nombre"].ToString();
                item.Contactos.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.Contactos.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                item.Contactos.Correo          = reader["Correo"].ToString();
                item.Contactos.Telefono        = reader["Telefono"].ToString();
                item.Contactos.Celular         = reader["Celular"].ToString();
                item.Contactos.FechaCumpleaños = reader["FechaCumpleaños"].ToString();
                item.Empresas.Nombre           = reader["Empresa"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Modelos> SeleccionarContactosEnTemasEnProceso(string idconfiguracion)
        {
            string consultaanterior="SELECT con.Nombre + ' ' + con.ApellidoPaterno + ' ' + con.ApellidoMaterno AS Contacto, opo.Nombre AS Tema " +
            "FROM contactos con " +
            "INNER JOIN OportunidadesResponsables opor ON opor.IdAsignado = con.id " +
            "INNER JOIN oportunidades opo ON opo.id = opor.IdOportunidad";

            string consulta = "SELECT opor.Id, con.Nombre, con.ApellidoPaterno, ISNULL(ApellidoMaterno, '') AS ApellidoMaterno , emp.nombre AS Empresa, opor.Nombre, opor.Estado, cla.Nombre AS Clasificacion, scla.Nombre AS SubClasificacion, opor.Fecha, opor.Cierre, " +
            "CASE " +
            "WHEN opor.Estado=1 AND DATEDIFF(DAY,opor.cierre,getdate()) <= -16 THEN 'EnProceso1' " +
            "WHEN opor.estado = 1 AND(DATEDIFF(DAY, opor.cierre, getdate()) IN (-15,-14,-13,-12,-11,-10,-9,-8,-7)) THEN 'EnProceso2' " +
            "WHEN opor.estado = 1 AND(DATEDIFF(DAY, opor.cierre, getdate()) IN (-6,-5,-4,-3,-2,-1)) THEN 'EnProceso3' " +
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
            "INNER JOIN OportunidadesResponsables opre ON opre.IdOportunidad=opor.id " +
            "LEFT JOIN Contactos con ON con.Id=opre.IdAsignado " +
            "INNER JOIN oecu ON oecu.IdOportunidad=opor.Id " +
            "LEFT JOIN empresas emp ON emp.id = oecu.IdEmpresa " +
            "WHERE con.idconfiguracion=@idconfiguracion";

            //b.ExecuteCommandQuery(consulta);
            //List <Modelos> resultado = new List<Modelos>();
            //var reader = b.ExecuteReader();
            //while (reader.Read())
            //{
            //    Modelos item = new Modelos();
            //    item.Contactos.Nombre = reader["Contacto"].ToString();
            //    item.Oportunidades.Nombre = reader["Tema"].ToString();
            //    resultado.Add(item);
            //}
            //reader = null;
            //b.ConnectionCloseToTransaction();
            //return resultado;

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Modelos item = new Modelos();

                item.Contactos.Nombre          = reader["Nombre"].ToString();
                item.Contactos.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.Contactos.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                item.Oportunidades.Id          = int.Parse(reader["Id"].ToString());
                item.Empresas.Nombre           = reader["Empresa"].ToString();
                item.Oportunidades.Nombre      = reader["Nombre"].ToString();
                item.Oportunidades.Estado      = int.Parse(reader["Estado"].ToString());
                item.Oportunidades.Fecha       = DateTime.Parse(reader["Fecha"].ToString());
                item.Oportunidades.Cierre      = DateTime.Parse(reader["Cierre"].ToString());
                item.Clasificacion.Nombre      = reader["Clasificacion"].ToString();
                item.Subclasificacion.Nombre   = reader["SubClasificacion"].ToString();

                item.Bitacora.NombreEstado = reader["EstadoActual"].ToString();

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;

        }

        public string SeleccionarIdContactoDeIdUsuario(string idusuario)
        {
            b.ExecuteCommandQuery("SELECT id FROM contactos WHERE idusuarioresponsable=@idusuario");
            b.AddParameter("idusuario", idusuario, SqlDbType.Int);
            return b.SelectString();
        }

        public List<ContactosEmpresas> Buscar(string Nombre)
        {
            b.ExecuteCommandQuery("SELECT con.Id AS IdContacto, empr.Id As IdEmpresa, con.Nombre, con.ApellidoPaterno, ISNULL(con.ApellidoMaterno, '') AS ApellidoMaterno , empr.Nombre AS Empresa, " +
            "con.Correo, con.Telefono, con.Celular " +
            "FROM ContactosEmpresas cone " +
            "INNER JOIN Empresas empr ON cone.IdEmpresa = empr.Id " +
            "LEFT JOIN Contactos con ON cone.idcontacto = con.Id " +
            "WHERE 1=1 " +
            "AND con.Nombre + ' ' + con.ApellidoPaterno + ' ' + con.ApellidoMaterno LIKE @nombre " +
            "OR empr.Nombre LIKE @nombre");
            b.AddParameter("@nombre", "%" + Nombre + "%", SqlDbType.NVarChar);
            List<ContactosEmpresas> resultado = new List<ContactosEmpresas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ContactosEmpresas item = new ContactosEmpresas();

                item.Contactos.Id               = int.Parse(reader["IdContacto"].ToString());
                item.Empresas.Id                = int.Parse(reader["IdEmpresa"].ToString());
                item.Empresas.Nombre            = reader["Empresa"].ToString();
                item.Contactos.Nombre           = reader["Nombre"].ToString();
                item.Contactos.ApellidoPaterno  = reader["ApellidoPaterno"].ToString();
                item.Contactos.ApellidoMaterno  = reader["ApellidoMaterno"].ToString();
                item.Contactos.Correo           = reader["Correo"].ToString();
                item.Contactos.Telefono         = reader["Telefono"].ToString();
                item.Contactos.Celular          = reader["Celular"].ToString();
                
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        public List<Modelos> Buscar1(string Nombre)
        {
            string consulta = "" +
            "SELECT Id, Nombre, apellidopaterno, ISNULL(apellidomaterno, '') AS ApellidoMaterno " +
            "FROM contactos " +
            "WHERE apellidopaterno + ' ' + apellidomaterno + ' ' + Nombre  LIKE '%' + @nombre + '%' " +
            "UNION " +
            "SELECT Id, Nombre AS Empresa " +
            "FROM empresas " +
            "WHERE nombre LIKE '%' + @nombre + '%' " +
            "UNION " +
            "SELECT tar.Id, tar.Asunto AS Tarea " +
            "FROM tuce " +
            "INNER JOIN tareas tar ON TUCE.IdTarea = tar.Id " +
            "INNER JOIN Contactos con ON tuce.IdContacto = con.Id " +
            "INNER JOIN Empresas emp ON tuce.IdEmpresa = emp.Id " +
            "INNER JOIN Usuarios usu ON tuce.idusuario = usu.id " +
            "WHERE tar.asunto LIKE '%' + @nombre + '%' " +
            "UNION " + 
            "SELECT opo.Id, opo.Nombre AS Oportunidad " +
            "FROM oecu " +
            "INNER JOIN oportunidades opo ON oecu.IdOportunidad = opo.Id " +
            "INNER JOIN Contactos con ON oecu.IdContacto = con.Id " +
            "INNER JOIN Empresas emp ON oecu.IdEmpresa = emp.Id " +
            "INNER JOIN Usuarios usu ON oecu.idusuario = usu.id " +
            "WHERE opo.Nombre LIKE '%' + @nombre + '%' ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", Nombre, SqlDbType.NVarChar);
            List<Modelos> resultado = new List<Modelos>();
            var reader = b.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    Modelos item = new Modelos();

                    item.Contactos.Id              = int.Parse(reader["Id"].ToString());
                    item.Contactos.Nombre          = reader["Nombre"].ToString();
                    item.Contactos.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                    item.Contactos.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                    resultado.Add(item);
                }

                while (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        Modelos item = new Modelos();
                        item.Empresas.Id = int.Parse(reader["Id"].ToString());
                        item.Empresas.Nombre = reader["Empresa"].ToString();
                        resultado.Add(item);
                    }
                }
                while (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        Modelos item = new Modelos();
                        item.Tareas.Id = int.Parse(reader["Id"].ToString());
                        item.Tareas.Asunto = reader["Tarea"].ToString();
                        resultado.Add(item);
                    }
                }
                while (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        Modelos item = new Modelos();
                        item.Oportunidades.Id = int.Parse(reader["Id"].ToString());
                        item.Oportunidades.Nombre = reader["Oportunidad"].ToString();
                        resultado.Add(item);
                    }
                }

            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        public List<Contactos> BuscarParecidos(Contactos items)
        {
            b.ExecuteCommandQuery("SELECT id, nombre, apellidopaterno,apellidomaterno " +
            "FROM contactos WHERE apellidopaterno LIKE '%' + @apellidopaterno + '%' AND apellidomaterno LIKE '%' + @apellidomaterno + '%'");
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidopaterno", items.ApellidoPaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidomaterno", items.ApellidoMaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@correo", items.Correo, SqlDbType.NVarChar);
            b.AddParameter("@telefono", items.Telefono, SqlDbType.NVarChar);
            b.AddParameter("@celular", items.Celular, SqlDbType.NVarChar);
            List<Contactos> resultado = new List<Contactos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Contactos item       = new Contactos();
                item.Id              = int.Parse(reader["Id"].ToString());
                item.Nombre          = reader["Nombre"].ToString();
                item.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public string BuscarParecido(Contactos items)
        {
            b.ExecuteCommandQuery("IF EXISTS(SELECT 1 " +
            "FROM contactos WHERE nombre=@nombre AND apellidopaterno=@apellidopaterno AND apellidomaterno=@apellidomaterno) " +
            " SELECT 1 " +
            "ELSE " +
            "SELECT 0");
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidopaterno", items.ApellidoPaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidomaterno", items.ApellidoMaterno.ToUpper(), SqlDbType.NVarChar);
            return b.SelectString();
        }

        public ContactosEmpresas SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT cont.Id AS IdContacto,cont.Nombre,cont.ApellidoPaterno,cont.ApellidoMaterno,cont.Correo,cont.Correo,cont.Telefono,cont.Celular," +
            "cont.Direccion,cont.Ciudad,cont.Estado,cont.Cargo,cont.FechaCumpleaños,cont.TipoContacto,cont.Notas,cont.ContactoUsuario, " +
            "empr.Id AS IdEmpresa, empr.Nombre AS EmpresaNombre " +
            "FROM contactos cont " +
            "LEFT JOIN contactosempresas coem ON cont.Id = coem.IdContacto " +
            "LEFT JOIN empresas empr ON empr.id = coem.idempresa WHERE cont.id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            ContactosEmpresas resultado = new ContactosEmpresas();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {                
                resultado.Contactos.Id              = int.Parse(reader["IdContacto"].ToString());
                resultado.Contactos.Nombre          = reader["Nombre"].ToString();
                resultado.Empresas.Id               = reader["IdEmpresa"].ToString() == "" ? 0 : int.Parse(reader["IdEmpresa"].ToString());
                resultado.Empresas.Nombre           = reader["EmpresaNombre"].ToString();
                resultado.Contactos.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                resultado.Contactos.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                resultado.Contactos.Correo          = reader["Correo"].ToString();
                resultado.Contactos.Telefono        = reader["Telefono"].ToString();
                resultado.Contactos.Celular         = reader["Celular"].ToString();
                resultado.Contactos.Direccion       = reader["Direccion"].ToString();
                resultado.Contactos.Ciudad          = reader["CIudad"].ToString();
                resultado.Contactos.Estado          = reader["Estado"].ToString() == "" ? 0 : int.Parse(reader["Estado"].ToString());
                resultado.Contactos.Cargo           = reader["Cargo"].ToString();
                resultado.Contactos.FechaCumpleaños = reader["FechaCumpleaños"].ToString();
                resultado.Contactos.TipoContacto    = reader["TipoContacto"].ToString() == "" ? 0 : int.Parse(reader["TipoContacto"].ToString());
                resultado.Contactos.Notas           = reader["Notas"].ToString();
                resultado.Contactos.ContactoUsuario = int.Parse(reader["ContactoUsuario"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Contactos> SeleccionarContactosPorEmpresa(string id)
        {
            b.ExecuteCommandQuery("SELECT con.Id, con.Nombre, con.ApellidoPaterno, ISNULL(con.ApellidoMaterno, '') AS ApellidoMaterno " +
            "FROM contactos con " +
            "INNER JOIN contactosempresas ce ON con.id = ce.IdContacto " +
            "INNER JOIN Empresas emp ON emp.Id = ce.idempresa " +
            "WHERE emp.id = @id ");
            b.AddParameter("@id", id, SqlDbType.Int);
            List<Contactos> resultado = new List<Contactos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Contactos item       = new Contactos();
                item.Id              = int.Parse(reader["Id"].ToString());
                item.Nombre          = reader["Nombre"].ToString();
                item.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<Contactos> SeleccionarContactosPorConfiguracion(string id)
        {
            b.ExecuteCommandQuery("SELECT con.Id, con.Nombre,con.ApellidoPaterno,ISNULL(con.ApellidoMaterno,'') AS ApellidoMaterno " +
            "FROM contactos con " +
            "INNER JOIN configuracion conf ON conf.id = con.IdConfiguracion " +
            "WHERE conf.id=@id ");
            b.AddParameter("@id", id, SqlDbType.Int);
            List<Contactos> resultado = new List<Contactos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Contactos item       = new Contactos();
                item.Id              = int.Parse(reader["Id"].ToString());
                item.Nombre          = reader["Nombre"].ToString();
                item.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                item.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public string SeleccionarValidarSiContactoPerteneceAEmpresa(string idcontacto)
        {
            b.ExecuteCommandQuery("SELECT idempresa FROM contactosempresas WHERE idcontacto=@id");
            b.AddParameter("@id", idcontacto, SqlDbType.Int);
            return b.SelectString();
        }

        /// <summary>
        /// Obtiene los campos que se mostrarán del contacto y la empresa en la búsqueda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactosEmpresas SeleccionarPorIdDetalleContactoEmpresa(string id)
        {
            //b.ExecuteCommandQuery("SELECT cont.Id, cont.Nombre + ' ' + cont.ApellidoPaterno + ' ' + cont.ApellidoMaterno AS Contacto,cont.Correo,cont.Correo,cont.Telefono,cont.Celular,cont.Direccion,cont.Ciudad,cont.Estado,cont.Cargo,cont.FechaCumpleaños,cont.TipoContacto,cont.Notas," +
            //"empr.Id AS IdEmpresa, empr.Nombre AS Empresa, empr.Prospecto, empr.Cliente, empr.Competidor " +
            //"FROM contactos cont " +
            //"INNER JOIN contactosempresas coem ON cont.Id = coem.IdContacto " +
            //"INNER JOIN empresas empr ON empr.id = coem.idempresa " +
            //"WHERE cont.id=@id");
            //b.AddParameter("@id", id, SqlDbType.Int);
            //ContactosBuscarDetalle resultado = new ContactosBuscarDetalle();
            //var reader = b.ExecuteReader();
            //while (reader.Read())
            //{
            //    resultado.IdContacto  = int.Parse(reader["Id"].ToString());
            //    resultado.IdEmpresa   = int.Parse(reader["IdEmpresa"].ToString());
            //    resultado.Contacto    = reader["Contacto"].ToString();
            //    resultado.Cargo       = reader["Cargo"].ToString();
            //    resultado.Telefono    = reader["Telefono"].ToString();
            //    resultado.Correo      = reader["Correo"].ToString();
            //    resultado.Ciudad      = reader["Ciudad"].ToString();
            //    resultado.Notas       = reader["Notas"].ToString();
            //    resultado.Empresa     = reader["Empresa"].ToString();
            //    resultado.Prospecto   = reader["Prospecto"] == null ? false : bool.Parse(reader["Prospecto"].ToString());
            //    resultado.Cliente     = reader["Cliente"] == null ? false: bool.Parse(reader["Cliente"].ToString());
            //    resultado.Competidor  = reader["Competidor"] == null ? false : bool.Parse(reader["Competidor"].ToString());
            //}
            //reader = null;
            //b.ConnectionCloseToTransaction();
            //return resultado;

            b.ExecuteCommandQuery("SELECT cont.Id AS IdContacto, cont.Nombre, cont.ApellidoPaterno, ISNULL(cont.ApellidoMaterno, '') AS ApellidoMaterno,cont.Correo,cont.Correo,cont.Telefono,cont.Celular,cont.Direccion,cont.Ciudad,cont.Estado,cont.Cargo,cont.FechaCumpleaños,cont.TipoContacto,cont.Notas," +
            "empr.Id AS IdEmpresa, empr.Nombre AS Empresa, empr.Prospecto, empr.Cliente, empr.Competidor " +
            "FROM contactos cont " +
            "INNER JOIN contactosempresas coem ON cont.Id = coem.IdContacto " +
            "INNER JOIN empresas empr ON empr.id = coem.idempresa " +
            "WHERE cont.id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            ContactosEmpresas resultado = new ContactosEmpresas();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Contactos.Id              = int.Parse(reader["IdContacto"].ToString());
                resultado.Empresas.Id               = int.Parse(reader["IdEmpresa"].ToString());
                resultado.Contactos.Nombre          = reader["Nombre"].ToString();
                resultado.Contactos.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                resultado.Contactos.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                resultado.Contactos.Cargo           = reader["Cargo"].ToString();
                resultado.Contactos.Telefono        = reader["Telefono"].ToString();
                resultado.Contactos.Correo          = reader["Correo"].ToString();
                resultado.Contactos.Ciudad          = reader["Ciudad"].ToString();
                resultado.Contactos.Notas           = reader["Notas"].ToString();
                resultado.Empresas.Nombre           = reader["Empresa"].ToString();
                resultado.Empresas.Prospecto        = reader["Prospecto"] == null ? false : bool.Parse(reader["Prospecto"].ToString());
                resultado.Empresas.Cliente          = reader["Cliente"] == null ? false : bool.Parse(reader["Cliente"].ToString());
                resultado.Empresas.Competidor       = reader["Competidor"] == null ? false : bool.Parse(reader["Competidor"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        /// <summary>
        /// Agregar contacto, devuelve el id asignado
        /// </summary>
        /// <param name="items">Valores de entrada</param>
        /// <returns>Id asignado</returns>
        public int Agregar(Contactos items)
        {
            string consulta = "IF EXISTS(SELECT correo FROM contactos WHERE correo LIKE '%' + @correo + '%') " +
            "   SELECT 0 " +
            "ELSE " +
            "   INSERT INTO contactos (nombre,apellidopaterno,apellidomaterno,correo,telefono,celular,idconfiguracion) " +
            "   VALUES(@nombre,@apellidopaterno,@apellidomaterno,@correo,@telefono,@celular,@idconfiguracion); " +
            "   SELECT @@IDENTITY";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidopaterno", items.ApellidoPaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidomaterno", items.ApellidoMaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@correo", items.Correo, SqlDbType.NVarChar);
            b.AddParameter("@telefono", items.Telefono, SqlDbType.NVarChar);
            b.AddParameter("@celular", items.Celular, SqlDbType.NVarChar);
            b.AddParameter("@idconfiguracion", items.IdConfiguracion, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }

        public int Agregar3(Contactos items)
        {
            string consulta = "IF EXISTS(SELECT correo FROM contactos WHERE correo LIKE '%' + @correo + '%') " +
            "   SELECT 0 " +
            "ELSE " +
            "   INSERT INTO contactos (nombre,apellidopaterno,apellidomaterno,correo,telefono,celular,direccion,ciudad,estado,cargo,fechacumpleaños,tipocontacto,notas,idconfiguracion) " +
            "   VALUES(@nombre,@apellidopaterno,@apellidomaterno,@correo,@telefono,@celular,@direccion,@ciudad,@estado,@cargo,@cumpleaños,@tipocontacto,@notas,@idconfiguracion); " +
            "   SELECT @@IDENTITY";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidopaterno", items.ApellidoPaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidomaterno", items.ApellidoMaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@correo", items.Correo, SqlDbType.NVarChar);
            b.AddParameter("@telefono", items.Telefono, SqlDbType.NVarChar);
            b.AddParameter("@celular", items.Celular, SqlDbType.NVarChar);
            b.AddParameter("@direccion", items.Direccion, SqlDbType.NVarChar);
            b.AddParameter("@ciudad", items.Ciudad, SqlDbType.NVarChar);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@cargo", items.Cargo, SqlDbType.NVarChar);
            b.AddParameter("@cumpleaños", items.FechaCumpleaños, SqlDbType.NVarChar);
            b.AddParameter("@tipocontacto", items.TipoContacto, SqlDbType.Int);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@idconfiguracion", items.IdConfiguracion, SqlDbType.Int);
            return b.SelectWithReturnValue();
        }

        /// <summary>
        /// Agregar contacto de todas formas, sin importar si esta duplicado, devuelve el id asignado
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int Agregar2(Contactos items)
        {
            string consulta = 
            "INSERT INTO contactos (nombre,apellidopaterno,apellidomaterno,correo,telefono,celular) " +
            "VALUES(@nombre,@apellidopaterno,@apellidomaterno,@correo,@telefono,@celular); " +
            "SELECT @@IDENTITY";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidopaterno", items.ApellidoPaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidomaterno", items.ApellidoMaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@correo", items.Correo, SqlDbType.NVarChar);
            b.AddParameter("@telefono", items.Telefono, SqlDbType.NVarChar);
            b.AddParameter("@celular", items.Celular, SqlDbType.NVarChar);
            return b.SelectWithReturnValue();
        }

        public int Modificar(Contactos items)
        {
            b.ExecuteCommandQuery("UPDATE CONTACTOS SET nombre=@nombre, apellidopaterno=@apellidopaterno, apellidomaterno=@apellidomaterno," +
            "correo=@correo,telefono=@telefono,celular=@celular,direccion=@direccion,ciudad=@ciudad,estado=@estado,cargo=@cargo,fechacumpleaños=@fechacumpleaños," +
            "tipocontacto=@tipocontacto,notas=@notas " +
            "WHERE id=@id");
            b.AddParameter("@nombre", items.Nombre.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidopaterno", items.ApellidoPaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@apellidomaterno", items.ApellidoMaterno.ToUpper(), SqlDbType.NVarChar);
            b.AddParameter("@correo", items.Correo, SqlDbType.NVarChar);
            b.AddParameter("@telefono", items.Telefono, SqlDbType.NVarChar);
            b.AddParameter("@celular", items.Celular, SqlDbType.NVarChar);
            b.AddParameter("@direccion", items.Direccion, SqlDbType.NVarChar);
            b.AddParameter("@ciudad", items.Ciudad, SqlDbType.NVarChar);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@cargo", items.Cargo, SqlDbType.NVarChar);
            b.AddParameter("@fechacumpleaños", items.FechaCumpleaños, SqlDbType.NVarChar);
            b.AddParameter("@tipocontacto", items.TipoContacto == 0 ? 100 : items.TipoContacto, SqlDbType.Int);
            b.AddParameter("@notas", items.Notas, SqlDbType.NVarChar);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int ModificarContactoUsuario(Contactos items)
        {
            b.ExecuteCommandQuery("UPDATE contactos SET ContactoUsuario=1, IdUsuarioResponsable=@nuevo WHERE id=@id");
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            b.AddParameter("@nuevo", items.IdUsuarioResponsable, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }


    }
}