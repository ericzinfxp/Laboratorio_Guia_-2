using System;

namespace SoporteAcademicoUPN
{
    class Program
    {
        const int LONGITUD_MINIMA_CODIGO = 6;
        static readonly string[] TIPOS_PERMITIDOS = { "matrícula", "matricula", "pagos", "constancia", "plataforma", "otro" };

        static void Main(string[] args)
        {
            int opcion;
            bool solicitudRegistrada = false;

            string codigoEstudiante = "";
            string nombreEstudiante = "";
            string tipoConsulta = "";
            string descripcionBreve = "";
            string prioridadAtencion = "";

            do
            {
                MostrarMenuPrincipal();

                Console.Write("Seleccione una opción: ");
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = -1;
                }

                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        RegistrarDatosBasicos(out codigoEstudiante, out nombreEstudiante, out tipoConsulta, out descripcionBreve);

                        prioridadAtencion = CalcularPrioridadAtencion(tipoConsulta);

                        solicitudRegistrada = true;
                        Console.WriteLine($"\n[ÉXITO] Solicitud registrada correctamente. Prioridad asignada: {prioridadAtencion}");
                        break;

                    case 2:
                        if (solicitudRegistrada)
                        {
                            MostrarResumenSolicitud(codigoEstudiante, nombreEstudiante, tipoConsulta, prioridadAtencion, descripcionBreve);
                        }
                        else
                        {
                            Console.WriteLine("[AVISO] No hay ninguna solicitud registrada aún. Registre una con la opción 1.");
                        }
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

        static void MostrarMenuPrincipal()
        {
            Console.WriteLine("      SISTEMA DE SOPORTE ACADÉMICO - UPN          ");
            Console.WriteLine("  1. Registrar nueva solicitud de atención");
            Console.WriteLine("  2. Consultar resumen de la solicitud activa");
            Console.WriteLine("  0. Salir del programa");
        }

        static void MostrarResumenSolicitud(string codigo, string nombre, string tipo, string prioridad, string descripcion)
        {
            Console.WriteLine("          RESUMEN DE SOLICITUD DE ATENCIÓN        ");
            Console.WriteLine($"  Código Estudiante : {codigo}");
            Console.WriteLine($"  Estudiante        : {nombre}");
            Console.WriteLine($"  Tipo de Consulta  : {tipo}");
            Console.WriteLine($"  Prioridad Asignada: {prioridad}");
            Console.WriteLine($"  Descripción       : {descripcion}");
        }

        static void RegistrarDatosBasicos(out string codigo, out string nombre, out string tipo, out string descripcion)
        {
            Console.WriteLine("REGISTRO DE NUEVA SOLICITUD ");

            codigo = LeerCodigoEstudianteValidado(LONGITUD_MINIMA_CODIGO);

            nombre = LeerTextoObligatorioValidado("Ingrese nombre completo del estudiante: ", "Nombre del estudiante");

            tipo = LeerTipoConsultaValidado();

            descripcion = LeerTextoObligatorioValidado("Ingrese una descripción breve del caso: ", "Descripción breve");
        }

        static bool EsTextoValido(string texto)
        {
            return !string.IsNullOrWhiteSpace(texto);
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

        static string LeerTipoConsultaValidado()
        {
            string entrada;
            bool esValido;

            do
            {
                Console.Write("Ingrese tipo de consulta (Matrícula, Pagos, Constancia, Plataforma, Otro): ");
                entrada = (Console.ReadLine() ?? "").Trim();

                esValido = EsTipoConsultaValido(entrada);

                if (!esValido)
                {
                    Console.WriteLine("[ERROR] Categoría no permitida. Elija: Matrícula, Pagos, Constancia, Plataforma u Otro.\n");
                }

            } while (!esValido);

            return entrada;
        }

        static bool EsTipoConsultaValido(string tipoIngresado)
        {
            if (string.IsNullOrWhiteSpace(tipoIngresado))
            {
                return false;
            }

            string tipoNormalizado = tipoIngresado.ToLower();

            foreach (string tipoPermitido in TIPOS_PERMITIDOS)
            {
                if (tipoNormalizado == tipoPermitido)
                {
                    return true;
                }
            }

            return false;
        }
    }
}