using CRM.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    /// <summary>
    /// Clase que contiene un agrupado de todo loq ue tiene que ver con la bitacora
    /// </summary>
    public class Bitacoras
    {
        public Bitacora Bitacora;
        public BitacoraUsuariosDetalle BitacoraUsuariosDetalle;
        public Generico Generico; 
        public Usuarios Usuarios;

        public Bitacoras()
        {
            Bitacora = new Bitacora();
            BitacoraUsuariosDetalle = new BitacoraUsuariosDetalle();
            Generico = new Generico();
            Usuarios = new Usuarios();
        }
    }
}
