using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CRM.Utilerias
{
    /// <summary>
    /// Clase auxiliar para cambios en la configuracion
    /// </summary>
    public static class Titulos
    {

        public static string TDashboard(string idconfiguracion)
        {
            string valor = "";
            if (idconfiguracion == "2")
                valor = "CRM";
            else if (idconfiguracion == "3")
                valor = "Control de Gestión";
            return valor;
        }

        public static string Empresa(string idconfiguracion, string numerogramatical)
        {
            string valor = "";
            if (idconfiguracion == "2" && numerogramatical == "S")
                valor = "Empresa";
            else if (idconfiguracion == "2" && numerogramatical == "P")
                valor = "Empresas";
            else if (idconfiguracion == "3" && numerogramatical == "S")
                valor = "Empresa";
            else if (idconfiguracion == "3" && numerogramatical == "P")
                valor = "Empresas";
            return valor;
        }

        public static string Contacto(string idconfiguracion, string numerogramatical)
        {
            string valor = "";
            if (idconfiguracion == "2" && numerogramatical == "S")
                valor = "Responsable";
            else if (idconfiguracion == "2" && numerogramatical == "P")
                valor = "Responsables";
            else if (idconfiguracion == "3" && numerogramatical == "S")
                valor = "Proveedor";
            else if (idconfiguracion == "3" && numerogramatical == "P")
                valor = "Proveedores";
            return valor;
        }

        public static string Oportunidades(string idconfiguracion, string numerogramatical)
        {
            string valor = "";
            if (idconfiguracion == "2" && numerogramatical == "S")
                valor = "Oportunidad";
            else if (idconfiguracion == "2" && numerogramatical == "P")
                valor = "Oportunidades";
            else if (idconfiguracion == "3" && numerogramatical == "S")
                valor = "Asunto";
            else if (idconfiguracion == "3" && numerogramatical == "P")
                valor = "Asuntos";
            return valor;
        }
    }
}