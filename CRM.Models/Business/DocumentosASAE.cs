using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CRM.Models.Models;
using CRM.Models.Business.Interfaces;

namespace CRM.Models.Business
{
    public class DocumentosASAE : Repository.DocumentosASAERepository, IValidacion
    {
        public List<mod.DocumentosASAE> Seleccionar_Registros()
        {
            return Seleccionar();
        }

        public List<mod.DocumentosASAE> Seleccionar_Registros_PorClasificacion(string idclasificacion)
        {
            return SeleccionarPorClasificacion(idclasificacion);
        }

        public List<mod.DocumentosASAE> Seleccionar_Registros_PorClasificacionSubClasificacion(string idclasificacion, string idsubclasificacion)
        {
            return SeleccionarPorClasificacionSubClasificacion(idclasificacion, idsubclasificacion);
        }

        public mod.DocumentosASAE Seleccionar_PorId(string id)
        {
            return SeleccionarPorId(id);
        }

        public int Seleccionar_SiUsuarioEsCreadorDocumento(string iddocumento, string idusuario)
        {
            return SeleccionarSiUsuarioEsCreadorDocumento(iddocumento, idusuario);
        }

        public List<mod.DocumentosASAE> Seleccionar_BusquedaPorNombre(string nombre)
        {
            return SeleccionarBusquedaPorNombre(nombre);
        }

        public string GenerarCadenaListaClasificaciones()
        {
            var padres = SeleccionarPadresDisponibles();
            var hijos = SeleccionarHijos();

            var cadena = "[";
            for(int i = 0;i<padres.Count(); i++)
            {
                cadena += "{ 'text' : '" + padres[i].Nombre + "', 'children' : [";

                for (int j = 0; j<hijos.Count(); j++)
                {
                    if (padres[i].Id.ToString() == hijos[j].IdUsuario.ToString())
                    {
                        cadena += "{ 'id' : '" + hijos[j].Id.ToString() + "', 'text' : '" + hijos[j].Nombre.ToString() + "', 'icon':'jstree-file' },";
                    }
                }
                cadena = cadena.Substring(0, cadena.Length - 1);
                cadena += "]},";
            }
            cadena = cadena.Substring(0, cadena.Length - 1);
            cadena += "]";
            return cadena;

        }


        public bool PropietarioPermiso(string iddocumento, string idusuario)
        {
            DocumentosASAE documentos = new DocumentosASAE();
            var modificar = SeleccionarSiUsuarioEsCreadorDocumento(iddocumento, idusuario);
            if (modificar == 1 || idusuario == "2")
                return true;
            else
                return false;
        }

        public int Agregar_Registro(mod.DocumentosASAE items)
        {
            return Agregar(items);
        }

        public int Modificar_Registro(mod.DocumentosASAE items)
        {
            return Modificar(items);
        }

        public int Modificar_Version(mod.DocumentosASAE items)
        {
            return ModificarVersion(items);
        }

        public int Eliminar_Registro(string idregistro)
        {
            return Eliminar(idregistro);
        }
    }
}
