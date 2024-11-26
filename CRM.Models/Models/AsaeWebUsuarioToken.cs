using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class AsaeWebUsuarioToken
    {
		public int Id { get; set; }
		public int IdUsuario { get; set; }
		public int IdEvento { get; set; }
		public string Token { get; set; }
		public bool Activo { get; set; }
		public DateTime FechaRegistro { get; set; }
	}
}
