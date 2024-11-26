using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Correo
{
    public class Avisos
    {
        ReferenciaDeServicioDeCorreoDeASAE.CorreoSoapClient correoSoap = new ReferenciaDeServicioDeCorreoDeASAE.CorreoSoapClient();

        internal AccesoDatos b { get; set; } = new AccesoDatos();
        string ruta = "http://187.248.23.163:1053/";

        public void EnvioAvisos()
        {
            if (SelecccionarUsuariosParaEnviarAviso().Count > 0)
            {
                foreach (var item in SelecccionarUsuariosParaEnviarAviso())
                {
                    var tituloasunto = "Aviso de Asignación";
                    var asunto = SeleccionarNombreTemaPorIdoportunidad(item.Id.ToString());
                    var empresaasignada = SeleccionarNombreEmpresaPorIdOportunidad(item.Id.ToString());
                    var vencimiento = SeleccionarVencimientoTemaPorIdoportunidad(item.Id.ToString());
                    var titulocorreo = "";
                    var cuerpocorreo = "<p>Hola " + item.Nombre + "</p>";
                    if (item.Empresa == 3)
                    {
                        titulocorreo = "Control de Gestión - Aviso";
                        cuerpocorreo += "<p>Se le ha asignado el siguiente objeto para su atención:</p>";
                    }
                    else if (item.Empresa == 2)
                    {
                        titulocorreo = "CRM - Aviso";
                        cuerpocorreo += "<p>Se le ha asignado a la siguiente oportunidad para su atención:</p>";
                    }
                    cuerpocorreo += "<h3>" + tituloasunto + "</h3>" +
                        "<h3>Empresa: " + empresaasignada + "</h3>" +
                        "<h3>Vencimiento: " + vencimiento + "</h3>" +
                        "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
                        "<h4><a href='" + ruta + "'>" + ruta + "</a></h4>" +
                        "<br /><br /><br />";
                    EnviarCorreo(item.Correo, titulocorreo, asunto, cuerpocorreo);
                }
                //Enviar correo de aviso al creador del objeto/oportunidad
                foreach (var item in SelecccionarUsuariosParaEnviarAviso())
                {
                    var usuario = SeleccionarCreadorOportunidad(item.Id.ToString());
                    var asunto = SeleccionarNombreTemaPorIdoportunidad(item.Id.ToString());
                    var cuerpocorreo = "<h3>Hola " + usuario.Nombre + "</h3>" +
                        "<p>Este correo es para recordarle que se ha enviado un aviso a un usuario asignado para: " + asunto + "</p>" +
                        "<br /><br /><br />";
                    EnviarCorreo(usuario.Correo, "Aviso", "Recordatorio de Aviso", cuerpocorreo);
                }
            }
        }

        public void EnvioEscalaciones()
        {
            if (SelecccionarUsuariosParaEnviarEscalacion().Count > 0)
            {
                foreach (var item in SelecccionarUsuariosParaEnviarEscalacion())
                {
                    var tituloasunto = "Escalación de Asignación";
                    var asunto = SeleccionarNombreTemaPorIdoportunidad(item.Id.ToString());
                    var empresaasignada = SeleccionarNombreEmpresaPorIdOportunidad(item.Id.ToString());
                    var vencimiento = SeleccionarVencimientoTemaPorIdoportunidad(item.Id.ToString());
                    var titulocorreo = "";
                    var cuerpocorreo = "<p>Hola " + item.Nombre + "</p>";
                    if (item.Empresa == 3)
                    {
                        titulocorreo = "Control de Gestión - Escalación";
                        cuerpocorreo += "<p>Se le ha asignado el siguiente objeto para su atención:</p>";
                    }
                    else if (item.Empresa == 2)
                    {
                        titulocorreo = "CRM - Escalación";
                        cuerpocorreo += "<p>Se le ha asignado a la siguiente oportunidad para su atención:</p>";
                    }
                    cuerpocorreo += "<h3>" + tituloasunto + "</h3>" +
                        "<h3>Empresa: " + empresaasignada + "</h3>" +
                        "<h3>Vencimiento: " + vencimiento + "</h3>" +
                        "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
                        "<h4><a href='" + ruta + "'>" + ruta + "</a></h4>" +
                        "<br /><br /><br />";
                    EnviarCorreo(item.Correo, titulocorreo, asunto, cuerpocorreo);
                }

                //Enviar correo de escalación al creador del objeto/oportunidad
                foreach (var item in SelecccionarUsuariosParaEnviarAviso())
                {
                    var usuario = SeleccionarCreadorOportunidad(item.Id.ToString());
                    var asunto = SeleccionarNombreTemaPorIdoportunidad(item.Id.ToString());
                    var cuerpocorreo = "<h3>Hola " + usuario.Nombre + "</h3>" +
                        "<p>Este correo es para recordarle que se ha enviado un aviso de escalación a un usuario asignado para: " + asunto + "</p>" +
                        "<br /><br /><br />";
                    EnviarCorreo(usuario.Correo, "Aviso", "Recordatorio de Aviso", cuerpocorreo);
                }
            }
        }

        public void EnvioAvisosActividadesOportunidades()
        {
            if (SelecccionarUsuariosParaEnviarOportunidadesActividades().Count > 0)
            {
                foreach (var item in SelecccionarUsuariosParaEnviarOportunidadesActividades())
                {
                    var tituloasunto = "Aviso de Actividades de Oportunidad";
                    var asunto = SeleccionarNombreTemaPorIdoportunidad(item.Id.ToString());
                    var empresaasignada = SeleccionarNombreEmpresaPorIdOportunidad(item.Id.ToString());
                    var vencimiento = SeleccionarVencimientoTemaPorIdoportunidad(item.Id.ToString());
                    var titulocorreo = "";
                    var cuerpocorreo = "<p>Hola " + item.Nombre + "</p>";
                    if (item.Empresa == 3)
                    {
                        titulocorreo = "Control de Gestión - Aviso de Actividades de Oportunidad";
                    }
                    else if (item.Empresa == 2)
                    {
                        titulocorreo = "CRM - Aviso de Actividades de Oportunidad";
                        cuerpocorreo += "<p>Se le indica la siguiente información:</p>";
                    }
                    cuerpocorreo += "<h3>" + tituloasunto + "</h3>" +
                        "<h3>Empresa: " + empresaasignada + "</h3>" +
                        "<h3>Vencimiento: " + vencimiento + "</h3>" +
                        "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
                        "<h4><a href='" + ruta + "'>" + ruta + "</a></h4>" +
                        "<br /><br /><br />";
                    EnviarCorreo(item.Correo, titulocorreo, asunto, cuerpocorreo);
                }

                //Enviar correo de escalación al creador del objeto/oportunidad
                foreach (var item in SelecccionarUsuariosParaEnviarAviso())
                {
                    var usuario = SeleccionarCreadorOportunidad(item.Id.ToString());
                    var asunto = SeleccionarNombreTemaPorIdoportunidad(item.Id.ToString());
                    var cuerpocorreo = "<h3>Hola " + usuario.Nombre + "</h3>" +
                        "<p>" + item.Notas + "</p>" +
                        "<br /><br /><br />";
                    EnviarCorreo(usuario.Correo, "Aviso de Actividades de Oportunidades", "Mensaje Importante", cuerpocorreo);
                }
            }
        }

        public void EnvioAvisosOportunidadesMismaFechaCadaAño()
        {
            foreach (var item in SeleccionarOportunidadesAEnviarAvisosCadaAñoEnLaMismaFecha())
            {
                if (item.Mensaje == "Primero" || item.Mensaje == "Segundo")
                {
                    //Enviar un mensaje a los involucrados en la oportunidad
                }
            }
        }

        public void EnvioMarketing()
        {
            //Procesa los correos que deberán enviarse a los contactos dentro de una campaña de mercadeo

            //Seleccionar campaña para ejecutarla (una por una)
            if (SeleccionarCampañas().Count() > 0)
            {
                foreach (var items in SeleccionarCampañas())
                {
                    var correodetalle = CorreoCampaña(items.Id.ToString());

                    //Seleccionar cada contacto para la campaña para enviarle el correo
                    foreach (var itms in ContactosDeCampaña(items.Id.ToString()))
                    {
                        //Seleccionar el correo y enviarlo
                        if (correodetalle.Cuerpo.Contains("[nombre]"))
                        {
                            string nombre = itms.Nombre;
                            correodetalle.Cuerpo = correodetalle.Cuerpo.Replace("[nombre]", nombre);
                            if (correodetalle.Cuerpo.Contains("[campa]"))
                                correodetalle.Cuerpo = correodetalle.Cuerpo.Replace("[campa]", CRM.Utilerias.Cifrado.Encriptar(items.Id.ToString()));
                            if (correodetalle.Cuerpo.Contains("[conta]"))
                                correodetalle.Cuerpo = correodetalle.Cuerpo.Replace("[conta]", CRM.Utilerias.Cifrado.Encriptar(itms.Id.ToString()));
                            EnviarCorreo(itms.Correo, "", correodetalle.Asunto, correodetalle.Cuerpo);
                        }
                        else
                        {
                            EnviarCorreo(itms.Correo, "", correodetalle.Asunto, correodetalle.Cuerpo);
                        }
                    }
                }
            }
            ActualizarCampañasProcesadas();
        }

        public void EnvioRecordatorios()
        {
            if (SeleccionarCampañasConRecordatorios().Count() > 0)
            {
                foreach (var items in SeleccionarCampañasConRecordatorios())
                {
                    var correodetalle = SeleccionarCorreoCampañasConRecordatorios(items.IdCampaña.ToString());

                    //Seleccionar cada contacto para la campaña para enviarle el correo
                    foreach (var itms in ContactosDeCampaña(items.Id.ToString()))
                    {
                        //Seleccionar el correo y enviarlo
                        if (correodetalle.Cuerpo.Contains("[nombre]"))
                        {
                            string nombre = itms.Nombre;
                            correodetalle.Cuerpo = correodetalle.Cuerpo.Replace("[nombre]", nombre);
                            EnviarCorreo(itms.Correo, "", correodetalle.Asunto, correodetalle.Cuerpo);
                        }
                        else
                        {
                            EnviarCorreo(itms.Correo, "", correodetalle.Asunto, correodetalle.Cuerpo);
                        }
                    }
                }
            }
        }

        public bool EnviarCorreo(string correoaqueinseenvia, string titulo, string asunto, string mensaje)
        {
            if (correoSoap.CorreoMetPrivado("mail.asae.com.mx", 25, "soporte-aplicaciones@asae.com.mx", "$%65hgy#19_",
                correoaqueinseenvia, //A quién se va a enviar el correo
                titulo,              //Titulo de quién lo envia
                asunto,              //Asunto del correo
                mensaje) == "Correo Enviado")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region ************************* Acceso a datos *************************

        public List<CRM.Models.Models.Usuarios> SelecccionarUsuariosParaEnviarAviso()
        {
            string consulta = "SELECT avi.idoportunidad,avi.idusuario,usu.Correo, opor.IdConfiguracion, usu.Nombre + ' ' + ISNULL(usu.ApellidoPaterno,'') + ' ' + ISNULL(usu.ApellidoMaterno,'') AS Nombre " +
            "FROM aviso avi " +
            "INNER JOIN usuarios usu ON usu.id = avi.IdUsuario " +
            "INNER JOIN oportunidades opor ON opor.id=avi.IdOportunidad " +
            "WHERE avi.fecha = CONVERT(VARCHAR, GETDATE(), 23)";
            b.ExecuteCommandQuery(consulta);
            List<CRM.Models.Models.Usuarios> resultado = new List<CRM.Models.Models.Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                CRM.Models.Models.Usuarios item = new CRM.Models.Models.Usuarios();
                item.Id = int.Parse(reader["idoportunidad"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                item.IdUsuario = int.Parse(reader["idusuario"].ToString());
                item.Correo = reader["Correo"].ToString();
                item.Empresa = int.Parse(reader["idconfiguracion"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        public List<CRM.Models.Models.Usuarios> SelecccionarUsuariosParaEnviarEscalacion()
        {
            string consulta = "SELECT esc.idoportunidad, esc.idusuariocontacto, usu.Correo, opor.IdConfiguracion, usu.Nombre + ' ' + ISNULL(usu.ApellidoPaterno,'') + ' ' + ISNULL(usu.ApellidoMaterno,'') AS Nombre " +
            "FROM escalacion esc " +
            "INNER JOIN usuarios usu ON usu.id = esc.IdUsuariocontacto " +
            "INNER JOIN oportunidades opor ON opor.id = esc.IdOportunidad " +
            "WHERE esc.fecha = CONVERT(VARCHAR, GETDATE(), 23)";
            b.ExecuteCommandQuery(consulta);
            List<CRM.Models.Models.Usuarios> resultado = new List<CRM.Models.Models.Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                CRM.Models.Models.Usuarios item = new CRM.Models.Models.Usuarios();
                item.Id = int.Parse(reader["idoportunidad"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                item.IdUsuario = int.Parse(reader["idusuariocontacto"].ToString());
                item.Correo = reader["Correo"].ToString();
                item.Empresa = int.Parse(reader["idconfiguracion"].ToString());
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        //Envío de correos de las actividades

        public List<CRM.Models.Models.Usuarios> SelecccionarUsuariosParaEnviarOportunidadesActividades()
        {
            string consulta = "SELECT oa.idoportunidad, oa.idusuario, usu.Correo, opor.IdConfiguracion, usu.Nombre + ' ' + ISNULL(usu.ApellidoPaterno,'') + ' ' + ISNULL(usu.ApellidoMaterno,'') AS Nombre, oa.notas " +
            "FROM OportunidadesActividades oa " +
            "INNER JOIN usuarios usu ON usu.id = oa.idusuario " +
            "INNER JOIN oportunidades opor ON opor.id = oa.IdOportunidad " +
            "WHERE oa.fecha = CONVERT(VARCHAR, GETDATE(), 23)";
            b.ExecuteCommandQuery(consulta);
            List<CRM.Models.Models.Usuarios> resultado = new List<CRM.Models.Models.Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                CRM.Models.Models.Usuarios item = new CRM.Models.Models.Usuarios();
                item.Id = int.Parse(reader["idoportunidad"].ToString());
                item.Nombre = reader["Nombre"].ToString();
                item.IdUsuario = int.Parse(reader["idusuario"].ToString());
                item.Correo = reader["Correo"].ToString();
                item.Empresa = int.Parse(reader["idconfiguracion"].ToString());
                item.Notas = reader["notas"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        //Fin de Envío de correos de las actividades

        public string SeleccionarNombreTemaPorIdoportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT nombre FROM oportunidades WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.SelectString();
        }

        public string SeleccionarNombreEmpresaPorIdOportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT emp.nombre " +
            "FROM oportunidades opor " +
            "INNER JOIN oecu ON oecu.IdOportunidad = opor.id " +
            "INNER JOIN empresas emp ON emp.id = oecu.IdEmpresa " +
            "WHERE opor.id = @id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.SelectString();
        }

        public string SeleccionarVencimientoTemaPorIdoportunidad(string id)
        {
            b.ExecuteCommandQuery("SELECT cierre FROM oportunidades WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.SelectString();
        }

        public CRM.Models.Models.Usuarios SeleccionarCreadorOportunidad(string id)
        {
            string consulta = "SELECT usu.nombre + ' ' + ISNULL(usu.ApellidoPaterno, '') + ' ' + ISNULL(usu.ApellidoMaterno, '') AS Nombre, usu.correo " +
            "FROM oecu " +
            "INNER JOIN usuarios usu ON usu.Id = oecu.IdUsuario " +
            "INNER JOIN Oportunidades opor ON opor.Id = oecu.IdOportunidad " +
            "WHERE oecu.IdOportunidad=@idoportunidad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idoportunidad", id, SqlDbType.Int);
            CRM.Models.Models.Usuarios resultado = new CRM.Models.Models.Usuarios();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.Correo = reader["Correo"].ToString();
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        //Procesa los datos de las estadísticas

        /// <summary>
        /// Actualiza los datos de la tablas que contienen los acumulados de proyectos e importes por mes
        /// </summary>
        public void ActualizaProyectosImportes()
        {
            b.ExecuteCommandSP("EstadisticasProyectosImportes_Actualizar");
            b.SelectString();
        }

        public List<enviosmismafecha> SeleccionarOportunidadesAEnviarAvisosCadaAñoEnLaMismaFecha()
        {
            string consulta = "" +
            //"SELECT opo.nombre, emp.nombre, opo.Cierre, MONTH(GETDATE()) AS MesActual, MONTH(cierre) AS MesCierre, " +
            //"CAST(DAY(GETDATE()) AS VARCHAR) + '/' + CAST(MONTH(GETDATE()) AS VARCHAR)  AS DiaHoy, " +
            //"CAST(DAY(cierre) AS VARCHAR)  + '/' + CAST(MONTH(cierre) AS VARCHAR) AS DiaCierre, " +
            //"DATEDIFF(d, getdate(), cierre) DiasFaltanParaAvisos, " +
            "SELECT opo.id, " +
            "CASE " +
            "    WHEN DATEDIFF(d, getdate(), cierre) = 30 THEN 'Primero' " +
            "    WHEN DATEDIFF(d, getdate(), cierre) = 15 THEN 'Segundo' " +
            "    ELSE '' " +
            "END AS Mensaje " +
            "FROM oportunidades opo " +
            "INNER JOIN oecu ON oecu.IdOportunidad = opo.Id " +
            "INNER JOIN Empresas emp ON emp.Id = oecu.IdEmpresa " +
            "WHERE opo.Estado=1 " +
            "AND oecu.idconfiguracion=3 " +
            "AND repetirproximoaño=1";
            b.ExecuteCommandQuery(consulta);
            List<enviosmismafecha> resultado = new List<enviosmismafecha>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                enviosmismafecha item = new enviosmismafecha();
                item.Id = int.Parse(reader["id"].ToString());
                item.Mensaje = reader["mensaje"].ToString();
                resultado.Add(item);
            }
            reader = null;
            b.CloseConnection();
            return resultado;

        }

        public void UsuariosQueDanSeguimientoAOportunidadAvisoCadaAño(string idoportunidad)
        {


        }

        //Procedimientos para marketing

        //Contar cuantas campañas se llevaran a cabo este día
        public int ContarCampañas()
        {
            string consulta = "SELECT COUNT(id) FROM marketing WHERE CONVERT(VARCHAR, inicio, 23)=CONVERT(VARCHAR, GETDATE(), 23) AND estado=1;";
            b.ExecuteCommandQuery(consulta);
            return int.Parse(b.SelectString());
        }

        //Seleccionar campaña para ejecutarla (una por una)
        public List<CRM.Models.Models.Marketing> SeleccionarCampañas()
        {
            string consulta = "SELECT id FROM marketing WHERE CONVERT(VARCHAR, envios, 23)=CONVERT(VARCHAR, GETDATE(), 23) AND estado=1";
            b.ExecuteCommandQuery(consulta);
            List<CRM.Models.Models.Marketing> resultado = new List<CRM.Models.Models.Marketing>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                CRM.Models.Models.Marketing item = new CRM.Models.Models.Marketing();
                item.Id = int.Parse(reader["Id"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        //Seleccionar los nombres de los contactos y su correo para enviárselos
        public List<CRM.Models.Models.Contactos> ContactosDeCampaña(string idcampaña)
        {
            string consulta = "SELECT con.id, con.nombre, con.apellidopaterno, con.apellidomaterno, con.correo " +
            "FROM marketingcontactos mkc " +
            "INNER JOIN contactos con ON con.id = mkc.IdContacto " +
            "WHERE mkc.IdCampaña = @idcampaña";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            List<CRM.Models.Models.Contactos> resultado = new List<CRM.Models.Models.Contactos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                CRM.Models.Models.Contactos item = new CRM.Models.Models.Contactos();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString() + " " + reader["apellidopaterno"].ToString() + " " + reader["apellidomaterno"].ToString();
                item.Correo = reader["correo"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        //Armar el correo de la campaña y asignar el contacto si lo llevase el correo
        public CRM.Models.Models.MarketingCorreo CorreoCampaña(string idcampaña)
        {
            string consulta = "SELECT asunto,cuerpo FROM MarketingCorreo WHERE idcampaña=@idcampaña";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            CRM.Models.Models.MarketingCorreo resultado = new CRM.Models.Models.MarketingCorreo();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Asunto = reader["asunto"].ToString();
                resultado.Cuerpo = reader["cuerpo"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        //Actualizador del estado de las campañas ya ejecutado su envío de correo
        public int ActualizarCampañasProcesadas()
        {
            string consulta = "UPDATE marketing SET estado = 2 WHERE CONVERT(VARCHAR, envios, 23)= CONVERT(VARCHAR, GETDATE(), 23) AND CONVERT(VARCHAR, DAY(envios), 23)= DAY(GETDATE()) AND estado = 1";
            b.ExecuteCommandQuery(consulta);
            return b.InsertUpdateDelete();
        }

        //Seleccionar las campañas con recordatorios
        public List<CRM.Models.Models.MarketingRecordatorios> SeleccionarCampañasConRecordatorios()
        {
            string consulta = "SELECT idcampaña FROM marketingrecordatorios WHERE CONVERT(VARCHAR, fechaenvio, 23)=CONVERT(VARCHAR, GETDATE(), 23) ";
            b.ExecuteCommandQuery(consulta);
            List<CRM.Models.Models.MarketingRecordatorios> resultado = new List<CRM.Models.Models.MarketingRecordatorios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                CRM.Models.Models.MarketingRecordatorios item = new CRM.Models.Models.MarketingRecordatorios();
                item.IdCampaña = int.Parse(reader["idcampaña"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        //Seleccionar el correo de la campañas con recordatorios
        public CRM.Models.Models.MarketingRecordatorios SeleccionarCorreoCampañasConRecordatorios(string idcampaña)
        {
            string consulta = "SELECT asunto, cuerpo FROM marketingrecordatorios WHERE CONVERT(VARCHAR, fechaenvio, 23)=CONVERT(VARCHAR, GETDATE(), 23) AND idcampaña=@idcampaña ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idcampaña", idcampaña, SqlDbType.Int);
            CRM.Models.Models.MarketingRecordatorios resultado = new CRM.Models.Models.MarketingRecordatorios();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Asunto = reader["asunto"].ToString();
                resultado.Cuerpo = reader["cuerpo"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        //Selecciona los correos que se enviarán a los contactos como recordatorios de una campaña
        public List<CRM.Models.Models.MarketingRecordatorios> SeleccionarCorreosDeRecordatorios()
        {
            string consulta = "SELECT id, idcampaña, asunto, cuerpo, fechaenvio FROM marketingrecordatorios WHERE CONVERT(VARCHAR, fechaenvio, 23)=CONVERT(VARCHAR, GETDATE(), 23) ";
            b.ExecuteCommandQuery(consulta);
            List<CRM.Models.Models.MarketingRecordatorios> resultado = new List<CRM.Models.Models.MarketingRecordatorios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                CRM.Models.Models.MarketingRecordatorios item = new CRM.Models.Models.MarketingRecordatorios();
                item.Id = int.Parse(reader["id"].ToString());
                item.IdCampaña = int.Parse(reader["idcampaña"].ToString());
                item.Asunto = reader["asunto"].ToString();
                item.Cuerpo = reader["cuerpo"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        #endregion

    }

    public class enviosmismafecha
    {
        public int Id { get; set; }
        public string Mensaje { get; set; }
    }
}
