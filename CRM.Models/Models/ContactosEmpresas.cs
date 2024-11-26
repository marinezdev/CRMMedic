using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class ContactosEmpresas
    {
        public Contactos Contactos { get; set; }
        public Empresas Empresas { get; set; }
        public UDN UDN { get; set; }
        public Usuarios Usuarios { get; set; }

        public ContactosEmpresas()
        {
            Contactos = new Contactos();
            Empresas = new Empresas();
            UDN = new UDN();
            Usuarios = new Usuarios();
        }
    }
}