using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using CRM.Models.Models;
using System.Collections.Generic;

namespace CRM.Controllers
{
    public class DocumentosASAEController : Utilerias.Comun
    {
        // GET: DocumentosASAE

        /// <summary>
        /// En desuso
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inicio()
        {
            //ViewBag.Constitucion = n.documentosasae.Seleccionar_Registros_PorClasificacion("5"); //Marketing
            //ViewBag.Financiera = n.documentosasae.Seleccionar_Registros_PorClasificacion("1");
            //ViewBag.Formatos = n.documentosasae.Seleccionar_Registros_PorClasificacion("3");
            //ViewBag.Legal = n.documentosasae.Seleccionar_Registros_PorClasificacion("2");
            //ViewBag.Politicas = n.documentosasae.Seleccionar_Registros_PorClasificacion("4");
            //ViewBag.Ventas = n.documentosasae.Seleccionar_Registros_PorClasificacion("6"); //Ventas

            //ViewBag.PP0401 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "1");
            //ViewBag.PP0402 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "2");
            //ViewBag.PP0403 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "3");
            //ViewBag.PP0404 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "4");
            //ViewBag.PP0405 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "5");
            //ViewBag.PP0406 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "6");
            //ViewBag.PP0407 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "7");
            //ViewBag.PP0408 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "8");
            //ViewBag.PP0409 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "9");
            //ViewBag.PP0410 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "10");
            //ViewBag.PP0411 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "11");
            //ViewBag.PP0412 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "12");
            //ViewBag.PP0413 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "13");
            //ViewBag.PP0414 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "14");
            //ViewBag.PP0415 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "15");
            //ViewBag.PP0416 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "16");
            //ViewBag.PP0417 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "17");
            //ViewBag.PP0418 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "18");
            //ViewBag.PP0419 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "19");
            //ViewBag.PP0420 = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("4", "20");

            //ViewBag.VtaCRM = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("6", "21");
            //ViewBag.VtaTickets = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("6", "22");
            //ViewBag.VtaWorkFlow = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("6", "23");
            //ViewBag.VtaXpinnit = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("6", "24");
            //ViewBag.VtaJobCtrl = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("6", "25");
            //ViewBag.VtaASAE = n.documentosasae.Seleccionar_Registros_PorClasificacionSubClasificacion("6", "26");



            return View();
        }

        public ActionResult Agregar()
        {
            ViewBag.SubClas = n.documentosasaesubclasificaciones.Seleccionar_Registros();
            return View();
        }

        //*************** Procesos Json

