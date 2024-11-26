using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Models
{
    /// <summary>
    /// Controla los tipos de archivos para permitir subir, asignando un tamaño máximo
    /// </summary>
    public class DocumentosASAETipoArchivo
    {
        public int Id { get; set; }
        public string Extensiones { get; set; }
        public int TamMaxPermitido { get; set; }
        public bool Permitir { get; set; }

    }
}
