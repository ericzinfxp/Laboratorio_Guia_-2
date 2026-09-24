using System;
namespace SoporteAcademicoUPN
{
    class Program
    {
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
            Console.Write("Ingrese código de estudiante: ");
            codigo = Console.ReadLine() ?? "";
            Console.Write("Ingrese nombre completo del estudiante: ");
            nombre = Console.ReadLine() ?? "";
            Console.Write("Ingrese tipo de consulta (Matrícula, Pagos, Constancia, Plataforma, Otro): ");
            tipo = Console.ReadLine() ?? "";
            Console.Write("Ingrese una descripción breve del caso: ");
            descripcion = Console.ReadLine() ?? "";
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