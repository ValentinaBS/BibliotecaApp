using BibliotecaApp;
using System;

namespace Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca();

            Console.WriteLine("Bienvenido/a a BibliotecaApp");

            // 1) Cargar libros
            Console.WriteLine("\nCargando libros...");
            for (int i = 1; i <= 5; i++)
            {
                string titulo = $"Libro{i}";
                biblioteca.agregarLibro(titulo, $"Autor{i}", $"Editorial{i}");
                Console.WriteLine($"Agregado: {titulo}");
            }

            // 2) Dar de alta lectores
            Console.WriteLine("\nDando de alta lectores...");
            biblioteca.altaLector("Marcos", "12345678");
            biblioteca.altaLector("Lara", "87654321");
            Console.WriteLine("Lectores dados de alta: Marcos(12345678), Lara(87654321)");

            // Mostrar estado inicial
            Console.WriteLine("\nEstado inicial (libros):");
            biblioteca.listarLibros();

            // 3) Caso: préstamo exitoso
            Console.WriteLine("\nPrueba 1: prestar 'Libro2' a DNI 12345678");
            MostrarResultado(biblioteca.prestarLibro("Libro2", "12345678"));

            // 4) Caso: libro inexistente
            Console.WriteLine("\nPrueba 2: prestar 'Libro100' a DNI 12345678");
            MostrarResultado(biblioteca.prestarLibro("Libro100", "12345678"));

            // 5) Caso: lector inexistente
            Console.WriteLine("\nPrueba 3: prestar 'Libro3' a DNI 00000000");
            MostrarResultado(biblioteca.prestarLibro("Libro3", "00000000"));

            // 6) Caso: tope de préstamos
            Console.WriteLine("\nPrueba 4: Dar 3 préstamos a Lara y luego intentar un cuarto");
            MostrarResultado(biblioteca.prestarLibro("Libro1", "87654321")); // 1
            MostrarResultado(biblioteca.prestarLibro("Libro3", "87654321")); // 2
            MostrarResultado(biblioteca.prestarLibro("Libro4", "87654321")); // 3
            MostrarResultado(biblioteca.prestarLibro("Libro5", "87654321")); // 4to (tiene que fallar por el límite de 3 préstamos)

            // 7) Mostrar estado final
            biblioteca.listarLibros();

            Console.WriteLine("\nPresione ENTER para cerrar.");
            Console.ReadLine();

            Console.WriteLine("Bienvenido/a a BibliotecaApp");
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine();
                Console.WriteLine("Elija una opción:");
                Console.WriteLine("1) Agregar libro");
                Console.WriteLine("2) Dar de alta lector");
                Console.WriteLine("3) Listar libros");
                Console.WriteLine("4) Prestar libro");
                Console.WriteLine("5) Salir");
                Console.Write("Opción: ");

                string opcion = Console.ReadLine()?.Trim();

                switch (opcion)
                {
                    case "1":
                        AgregarLibroInteractivo(biblioteca);
                        break;

                    case "2":
                        AltaLectorInteractivo(biblioteca);
                        break;

                    case "3":
                        biblioteca.listarLibros();
                        break;

                    case "4":
                        PrestarLibroInteractivo(biblioteca);
                        break;

                    case "5":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        break;
                }
            }

            Console.WriteLine("Programa finalizado. Presione ENTER para cerrar.");
            Console.ReadLine();
        }

        static void MostrarResultado(ResultadoPrestamo resultado)
        {
            switch (resultado)
            {
                case ResultadoPrestamo.PRESTAMO_EXITOSO:
                    Console.WriteLine("-> PRESTAMO EXITOSO");
                    break;
                case ResultadoPrestamo.LIBRO_INEXISTENTE:
                    Console.WriteLine("-> LIBRO INEXISTENTE");
                    break;
                case ResultadoPrestamo.TOPE_PRESTAMO_ALCANZADO:
                    Console.WriteLine("-> TOPE DE PRESTAMO ALCANZADO");
                    break;
                case ResultadoPrestamo.LECTOR_INEXISTENTE:
                    Console.WriteLine("-> LECTOR INEXISTENTE");
                    break;
            }
        }

        static void AgregarLibroInteractivo(Biblioteca biblioteca)
        {
            const int MAX_INTENTOS = 3;
            int intentos = 0;
            bool agregado = false;

            while (intentos < MAX_INTENTOS && !agregado)
            {
                Console.Write("Título: ");
                string titulo = Console.ReadLine()?.Trim() ?? "";
                Console.Write("Autor: ");
                string autor = Console.ReadLine()?.Trim() ?? "";
                Console.Write("Editorial: ");
                string editorial = Console.ReadLine()?.Trim() ?? "";

                try
                {

                    bool ok = biblioteca.agregarLibro(titulo, autor, editorial);
                    if (ok)
                    {
                        Console.WriteLine($"Libro '{titulo}' agregado correctamente.");
                        agregado = true;
                    }
                    else
                    {
                        Console.WriteLine($"No se pudo agregar. El libro '{titulo}' ya existe.");
                        agregado = true;
                    }
                }
                catch (ArgumentException ex)
                {
                    intentos++;
                    Console.WriteLine($"Error: {ex.Message}");
                    if (intentos < MAX_INTENTOS)
                    {
                        Console.WriteLine($"Intento {intentos} de {MAX_INTENTOS}. Vuelva a intentar \n");
                    }
                    else
                    {
                        Console.WriteLine("Se alcanzó el máximo de intentos. Regresando al menú principal.\n");
                    }
                }
            }
        }

        static void AltaLectorInteractivo(Biblioteca biblioteca)
        {
            const int MAX_INTENTOS = 3;
            int intentos = 0;
            bool registrado = false;

            while (intentos < MAX_INTENTOS && !registrado)
            {
                Console.Write("Nombre del lector: ");
                string nombre = Console.ReadLine()?.Trim() ?? "";
                Console.Write("DNI del lector: ");
                string dni = Console.ReadLine()?.Trim() ?? "";

                try
                {
                    bool ok = biblioteca.altaLector(nombre, dni);
                    if (ok)
                    {
                        Console.WriteLine($"Lector '{nombre}' (DNI {dni}) dado de alta.");
                        registrado = true;
                    }
                    else
                    {
                        Console.WriteLine($"El lector con DNI {dni} ya está registrado.");
                        registrado = true;
                    }
                }
                catch (ArgumentException ex)
                {
                    intentos++;
                    Console.WriteLine($"Error: {ex.Message}");

                    if (intentos < MAX_INTENTOS)
                    {
                        Console.WriteLine($"intento {intentos} de {MAX_INTENTOS}. Vuelva a intentarlo.\n");
                    }
                    else
                    {
                        Console.WriteLine("Se alcanzó el máximo de intentos. Regresando al menú principal.\n");
                    }
                }
            }
        }

        static void PrestarLibroInteractivo(Biblioteca biblioteca)
        {
            Console.Write("Título del libro a prestar: ");
            string titulo = Console.ReadLine()?.Trim() ?? "";
            Console.Write("DNI del lector: ");
            string dni = Console.ReadLine()?.Trim() ?? "";

            ResultadoPrestamo resultado = biblioteca.prestarLibro(titulo, dni);

            switch (resultado)
            {
                case ResultadoPrestamo.PRESTAMO_EXITOSO:
                    Console.WriteLine("-> PRESTAMO EXITOSO");
                    break;
                case ResultadoPrestamo.LIBRO_INEXISTENTE:
                    Console.WriteLine("-> LIBRO INEXISTENTE");
                    break;
                case ResultadoPrestamo.TOPE_PRESTAMO_ALCANZADO:
                    Console.WriteLine("-> TOPE DE PRESTAMO ALCANZADO");
                    break;
                case ResultadoPrestamo.LECTOR_INEXISTENTE:
                    Console.WriteLine("-> LECTOR INEXISTENTE");
                    break;
            }
        }
    }
}
