using CRM.Models;
using CRM.Models.Repository;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CRM.Utilerias
{
    public static class CustomHelpers
    {        
        public static MvcHtmlString EstadosdeProceso(string estado, string idoportunidad)
        {                        
            if (estado == "EnProceso1") //Mas de 60 días
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id="+ idoportunidad +"'><i class='fas fa-cogs' style='color: lightblue' data-toggle='tooltip' data-placement='left' title='En Proceso'></i></a></td>");
            }
            else if (estado == "EnProceso2") //Sin terminar a 15 días
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id=" + idoportunidad + "'><i class='fas fa-flag' style='color: yellow' data-toggle='tooltip' data-placement='left' title='Alerta'></i></a></td>");
            }
            else if (estado == "EnProceso3") //Sin terminar a 7 días
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id=" + idoportunidad + "'><i class='fas fa-flag' style='color: orange' data-toggle='tooltip' data-placement='left' title='Por Vencer'></i></a></td>");
            }
            else if (estado == "EnProceso4")
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id=" + idoportunidad + "'><i class='fas fa-flag' style='color: red' data-toggle='tooltip' data-placement='left' title='Vencido'></i></a></td>");
            }
            else if (estado == "Cerrado") //Cerrado por el propietario por negociación
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id=" + idoportunidad + "'><i class='fas fa-hands-helping' style='color: coral' data-toggle='tooltip' data-placement='left' title='Cerrado'></i></a></td>");
            }
            else if (estado == "CerradoPerdido") //Cerrado Perdido
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id=" + idoportunidad + "'><i class='fas fa-sad-tear' style='color: red' data-toggle='tooltip' data-placement='left' title='Cerrado Perdido'></i></a></td>");
            }
            else if (estado == "CerradoGanado") //Cerrado Ganado
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id=" + idoportunidad + "'><i class='far fa-handshake' style='color: green' data-toggle='tooltip' data-placement='left' title='Cerrado Ganado'></i></a></td>");
            }
            //else if (estado == "Terminado") //Terminado
            //{
            //    return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id=" + idoportunidad + "'><i class='fas fa-check-circle' style='color: greenyellow' data-toggle='tooltip' data-placement='left' title='Terminado'></i></a></td>");
            //}
            else if (estado == "Cancelado") //Cancelado por el propietario
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id=" + idoportunidad + "'><i class='fas fa-times-circle' style='color: darkkhaki' data-toggle='tooltip' data-placement='left' title='Cancelado'></i></a></td>");
            }
            else if (estado == "Suspendido") //Suspendido por el propietario
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id=" + idoportunidad + "'><i class='fas fa-times-circle' style='color: deeppink' data-toggle='tooltip' data-placement='left' title='Suspendido'></i></a></td>");
            }
            else if (estado == "Reasignar") //Reasignar
            {
                return MvcHtmlString.Create("<td style='background-color: gray'>Reasignar</td>");
            }
            else
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id=" + idoportunidad + "'><i class='fas fa-exclamation-circle' data-toggle='tooltip' data-placement='left' title='No Definido (requiere atención)'></i></a></td>");
            }
        }

        public static MvcHtmlString EstadosdeProcesoSABE(string estado, string idoportunidad)
        {
            if (estado == "EnProceso1") //Mas de 60 días
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Edicion?Id=" + idoportunidad + "'><i class='fas fa-cogs' style='color: lightblue' data-toggle='tooltip' data-placement='left' title='En Proceso'></i></a></td>");
            }
            else if (estado == "EnProceso2") //Sin terminar a 15 días
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Edicion?Id=" + idoportunidad + "'><i class='fas fa-flag' style='color: yellow' data-toggle='tooltip' data-placement='left' title='Alerta'></i></a></td>");
            }
            else if (estado == "EnProceso3") //Sin terminar a 7 días
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Edicion?Id=" + idoportunidad + "'><i class='fas fa-flag' style='color: orange' data-toggle='tooltip' data-placement='left' title='Por Vencer'></i></a></td>");
            }
            else if (estado == "EnProceso4")
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Edicion?Id=" + idoportunidad + "'><i class='fas fa-flag' style='color: red' data-toggle='tooltip' data-placement='left' title='Vencido'></i></a></td>");
            }
            else if (estado == "Cerrado") //Cerrado por el propietario por negociación
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Edicion?Id=" + idoportunidad + "'><i class='fas fa-hands-helping' style='color: coral' data-toggle='tooltip' data-placement='left' title='Cerrado'></i></a></td>");
            }
            else if (estado == "CerradoPerdido") //Cerrado Perdido
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id=" + idoportunidad + "'><i class='fas fa-sad-tear' style='color: red' data-toggle='tooltip' data-placement='left' title='Cerrado Perdido'></i></a></td>");
            }
            else if (estado == "CerradoGanado") //Cerrado Ganado
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Edicion?Id=" + idoportunidad + "'><i class='far fa-handshake' style='color: green' data-toggle='tooltip' data-placement='left' title='Cerrado Ganado'></i></a></td>");
            }
            //else if (estado == "Terminado") //Terminado
            //{
            //    return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Editar?Id=" + idoportunidad + "'><i class='fas fa-check-circle' style='color: greenyellow' data-toggle='tooltip' data-placement='left' title='Terminado'></i></a></td>");
            //}
            else if (estado == "Cancelado") //Cancelado por el propietario
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Edicion?Id=" + idoportunidad + "'><i class='fas fa-times-circle' style='color: darkkhaki' data-toggle='tooltip' data-placement='left' title='Cancelado'></i></a></td>");
            }
            else if (estado == "Suspendido") //Suspendido por el propietario
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Edicion?Id=" + idoportunidad + "'><i class='fas fa-times-circle' style='color: deeppink' data-toggle='tooltip' data-placement='left' title='Suspendido'></i></a></td>");
            }
            else if (estado == "Reasignar") //Reasignar
            {
                return MvcHtmlString.Create("<td style='background-color: gray'>Reasignar</td>");
            }
            else
            {
                return MvcHtmlString.Create("<td align='center'><a href='/Oportunidades/Edicion?Id=" + idoportunidad + "'><i class='fas fa-exclamation-circle' data-toggle='tooltip' data-placement='left' title='No Definido (requiere atención)'></i></a></td>");
            }
        }

        public static MvcHtmlString ResponsablesSiNo(string idoportunidad)
        {            
            Comun comun = new Comun();
                        
            OportunidadesResponsablesRepositorio orr = new OportunidadesResponsablesRepositorio();
            BitacoraUsuariosDetalleRepositorio budr = new BitacoraUsuariosDetalleRepositorio();
            BitacoraUsuariosRepositorio bur = new BitacoraUsuariosRepositorio();


            string cadena = string.Empty;
            if (comun.n.bitacorausuarios.Seleccionar_SiOportunidadTieneResponsablesAsignados(idoportunidad) == 1)
            {
                cadena = "<td align='center'><i class='fas fa-users' style='color: green' data-toggle='tooltip' data-placement='left' title='Con Responsables Asignados (Dé click para mostrar la bitácora)' onclick='UsuariosBitacoraOportunidad(" + idoportunidad + ")'></i></td>";
            }
            else if (comun.n.bitacorausuarios.Seleccionar_SiOportunidadTieneResponsablesAsignados(idoportunidad) == 0)
            {
                cadena = "<td align='center'><i class='fas fa-user-slash' style='color: orange' data-toggle='tooltip' data-placement='top' title='Sin Responsables Asignados'></i></td>";
            }
            return MvcHtmlString.Create(cadena);

        }

        public static MvcHtmlString ResponsablesSiNoASAE(string idoportunidad)
        {
            /*
            Comun comun = new Comun();
            comun.oporrespr.SeleccionarSiOportunidadTieneResponsablesAsignados(idoportunidad);
            comun.budr.SeleccionarSiOportunidadTieneUsuariosResponsablesAsignados(idoportunidad);
            */
            OportunidadesResponsablesRepositorio orr = new OportunidadesResponsablesRepositorio();
            BitacoraUsuariosDetalleRepositorio budr = new BitacoraUsuariosDetalleRepositorio();
            BitacoraUsuariosRepositorio bur = new BitacoraUsuariosRepositorio();

            string cadena = string.Empty;
            //if (bur.SeleccionarSiOportunidadTieneResponsablesAsignados(idoportunidad) == 1)
            //{
            cadena = "<td align='center'><i class='fas fa-users' style='color: green' data-toggle='tooltip' data-placement='left' title='Con Responsables Asignados (Dé click para mostrar la bitácora)' onclick='UsuariosBitacoraOportunidad(" + idoportunidad + ")'></i></td>";
            //}
            //else if (bur.SeleccionarSiOportunidadTieneResponsablesAsignados(idoportunidad) == 0)
            //{
            //    cadena = "<td align='center'><i class='fas fa-user-slash' style='color: orange' data-toggle='tooltip' data-placement='top' title='Sin Responsables Asignados'></i></td>";
            //}
            return MvcHtmlString.Create(cadena);

        }

        public static MvcHtmlString EtiquetaNombre(string idconfiguracion)
        {
            string nombre = string.Empty;
            if (idconfiguracion == "3")
            {
                nombre = "Asunto";
            }
            else if (idconfiguracion == "2")
            {
                nombre = "Descripción";
            }

            return MvcHtmlString.Create("<label for='Asunto' class='col-md-4 labe1'>" + nombre + "</label>");
        }

        public static MvcHtmlString ActividadesDeOportunidad(string idoportunidad)
        {
            Comun comun = new Comun();

            string cadena = string.Empty;
            if (comun.n.oportunidadesactividades.Seleccionar_CuantasActividadesTieneOportunidad(idoportunidad) > 0)
            {
                cadena = "<td align='center'><i class='fas fa-layer-group' style='color: green' data-toggle='tooltip' data-placement='left' title='Mostrar Actividades (Dé click para mostrar)' onclick='UsuariosActividadesOportunidad(" + idoportunidad + ")'></i></td>";
            }
            else
            {
                cadena = "<td align='center'><i class='fas fa-layer-group' style='color: orange' data-toggle='tooltip' data-placement='left' title='No se han agregado actividades'></i></td>";
            }
            return MvcHtmlString.Create(cadena);

        }

        public static MvcHtmlString ArchivosDeOportunidad(string idoportunidad)
        {
            OportunidadesActividadesRepositorio oar = new OportunidadesActividadesRepositorio();
            ArchivosRepositorio ar = new ArchivosRepositorio();
            Comun comun = new Comun();

            string cadena = string.Empty;
            if (comun.n.archivos.Cuantos_ArchivosTieneOportunidad(idoportunidad) > 0)
            {
                cadena = "<td align='center'><i class='fas fa-file' style='color: green' data-toggle='tooltip' data-placement='left' title='Mostrar Archivos (Dé click para mostrar)' onclick='ArchivosOportunidad(" + idoportunidad + ")'></i></td>";
            }
            else
            {
                cadena = "<td align='center'><i class='fas fa-file' style='color: orange' data-toggle='tooltip' data-placement='left' title='No se han agregado archivos'></i></td>";
            }
            return MvcHtmlString.Create(cadena);

        }

        public static MvcHtmlString NombreUsuario(string idusuario)
        {
            Comun comun = new Comun();
            string cadena = string.Empty;
            if (string.IsNullOrEmpty(idusuario) || idusuario == "0")
            {
                cadena = "<span>No Aplica</span>";
            }
            else
            {
                cadena = comun.n.usuarios.Seleccionar_Nombre(idusuario);
            }
            return MvcHtmlString.Create(cadena);
        }

        public static MvcHtmlString NombreCreadorOportunidad(string idoportunidad)
        {
            Comun comun = new Comun();
            var nombre = comun.n.usuarios.Seleccionar_Nombre(comun.n.usuarios.Seleccionar_CreadorOportunidad(idoportunidad));
            return MvcHtmlString.Create(nombre);
        }

        public static MvcHtmlString SemaforoCompletadoEmpresa(string idempresa)
        {
            //Revisa si los datos de la empresa está o no completados
            Comun comun = new Comun();
            string cadena = string.Empty;
            if (comun.n.empresas.Seleccionar_SiCamposIncompletos(idempresa) == "Completo")
            {
                cadena = "<td align='center'><i class='fas fa-check' style='color: green' data-toggle='tooltip' data-placement='bottom' title='Datos Completos'></i></td>";
            }
            else
            {
                cadena = "<td align='center'><i class='fas fa-times' style='color: orange' data-toggle='tooltip' data-placement='top' title='Datos Incompletos'></i></td>";
            }
            return MvcHtmlString.Create(cadena);
        }

        public static MvcHtmlString TipoDireccion(string idtipodireccion)
        {            
            string cadena = string.Empty;
            if (idtipodireccion == "1")
            {
                cadena = "Dirección";
            }
            else
            {
                cadena = "Fiscal";
            }
            return MvcHtmlString.Create(cadena);
        }

        /// <summary>
        /// Crea un menú dinámico de un nivel desde una base de datos
        /// </summary>
        /// <param name="idrol"></param>
        /// <param name="idconfiguracion"></param>
        /// <returns></returns>
        public static string Menu(string idrol, string idconfiguracion)
        {
            Models.Business.Menu m = new Models.Business.Menu();
            string cadena = string.Empty;
            foreach (var item in m.Seleccionar_Registros(idrol, idconfiguracion))
            {
                cadena += "<li class='nav-item' id='" + item.Menu.IdJquery + "'>";
                cadena += "<a href=" + item.Menu.Ruta + ">";
                cadena += "<i class='" + item.Menu.Icono + "'></i>";
                cadena += "<p>" + item.Menu.Nombre + "</p>";
                cadena += "</a>";
                cadena += "</li>";
            }
            cadena += "<li>&nbsp;</li>";
            return cadena;
        }

        public static bool ValidarSiUsuarioPuedeVerOportunidadesNoSuyas(string idoportunidad, string idusuario)
        {
            //Comun comun = new Comun();
            //if (comun.n.usuarios.Seleccionar_CreadorOportunidad(idoportunidad) == idusuario
            //    || comun.n.compartiroportunidades.Validar_SiUsuarioPuedeModificar(idoportunidad, idusuario)
            //    || idusuario == "2")
            if (new ValidacionPropietarioPermiso().OportunidadValidarPropietarioPermiso(idoportunidad, idusuario))
                return true;
            else
                return false;
        }

        //********************* Documentos

        public static MvcHtmlString EstadosMarketing(string idestado)
        {
            string cadena = string.Empty;
            if (idestado == "1")
                cadena = "<div class='text-success'>" + CRM.Models.Catalogos.SeleccionarNombrePorId(idestado, "MarketingEstado") + "</div>";
            else if (idestado == "2" || idestado == "3")
                cadena = "<div class='text-warning'>" + CRM.Models.Catalogos.SeleccionarNombrePorId(idestado, "MarketingEstado") + "</div>";
            return MvcHtmlString.Create(cadena);
        }

        /// <summary>
        /// Crea dinámicamente clasificación y subclasificación de documentos desde una tabla
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString ClasificacionesSubClasificacionesDocumentosASAE()
        {
            string cadena = string.Empty;
            foreach (var items in Catalogos.Seleccionar("DocumentosASAEClasificacion"))
            {
                var encabezado = "heading" + items.Id;
                var colapso = "colapso" + items.Id;
                var gcolapso = "#colapso" + items.Id;

                cadena += "<div class='card'>";
                cadena += "<div class='card-header collapsed' id=" + encabezado + " data-toggle='collapse' data-target=" + gcolapso+ " aria-expanded='false' aria-controls=" + colapso +" role='button'>";
                cadena += "<div class='span-icon'>";
                cadena += "<div class='fas fa-folder text-success'></div>";
                cadena += "</div>";
                cadena += "<div class='span-title text-success'>";
                cadena += items.Nombre;
                cadena += "</div>";
                cadena += "<div class='span-mode text-success'></div>";
                cadena += "</div>";

                cadena += "<div id=" + colapso + " class='collapse' aria-labelledby=" + encabezado + " data-parent='#accordion'>";
                cadena += "<div class='card-body'>";
                
                if (Catalogos.SeleccionarClasificacionesDocumentosASAE(items.Id.ToString()).Count() > 0)
                {
                    cadena += "<table class='table table-striped table-hover table-bordered table-head-bg-success' width='100%'>";
                    cadena += "<thead>";
                    cadena += "<tr><th>Nombre</th><th>Fecha</th><th>Descripción</th><th>Versión</th><th>Vigencia</th><th>Usuarios</th></tr>";
                    cadena += "</thead>";
                    cadena += "<tbody>";
                    foreach (var datos in Catalogos.SeleccionarClasificacionesDocumentosASAE(items.Id.ToString()))
                    {
                        var vigente = datos.Vigencia == true ? "Sí" : "No";
                        cadena += "<tr>";
                        cadena += "<td><i class='fas fa-file text-info'></i>&nbsp;<a href='#' onclick='ModificarRegistro(@datos.Id);'>" + datos.Nombre + "</a></td>";
                        cadena += "<td>" + datos.Fecha + "</td>";
                        cadena += "<td>" + datos.Descripcion + "</td>";
                        cadena += "<td class='text-center'>" + datos.VersionUsuarios + "</td>";
                        cadena += "<td class='text-center'>" + vigente + "</td>";
                        cadena += "<td class='text-center'>";
                        cadena += "<h4>";
                        cadena += "<a href='#' onclick='CargarUsuarios(" + datos.Id + ");'><i class='fas fa-users text-secondary' data-toggle='tooltip' data-placement='left' title='Usuarios asignados'></i></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        cadena += "</h4>";
                        cadena += "</td>";
                        cadena += "</tr>";
                    }
                    cadena += "</tbody>";
                    cadena += "</table>";
                }
                else
                {
                    cadena += "<label class='label label-align-center'>No se han agregado documentos.</label>";
                }

                foreach (var itemss in Catalogos.SeleccionarSubConsulta("DocumentosASAESubClasificacion", "IdClasificacion", items.Id.ToString()))
                {
                    var encabezadoo = "headingg" + itemss.Id;
                    var colapsoo = "colapsoo" + itemss.Id;
                    var gcolapsoo = "#colapsoo" + itemss.Id;
                    cadena += "<div class='card'>";
                    cadena += "<div class='card-header collapsed' id=" + encabezadoo + " data-toggle='collapse' data-target=" + gcolapsoo + " aria-expanded='false' aria-controls=" + colapsoo + " role='button'>";
                    cadena += "<div class='span-icon'>";
                    cadena += "<div class='fas fa-folder'></div>";
                    cadena += "</div>";
                    cadena += "<div class='span-title'>";
                    cadena += itemss.Nombre;
                    cadena += "</div>";
                    cadena += "<div class='span-mode'></div>";
                    cadena += "</div>";

                    cadena += "<div id=" + colapsoo + " class='collapse' aria-labelledby=" + encabezadoo +" data-parent='#accordion'>";
                    cadena += "<div class='card-body' style='background-color: gainsboro'>";
                    
                        if (Catalogos.SeleccionarClasificacionSubClasificacionDocumentosASAE(items.Id.ToString(), itemss.Id.ToString()).Count() > 0)
                        {
                        cadena += "<table class='table table-striped table-hover table-bordered table-head-bg-secondary'>";
                        cadena += "<thead>";
                        cadena += "<tr><th>Nombre</th><th>Fecha</th><th>Descripción</th><th>Versión</th><th>Vigencia</th><th>Usuarios</th></tr>";
                        cadena += "</thead>";
                        cadena += "<tbody>";
                            foreach (var datoss in Catalogos.SeleccionarClasificacionSubClasificacionDocumentosASAE(items.Id.ToString(), itemss.Id.ToString()))
                            {
                                var vigentee = datoss.Vigencia == true ? "Sí" : "No";
                                cadena += "<tr>";
                                cadena += "<td><i class='fas fa-file text-info'></i><a href='#' onclick='ModificarRegistro("+ datoss.Id +");'>" + datoss.Nombre + "</a></td>";
                                cadena += "<td>" + datoss.Fecha + "</td>";
                                cadena += "<td>" + datoss.Descripcion + "</td>";
                                cadena += "<td class='text-center'>" + datoss.VersionUsuarios + "</td>";
                                cadena += "<td class='text-center'>" + vigentee + "</td>";
                                cadena += "<td class='text-center'>";
                                cadena += "<h4>";
                                cadena += "<a href='#' onclick='CargarUsuarios(" + datoss.Id + ");><i class='fas fa-users text-secondary' data-toggle='tooltip' data-placement='left' title='Usuarios asignados'></i></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                cadena += "</h4>";
                                cadena += "</td>";
                                cadena += "</tr>";
                            }
                        cadena += "</tbody>";
                        cadena += "</table>";
                        }
                        else
                        {
                            cadena += "<label class='label label-align-center'>No se han agregado documentos.</label>";
                        }

                    cadena += "</div>";
                    cadena += "</div>";
                    cadena += "</div>";
                }
                cadena += "</div>";
                cadena += "</div>";
                cadena += "</div>";
            }
            return MvcHtmlString.Create(cadena);
        }

        //********************* Campañas

        public static MvcHtmlString EstadoCampaña(int estado)
        {
            string devolver = "";
            if (estado == 1)
            {
                devolver = "En Proceso";
            }
            else if (estado == 2)
            {
                devolver = "Terminada";
            }
            else if (estado == 3)
            {
                devolver = "Cancelada";
            }

            return MvcHtmlString.Create(devolver);
        }

        public static MvcHtmlString EnviarANombre(int id)
        {
            string enviara = "";
            if (id == 1)
            {
                enviara = "Todos";
            }
            else if (id == 2)
            {
                enviara = "Confirmados";
            }
            else if (id == 3)
            {
                enviara = "No Confirmados";
            }
            else if (id == 4)
            {
                enviara = "Asistió";
            }
            else if (id == 5)
            {
                enviara = "No asistió";
            }

            return MvcHtmlString.Create(enviara);
        }

        public static MvcHtmlString NombreArchivoICS(string idcampaña)
        {
            Comun comun = new Comun();
            var nombrearchivo = comun.n.marketing.Seleccionar_PorId(idcampaña).Consecutivo + ".ics";
            return MvcHtmlString.Create("<a href=/ICS/" + nombrearchivo + ">Descargar archivo</a>");
        }

        public static MvcHtmlString CadenaAgenda(string idcampaña)
        {
            Comun comun = new Comun();
            var nombrearchivo = comun.n.marketing.Seleccionar_PorId(idcampaña).Consecutivo + ".ics";
            return MvcHtmlString.Create("<pre><code>&lt;a href=&quot;http://187.248.23.163:1053/ICS/" + nombrearchivo + "&quot;&gt;&lt;img src=&quot;http://187.248.23.163:1053/assets/img/calendario.png&quot; /&gt;&lt;/a&gt;</code></pre>");
        }

        public static MvcHtmlString ContenidoPlantillas()
        {
            Comun comun = new Comun();
            var datos = comun.n.marketingcorreoplantillas.Seleccionar_();
          
            string contenido = "";

            contenido += "<div class='row'>";

            for (int i = 0; i < datos.Count();)
            {
                contenido += "<div class='col-md-3'>";
                contenido += "   <div class='card'>";
                contenido += "       <div class='card-body'>";
                contenido += "           <div class='card-text'>"+ datos[i].Nombre +"</div>";
                contenido += "           <center><img src='../../assets/img/"+ datos[i].Nombre + ".png' style='border:1px solid gray;' class='responsiva' /></center>";
                contenido += "           <br />";
                contenido += "           <div class='text-center text-small'>";
                contenido += "              <a href='#' onclick='CopiarCodigo("+ datos[i].Id + ");'>Copiar</a> | <a href='#' data-toggle='modal' data-target='#ModalVistaPrevia' data-id='"+ datos[i].Nombre +".png'>Vista Previa</a>";
                contenido += "           </div>";
                contenido += "       </div>";
                contenido += "   </div>";
                contenido += "</div>";
                i++;
            }
            contenido += "</div>";

            return MvcHtmlString.Create(contenido);
        }


        //******************************** Selects *******************************

        public static MvcHtmlString SelectResponsables(string idconfiguracion, string idempresa, string idusuario, string idrol)
        {
            Comun comun = new Comun();

            string cadena = string.Empty;
            cadena = "<select class='form-control form-control-sm input-solid' id='Responsables' name='Responsables' required style='width: 200px'>";
            
            cadena += "<option value=''>&nbsp;</option>";
            if (idconfiguracion == "3")
            {
                foreach (var item in comun.n.usuarios.Seleccionar_UsuariosResponsables(idconfiguracion, idempresa, idusuario, idrol))
                {
                    cadena += "<option value=" + item.Id + ">" + item.ApellidoPaterno.Trim() + " " + item.ApellidoMaterno.Trim() + " " + item.Nombre + "</option>";
                }
             }
            else if (idconfiguracion == "2")
            {
                foreach (var item in comun.n.usuarios.Seleccionar_UsuariosGerentes(idconfiguracion,idusuario, idrol))
                {
                    cadena += "<option value=" + item.Id + ">" + item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno + "</option>";
                }
            }
            cadena += "</select>";
            
            return MvcHtmlString.Create(cadena);
        }

        public static MvcHtmlString SelectResponsablesOptionsSABE(string idconfiguracion, string idempresa, string idusuario, string idrol)
        {
            Comun comun = new Comun();

            string cadena = string.Empty;
            cadena += "<option value=''>&nbsp;</option>";
            foreach (var item in comun.n.usuarios.Seleccionar_UsuariosResponsables(idconfiguracion, idempresa, idusuario, idrol))
            {
                cadena += "<option value=" + item.Id + ">" + item.ApellidoPaterno.Trim() + " " + item.ApellidoMaterno.Trim() + " " + item.Nombre + "</option>";
            }
                       
            return MvcHtmlString.Create(cadena);
        }

        public static MvcHtmlString SelectResponsablesOptionsASAE(string idconfiguracion, string idusuario)
        {
            UsuariosRepositorio ur = new UsuariosRepositorio();
            Comun comun = new Comun();

            string cadena = string.Empty;
            cadena += "<option value=''>&nbsp;</option>";
            foreach (var item in comun.n.usuarios.Seleccionar_UsuariosResponsablesASAE(idconfiguracion, idusuario))
            {
                cadena += "<option value=" + item.Id + ">" + item.ApellidoPaterno.Trim() + " " + item.ApellidoMaterno.Trim() + " " + item.Nombre + "</option>";
            }            
            return MvcHtmlString.Create(cadena);
        }

        public static MvcHtmlString SelectResponsablesOptionsdeASAE(string idusuario)
        {
            UsuariosRepositorio ur = new UsuariosRepositorio();
            Comun comun = new Comun();

            string cadena = string.Empty;
            cadena += "<option value=''>&nbsp;</option>";
            foreach (var item in comun.n.usuarios.Seleccionar_UsuariosResponsablesdeASAE(idusuario))
            {
                cadena += "<option value=" + item.Id + ">" + item.Nombre + " " + item.ApellidoPaterno.Trim() + " " + item.ApellidoMaterno.Trim() +"</option>";
            }
            return MvcHtmlString.Create(cadena);
        }

        public static MvcHtmlString SelectEmpresas(string idconfiguracion, string IdRol, string IdUsuario)
        {
            Comun comun = new Comun();

            string cadena = string.Empty;
            cadena = "<select class='form-control form-control-sm input-solid' id='Empresa' name='Empresa' required style='width: 180px' onchange='DireccionEmpresa()'>";

            cadena += "<option value=''>&nbsp;</option>";
            foreach (var item in comun.n.empresas.Seleccionar_Registros(idconfiguracion, IdRol, IdUsuario))
            {
                cadena += "<option value=" + item.Id + ">" + item.Nombre + "</option>";
            }
            cadena += "</select>";

            return MvcHtmlString.Create(cadena);
        }

        public static MvcHtmlString SelectEmpresaSABEEdicion(string idconfiguracion, string IdRol, string IdUsuario)
        {
            Comun comun = new Comun();

            string cadena = string.Empty;
            cadena = "<select class='form-control form-control-sm input-solid' id='Empresa' name='Empresa' required style='width: 220px' onchange='DireccionEmpresa()'>";

            cadena += "<option value=''>&nbsp;</option>";
            foreach (var item in comun.n.empresas.Seleccionar_Registros(idconfiguracion, IdRol, IdUsuario))
            {
                cadena += "<option value=" + item.Id + ">" + item.Nombre + "</option>";
            }
            cadena += "</select>";

            return MvcHtmlString.Create(cadena);
        }

        public static MvcHtmlString SelectEmpresasProveedor()
        {
            Comun comun = new Comun();

            string cadena = string.Empty;
            cadena = "<select class='form-control form-control-sm input-solid' id='Empresa' name='Empresa' required style='width: 180px'>";

            cadena += "<option value=''>&nbsp;</option>";
            foreach (var item in comun.n.empresasproveedores.Seleccionar_Registros())
            {
                cadena += "<option value=" + item.Id + ">" + item.Nombre + "</option>";
            }
            cadena += "</select>";

            return MvcHtmlString.Create(cadena);
        }

        public static MvcHtmlString SelectUnidadDeNegocio()
        {
            UDNRepositorio udnr = new UDNRepositorio();
            Comun comun = new Comun();

            string cadena = string.Empty;
            cadena = "<select class='form-control form-control-sm input-solid' id='UDN' name='UDN' required style='width: 200px'>";

            cadena += "<option value=''>&nbsp;</option>";
            foreach (var item in comun.n.udn.Seleccionar_Registros())
            {
                cadena += "<option value=" + item.Id + ">" + item.Nombre + "</option>";
            }
            cadena += "</select>";

            return MvcHtmlString.Create(cadena);
        }

        public static string EstadoPorCodigoPostal(string cp)
        {
            Comun comun = new Comun();
            if (!string.IsNullOrEmpty(cp))
                return comun.n.codigopostal.Seleccionar_EstadoPorCodigoPostal(cp);
            else
                return "";
        }

        public static MvcHtmlString TipoActividad()
        {
            string cadena = string.Empty;
            cadena = "<select class='form-control form-control-sm input-solid' id='TipoActividad' name='TipoActividad' required style='width: 200px'>";

            cadena += "<option value=''>&nbsp;</option>";
            foreach (var item in Catalogos.Seleccionar("TipoActividad"))
            {
                cadena += "<option value=" + item.Id + ">" + item.Nombre + "</option>";
            }
            cadena += "</select>";

            return MvcHtmlString.Create(cadena);
            
        }

        public static MvcHtmlString Roles()
        {
            string cadena = string.Empty;
            cadena = "<select class='form-control form-control-sm input-solid' id='Roles' name='Roles' required style='width: 200px'>";

            cadena += "<option value=''>&nbsp;</option>";
            foreach (var item in Catalogos.Seleccionar("Roles"))
            {
                cadena += "<option value=" + item.Id + ">" + item.Nombre + "</option>";
            }
            cadena += "</select>";

            return MvcHtmlString.Create(cadena);

        }

        public static MvcHtmlString ConfiguracionEmpresas()
        {
            string cadena = string.Empty;
            cadena = "<select class='form-control form-control-sm input-solid' id='Empresa' name='Empresa' required style='width: 200px'>";

            cadena += "<option value=''>&nbsp;</option>";
            foreach (var item in Catalogos.Seleccionar("ConfiguracionEmpresas"))
            {
                cadena += "<option value=" + item.Id + ">" + item.Nombre + "</option>";
            }
            cadena += "</select>";

            return MvcHtmlString.Create(cadena);

        }
    }


}