using System;

namespace SoporteAcademicoUPN
{
    class Program
    {
        static void Main(string[] args)
        {
            const int LONGITUD_MINIMA_CODIGO = 6;
            const int CAPACIDAD_MAXIMA = 3; 
            string[] tiposPermitidos = { "matrícula", "matricula", "pagos", "constancia", "plataforma", "otro" };

            string[] codigosEstudiantes = new string[CAPACIDAD_MAXIMA];
            string[] nombresEstudiantes = new string[CAPACIDAD_MAXIMA];
            string[] tiposConsultas = new string[CAPACIDAD_MAXIMA];
            string[] descripcionesBreves = new string[CAPACIDAD_MAXIMA];
            string[] prioridadesAtencion = new string[CAPACIDAD_MAXIMA];

            int contadorSolicitudes = 0; 
            int opcion;

            do
            {
                MostrarMenuPrincipal(contadorSolicitudes, CAPACIDAD_MAXIMA);

                Console.Write("Seleccione una opción: ");
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = -1;
                }

                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        if (contadorSolicitudes < CAPACIDAD_MAXIMA)
                        {
                            string codigoTemp, nombreTemp, tipoTemp, descripcionTemp;

                            RegistrarDatosBasicos(
                                out codigoTemp, 
                                out nombreTemp, 
                                out tipoTemp, 
                                out descripcionTemp, 
                                LONGITUD_MINIMA_CODIGO, 
                                tiposPermitidos
                            );

                            string prioridadTemp = CalcularPrioridadAtencion(tipoTemp);

                            codigosEstudiantes[contadorSolicitudes] = codigoTemp;
                            nombresEstudiantes[contadorSolicitudes] = nombreTemp;
                            tiposConsultas[contadorSolicitudes] = tipoTemp;
                            descripcionesBreves[contadorSolicitudes] = descripcionTemp;
                            prioridadesAtencion[contadorSolicitudes] = prioridadTemp;

                            contadorSolicitudes++; 

                            Console.WriteLine($"\n[ÉXITO] Solicitud N° {contadorSolicitudes} registrada correctamente. Prioridad: {prioridadTemp}");
                        }
                        else
                        {
                            Console.WriteLine($"[ALERTA] Capacidad máxima alcanzada ({CAPACIDAD_MAXIMA} solicitudes). No se pueden registrar más casos.");
                        }
                        break;

                    case 2:
                        MostrarTodasLasSolicitudes(
                            codigosEstudiantes, 
                            nombresEstudiantes, 
                            tiposConsultas, 
                            prioridadesAtencion, 
                            descripcionesBreves, 
                            contadorSolicitudes
                        );
                        break;

                    case 0:
                        Console.WriteLine("Saliendo del Sistema de Soporte Académico UPN... ¡Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("[ERROR] Opción no válida. Ingrese un número perteneciente al menú.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (opcion != 0);
        }

        static void MostrarMenuPrincipal(int registrados, int capacidad)
        {
            Console.WriteLine("      SISTEMA DE SOPORTE ACADÉMICO - UPN          ");
            Console.WriteLine($"  Solicitudes registradas: [{registrados}/{capacidad}]");
            Console.WriteLine("  1. Registrar nueva solicitud de atención");
            Console.WriteLine("  2. Consultar catálogo de solicitudes registradas");
            Console.WriteLine("  0. Salir del programa");
        }

        static void MostrarTodasLasSolicitudes(
            string[] codigos, 
            string[] nombres, 
            string[] tipos, 
            string[] prioridades, 
            string[] descripciones, 
            int totalRegistrados)
        {
            if (totalRegistrados == 0)
            {
                Console.WriteLine("[AVISO] No hay ninguna solicitud registrada aún. Por favor seleccione la opción 1.");
                return;
            }

            Console.WriteLine($"      LISTADO DE SOLICITUDES REGISTRADAS ({totalRegistrados})");

            for (int i = 0; i < totalRegistrados; i++)
            {
                Console.WriteLine($"\n- SOLICITUD N° {i + 1} ");
                Console.WriteLine($"  Código Estudiante : {codigos[i]}");
                Console.WriteLine($"  Estudiante        : {nombres[i]}");
                Console.WriteLine($"  Tipo de Consulta  : {tipos[i]}");
                Console.WriteLine($"  Prioridad Asignada: {prioridades[i]}");
                Console.WriteLine($"  Descripción       : {descripciones[i]}");
            }
        }

        static void RegistrarDatosBasicos(
            out string codigo, 
            out string nombre, 
            out string tipo, 
            out string descripcion, 
            int minLongitudCodigo, 
            string[] tiposPermitidos)
        {
            Console.WriteLine(" REGISTRO DE NUEVA SOLICITUD ");

            codigo = LeerCodigoEstudianteValidado(minLongitudCodigo);
            nombre = LeerTextoObligatorioValidado("Ingrese nombre completo del estudiante: ", "Nombre del estudiante");
            tipo = LeerTipoConsultaValidado(tiposPermitidos);
            descripcion = LeerTextoObligatorioValidado("Ingrese una descripción breve del caso: ", "Descripción breve");
        }

        static string LeerCodigoEstudianteValidado(int minLongitud)
        {
            string entrada;
            bool esValido;

            do
            {
                Console.Write($"Ingrese código de estudiante (mínimo {minLongitud} caracteres): ");
                entrada = (Console.ReadLine() ?? "").Trim();

                esValido = EsCodigoValido(entrada, minLongitud);

                if (!esValido)
                {
                    Console.WriteLine($"[ERROR] El código no puede estar vacío ni tener menos de {minLongitud} caracteres.\n");
                }

            } while (!esValido);

            return entrada;
        }

        static bool EsCodigoValido(string codigo, int minLongitud)
        {
            return !string.IsNullOrWhiteSpace(codigo) && codigo.Length >= minLongitud;
        }

        static string LeerTipoConsultaValidado(string[] listaPermitida)
        {
            string entrada;
            bool esValido;

            do
            {
                Console.Write("Ingrese tipo de consulta (Matrícula, Pagos, Constancia, Plataforma, Otro): ");
                entrada = (Console.ReadLine() ?? "").Trim();

                esValido = EsTipoConsultaValido(entrada, listaPermitida);

                if (!esValido)
                {
                    Console.WriteLine("[ERROR] Categoría no permitida. Elija: Matrícula, Pagos, Constancia, Plataforma u Otro.\n");
                }

            } while (!esValido);

            return entrada;
        }

        static bool EsTipoConsultaValido(string tipoIngresado, string[] listaPermitida)
        {
            if (string.IsNullOrWhiteSpace(tipoIngresado) || listaPermitida == null)
            {
                return false;
            }

            string tipoNormalizado = tipoIngresado.ToLower();

            foreach (string tipoPermitido in listaPermitida)
            {
                if (tipoNormalizado == tipoPermitido)
                {
                    return true;
                }
            }

            return false;
        }

        static string LeerTextoObligatorioValidado(string mensajePrompt, string nombreCampo)
        {
            string entrada;
            bool esValido;

            do
            {
                Console.Write(mensajePrompt);
                entrada = (Console.ReadLine() ?? "").Trim();

                esValido = EsTextoValido(entrada);

                if (!esValido)
                {
                    Console.WriteLine($"[ERROR] El campo '{nombreCampo}' es obligatorio. No puede quedar vacío.\n");
                }

            } while (!esValido);

            return entrada;
        }

        static bool EsTextoValido(string texto)
        {
            return !string.IsNullOrWhiteSpace(texto);
        }

        static string CalcularPrioridadAtencion(string tipoConsulta)
        {
            if (string.IsNullOrWhiteSpace(tipoConsulta))
            {
                return "Baja";
            }

            string tipoNormalizado = tipoConsulta.ToLower().Trim();

            switch (tipoNormalizado)
            {
                case "matrícula":
                case "matricula":
                case "pagos":
                    return "Alta";

                case "plataforma":
                case "constancia":
                    return "Media";

                case "otro":
                default:
                    return "Baja";
            }
        }
    }
}