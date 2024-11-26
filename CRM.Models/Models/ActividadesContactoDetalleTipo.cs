using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class ActividadesContactoDetalleTipo
    {
        public ActividadesContacto ActividadesContacto;
        public ActividadesContactoDetalle ActividadesContactoDetalle;
        public Catalogos Catalogos;
        public Usuarios Usuarios;

        public ActividadesContactoDetalleTipo()
        {
            ActividadesContacto = new ActividadesContacto();
            ActividadesContactoDetalle = new ActividadesContactoDetalle();
            Catalogos = new Catalogos();
            Usuarios = new Usuarios();
        }
    }
}
