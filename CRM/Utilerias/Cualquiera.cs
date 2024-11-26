using CRM.Models;
using CRM.Models.Business;
using CRM.Models.Business.Inyeccion;
using Microsoft.Ajax.Utilities;
using System.Web.Mvc;

namespace CRM.Utilerias
{
    /// <summary>
    /// Pruebas de accesibilidad
    /// </summary>
    public class Cualquiera : Business
    {
        //Esta clase es para probar como se conecta con la clase business para obtener el acceso a todas las instancias.
        public Cualquiera()
        {
            //Acceso directo a las clases por heredar de business
            usuarios.Seleccionar_Todos();
            estadisticas.Seleccionar_AsuntosPorEmpresaGeneral("2");
            oportunidades.Seleccionar_OportunidadesPorIdResponsable("2");
            configuracion.Seleccionar_Todo();

            //Llevar a cabo todas las tareas para las que se ha seleccionado todo el relajo


            //Accesar todos los con ascii 60<>62
            //Todo esto se manifiesta de alguna u otra forma, aunque es ideal para lo que se esta haciendo.
            //Es facil de llevar comodamente, aunque habra que ver la forma de encontrar algun otro teclado mas pequeño
            // y fácil de llevar a todos lados, no termina de convencerme.

            //Solicitar agregar un registro a un catalogo al administrador

            Models.Models.EmpresasMabe m = new Models.Models.EmpresasMabe();
            m.Observaciones = "Al parecer todo bien.";
            m.Activo = true;


        }

        public void Usuario2()
        {
            Comun comun = new Comun();

            Models.Models.ManejadorModelos mm = new Models.Models.ManejadorModelos();
            mm.Inicializar();
            mm.Usuarios.Nombre = "Juan";
            mm.Usuarios.ApellidoPaterno = "Perez";
            mm.Usuarios.ApellidoMaterno = "Perez";
            //comun.n.usuarios.Agregar_Registro();
        }

        public void Usuario3()
        {
            actividades.MetodoPublico(); //Accesible desde la clase negocio heredada
            //Instancia de la clase negocio
            Models.Business.Actividades ac = new Models.Business.Actividades();
            //ac.Agregar_Registro(ac);
            //ac.Agregar_Seleccionar(null);
            ac.MetodoPublico();
            ac.Seleccionar_Registros();            
            //Instancia de la clase repositorio
            Models.Repository.ActividadesRepositorio ar = new Models.Repository.ActividadesRepositorio();
            ar.MetodoPublico(); //Unico método accesible por instancia porque es público, los demás no son accesibles
        }

    }

    public class Cualquiera2 : Models.Business.Usuarios
    {
        public void Calculacion()
        {
            Calcular(); //Accesible porque se  hereda la clase donde se creó aunque el modificador de acceso sea protected
        }

        
    }

    public class Cualquiera3 : Utilerias.Comun
    {
        private void SinFuncionalidad()
        {
            CRM.Utilerias.CustomHelpers.SelectResponsables("2", "1", "2", "2");
            ViewBag.quitado = "que tal";
            var x = PruebaCualquiera3(ViewBag.quitado);
        }

        public ControllerBase PruebaCualquiera3(ControllerBase viewbag)
        {
            viewbag.ViewBag.Datos = "Hola";
            return viewbag;
        }
    }

    //*************** Procedimientos para inyección de dependencias

    public interface IValidacion
    {
        bool PropietarioPermiso(string id, string idusuario);        
    }

    public class ValidacionOportunidad : Comun, IValidacion
    {
        public bool PropietarioPermiso(string idoportunidad, string idusuario)
        {
            return n.compartiroportunidades.Validar_OportunidadUsuarioPermiso(idoportunidad, idusuario);
        }
    }

    public class ValidacionEmpresa : Comun, IValidacion
    {
        public bool PropietarioPermiso(string idempresa, string idusuario)
        {
            return n.compartirempresa.Validar_EmpresaUsuarioPermiso(idempresa, idusuario);
        }
    }

    public class ValidacionContacto : Comun, IValidacion
    {
        public bool PropietarioPermiso(string idcontacto, string idusuario)
        {
            var idcreador = n.contactos.Seleccionar_CreadorContacto(idcontacto);
            var modificar = n.compartircontactos.Validar_SiUsuarioPuedeModificar(idcontacto, idusuario);
            if (idcreador == idusuario || modificar || idusuario == "2")
                return true;
            else
                return false;
        }
    }

