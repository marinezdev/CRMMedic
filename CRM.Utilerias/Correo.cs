using System;

namespace CRM.Utilerias
{
    public class Correo
    {
        readonly WSCorreo.CorreoSoapClient correoSoap = new WSCorreo.CorreoSoapClient();
        private string ruta = "http://187.248.23.163:1053/";

        public bool EnviarCorreo(string correoaqueinseenvia, string titulo, string asunto, string mensaje)
        {
            bool respuesta = false;
            string EMailAnswerSended = "";

            try
            {
                EMailAnswerSended = correoSoap.CorreoMetPrivado("mail.asae.com.mx", 25, "soporte-aplicaciones@asae.com.mx", "$%65hgy#19_",
                                                                correoaqueinseenvia, //A quién se va a enviar el correo
                                                                titulo,              //Titulo de quién lo envia
                                                                asunto,              //Asunto del correo
                                                                mensaje);
                if (EMailAnswerSended == "Correo Enviado")
                {
                    respuesta = true;
                }
            }
            catch (Exception ex) 
            {
                // ### Pendiente: Agregar log de errores en envio de correos
                // ### Pendiente: Agregar log de correos enviados correctamente

                string _errorMessage = ex.Message.ToString();
            }

            return respuesta;
        }

        public void AgendaParaEventos(string correo, string correocopia, string de, string asunto, string cuerpo, 
        string nombrearchivoics, string tituloeventoagenda, string fechainicioagenda, string fechafinagenda, string organizador, string correoorganizador, 
        string asuntoagenda, string descripcionagenda, string ubicacionagenda)
        {
            correoSoap.CorreoAsaeCRM_AgendaEvento(
                        "mail.asae.com.mx", 25, "soporte-aplicaciones@asae.com.mx", "$%65hgy#19_"
                        ,correo
                        ,correocopia
                        ,de
                        ,asunto
                        ,cuerpo
                        ,nombrearchivoics                   // nombre del archivo ICS
                        ,tituloeventoagenda                 // Título del Evento en la agenda
                        ,DateTime.Parse(fechainicioagenda)  // Fecha de Incio del Evento [Datetime]
                        ,DateTime.Parse(fechafinagenda)     // Fecha de Terminio del Evento [Datetime]
                        ,organizador                        // nombre de la persona que organiza
                        ,correoorganizador                  // correo de la persona que organiza
                        ,asuntoagenda                       // Asunto dentro de la agenda
                        ,descripcionagenda                  // descripción del evento
                        ,ubicacionagenda                    // Ubicación del evento
                        );

        }

        public bool EnviarCorreoAltaGerente(string nombre, string correogerente, string titulocorreo, string idconfiguracion) 
        {
            var cuerpocorreo = "<p>Hola " + nombre.ToUpper() + "</p>" +
            "<p>Ha sido agregado al sistema para llevar a cabo tareas de responsabilidad próximamente.</p>" + 
            "<p>Le llegará un correo con un nueva oportunidad para su atención, sus claves de acceso son las siguientes: </p>" +
            "<h3>Correo de acceso: " + correogerente + "</h3>" +
            "<h3>Contraseña: " + Funciones.ClaveSalida() + "</h3>" +
            "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
            "<h4><a href='"+ ruta + "'>" + ruta + "</a></h4>" +
            "<p>La clave de acceso debe ser cambiada por una personalizada inmediatamente en la sección Cambiar Contraseña, " +
            "de lo contrario se bloqueará el acceso al sistema.</p><br /><br /><br />";

            string cuerpodelcorreo = "<p>Hola <span style=\"font-weight: bold;\">" + nombre.ToUpper() + "</span></p><p>Ha sido agregado al sistema de " +
                "<span style=\"font-size: 18px; color: rgb(66, 66, 66);\">Gestión de Asuntos CRM</span> para llevar a cabo tareas de responsabilidad próximamente.</p>" +
                "<p>Le llegará un correo con una nueva oportunidad para su atención, sus claves de acceso son las siguientes:</p>" +
                "<p>Correo de acceso: <span style=\"font-weight: bold; \">" + correogerente + "</span></p>" +
                "<p>Contraseña:<span style=\"font-weight: bold;\">5q8T3w</span></p>" +
                "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
                "<p><a href='" + ruta + "'>'" + ruta + "'</a></p>" +
                "<p><span style=\"color: rgb(255, 156, 0);\"> La clave de acceso debe ser cambiada por una personalizada " +
                "<span style=\"text-decoration-line: underline;\"> inmediatamente</span> en la sección Cambiar Contraseña, de lo contrario, se bloqueará el acceso al sistema.</span></p>" +
                "<br /><br /><br />";

            return EnviarCorreo(correogerente, titulocorreo, "Nueva clave de acceso para CRM", cuerpocorreo);
        }

