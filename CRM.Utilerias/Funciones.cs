using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace CRM.Utilerias
{
    public static class Funciones
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

        public static string Etiqueta(string target, string text)
        {
            return string.Format("<label for='{0}'>{1}</label>", target, text);
        }

        /// <summary>
        /// Obtiene una lista de estados de la república
        /// </summary>
        /// <returns></returns>
        public static List<Generico> Estados()
        {
            List<Generico> estados = new List<Generico>()
            {
                new Generico() {Id = 1, Nombre="Aguascalientes" },
                new Generico() {Id = 2, Nombre="Baja California Norte" },
                new Generico() {Id = 3, Nombre="Baja California Sur" },
                new Generico() {Id = 4, Nombre="Campeche" },
                new Generico() {Id = 5, Nombre="Coahuila" },
                new Generico() {Id = 6, Nombre="Colima" },
                new Generico() {Id = 7, Nombre="Chiapas" },
                new Generico() {Id = 8, Nombre="Chihuahua" },
                new Generico() {Id = 9, Nombre="CDMX" },
                new Generico() {Id = 10, Nombre="Durango" },
                new Generico() {Id = 11, Nombre="Estado De Mexico" },
                new Generico() {Id = 12, Nombre="Guanajuato" },
                new Generico() {Id = 13, Nombre="Guerrero" },
                new Generico() {Id = 14, Nombre="Hidalgo" },
                new Generico() {Id = 15, Nombre="Jalisco" },
                new Generico() {Id = 16, Nombre="Michoacan" },
                new Generico() {Id = 17, Nombre="Morelos" },
                new Generico() {Id = 18, Nombre="Nayarit" },
                new Generico() {Id = 19, Nombre="Nuevo León" },
                new Generico() {Id = 20, Nombre="Oaxaca" },
                new Generico() {Id = 21, Nombre="Puebla" },
                new Generico() {Id = 22, Nombre="Querétaro" },
                new Generico() {Id = 23, Nombre="Quintana Roo" },
                new Generico() {Id = 24, Nombre="San Luis Potosí" },
                new Generico() {Id = 25, Nombre="Sinaloa" },
                new Generico() {Id = 26, Nombre="Sonora" },
                new Generico() {Id = 27, Nombre="Tabasco" },
                new Generico() {Id = 28, Nombre="Tamaulipas" },
                new Generico() {Id = 29, Nombre="Tlaxcala" },
                new Generico() {Id = 30, Nombre="Veracruz" },
                new Generico() {Id = 31, Nombre="Yucatán" },
                new Generico() {Id = 32, Nombre="Zacatecas" },
                new Generico() {Id = 33, Nombre="" }
            };

            return estados;
        }

        /// <summary>
        /// Devuelve el nombre del estado de acuerdo a su id
        /// </summary>
        /// <param name="id"></param>
        public static string EstadosNombre(string id)
        {
            string Nombre = "";
            switch (id)
            {
                case "1":
                    Nombre = "Aguascalientes";
                    break;
                case "2":
                    Nombre = "Baja California Norte";
                    break;
                case "3":
                    Nombre = "Baja California Sur";
                    break;
                case "4":
                    Nombre = "Campeche";
                    break;
                case "5":
                    Nombre = "Coahuila";
                    break;
                case "6":
                    Nombre = "Colima";
                    break;
                case "7":
                    Nombre = "Chiapas";
                    break;
                case "8":
                    Nombre = "Chihuahua";
                    break;
                case "9":
                    Nombre = "CDMX";
                    break;
                case "10":
                    Nombre = "Durango";
                    break;
                case "11":
                    Nombre = "Estado De Mexico";
                    break;
                case "12":
                    Nombre = "Guanajuato";
                    break;
                case "13":
                    Nombre = "Guerrero";
                    break;
                case "14":
                    Nombre = "Hidalgo";
                    break;
                case "15":
                    Nombre = "Jalisco";
                    break;
                case "16":
                    Nombre = "Michoacan";
                    break;
                case "17":
                    Nombre = "Morelos";
                    break;
                case "18":
                    Nombre = "Nayarit";
                    break;
                case "19":
                    Nombre = "Nuevo León";
                    break;
                case "20":
                    Nombre = "Oaxaca";
                    break;
                case "21":
                    Nombre = "Puebla";
                    break;
                case "22":
                    Nombre = "Querétaro";
                    break;
                case "23":
                    Nombre = "Quintana Roo";
                    break;
                case "24":
                    Nombre = "San Luis Potosí";
                    break;
                case "25":
                    Nombre = "Sinaloa";
                    break;
                case "26":
                    Nombre = "Sonora";
                    break;
                case "27":
                    Nombre = "Tabasco";
                    break;
                case "28":
                    Nombre = "Tamaulipas";
                    break;
                case "29":
                    Nombre = "Tlaxcala";
                    break;
                case "30":
                    Nombre = "Veracruz";
                    break;
                case "312":
                    Nombre = "Yucatán";
                    break;
                case "32":
                    Nombre = "Zacatecas";
                    break;
            }
            return Nombre;
        }

        public static List<Generico> Sectores()
        {
            List<Generico> sectores = new List<Generico>()
            {
                new Generico() {Id = 1, Nombre="Administración gubernamental" },
                new Generico() {Id = 2, Nombre="Aeronáutica / Aviación" },
                new Generico() {Id = 3, Nombre="Agricultura" },
                new Generico() {Id = 4, Nombre="Alimentación y bebidas" },
                new Generico() {Id = 5, Nombre="Almacenamiento" },
                new Generico() {Id = 7, Nombre="Animación" },
                new Generico() {Id = 8, Nombre="Apuestas y casinos" },
                new Generico() {Id = 9, Nombre="Arquitectura y planificación" },
                new Generico() {Id = 10, Nombre="Artesania" },
                new Generico() {Id = 11, Nombre="Artes escénicas" },
                new Generico() {Id = 12, Nombre="Artículos de consumo" },
                new Generico() {Id = 13, Nombre="Artículos de lujo y joyas" },
                new Generico() {Id = 14, Nombre="Artículos deportivos" },
                new Generico() {Id = 15, Nombre="Asuntos internacionales" },
                new Generico() {Id = 16, Nombre="Atención a la salud mental" },
                new Generico() {Id = 17, Nombre="Atención sanitaria y hospitalaria" },
                new Generico() {Id = 18, Nombre="Automación industrial" },
                new Generico() {Id = 19, Nombre="Banca" },
                new Generico() {Id = 20, Nombre="Banca de inversiones" },
                new Generico() {Id = 21, Nombre="Bellas artes" },
                new Generico() {Id = 22, Nombre="Bibliotecas" },
                new Generico() {Id = 23, Nombre="Bienes inmobiliarios" },
                new Generico() {Id = 24, Nombre="Bienes inmobiliarios Comerciales" },
                new Generico() {Id = 25, Nombre="Biotecnología" },
                new Generico() {Id = 26, Nombre="Capital de Riesgo y capital privado" },
                new Generico() {Id = 27, Nombre="Construcción" },
                new Generico() {Id = 28, Nombre="Construcción naval" },
                new Generico() {Id = 29, Nombre="Consultoría de estrategia y operaciones" },
                new Generico() {Id = 30, Nombre="Contabilidad" },
                new Generico() {Id = 31, Nombre="Cosmética" },
                new Generico() {Id = 32, Nombre="Cristal, cerámica y hormigón" },
                new Generico() {Id = 33, Nombre="Cumplimiento de la ley" },
                new Generico() {Id = 34, Nombre="Departamento de defensa y del espacio exterior" },
                new Generico() {Id = 35, Nombre="Deportes" },
                new Generico() {Id = 36, Nombre="Derecho" },
                new Generico() {Id = 37, Nombre="Desarrollo de programación" },
                new Generico() {Id = 38, Nombre="Desarrollo y comercio internacional" },
                new Generico() {Id = 39, Nombre="Diseño" },
                new Generico() {Id = 40, Nombre="Diseño gráfico" },
                new Generico() {Id = 41, Nombre="Dotación y selección de personal" },
                new Generico() {Id = 42, Nombre="Educació primaria / secundaria" },
                new Generico() {Id = 43, Nombre="Ejército" },
                new Generico() {Id = 44, Nombre="E-learning" },
                new Generico() {Id = 45, Nombre="Electónica de consumo" },
                new Generico() {Id = 46, Nombre="Embalaje y contenedores" },
                new Generico() {Id = 47, Nombre="Energía renovable y medio ambiente" },
                new Generico() {Id = 48, Nombre="Enseñanza superior" },
                new Generico() {Id = 49, Nombre="entretenimiento" },
                new Generico() {Id = 50, Nombre="Envío de paquetes y carga" },
                new Generico() {Id = 51, Nombre="Equipos informáticos" },
                new Generico() {Id = 52, Nombre="Escitura y edición" },
                new Generico() {Id = 53, Nombre="Filantropía" },
                new Generico() {Id = 54, Nombre="Fotografía" },
                new Generico() {Id = 55, Nombre="Gabinetes estratégicos" },
                new Generico() {Id = 56, Nombre="Ganadería" },
                new Generico() {Id = 57, Nombre="Gestión de inversiones" },
                new Generico() {Id = 58, Nombre="Gestión de organizaciones sin ánimo de lucro" },
                new Generico() {Id = 59, Nombre="Gestión educativa" },
                new Generico() {Id = 60, Nombre="Hostelería" },
                new Generico() {Id = 61, Nombre="Importación y exportación" },
                new Generico() {Id = 62, Nombre="Imprenta" },
                new Generico() {Id = 63, Nombre="Industria aereoespacial y aviación" },
                new Generico() {Id = 64, Nombre="Industria farmacéutica" },
                new Generico() {Id = 65, Nombre="Industria textil y moda" },
                new Generico() {Id = 66, Nombre="Ingeniería civil" },
                new Generico() {Id = 67, Nombre="ingeniería industrial o mecánica" },
                new Generico() {Id = 68, Nombre="Instalaciones y servicios recreativas" },
                new Generico() {Id = 69, Nombre="Instituciones religiosas" },
                new Generico() {Id = 70, Nombre="Interonexión en red" },
                new Generico() {Id = 71, Nombre="Internet" },
                new Generico() {Id = 72, Nombre="investigación" },
                new Generico() {Id = 73, Nombre="Investigación de mercado" },
                new Generico() {Id = 74, Nombre="Judicial" },
                new Generico() {Id = 75, Nombre="Lácteos" },
                new Generico() {Id = 76, Nombre="Logística y cadena de suministro" },
                new Generico() {Id = 77, Nombre="Manufactura eléctrica / electrónica" },
                new Generico() {Id = 78, Nombre="Manufactura ferroviaria" },
                new Generico() {Id = 79, Nombre="Maquinaria" },
                new Generico() {Id = 80, Nombre="Marketing y publicidad" },
                new Generico() {Id = 81, Nombre="Materiales de construcción" },
                new Generico() {Id = 82, Nombre="Material y equipo de negocios" },
                new Generico() {Id = 83, Nombre="Medicina alternativa" },
                new Generico() {Id = 84, Nombre="Medios de comunicación en línea" },
                new Generico() {Id = 85, Nombre="Medios de difusión" },
                new Generico() {Id = 86, Nombre="Mercaodos de capital" },
                new Generico() {Id = 87, Nombre="Minería y metalurgia" },
                new Generico() {Id = 88, Nombre="Mobiliario" },
                new Generico() {Id = 89, Nombre="Museos e instituciones" },
                new Generico() {Id = 90, Nombre="Música" },
                new Generico() {Id = 91, Nombre="Nanotecnología" },
                new Generico() {Id = 92, Nombre="Naval" },
                new Generico() {Id = 93, Nombre="Ocio, viajes y turismo" },
                new Generico() {Id = 94, Nombre="Oficina ejecutiva" },
                new Generico() {Id = 95, Nombre="Oficina legislativa" },
                new Generico() {Id = 96, Nombre="Organización cívica y social" },
                new Generico() {Id = 97, Nombre="Organización política" },
                new Generico() {Id = 98, Nombre="Películas y cine" },
                new Generico() {Id = 99, Nombre="Periódicos" },
                new Generico() {Id = 100, Nombre="Petróleo y energía" },
                new Generico() {Id = 101, Nombre="Piscicultura" },
                new Generico() {Id = 102, Nombre="Plásticos" },
                new Generico() {Id = 103, Nombre="Política pública" },
                new Generico() {Id = 104, Nombre="Producción alimentaria" },
                new Generico() {Id = 105, Nombre="Producción multimedia" },
                new Generico() {Id = 106, Nombre="Productos de papel y forestales" },
                new Generico() {Id = 107, Nombre="Productos químicos" },
                new Generico() {Id = 108, Nombre="Profesiones médicas" },
                new Generico() {Id = 109, Nombre="Protección civil" },
                new Generico() {Id = 110, Nombre="Publicaciones" },
                new Generico() {Id = 111, Nombre="Recaudación de fondos" },
                new Generico() {Id = 112, Nombre="Recursos Humanos" },
                new Generico() {Id = 113, Nombre="Relaciones gubernamentales" },
                new Generico() {Id = 114, Nombre="Relaciones públicas y comunicaciones" },
                new Generico() {Id = 115, Nombre="Resolución de conflictos por terceras partes" },
                new Generico() {Id = 116, Nombre="Restaurantes" },
                new Generico() {Id = 117, Nombre="Sanidad, bienestar y ejercicio" },
                new Generico() {Id = 118, Nombre="Sector automovilístico" },
                new Generico() {Id = 119, Nombre="Sector textil" },
                new Generico() {Id = 120, Nombre="Seguridad del ordenador y de las redes" },
                new Generico() {Id = 121, Nombre="Seguridad e investigaciones" },
                new Generico() {Id = 122, Nombre="Seguros" },
                new Generico() {Id = 123, Nombre="Semiconductores" },
                new Generico() {Id = 124, Nombre="Servicio al consumidor" },
                new Generico() {Id = 125, Nombre="Servicios de información" },
                new Generico() {Id = 126, Nombre="Servicios de eventos" },
                new Generico() {Id = 127, Nombre="Servicios financieros" },
                new Generico() {Id = 128, Nombre="Servicios infraestruturales" },
                new Generico() {Id = 129, Nombre="Servicios jurídicos" },
                new Generico() {Id = 130, Nombre="Servicios médicos" },
                new Generico() {Id = 131, Nombre="Servicios medioambientales" },
                new Generico() {Id = 132, Nombre="Servicios para el individuo y la familia" },
                new Generico() {Id = 133, Nombre="Servicios públicos" },
                new Generico() {Id = 134, Nombre="Servicios y tecnologías de la información" },
                new Generico() {Id = 135, Nombre="Software" },
                new Generico() {Id = 136, Nombre="Subcontrataciones / Offshoring" },
                new Generico() {Id = 137, Nombre="Supermercados" },
                new Generico() {Id = 138, Nombre="Tabaco" },
                new Generico() {Id = 139, Nombre="Tecnología inalámbrica" },
                new Generico() {Id = 140, Nombre="Telecomunicaciones" },
                new Generico() {Id = 141, Nombre="Traducción y localización" },
                new Generico() {Id = 142, Nombre="Transporte por carretera o ferrocarril" },
                new Generico() {Id = 143, Nombre="Venta al por mayor" },
                new Generico() {Id = 144, Nombre="Venta al por menor" },
                new Generico() {Id = 145, Nombre="Veterinaria" },
                new Generico() {Id = 146, Nombre="Videojuegos" },
                new Generico() {Id = 147, Nombre="Vinos y licores" },
            };

            return sectores;
        }

        public static string SectoresNombre(string id)
        {
            string Nombre = "";
            switch (id)
            {                
                case "1": Nombre = "Administración gubernamental"; 
                    break;
                    case "2": Nombre = "Aeronáutica / Aviación"; 
                    break;
                case "3": Nombre="Agricultura"; 
                    break;
                case "4": Nombre="Alimentación y bebidas"; 
                    break;
                case "5": Nombre="Almacenamiento"; 
                    break;
                case "7": Nombre="Animación"; 
                    break;
                case "8": Nombre="Apuestas y casinos"; 
                    break;
                case "9": Nombre="Arquitectura y planificación"; 
                    break;
                case "10": Nombre="Artesania"; 
                    break;
                case "11": Nombre="Artes escénicas"; 
                    break;
                case "12": Nombre="Artículos de consumo"; 
                    break;
                case "13": Nombre="Artículos de lujo y joyas"; 
                    break;
                case "14": Nombre="Artículos deportivos"; 
                    break;
                case "15": Nombre="Asuntos internacionales"; 
                    break;
                case "16": Nombre="Atención a la salud mental"; 
                    break;
                case "17": Nombre="Atención sanitaria y hospitalaria"; 
                    break;
                case "18": Nombre="Automación industrial"; 
                    break;
                case "19": Nombre="Banca"; 
                    break;
                case "20": Nombre="Banca de inversiones"; 
                    break;
                case "21": Nombre="Bellas artes"; 
                    break;
                case "22": Nombre="Bibliotecas"; 
                    break;
                case "23": Nombre="Bienes inmobiliarios"; 
                    break;
                case "24": Nombre="Bienes inmobiliarios Comerciales"; 
                    break;
                case "25": Nombre="Biotecnología"; 
                    break;
                case "26": Nombre="Capital de Riesgo y capital privado"; 
                    break;
                case "27": Nombre="Construcción"; 
                    break;
                case "28": Nombre="Construcción naval"; 
                    break;
                case "29": Nombre="Consultoría de estrategia y operaciones"; 
                    break;
                case "30": Nombre="Contabilidad"; 
                    break;
                case "31": Nombre="Cosmética"; 
                    break;
                case "32": Nombre="Cristal, cerámica y hormigón"; 
                    break;
                case "33": Nombre="Cumplimiento de la ley"; 
                    break;
                case "34": Nombre="Departamento de defensa y del espacio exterior"; 
                    break;
                case "35": Nombre="Deportes"; 
                    break;
                case "36": Nombre="Derecho"; 
                    break;
                case "37": Nombre="Desarrollo de programación"; 
                    break;
                case "38": Nombre="Desarrollo y comercio internacional"; 
                    break;
                case "39": Nombre="Diseño"; 
                    break;
                case "40": Nombre="Diseño gráfico"; 
                    break;
                case "41": Nombre="Dotación y selección de personal"; 
                    break;
                case "42": Nombre="Educació primaria / secundaria"; 
                    break;
                case "43": Nombre="Ejército"; 
                    break;
                case "44": Nombre="E-learning"; 
                    break;
                case "45": Nombre="Electónica de consumo"; 
                    break;
                case "46": Nombre="Embalaje y contenedores"; 
                    break;
                case "47": Nombre="Energía renovable y medio ambiente"; 
                    break;
                case "48": Nombre="Enseñanza superior"; 
                    break;
                case "49": Nombre="entretenimiento"; 
                    break;
                case "50": Nombre="Envío de paquetes y carga"; 
                    break;
                case "51": Nombre="Equipos informáticos"; 
                    break;
                case "52": Nombre="Escitura y edición"; 
                    break;
                case "53": Nombre="Filantropía"; 
                    break;
                case "54": Nombre="Fotografía"; 
                    break;
                case "55": Nombre="Gabinetes estratégicos"; 
                    break;
                case "56": Nombre="Ganadería"; 
                    break;
                case "57": Nombre="Gestión de inversiones"; 
                    break;
                case "58": Nombre="Gestión de organizaciones sin ánimo de lucro"; 
                    break;
                case "59": Nombre="Gestión educativa"; 
                    break;
                case "60": Nombre="Hostelería"; 
                    break;
                case "61": Nombre="Importación y exportación"; 
                    break;
                case "62": Nombre="Imprenta";
                    break;
                case "63": Nombre="Industria aereoespacial y aviación"; 
                    break;
                case "64": Nombre="Industria farmacéutica"; 
                    break;
                case "65": Nombre="Industria textil y moda";
                    break;
                case "66": Nombre="Ingeniería civil"; 
                    break;
                case "67": Nombre="ingeniería industrial o mecánica";
                    break;
                case "68": Nombre="Instalaciones y servicios recreativas"; 
                    break;
                case "69": Nombre="Instituciones religiosas"; 
                    break;
                case "70": Nombre="Interonexión en red"; 
                    break;
                case "71": Nombre="Internet";
                    break;
                case "72": Nombre="investigación"; 
                    break;
                case "73": Nombre="Investigación de mercado"; 
                    break;
                case "74": Nombre="Judicial"; 
                    break;
                case "75": Nombre="Lácteos";
                    break;
                case "76": Nombre="Logística y cadena de suministro"; 
                    break;
                case "77": Nombre="Manufactura eléctrica / electrónica"; 
                    break;
                case "78": Nombre="Manufactura ferroviaria";
                    break;
                case "79": Nombre="Maquinaria"; 
                    break;
                case "80": Nombre="Marketing y publicidad";
                    break;
                case "81": Nombre="Materiales de construcción"; 
                    break;
                case "82": Nombre="Material y equipo de negocios"; 
                    break;
                case "83": Nombre="Medicina alternativa"; 
                    break;
                case "84": Nombre="Medios de comunicación en línea"; 
                    break;
                case "85": Nombre="Medios de difusión";
                    break;
                case "86": Nombre="Mercaodos de capital"; 
                    break;
                case "87": Nombre="Minería y metalurgia";
                    break;
                case "88": Nombre="Mobiliario"; 
                    break;
                case "89": Nombre="Museos e instituciones"; 
                    break;
                case "90": Nombre="Música"; 
                    break;
                case "91": Nombre="Nanotecnología";
                    break;
                case "92": Nombre="Naval"; 
                    break;
                case "93": Nombre="Ocio, viajes y turismo"; 
                    break;
                case "94": Nombre="Oficina ejecutiva"; 
                    break;
                case "95": Nombre="Oficina legislativa"; 
                    break;
                case "96": Nombre="Organización cívica y social"; 
                    break;
                case "97": Nombre="Organización política";
                    break;
                case "98": Nombre="Películas y cine";
                    break;
                case "99": Nombre="Periódicos"; 
                    break;
                case "100": Nombre="Petróleo y energía"; 
                    break;
                case "101": Nombre="Piscicultura";
                    break;
                case "102": Nombre="Plásticos";
                    break;
                case "103": Nombre="Política pública";
                    break;
                case "104": Nombre="Producción alimentaria"; 
                    break;
                case "105": Nombre="Producción multimedia"; 
                    break;
                case "106": Nombre="Productos de papel y forestales"; 
                    break;
                case "107": Nombre="Productos químicos"; 
                    break;
                case "108": Nombre="Profesiones médicas";
                    break;
                case "109": Nombre="Protección civil"; 
                    break;
                case "110": Nombre="Publicaciones"; 
                    break;
                case "111": Nombre="Recaudación de fondos"; 
                    break;
                case "112": Nombre="Recursos Humanos"; 
                    break;
                case "113": Nombre="Relaciones gubernamentales"; 
                    break;
                case "114": Nombre="Relaciones públicas y comunicaciones"; 
                    break;                
                case "115": Nombre="Resolución de conflictos por terceras partes"; 
                    break;
                case "116": Nombre="Restaurantes"; 
                    break;
                case "117": Nombre="Sanidad, bienestar y ejercicio"; 
                    break;
                case "118": Nombre="Sector automovilístico"; 
                    break;
                case "119": Nombre="Sector textil";
                    break;
                case "120": Nombre="Seguridad del ordenador y de las redes"; 
                    break;
                case "121": Nombre="Seguridad e investigaciones"; 
                    break;
                case "122": Nombre="Seguros"; 
                    break;
                case "123": Nombre="Semiconductores"; 
                    break;
                case "124": Nombre="Servicio al consumidor";
                    break;
                case "125": Nombre="Servicios de información"; 
                    break;
                case "126": Nombre="Servicios de eventos";
                    break;
                case "127": Nombre="Servicios financieros"; 
                    break;
                case "128": Nombre="Servicios infraestruturales"; 
                    break;
                case "129": Nombre="Servicios jurídicos"; 
                    break;
                case "130": Nombre="Servicios médicos"; 
                    break;
                case "131": Nombre="Servicios medioambientales"; 
                    break;
                case "132": Nombre="Servicios para el individuo y la familia"; 
                    break;
                case "133": Nombre="Servicios públicos"; 
                    break;
                case "134": Nombre="Servicios y tecnologías de la información"; 
                    break;
                case "135": Nombre="Software"; 
                    break;
                case "136": Nombre="Subcontrataciones / Offshoring"; 
                    break;
                case "137": Nombre="Supermercados"; 
                    break;
                case "138": Nombre="Tabaco"; 
                    break;
                case "139": Nombre="Tecnología inalámbrica"; 
                    break;
                case "140": Nombre="Telecomunicaciones"; 
                    break;
                    case "141": Nombre="Traducción y localización"; 
                    break;
                case "142": Nombre="Transporte por carretera o ferrocarril"; 
                    break;
                case "143": Nombre="Venta al por mayor"; 
                    break;
                case "144": Nombre="Venta al por menor"; 
                    break;
                case "145": Nombre="Veterinaria";
                    break;
                case "146": Nombre="Videojuegos"; 
                    break;
                case "147": Nombre="Vinos y licores"; 
                    break;
            }
            return Nombre;
        }

        public static List<Generico> TipoPersona()
        {
            List<Generico> tipopersona = new List<Generico>()
            {
                new Generico() {Id = 1, Nombre="Usuario" },
                new Generico() {Id = 2, Nombre="Responsable" },
                new Generico() {Id = 3, Nombre="Tomador de decisiones" },
                new Generico() {Id = 4, Nombre="Influencer" },
                new Generico() {Id = 100, Nombre="" }

            };

            return tipopersona;
        }

        public static string TipoPersonaNombre(string id)
        {
            string Nombre = "";
            switch (id)
            {
                case "1":
                    Nombre = "Usuario";
                    break;
                case "2":
                    Nombre = "Responsable";
                    break;
                case "3":
                    Nombre = "Tomador de decisiones";
                    break;
                case "4":
                    Nombre = "Influencer";
                    break;
                case "100":
                    Nombre = "";
                    break;
            }
            return Nombre;
        }

        public static List<Generico> PeriodoAtencion()
        {
            List<Generico> periodoatencion = new List<Generico>()
            {
                new Generico() {Id = 1, Nombre="Este mes" },
                new Generico() {Id = 2, Nombre="Este trimestre" },
                new Generico() {Id = 3, Nombre="Trimestre siguiente" },
                new Generico() {Id = 4, Nombre="Este año" },
                new Generico() {Id = 5, Nombre="Este semestre" },
                new Generico() {Id = 6, Nombre="Desconocido" },
                new Generico() {Id = 100, Nombre="" }

            };

            return periodoatencion;
        }

        public static string PeriodoAtencionNombre(string id)
        {
            string Nombre = "";
            switch (id)
            {
                case "1":
                    Nombre = "Este mes";
                    break;
                case "2":
                    Nombre = "Este trimestre";
                    break;
                case "3":
                    Nombre = "Trimestre siguiente";
                    break;
                case "4":
                    Nombre = "Este año";
                    break;
                case "5":
                    Nombre = "Este trimestre";
                    break;
                case "6":
                    Nombre = "Desconocido";
                    break;
                case "100":
                    Nombre = "";
                    break;
            }
            return Nombre;
        }

        public static List<Generico> Estado()
        {
            List<Generico> estado = new List<Generico>()
            {
                new Generico() {Id = 1, Nombre="En Proceso" },  //En proceso
                //new Generico() {Id = 2, Nombre="Cerrado" },     //Terminación por el usuario
                new Generico() {Id = 3, Nombre="Cerrado Perdido" }, //Cerrado peridida la oportunidad
                new Generico() {Id = 4, Nombre="Cerrado Ganado" },  //Cerrada ganada la oportunidad
                //new Generico() {Id = 5, Nombre="Terminado" },   //Flujo normal de terminación de un tema
                new Generico() {Id = 6, Nombre="Cancelado" },   //Cancelación por el usuario
                new Generico() {Id = 7, Nombre="Suspendido" },  //Suspendido por el usuario
                new Generico() {Id = 8, Nombre="Reasignar" }    //Reasignar 
            };

            return estado;
        }

        public static List<Generico> EstadosMabe()
        {
            List<Generico> estado = new List<Generico>()
            {
                new Generico() {Id = 1, Nombre="En Proceso" },  //En proceso
                new Generico() {Id = 2, Nombre="Cerrado" },     //Terminación por el usuario
                //new Generico() {Id = 3, Nombre="Cerrado Perdido" }, //Cerrado peridida la oportunidad
                //new Generico() {Id = 4, Nombre="Cerrado Ganado" },  //Cerrada ganada la oportunidad
                //new Generico() {Id = 5, Nombre="Terminado" },   //Flujo normal de terminación de un tema
                new Generico() {Id = 6, Nombre="Cancelado" },   //Cancelación por el usuario
                new Generico() {Id = 7, Nombre="Suspendido" },  //Suspendido por el usuario
                //new Generico() {Id = 8, Nombre="Reasignar" }    //Reasignar 
            };

            return estado;
        }

        public static string EstadoNombre(string id)
        {
            string Nombre = "";
            switch (id)
            {
                case "1":
                    Nombre = "En Proceso";
                    break;
                case "2":
                    Nombre = "Cerrado";
                    break;
                case "3":
                    Nombre = "Cerrado Perdido";
                    break;
                case "4":
                    Nombre = "Cerrado Ganado";
                    break;
                case "5":
                    Nombre = "Terminado";
                    break;
                case "6":
                    Nombre = "Cancelado";
                    break;
                case "7":
                    Nombre = "Suspendido";
                    break;
                case "8":
                    Nombre = "Reasignar";
                    break;
                case "100":
                    Nombre = "";
                    break;
            }
            return Nombre;
        }

        public static List<Generico> TipoActividad()
        {
            List<Generico> tipoactividad = new List<Generico>()
            {
                new Generico() {Id = 1, Nombre="Tarea" },
                new Generico() {Id = 2, Nombre="Llamada" },
                new Generico() {Id = 3, Nombre="Correo Electrónico" },
                new Generico() {Id = 4, Nombre="Carta" },
                new Generico() {Id = 5, Nombre="Cita" },
                new Generico() {Id = 6, Nombre="Videoconferencia" },
                new Generico() {Id = 7, Nombre="Comentario" },
                new Generico() {Id = 8, Nombre="Whatsapp" }
            };

            return tipoactividad;
        }

        public static string TipoActividadNombre(string id)
        {
            string Nombre = "";
            switch (id)
            {
                case "1":
                    Nombre = "Tarea";
                    break;
                case "2":
                    Nombre = "Llamada";
                    break;
                case "3":
                    Nombre = "Correo Electrónico";
                    break;
                case "4":
                    Nombre = "Carta";
                    break;
                case "5":
                    Nombre = "Cita";
                    break;
                case "6":
                    Nombre = "Videoconferencia";
                    break;
                case "7":
                    Nombre = "Comentario";
                    break;
                case "8":
                    Nombre = "Whatsapp";
                    break;
            }
            return Nombre;
        }

        public static List<Generico> TipoEmpresa()
        {
            List<Generico> tipoempresa = new List<Generico>()
            {
                new Generico() {Id = 1, Nombre="Interna" },
                new Generico() {Id = 2, Nombre="Externa" }
            };

            return tipoempresa;
        }

        public static string TipoEmpresaNombre(string id)
        {
            string Nombre = "";
            switch (id)
            {
                case "1":
                    Nombre = "Interna";
                    break;
                case "2":
                    Nombre = "Externa";
                    break;
            }
            return Nombre;
        }

        public static List<Generico> Meses()
        {
            List<Generico> mes = new List<Generico>()
            {
                new Generico() {Id = 1, Nombre="Enero" },
                new Generico() {Id = 2, Nombre="Febrero" },
                new Generico() {Id = 3, Nombre="Marzo" },
                new Generico() {Id = 4, Nombre="Abril" },
                new Generico() {Id = 5, Nombre="Mayo" },
                new Generico() {Id = 6, Nombre="Junio" },
                new Generico() {Id = 7, Nombre="Julio" },
                new Generico() {Id = 8, Nombre="Agosto" },
                new Generico() {Id = 9, Nombre="Septiembre" },
                new Generico() {Id = 10, Nombre="Octubre" },
                new Generico() {Id = 11, Nombre="Noviembre" },
                new Generico() {Id = 12, Nombre="Diciembre" }

            };

            return mes;
        }

        public static string ClaveSalida()
        {
            return "5q8T3w";
        }

        public static string MesNombre(int mes)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(mes);
        }

        /// <summary>
        /// Obtiene la extensión de un archivo sin importar su longitud
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public static string ObtenerExtensionArchivo(string archivo)
        {
            string extension = archivo.Substring(archivo.LastIndexOf("."));
            int extension_longitud = extension.Length;
            extension = archivo.Substring(archivo.LastIndexOf("."), extension_longitud);
            return extension;
        }

    }

    public class Generico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}