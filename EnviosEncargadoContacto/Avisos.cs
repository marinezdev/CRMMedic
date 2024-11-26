using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EnviosEncargadoContacto
{
    public class AccesoADatos
    {
        private AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<CRM.Models.Models.ActividadesContacto> ObtenerContactosAvisarEncargado()
        {
            b.ExecuteCommandQuery("SELECT id, idusuario, idcontacto FROM actividadescontacto WHERE avisar=1");
            var reader = b.ExecuteReader();
            List<CRM.Models.Models.ActividadesContacto> resultado = new List<CRM.Models.Models.ActividadesContacto>();
            while (reader.Read())
            {
                CRM.Models.Models.ActividadesContacto item = new CRM.Models.Models.ActividadesContacto();
                item.Id = int.Parse(reader["id"].ToString());
                item.IdUsuario = int.Parse(reader["idusuario"].ToString());
                item.IdContacto = int.Parse(reader["idcontacto"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected int DesactivarAvisoEncargadoContactos(int idactividadcontacto)
        {
            b.ExecuteCommandQuery("UPDATE actividadescontacto SET avisar=0 WHERE id=@id");
            b.AddParameter("@id", idactividadcontacto, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected string SeleccionarCorreoUsuario(int idusuario)
        {
            string consulta = "SELECT correo FROM usuarios WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", idusuario, SqlDbType.Int);
            return b.SelectString();
        }

        protected CRM.Models.Models.Contactos Seleccionar_ContactoDetalle(int idcontacto)
        {
            string consulta = "SELECT nombre + ' ' + apellidopaterno + ' ' + apellidomaterno AS nombre, correo " +
            "FROM contactos " +
            "WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", idcontacto, SqlDbType.Int);
            CRM.Models.Models.Contactos resultado = new CRM.Models.Models.Contactos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Nombre = reader["nombre"].ToString();
                resultado.Correo = reader["correo"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        public class Avisos : AccesoADatos
        {
            ReferenciaDeServicioDeCorreoDeASAE.CorreoSoapClient correoSoap = new ReferenciaDeServicioDeCorreoDeASAE.CorreoSoapClient();

            string ruta = "http://187.248.23.163:1053/";

            public void EnvioAvisoEncargadoContactosExternos()
            {
                foreach (var item in ObtenerContactosAvisarEncargado())
                {
                    //Obtener el correo del usuario a avisarle
                    var correo = SeleccionarCorreoUsuario(item.IdUsuario);
                    //Obtener detalles del nuevo contacto
                    var contacto = Seleccionar_ContactoDetalle(item.IdContacto);
                    //Enviar el correo
                    string titulo = "Atender nuevo contacto agregado";
                    string asunto = "Dar seguimiento y atención a nuevo contacto";
                    string mensaje = "Se ha agregado a " + contacto.Nombre + " a CRM.<br/>";
                    mensaje += "Para dar el seguimiento correspondiente, dé click en la siguiente liga para acceder: <br>" + ruta + "<br><br>";
                    EnviarCorreo(correo, titulo, asunto, mensaje);
                    //Desactivar el registro para que no se envíe más
                    DesactivarAvisoEncargadoContactos(item.Id);
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
        }
    }
}
