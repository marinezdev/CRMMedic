using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace CRM.Utilerias
{
    public static class Funciones_
    {
        /// <summary>
        /// Convierte una fecha a formato de búsqueda
        /// </summary>
        /// <param name="inicia"></param>
        /// <param name="termina"></param>
        /// <param name="inicial"></param>
        /// <param name="final"></param>
        public static void FormatoFechaParaBuscar(string inicia, string termina, ref string inicial, ref string final)
        {
            string año = inicia.Substring(6, 4);
            string mes = inicia.Substring(3, 2);
            string dia = inicia.Substring(0, 2);
            inicial = año + "-" + mes + "-" + dia + " 00:00:00";
            string añof = termina.Substring(6, 4);
            string mesf = termina.Substring(3, 2);
            string diaf = termina.Substring(0, 2);
            final = añof + "-" + mesf + "-" + diaf + " 23:59:59";
        }

        public static string PrepararFechaParaAgregar(DateTime fecha)
        {
            return string.Format("{0:yyyy/MM/dd}", DateTime.Parse(fecha.ToShortDateString()));
        }

        public static string PrepararHoraParaAgregar(string hora)
        {
            return string.Format("{0:HH:mm}", TimeSpan.ParseExact(hora, "HH:mm", CultureInfo.CurrentCulture));
        }
    }
}