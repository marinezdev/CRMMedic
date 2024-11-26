using CRM.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;

namespace CRM.Models.Business
{
    public class ContactosEmpresas : ContactosEmpresasRepositorio
    {
        public bool Agregar_ContactoEmpresa(string idcontacto, string idempresa)
        {
            return AgregarContactoEmpresa(idcontacto, idempresa);
        }

        public int Agregar_SoloContacto(string id)
        {
            return AgregarSoloContacto(id);
        }

        public int Actualizar_ContactoEmpresa(string idcontacto, string idempresa)
        {
            return ActualizarContactoEmpresa(idcontacto, idempresa);
        }

        public bool Agregar_SoloEmpresa(string idempresa)
        {
            return AgregarSoloEmpresa(idempresa);
        }
    }
}