    public class ValidacionActividades : Comun, IValidacion
    {
        public bool PropietarioPermiso(string idactividad, string idusuario)
        {
            var idcreador = n.oportunidadesactividades.Seleccionar_SiUsuarioEsCreadorActividad(idactividad, idusuario);
            if (idcreador == int.Parse(idusuario) || idusuario == "2")
                return true;
            else
                return false;
        }
    }

    public class ValidacionDocumento : Comun, IValidacion
    {
        public bool PropietarioPermiso(string  iddocumento, string idusuario)
        {
            var idcreador = n.documentosasae.Seleccionar_SiUsuarioEsCreadorDocumento(iddocumento, idusuario);
            if (idcreador == int.Parse(idusuario) || idusuario == "2")
                return true;
            else
                return false;
        }
    }

    public class ConcentradorValidacion
    {
        protected IValidacion validacion;

        public ConcentradorValidacion(IValidacion _ivalidacion)
        {
            validacion = _ivalidacion;
        }

        public bool PropietarioPermiso(string id, string idusuario)
        {
            return validacion.PropietarioPermiso(id, idusuario);
        }
    }

    /// <summary>
    /// Clase contenedora de la validación para cada uno de los procedimientos necesarios a validar
    /// </summary>
    /// <example>
    /// Cómo se aplica:
    /// var validacion = new ValidacionPropietarioPermiso().ContactoValidarPropietarioPermiso(idcontacto, idusuario);
    /// Validación directa en un if
    /// if (new ValidacionPropietarioPermiso().ContactoValidarPropietarioPermiso(idcontacto, idusuario))
    /// </example>
    public class ValidacionPropietarioPermiso
    {
        /// <summary>
        /// Valida si el usuario es el creador de la oportunidad o si tiene permisos para modificarla
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idusuario"></param>
        /// <returns></returns>
        public bool OportunidadValidarPropietarioPermiso(string id, string idusuario)
        {
            var ejecutor = new ConcentradorValidacion(new ValidacionOportunidad());
            return ejecutor.PropietarioPermiso(id, idusuario);
        }

        /// <summary>
        /// Valida si el usuario es el creador de la empresa o si tiene permisos para modificarla
        /// </summary>
        /// <param name="idempresa"></param>
        /// <param name="idusuario"></param>
        /// <returns></returns>
        public bool EmpresaValidarPropietarioPermiso(string idempresa, string idusuario)
        {
            var ejecutor = new ConcentradorValidacion(new ValidacionEmpresa());
            return ejecutor.PropietarioPermiso(idempresa, idusuario);                
        }

        /// <summary>
        /// Valida si el usuario es el creador del contacto o si tiene permisos para modificarlo
        /// </summary>
        /// <param name="idcontacto"></param>
        /// <param name="idusuario"></param>
        /// <returns></returns>
        public bool ContactoValidarPropietarioPermiso(string idcontacto, string idusuario)
        {
            var ejecutor = new ConcentradorValidacion(new ValidacionContacto());
            return ejecutor.PropietarioPermiso(idcontacto, idusuario);
        }

        /// <summary>
        /// Valida si el usuario es el creador de la actividad o si tiene permisos para modificarla o eliminarla
        /// </summary>
        /// <param name="idactividad"></param>
        /// <param name="idusuario"></param>
        /// <returns></returns>
        public bool ActividadValidarPropiedad(string idactividad, string idusuario)
        {
            var ejecutor = new ConcentradorValidacion(new ValidacionActividades());
            return ejecutor.PropietarioPermiso(idactividad, idusuario);
        }

        /// <summary>
        /// Valida si el usuario subió el documento, por lo tanto, sería el propietario
        /// </summary>
        /// <param name="iddocumento"></param>
        /// <param name="idusuario"></param>
        /// <returns></returns>
        public bool DocumentoValidarPropiedad(string iddocumento, string idusuario)
        {
            var ejecutor = new ConcentradorValidacion(new ValidacionDocumento());
            return ejecutor.PropietarioPermiso(iddocumento, idusuario);
        }


        public void Prueba3()
        {
            //No se puede accesar ningun método con esta instanciación, todo debe pasar por herencia
            Models.Repository.ActividadesContactoDetalleRepositorio acdr = new Models.Repository.ActividadesContactoDetalleRepositorio();
        }
    }
}                                                                           