        public bool EnviarCorreoAltaResponsable(string nombre, string correoresponsable, string titulocorreo, string idconfiguracion) 
        {
            var cuerpocorreo = "<p>Hola " + nombre.ToUpper() + "</p>" +
            "<p>Ha sido agregado al sistema para llevar a cabo tareas de responsabilidad próximamente.</p>";
            cuerpocorreo += "<p>Le llegará un correo con un nueva oportunidad para su atención, sus claves de acceso son las siguientes:</p>";
            cuerpocorreo += "<h3>Clave de acceso: " + correoresponsable + "</h3>" +
            "<h3>Contraseña: " + Funciones.ClaveSalida() + "</h3>" +
            "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
            "<h4><a href='" + ruta + "'>" + ruta + "</a></h4>" +
            "<p>La contraseña debe ser cambiada por una personalizada inmediatamente en la sección Cambiar Contraseña, " +
            "de lo contrario se bloqueará el acceso al sistema.</p>" +
            "<br /><br /><br />"; ;
            return EnviarCorreo(correoresponsable, titulocorreo, "Nueva clave de acceso para CRM", cuerpocorreo);
        }

        public bool EnviarCorreoBajaResponsable(string nombre, string correoresponsable, string titulocorreo, string idconfiguracion)
        {
            var cuerpocorreo = "<p>Hola " + nombre.ToUpper() + "</p>" +
            "<p>Ha sido desasignado de la tarea de responsabilidad previamente asignada.</p>" +
            "<p>Gracias por su atención. Si necesita revisar sus asignaciones, dé click en la siguiente liga para accesar:</p>" +
            "<h4><a href='" + ruta + "'>" + ruta + "</a></h4>" +
            "<p>La contraseña debe ser cambiada por una personalizada inmediatamente en la sección Cambiar Contraseña, " +
            "de lo contrario se bloqueará el acceso al sistema.</p>" +
            "<br /><br /><br />"; ;
            return EnviarCorreo(correoresponsable, titulocorreo, "Desasignación de Responsabilidad CRM", cuerpocorreo);
        }

        public bool EnviarCorreoModificacionResponsable(string nombre, string correoresponsable, string titulocorreo)
        {
            var cuerpocorreo = "<p>Hola " + nombre.ToUpper() + "</p>" +
            "<p>Se ha modificado su acceso al sistema cambiando su correo anterior por uno nuevo, accese de nuevo el sistema para comprobarlo y continuar sus asignaciones.</p>";
            cuerpocorreo += "<h3>Nueva Clave de acceso: " + correoresponsable + "</h3>" +
            "<h3>Contraseña: " + Funciones.ClaveSalida() + "</h3>" +
            "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
            "<h4><a href='" + ruta + "'>" + ruta + "</a></h4>" +
            "<p>La contraseña debe ser cambiada por una personalizada inmediatamente en la sección Cambiar Contraseña, " +
            "de lo contrario se bloqueará el acceso al sistema.</p>" +
            "<br /><br /><br />"; ;
            return EnviarCorreo(correoresponsable, titulocorreo, "Nueva correo de acceso para CRM", cuerpocorreo);
        }

        public bool EnviarCorreoEscalacion(string tituloasunto, string empresaasignada, string vencimiento, string correoresponsable, string titulocorreo, string idconfiguracion) 
        {
            var cuerpocorreo = "<p>Hola</p>";
            cuerpocorreo += "<p>Se le ha asignado a la siguiente oportunidad para su atención:</p>";
            cuerpocorreo += "<h3>" + tituloasunto + "</h3>" +
                "<h3>Empresa: " + empresaasignada + "</h3>" +
                "<h3>Vencimiento: " + vencimiento + "</h3>" +
                "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
                "<h4><a href='" + ruta + "'>" + ruta + "</a></h4>" +
                "<br /><br /><br />";
            return EnviarCorreo(correoresponsable, titulocorreo, "Escalación de Asunto", cuerpocorreo);
        }

