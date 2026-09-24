using System;

namespace SoporteAcademicoUPN
{
    class Program
    {
        const int LONGITUD_MINIMA_CODIGO = 6;

        static readonly string[] TIPOS_PERMITIDOS = { "matrícula", "matricula", "pagos", "constancia", "plataforma", "otro" };

        static void Main(string[] args)
        {
            Console.WriteLine("  SISTEMA DE SOPORTE ACADÉMICO - UPN  ");

            string codigoEstudiante;
            string nombreEstudiante;
            string tipoConsulta;
            string descripcionBreve;

            RegistrarDatosBasicos(out codigoEstudiante, out nombreEstudiante, out tipoConsulta, out descripcionBreve);

            MostrarResumenSolicitud(codigoEstudiante, nombreEstudiante, tipoConsulta, descripcionBreve);
        }

        static void RegistrarDatosBasicos(out string codigo, out string nombre, out string tipo, out string descripcion)
        {
            Console.WriteLine(" REGISTRO DE NUEVA SOLICITUD ");

            // Req. 2: Validación de código de estudiante
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
                    Console.WriteLine($"[ERROR] El código no puede estar vacío ni tener menos de {minLongitud} caracteres. Intente nuevamente.\n");
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
                    Console.WriteLine("[ERROR] Opción no permitida. Ingrese una categoría válida (Matrícula, Pagos, Constancia, Plataforma u Otro).\n");
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