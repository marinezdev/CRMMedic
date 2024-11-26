using CRM.Models.Business.Inyeccion;

namespace CRM.Models.Business
{
    public class ValidacionPropietarioPermiso
    {
        public bool OportunidadValidarPropietarioPermiso(string id, string idusuario)
        {
            var ejecutor = new ConcentradorValidacion(new CompartirOportunidades());
            return ejecutor.PropietarioPermiso(id, idusuario);
        }

        public bool EmpresaValidarPropietarioPermiso(string id, string idusuario)
        {
            var ejecutor = new ConcentradorValidacion(new CompartirEmpresa());
            return ejecutor.PropietarioPermiso(id, idusuario);
        }

        public bool ContactoValidarPropietarioPermiso(string id, string idusuario)
        {
            var ejecutor = new ConcentradorValidacion(new CompartirContactos());
            return ejecutor.PropietarioPermiso(id, idusuario);
        }

        public bool ActividadValidarPropiedad(string idactividad, string idusuario)
        {
            var ejecutor = new ConcentradorValidacion(new OportunidadesActividades());
            return ejecutor.PropietarioPermiso(idactividad, idusuario);
        }

        public bool DocumentoValidarPropiedad(string iddocumento, string idusuario)
        {
            var ejecutor = new ConcentradorValidacion(new DocumentosASAE());
            return ejecutor.PropietarioPermiso(iddocumento, idusuario);
        }


    }
}
