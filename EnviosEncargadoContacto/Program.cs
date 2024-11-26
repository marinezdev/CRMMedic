using System;

namespace EnviosEncargadoContacto
{
    class Program
    {
        static void Main(string[] args)
        {
            AccesoADatos.Avisos avisos = new AccesoADatos.Avisos();

            avisos.EnvioAvisoEncargadoContactosExternos();
            Environment.Exit(0);
        }
    }
}