        public JsonResult CargaInicialArchivos()
        {
            return Json(n.documentosasae.Seleccionar_Registros(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ArchivoSeleccionarPorId(string Id)
        {
            return Json(n.documentosasae.Seleccionar_PorId(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistroModificadoGuardar(string Id, string cl, string de, string ve, string vi, string us, string vu, string fo, string sc)
        {
            if (n.documentosasaeusuarios.ValidarSiUsuarioEstaAsignadoADocumento(Id, us))
            {
                Models.Models.DocumentosASAE items = new Models.Models.DocumentosASAE() {
                    Id = int.Parse(Id),
                    Clasificacion = int.Parse(cl),
                    Descripcion = de,
                    Version = ve,
                    Vigencia = vi == "1" ? true : false,
                    VersionUsuarios = vu,
                    FechaOficial = !string.IsNullOrEmpty(fo) ? DateTime.Parse(fo) : DateTime.Now,
                    SubClasificacion = string.IsNullOrEmpty(sc) ? 0 : int.Parse(sc)
                };
                n.documentosasae.Modificar_Registro(items);
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("-1", JsonRequestBehavior.AllowGet);
        }  

        /// <summary>
        /// Subir archivo por primera vez
        /// </summary>
        /// <returns></returns>
        public JsonResult SubirArchivo()
        {
            int valido = 0;
            // checar si el usuario seleccionó un archivo para subir
            if (Request.Files.Count > 0)
            {
                
                try
                {
                    var clasificado = Request.Form.GetValues(0);
                    var descripcion = Request.Form.GetValues(1);
                    var version = Request.Form.GetValues(2);
                    var vigencia = Request.Form.GetValues(3);
                    var versionusuarios = Request.Form.GetValues(4);
                    var fechaoficial = Request.Form.GetValues(5);
                    var subclasificado = Request.Form.GetValues(6);

                    HttpFileCollectionBase files = Request.Files;

                    HttpPostedFileBase file = files[0];
                    string fileName = file.FileName;


                    //Procesar la extensión del archivo sin importar su longitud
                    string extension = Utilerias.Funciones.ObtenerExtensionArchivo(fileName);

                    //Validar si la extensión del archivo esta permitida
                    if (!n.documentosasaetipoarchivo.ValidarSiPermitido(extension))
                    {
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }

                    // crear un directorio para subir los archivos si no existe
                    string ruta1 = Path.Combine(Server.MapPath("~/Documentos/"), fileName);

                    if (System.IO.File.Exists(ruta1))
                    {
                        return Json(2, JsonRequestBehavior.AllowGet);
                    }

                    // guardar el archivo
                    file.SaveAs(ruta1);

                    if (ruta1.Contains(fileName))
                    {
                        //Agregar los archivos por aqui
                        Models.Models.DocumentosASAE items = new Models.Models.DocumentosASAE()
                        {
                            Nombre = fileName,
                            IdUsuario = int.Parse(Session["IdUsuario"].ToString()),
                            Clasificacion = int.Parse(clasificado[0]),
                            Descripcion = descripcion[0],
                            Version = version[0],
                            Vigencia = vigencia[0] == "1" ? true : false,
                            VersionUsuarios = versionusuarios[0],
                            FechaOficial = string.IsNullOrEmpty(fechaoficial[0]) ? DateTime.Now : DateTime.Parse(fechaoficial[0]),
                            SubClasificacion = string.IsNullOrEmpty(subclasificado[0]) ? 0 : int.Parse(subclasificado[0])
                        };
                        int obtenido = n.documentosasae.Agregar_Registro(items);

                        //Agregar el usuario originador
                        Models.Models.DocumentosASAEUsuarios items2 = new Models.Models.DocumentosASAEUsuarios()
                        {
                            IdUsuario = int.Parse(Session["IdUsuario"].ToString()),
                            IdClasificacion = 1,  //int.Parse(clasificado[0]),
                            IdDocumento = obtenido,
                        };
                        n.documentosasaeusuarios.Agregar_Registro(items2);

                        //Agregar el documento al histórico
                        Models.Models.DocumentosASAEHistorico items3 = new Models.Models.DocumentosASAEHistorico()
                        {
                            IdDocumento = obtenido,
                            Nombre = fileName,
                            Version = 1,
                            IdUsuario = int.Parse(Session["IdUsuario"].ToString()),
                            IdUsuarioPosicion = 1,
                            Comentarios = "Agregado por primera vez"
                        };
                        n.documentosasaehistorico.Agregar_Registro(items3);

                        return Json(0, JsonRequestBehavior.AllowGet);
                    }                    
                }
                catch (Exception e)
                {
                    return Json("error" + e.Message);
                }
            }

            return Json("Nada");
        }

        public JsonResult Autorizacion(string iddocumento, string notas)
        {
            if (n.documentosasaeusuarios.ValidarSiUsuarioEstaAsignadoADocumento(iddocumento, Session["IdUsuario"].ToString()))
            {
                //Models.Models.DocumentosASAEHistorico items = new Models.Models.DocumentosASAEHistorico();
                Models.Models.DocumentosASAEHistorico items = n.documentosasaehistorico.Seleccionar_DetalleUltimaVersionDocumento(iddocumento);
                string archivo = items.Nombre;
                int versionactual = items.Version;
                string archivoanterior = "";
                string extension = "";
                int posicion1 = 0;
                if (versionactual == 1)
                {
                    posicion1 = archivo.Substring(0, archivo.LastIndexOf(".")).Length;
                    extension = items.Nombre.Substring(items.Nombre.LastIndexOf("."));

                }
                string nombre1 = archivo.Substring(0, posicion1);
                string nombre2 = (items.Version < 10) ? "_0" + (items.Version).ToString() : "_" + (items.Version).ToString();
                archivoanterior = nombre1 + nombre2 + extension;

                //Actualizar el antepenúltimo registro
                Models.Models.DocumentosASAEHistorico items1 = new Models.Models.DocumentosASAEHistorico() {
                    Nombre = archivoanterior,
                    IdDocumento = int.Parse(iddocumento),
                    Version = items.Version
                };
                n.documentosasaehistorico.Modificar_AnteriorAlaUltimaVersion(items1);

                //Guardar los datos recibidos en el histórico
                Models.Models.DocumentosASAEHistorico items2 = new Models.Models.DocumentosASAEHistorico()
                {
                    IdDocumento = int.Parse(iddocumento),
                    Comentarios = string.IsNullOrEmpty(notas) ? "El usuario no agrego comentarios" : notas,
                    Nombre = archivo,
                    Version = items.Version + 1,
                    IdUsuario = int.Parse(Session["IdUsuario"].ToString()),
                    IdUsuarioPosicion = n.documentosasaeusuarios.Seleccionar_Posicion(Session["IdUsuario"].ToString(), iddocumento)
                };
                n.documentosasaehistorico.Agregar_Autorizacion(items2);

                //Actualizar la version en documentos asae
                Models.Models.DocumentosASAE items3 = new Models.Models.DocumentosASAE() {
                    Id = int.Parse(iddocumento),
                    Version = int.Parse((items.Version + 1).ToString()).ToString()
                };
                n.documentosasae.Modificar_Version(items3);

                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("-1", JsonRequestBehavior.AllowGet);
        }

        public JsonResult QuitarArchivo(string idarchivo, string idusuario)
        {
            if (n.documentosasaeusuarios.ValidarSiUsuarioEstaAsignadoADocumento(idarchivo, Session["IdUsuario"].ToString()))
            {
                var _NombreArchivo = n.documentosasae.Seleccionar_PorId(idarchivo).Nombre;
                string _ruta = Path.Combine(Server.MapPath("~/Documentos"), _NombreArchivo);
                if (System.IO.File.Exists(_ruta))
                {
                    System.IO.File.Delete(_ruta);

                    //Eliminar todos los archivos relacionados
                    foreach(var archivos in n.documentosasaehistorico.Seleccionar_PorIdDocumento(idarchivo))
                    {
                        var _NombreDeArchivo = archivos.DocumentosASAEHistorico.Nombre;
                        string _rutta = Path.Combine(Server.MapPath("~/Documentos"), _NombreDeArchivo);
                        System.IO.File.Delete(_rutta);
                    }

                    //Eliminar tambien de la bd
                    n.documentosasae.Eliminar_Registro(idarchivo);
                    n.documentosasaehistorico.Eliminar_Registro(idarchivo);
                    n.documentosasaeusuarios.Eliminar_Registro(idarchivo);
                }

                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EnviarCorreo(string eUsuario, string eAsignado, string eDescripcion, string IdRegistro)
        {
            //Enviar un correo a un usuario relacionado con el archivo
            if (n.documentosasaeusuarios.ValidarSiUsuarioEstaAsignadoADocumento(IdRegistro, Session["IdUsuario"].ToString()))
            {
                //Asignar al usuario con la posición seleccionada y guardarla. 
                Models.Models.DocumentosASAEUsuarios items = new Models.Models.DocumentosASAEUsuarios()
                {
                    IdUsuario = int.Parse(eUsuario),
                    IdClasificacion = int.Parse(eAsignado),
                    IdDocumento = int.Parse(IdRegistro)
                };
                n.documentosasaeusuarios.Agregar_Registro(items);
                //Obtener el correo del usuario
                var correodelusuario = n.usuarios.Seleccionar_PorId(eUsuario).Usuarios.Correo;
                //Obtener el nombre del documento para agregarlo en el correo
                var docnombre = n.documentosasae.Seleccionar_PorId(IdRegistro).Nombre;

                correo.EnviarCorreo(correodelusuario, "Manejo de documentos de ASAE", "Documento " + docnombre, eDescripcion + "<br><br>");
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("-1", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarHistoricoDocumento(string iddocumento)
        {
            return Json(n.documentosasaehistorico.Seleccionar_PorIdDocumento(iddocumento), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeleccionarUsuariosAsignados(string iddocumento)
        {
            return Json(n.documentosasaeusuarios.Seleccionar_UsuariosAsignados(iddocumento), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReSubirArchivo()
        {            
            // checar si el usuario seleccionó un archivo para subir
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;

                    var notas = Request.Form["notas"];
                    var iddoc = Request.Form["iddoc"];

                    if (n.documentosasaeusuarios.ValidarSiUsuarioEstaAsignadoADocumento(iddoc, Session["IdUsuario"].ToString()))
                    {

                        HttpPostedFileBase file = files[0];
                        string fileName = file.FileName;

                        // crear un directorio para subir los archivos si no existe
                        string ruta1 = Path.Combine(Server.MapPath("~/Documentos/"), fileName);

                        //Procesar la extensión del archivo sin importar su longitud
                        string extension = Utilerias.Funciones.ObtenerExtensionArchivo(fileName);

                        string nombrearchivo = fileName.Substring(0, fileName.LastIndexOf("."));
                        string ultimaversion = n.documentosasaehistorico.Seleccionar_UltimaVersionDocumento(iddoc).ToString();

                        string subguion = (int.Parse(ultimaversion) < 10) ? "_0" : "_";

                        string archivofinal = nombrearchivo + subguion + ultimaversion + extension;

                        //Buscar un archivo que se llame igual en el directorio y cambiarlo a una nueva version
                        string ruta2 = Path.Combine(Server.MapPath("~/Documentos/"), archivofinal);
                        System.IO.File.Move(ruta1, ruta2);

                        //Guardar el archivo nuevo
                        file.SaveAs(ruta1);

                        //Guardar el histórico
                        Models.Models.DocumentosASAEHistorico items = new Models.Models.DocumentosASAEHistorico()
                        {
                            IdDocumento = int.Parse(iddoc),
                            Nombre = fileName,
                            Fecha = DateTime.Now,
                            Version = int.Parse(ultimaversion + 1),
                            IdUsuario = int.Parse(Session["IdUsuario"].ToString()),
                            Comentarios = notas,
                            IdUsuarioPosicion = n.documentosasaeusuarios.Seleccionar_Posicion(Session["IdUsuario"].ToString(), iddoc)
                        };
                        n.documentosasaehistorico.Agregar_Registro(items);

                        //Actualizar el registro anterior del histórico
                        Models.Models.DocumentosASAEHistorico items2 = new Models.Models.DocumentosASAEHistorico()
                        {
                            IdDocumento = int.Parse(iddoc),
                            Nombre = archivofinal,
                            Version = int.Parse(ultimaversion)
                        };
                        n.documentosasaehistorico.Modificar_Registro(items2);

                        //Actualizar la versión en la tabla principal
                        Models.Models.DocumentosASAE items3 = new Models.Models.DocumentosASAE()
                        {
                            Id = int.Parse(iddoc),
                            Version = int.Parse((int.Parse(ultimaversion) + 1).ToString()).ToString()
                        };
                        n.documentosasae.Modificar_Version(items3);


                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json("-1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json("error" + e.Message);
                }
            }

            return Json("Nada");
        }

        //*************** Procesos Json manejo de tipo de archivo
        public JsonResult SeleccionarSiPermitido(string extension)
        {
            int resultado;
            if (n.documentosasaetipoarchivo.ValidarSiPermitido(extension))
                resultado = 1; //Permitido
            else if (n.documentosasaetipoarchivo.ValidarSiPermitido(extension))
                resultado = 2; //Checar con el administrador
            else
                resultado = 3; //Error desconocido
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        //Obtener las subclasificaciones
        public JsonResult SeleccionarSubclasificacionesPorClasificacion(string idclasificacion)
        {
            List<Subclasificacion> sc = new List<Subclasificacion>();
            if (int.Parse(idclasificacion) > 0)
                sc = n.documentosasaesubclasificaciones.Seleccionar_SubClasificacionesPorClasificacion(idclasificacion);
            else
                sc = null;
            return Json(sc, JsonRequestBehavior.AllowGet);
        }

        //Validar si el usuario puede leer/bajar archivos

        public JsonResult ValidaSiUsuarioPuedeDescargarDocumento(string iddocumento)
        {
            if (n.documentosasaeusuarios.ValidarSiUsuarioEstaAsignadoADocumento(iddocumento, Session["IdUsuario"].ToString()) || Session["IdUsuario"].ToString() == "2")
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        //Validar si usuario puede agregar clasificacion/subclasificacion
        public JsonResult ValidaSiUsuarioPuedeAgregarClasificacionSubClasificacion(string idusuario)
        {
            if (n.usuarios.Validar_SiPuedeAgregarClasSubClasDocumentos(idusuario))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        //Buscar documentos
        public JsonResult BuscarDocumentos(string nombre)
        {
            if (!string.IsNullOrEmpty(nombre))
                return Json(n.documentosasae.Seleccionar_BusquedaPorNombre(nombre), JsonRequestBehavior.AllowGet);
            else
                return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}
