using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Models
{
    public class Empresas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Extension { get; set; }
        public string Correo { get; set; }
        public string PaginaWeb { get; set; }
        public string Direccion { get; set; }
        public string CP { get; set; }
        public string Ciudad { get; set; }
        public string hCiudad { get; set; }
        public int Estado { get; set; }
        public string EstadoNombre { get; set; }
        public int Pais { get; set; }
        public int Sector { get; set; }
        public string SectorNombre { get; set; }
        public bool Prospecto { get; set; }
        public bool Cliente { get; set; }
        public bool Competidor { get; set; }
        public DateTime Alta { get; set; }
        public int IdUsuario { get; set; }
        public string RFC { get; set; }
        public int IdConfiguracion { get; set; }
        public int InternaExterna { get; set; }
        public int IdUDN { get; set; }
        public string UDNNombre { get; set; }
        public bool? Activo { get; set; }
        public int Tipo { get; set; }
        public string CreadorNombre { get; set; }

        //Sólo para conteos de filtros
        public int internas { get; set; }
        public int externas { get; set; }
        public int clientes { get; set; }
        public int prospectos { get; set; }
        public int outsorcings { get; set; }
        public int servicios { get; set; }
        public int soluciones { get; set; }
        public int valdemar { get; set; }
        public int comisiones { get; set; }
        public int redes { get; set; }
        public int sistemas { get; set; }

    }
}