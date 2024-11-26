using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRM.Models.Models
{
    public class ActividadesContactoDetalle
    {
		public int Id { get; set; }
		public int IdActividadContacto { get; set; }
		public int IdTipoActividad { get; set; }
		public DateTime Fecha { get; set; }
		public string Notas { get; set; }
		public DateTime Creado { get; set; }
	}
}
