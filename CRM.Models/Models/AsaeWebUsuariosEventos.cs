using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class AsaeWebUsuariosEventos
    {
		public int Id { get; set; }
		public int IdEvento { get; set; }
		public string Nombre { get; set; }
		public string ApellidoPaterno { get; set; }
		public string ApellidoMaterno { get; set; }
		public string NombreEmpresa { get; set; }
		public string CorreoEmpresa { get; set; }
		public string CorreoPersonal { get; set; }
		public string TelefonoMovil { get; set; }
		public string TelefonoLocal { get; set; }
		public bool Confirmado { get; set; }
		public bool Activo { get; set; }
		public DateTime FechaRegistro { get; set; }

		
		
		//Para estadísticas

		public int Totales { get; set; }
		public int Activos { get; set; }
		public int Inactivos { get; set; }
		public int Confirmados { get; set; }

	}
}
