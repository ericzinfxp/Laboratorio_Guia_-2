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
                        solicitudRegistrada = true;
                        Console.WriteLine("\n[ÉXITO] Solicitud registrada correctamente en el sistema.");
                        break;

                    case 2:
                        if (solicitudRegistrada)
                        {
                            MostrarResumenSolicitud(codigoEstudiante, nombreEstudiante, tipoConsulta, descripcionBreve);
                        }
                        else
                        {
                            Console.WriteLine("[AVISO] No hay ninguna solicitud registrada aún. Por favor seleccione la opción 1.");
                        }
                        break;

                    case 0:
                        Console.WriteLine("Saliendo del Sistema de Soporte Académico UPN... ¡Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("[ERROR] Opción no válida. Por favor, ingrese un número del menú.");
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

        static void RegistrarDatosBasicos(out string codigo, out string nombre, out string tipo, out string descripcion)
        {
            Console.WriteLine("--- REGISTRO DE NUEVA SOLICITUD ---");

            codigo = LeerCodigoEstudianteValidado(LONGITUD_MINIMA_CODIGO);

            Console.Write("Ingrese nombre completo del estudiante: ");
            nombre = Console.ReadLine() ?? "";

            tipo = LeerTipoConsultaValidado();

            Console.Write("Ingrese una descripción breve del caso: ");
            descripcion = Console.ReadLine() ?? "";
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
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return false;
            }

            return codigo.Length >= minLongitud;
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
                    Console.WriteLine("[ERROR] Categoria no permitida. Elija: Matrícula, Pagos, Constancia, Plataforma u Otro.\n");
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

        static void MostrarResumenSolicitud(string codigo, string nombre, string tipo, string descripcion)
        {
            Console.WriteLine("          RESUMEN DE LA SOLICITUD REGISTRADA      ");
            Console.WriteLine($"Código Estudiante : {codigo}");
            Console.WriteLine($"Estudiante        : {nombre}");
            Console.WriteLine($"Tipo de Consulta  : {tipo}");
            Console.WriteLine($"Descripción       : {descripcion}");
        }
    }
}