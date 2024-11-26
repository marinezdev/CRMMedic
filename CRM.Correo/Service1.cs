using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Configuration;


namespace CRM.Correo
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {            
            EscribirAArchivo("Servicio iniciado {0}");
            ScheduleService();
            Avisos avisos = new Avisos();
            avisos.EnvioAvisos();
            avisos.EnvioEscalaciones();
            avisos.ActualizaProyectosImportes();
            avisos.EnvioAvisosActividadesOportunidades();
            avisos.EnvioMarketing();
            avisos.EnvioRecordatorios();
            EscribirAArchivo("Los procesos de actualización y envío de correo se ejecutaron satisfactoriamente. " + DateTime.Now);
        }

        protected override void OnStop()
        {
            EscribirAArchivo("Servicio detenido {0}");
            Schedular.Dispose();
        }

        private Timer Schedular;

        public void ScheduleService()
        {
            try
            {
                Schedular = new Timer(new TimerCallback(SchedularCallback));
                string mode = ConfigurationManager.AppSettings["Mode"].ToUpper();
                EscribirAArchivo("Modo del servicio: " + mode + " {0}");

                //Establecer el tiempo por default.
                DateTime scheduledTime = DateTime.MinValue;

                if (mode == "DAILY")
                {
                    //Obtener el tiempo agendado desde AppSettings.
                    scheduledTime = DateTime.Parse(ConfigurationManager.AppSettings["ScheduledTime"]);
                    if (DateTime.Now > scheduledTime)
                    {
                        //Si el tiempo agendado ya pasó establecer Schedule para el siguiente día.
                        scheduledTime = scheduledTime.AddDays(1);
                    }
                }

                if (mode.ToUpper() == "INTERVAL")
                {
                    //Obtener el intervalo en minutos desde AppSettings.
                    int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"]);

                    //Establecer el tiempo agendado agregando el intervalo al tiemp actual.
                    scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
                    if (DateTime.Now > scheduledTime)
                    {
                        //Sie el tiempo agendado ya ha pasado establecer Schedule para el siguiente intervalo.
                        scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
                    }
                }

                TimeSpan timeSpan = scheduledTime.Subtract(DateTime.Now);
                string schedule = string.Format("{0} dia(s) {1} hora(s) {2} minuto(s) {3} segundo(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);


                //Obtener la diferencia en minutos entre lo agendado y el tiempo actual.
                int dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

                //Cambiar el tiempo debido al Timer.
                Schedular.Change(dueTime, Timeout.Infinite);


            }
            catch (Exception ex)
            {
                EscribirAArchivo("Error en: {0} " + ex.Message + ex.StackTrace);

                //Stop the Windows Service.
                using (ServiceController serviceController = new ServiceController("CRM Servicio Correo"))
                {
                    serviceController.Stop();
                }
            }
        }

        private void SchedularCallback(object e)
        {
            EscribirAArchivo("Log del servicio: {0}");
            ScheduleService();
        }

        private void EscribirAArchivo(string text)
        {
            string path = ConfigurationManager.AppSettings["Ruta"];
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }
    }
}

