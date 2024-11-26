using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class MenuRoles
    {
        public int Id { get; set; }
        public int IdMenu { get; set; }
        public int IdRol { get; set; }
        public bool Accesible { get; set; }
    }
}
