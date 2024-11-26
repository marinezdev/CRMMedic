using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Utilerias;

namespace CRM.Controllers
{
    public class ConfirmacionController : BaseController
    {
        [HttpGet]
        public ActionResult Confirmacion(string idcampana, string idcontacto)
        {
            //Confirmar la asistencia de un contacto de una campaña
            if (n.marketing.Seleccionar_EstadoCampaña(Cifrado.Desencriptar(idcampana)))
            {
                n.marketingcontactos.Modificar_Confirmacion(Cifrado.Desencriptar(idcampana), Cifrado.Desencriptar(idcontacto));
                var nombre = n.contactos.Seleccionar_PorId(Cifrado.Desencriptar(idcontacto));
                ViewBag.Nombre = nombre.Contactos.Nombre + " " + nombre.Contactos.ApellidoPaterno + " " + nombre.Contactos.ApellidoMaterno;
                return View();
            }
            else
            {
                return View("CampañaTerminada");
            }
        }

        public ActionResult CampañaTerminada()
        {
            return View();
        }



    }
}
