using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    public class MenuMenuRoles
    {
        public Menu Menu { get; set; }
        public MenuRoles MenuRoles { get; set; }

        public MenuMenuRoles()
        {
            Menu = new Menu();
            MenuRoles = new MenuRoles();
        }
    }
}
