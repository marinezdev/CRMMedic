using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Clave { get; set; }
        public string Contraseña { get; set; }
        public string Correo { get; set; }
        public bool? Activo { get; set; }

        //Se incluye el detalle para lo que se ofreza a futuro
        public int IdUsuario { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public int Empresa { get; set; }
        public string EmpresaNombre { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Notas { get; set; }
        public int FisicaMoral { get; set; }
        public string RFC { get; set; }
        public int InternoExterno { get; set; }
        public bool? ArchivosPermiso { get; set; }
        public bool ClasSubClasPermiso { get; set; }
        public int IdUDN { get; set; }
        public bool Recordarme { get; set; }
    }
}