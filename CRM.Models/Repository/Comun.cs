using CRM.Models.Models;

namespace CRM.Models.Repository
{
    /// <summary>
    /// Instanciador de todos los accesos a la base de datos
    /// </summary>
    public class Comun
    {
        public ActividadesRepositorio actividades;
        public ArchivosBitacoraRepositorio archivosbitacora;
        public ArchivosRepositorio archivos;
        public AreaRepositorio area;
        public AvisoRepositorio avisos;
        public BitacoraRespositorio bitacora;
        public BitacoraUsuariosDetalleRepositorio bitacorausuariodetalle;
        public BitacoraUsuariosRepositorio bitacorausuario;
        public ClasificacionRepositorio clasificacion;
        public ClasificacionRolesContactosRespositorio clasificacionrolescontactos;
        public ClassSubClassEtiquetasRepositorio classsubclassetiquetas;
        public ClassSubClassRepositorio classsubclass;
        public CodigoPostalRespositorio codigopostal;
        public ConfiguracionRepositorio configuracion;
        public ContactosRepositorio contactos;
        public ContactoRolRepositorio contactorolrepositorio;
        public EmpresasRepositorio empresas;
        public EmpresasUsuariosRepositorio empresasusuarios;
        public EscalacionRepositorio escalacion;
        public EstadisticasRepositorio estadisticas;
        public EstadoOportunidadRepositorio estadooportunidad;
        public EtapasBitacoraRepositorio etapasbitacora;
        public EtapasOportunidadRepositorio etapasoportunidad;
        public Marketing marketing;
        public MonedaRepositorio moneda;
        public OportunidadesActividadesRepositorio oportunidadesactividades;
        public OportunidadesEmpresasContactosUsuariosRepositorio oportunidadesempresascontactosusuarios;
        public OportunidadesRepositorio oportunidades;
        public OportunidadesResponsablesRepositorio oportunidadesresponsables;
        public OportunidadesUsuariosRepositorio oportunidadesusuarios;
        public RolesRepositorio roles;
        public SubclasificacionRepositorio subclasificacion;
        public TareasRepositorio tareas;
        public TareasUsuariosContactosEmpresasRepositorio tareasusuarioscontactosempresas;
        public TipoPersonaRepositorio tipopersona;
        public UDNRepositorio udn;
        public UsuarioConfiguracionRepositorio usuarioconfiguracion;
        public UsuariosEmpresasOportunidadesRepositorio usuariosempresasoportunidades;
        public UsuariosRepositorio usuarios;
        public UsuariosRolesRepositorio usuariosroles;

        public Comun()
        {
            actividades                            = new ActividadesRepositorio();
            archivosbitacora                       = new ArchivosBitacoraRepositorio();
            archivos                               = new ArchivosRepositorio();
            area                                   = new AreaRepositorio();
            avisos                                 = new AvisoRepositorio();
            bitacora                               = new BitacoraRespositorio();
            bitacorausuariodetalle                 = new BitacoraUsuariosDetalleRepositorio();
            bitacorausuario                        = new BitacoraUsuariosRepositorio();
            clasificacion                          = new ClasificacionRepositorio();
            clasificacionrolescontactos            = new ClasificacionRolesContactosRespositorio();
            classsubclassetiquetas                 = new ClassSubClassEtiquetasRepositorio();
            classsubclass                          = new ClassSubClassRepositorio();
            codigopostal                           = new CodigoPostalRespositorio();
            configuracion                          = new ConfiguracionRepositorio();
            contactos                              = new ContactosRepositorio();
            contactorolrepositorio                 = new ContactoRolRepositorio();
            empresas                               = new EmpresasRepositorio();
            empresasusuarios                       = new EmpresasUsuariosRepositorio();
            escalacion                             = new EscalacionRepositorio();
            estadisticas                           = new EstadisticasRepositorio();
            estadooportunidad                      = new EstadoOportunidadRepositorio();
            etapasbitacora                         = new EtapasBitacoraRepositorio();
            etapasoportunidad                      = new EtapasOportunidadRepositorio();
            marketing                              = new Marketing();
            moneda                                 = new MonedaRepositorio();
            oportunidadesactividades               = new OportunidadesActividadesRepositorio();
            oportunidadesempresascontactosusuarios = new OportunidadesEmpresasContactosUsuariosRepositorio();
            oportunidades                          = new OportunidadesRepositorio();
            oportunidadesresponsables              = new OportunidadesResponsablesRepositorio();
            oportunidadesusuarios                  = new OportunidadesUsuariosRepositorio();
            roles                                  = new RolesRepositorio();
            subclasificacion                       = new SubclasificacionRepositorio();
            tareas                                 = new TareasRepositorio();
            tareasusuarioscontactosempresas        = new TareasUsuariosContactosEmpresasRepositorio();
            tipopersona                            = new TipoPersonaRepositorio();
            udn                                    = new UDNRepositorio();
            usuarioconfiguracion                   = new UsuarioConfiguracionRepositorio();
            usuariosempresasoportunidades          = new UsuariosEmpresasOportunidadesRepositorio();
            usuarios                               = new UsuariosRepositorio();
            usuariosroles                          = new UsuariosRolesRepositorio();
        }


    }
}
