using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class ContactosBuscarDetalle
    {
        public int IdContacto { get; set; }
        public int IdEmpresa { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Ciudad { get; set; }
        public string Cargo { get; set; }
        public string FechaCumpleaños { get; set; }
        public int TipoContacto { get; set; }
        public string Notas { get; set; }

        public string Empresa { get; set; }
        public string EmpresaCorreo { get; set; }
        public string EmpresaTelefono { get; set; }
        public string PaginaWeb { get; set; }
        public string EmpresaDireccion { get; set; }
        public string EmpresaCiudad { get; set; }
        public int EmpresaEstado { get; set; }
        public bool Prospecto { get; set; }
        public bool Cliente { get; set; }
        public bool Competidor { get; set; }
        public int Sector { get; set; }
    }
}