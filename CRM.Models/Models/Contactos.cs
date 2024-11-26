using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class Contactos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string CP { get; set; }
        public int Estado { get; set; }        
        public int Pais { get; set; }
        public string Cargo { get; set; }
        public string FechaCumpleaños { get; set; }
        public int? TipoContacto { get; set; }
        public int TipoPersona { get; set; }
        public string sTipoPersona { get; set; }
        public string Notas { get; set; }
        public DateTime Alta { get; set; }
        public int UsuarioAlta { get; set; }
        public int ContactoUsuario { get; set; }
        public int IdUsuarioResponsable { get; set; }
        public int IdConfiguracion { get; set; }
        public int IdUDN { get; set; }
        
        public int IdEmpresa { get; set; } 
        public int IdArea { get; set; }
        public bool? Activo { get; set; }
        public int Ingreso { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string CreadorNombre { get; set; }

        //Sólo para conteos de filtros        
        public int clientes { get; set; }
        public int prospectos { get; set; }
        public int hombre { get; set; }
        public int mujeres { get; set; }
        public int outsorcings { get; set; }
        public int servicios { get; set; }
        public int soluciones { get; set; }
        public int valdemar { get; set; }
        public int comisiones { get; set; }
        public int redes { get; set; }
        public int sistemas { get; set; }

        ///
        public int Resultado { get; set; }


        public string Area { get; set; }
    }
}