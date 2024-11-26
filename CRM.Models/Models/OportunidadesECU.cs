using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class OportunidadesECU
    {
        public Oportunidades Oportunidades { get; set; }
        public OportunidadesEmpresasContactosUsuarios OECU { get; set; }
        public Clasificacion Clasificacion { get; set; }
        public Subclasificacion SubClasificacion { get; set; }
        public ClassSubClass ClassSubClass { get; set; }
        public Escalacion Escalacion { get; set; }
        public UDN UDN { get; set; }

        public OportunidadesECU()
        {
            Oportunidades = new Oportunidades();
            OECU = new OportunidadesEmpresasContactosUsuarios();
            Clasificacion = new Clasificacion();
            SubClasificacion = new Subclasificacion();
            ClassSubClass = new ClassSubClass();
            Escalacion = new Escalacion();
            UDN = new UDN();
        }
    }
}