        public bool EnviarCorreoAviso(string nombre, string tituloasunto, string empresaasignada, string vencimiento, string correoresponsable, string titulocorreo, string idconfiguracion) 
        {
            var cuerpocorreo = "<p>Hola " + nombre.ToUpper() + "</p>";
            cuerpocorreo += "<p>Se le ha asignado a la siguiente oportunidad para su atención:</p>";
            cuerpocorreo += "<h3>" + tituloasunto + "</h3>" +
                "<h3>Empresa: " + empresaasignada + "</h3>" +
                "<h3>Vencimiento: " + vencimiento + "</h3>" +
                "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
                "<h4><a href='" + ruta + "'>" + ruta + "</a></h4>" +
                "<br /><br /><br />";
            return EnviarCorreo(correoresponsable, titulocorreo, "Aviso de Seguimiento", cuerpocorreo);
        }

        public bool EnviarCorreoCambioFechaVencimiento(string nombre, string tituloasunto, string empresaasignada, string vencimiento, string correoresponsable, string titulocorreo)
        {
            var cuerpocorreo = "<p>Hola " + nombre.ToUpper() + "</p>";
            cuerpocorreo += "<p>Se le informa que ha cambiado la fecha de vencimiento del siguiente asunto:</p>";
            cuerpocorreo += "<h3>" + tituloasunto + "</h3>" +
                "<h3>Empresa: " + empresaasignada + "</h3>" +
                "<h2>Vencimiento: " + vencimiento + "</h2>" +
                "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
                "<h4><a href='" + ruta + "'>" + ruta + "</a></h4>" +
                "<br /><br /><br />";
            return EnviarCorreo(correoresponsable, titulocorreo, "Cambio de fecha de vencimiento", cuerpocorreo);
        }

        public bool EnviarCorreoAsignadoAAsunto(string nombre, string tituloasunto, string empresaasignada, string vencimiento, string correoresponsable, string titulocorreo, string idconfiguracion) 
        {
            var cuerpocorreo = "<p>Hola " + nombre.ToUpper() + "</p>";
            cuerpocorreo += "<p>Se le ha asignado a la siguiente oportunidad para su atención del Sistema CRM:</p>";
            cuerpocorreo += "<h3> " + tituloasunto + " </h3>" +
            "<h3>Empresa: " + empresaasignada + "</h3>" +
            "<h3>Vencimiento: " + vencimiento + "</h3>" +
            "<p>Gracias por su atención. Dé click en la siguiente liga para accesar:</p>" +
            "<h4><a href='" + ruta + "'>" + ruta + "</a></h4>" +
            "<br /><br /><br />";
            return EnviarCorreo(correoresponsable, titulocorreo, "Asignación de Responsabilidades", cuerpocorreo);
        }

        public bool EnviarCorreoRecuperacionContraseña(string correoaquienseenvia) 
        {
            var asunto = "Recuperación de Contraseña";  
            var mensaje = "<p>Su contraseña para accesar es<p>" +
            "<h2>" + Funciones.ClaveSalida() + "</h2> " +
            "<p>Cambiéla por una personalizada en cuanto accese la aplicación o se bloqueará de nuevo el acceso.</p>" +
            "<p>Ruta para entrar en la aplicación:</p>" +
            "<h4><a href='" + ruta + "'>" + ruta + "</a></h4>" +
            "<br /><br /><br />";
            return EnviarCorreo(correoaquienseenvia, "CRM", asunto, mensaje);

        }

        public bool EnviarCorreoAvisoAltaBitacoraAResponsable(string correoresponsable, string usuario, string nombreasunto)
        {
            var asunto = "Alta de información en Bitácora por responsable";
            var mensaje = "<p>El usuario "+ usuario+ " ha dejado nueva información en la bitácora para ser revisada<p>" +
            "<p>del asunto " + nombreasunto +"</p>" +
            "<p>Dé click en la siguiente liga para poder accesarla:</p>" +
            "<h4><a href='" + ruta + "'>" + ruta + "</a></h4>" +
            "<br /><br /><br />";
            return EnviarCorreo(correoresponsable, "CRM", asunto, mensaje);
        }

        
    }
}
