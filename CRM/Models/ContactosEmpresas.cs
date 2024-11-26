using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class ContactosEmpresas
    {
        public Contactos Contactos { get; set; }
        public Empresas Empresas { get; set; }

        public ContactosEmpresas()
        {
            Contactos = new Contactos();
            Empresas = new Empresas();
        }
    }
}