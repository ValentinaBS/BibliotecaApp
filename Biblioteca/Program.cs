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

            //Biblioteca biblioteca = new Biblioteca();

            //Console.WriteLine("Bienvenido/a a BibliotecaApp");
            //bool salir = false;
            //while (!salir)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("Elija una opción:");
            //    Console.WriteLine("1) Agregar libro");
            //    Console.WriteLine("2) Dar de alta lector");
            //    Console.WriteLine("3) Listar libros");
            //    Console.WriteLine("4) Prestar libro");
            //    Console.WriteLine("5) Salir");
            //    Console.Write("Opción: ");

            //    string opcion = Console.ReadLine()?.Trim();

            //    if (opcion == "1")
            //    {
            //        AgregarLibroInteractivo(biblioteca);
            //    }
            //    else if (opcion == "2")
            //    {
            //        AltaLectorInteractivo(biblioteca);
            //    }
            //    else if (opcion == "3")
            //    {
            //        biblioteca.listarLibros();
            //    }
            //    else if (opcion == "4")
            //    {
            //        PrestarLibroInteractivo(biblioteca);
            //    }
            //    else if (opcion == "5")
            //    {
            //        salir = true;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Opción inválida. Intente de nuevo.");
            //    }
            //}

            //Console.WriteLine("Programa finalizado. Presione ENTER para cerrar.");
            //Console.ReadLine();
        }

        static void MostrarResultado(ResultadoPrestamo resultado)
        {
            if (resultado == ResultadoPrestamo.PRESTAMO_EXITOSO)
            {
                Console.WriteLine("-> PRESTAMO EXITOSO");
            }
            else if (resultado == ResultadoPrestamo.LIBRO_INEXISTENTE)
            {
                Console.WriteLine("-> LIBRO INEXISTENTE");
            }
            else if (resultado == ResultadoPrestamo.TOPE_PRESTAMO_ALCANZADO)
            {
                Console.WriteLine("-> TOPE DE PRESTAMO ALCANZADO");
            }
            else if (resultado == ResultadoPrestamo.LECTOR_INEXISTENTE)
            {
                Console.WriteLine("-> LECTOR INEXISTENTE");
            }
        }

        //static void AgregarLibroInteractivo(Biblioteca biblioteca)
        //{
        //    Console.Write("Título: ");
        //    string titulo = Console.ReadLine()?.Trim() ?? "";
        //    Console.Write("Autor: ");
        //    string autor = Console.ReadLine()?.Trim() ?? "";
        //    Console.Write("Editorial: ");
        //    string editorial = Console.ReadLine()?.Trim() ?? "";

        //    bool ok = biblioteca.agregarLibro(titulo, autor, editorial);
        //    if (ok)
        //    {
        //        Console.WriteLine($"Libro '{titulo}' agregado correctamente.");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"No se pudo agregar. El libro '{titulo}' ya existe.");
        //    }
        //}

        //static void AltaLectorInteractivo(Biblioteca biblioteca)
        //{
        //    Console.Write("Nombre del lector: ");
        //    string nombre = Console.ReadLine()?.Trim() ?? "";
        //    Console.Write("DNI del lector: ");
        //    string dni = Console.ReadLine()?.Trim() ?? "";

        //    bool ok = biblioteca.altaLector(nombre, dni);
        //    if (ok)
        //    {
        //        Console.WriteLine($"Lector '{nombre}' (DNI {dni}) dado de alta.");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"El lector con DNI {dni} ya está registrado.");
        //    }
        //}

        //static void PrestarLibroInteractivo(Biblioteca biblioteca)
        //{
        //    Console.Write("Título del libro a prestar: ");
        //    string titulo = Console.ReadLine()?.Trim() ?? "";
        //    Console.Write("DNI del lector: ");
        //    string dni = Console.ReadLine()?.Trim() ?? "";

        //    ResultadoPrestamo resultado = biblioteca.prestarLibro(titulo, dni);

        //    if (resultado == ResultadoPrestamo.PRESTAMO_EXITOSO)
        //    {
        //        Console.WriteLine("PRESTAMO EXITOSO");
        //    }
        //    else if (resultado == ResultadoPrestamo.LIBRO_INEXISTENTE)
        //    {
        //        Console.WriteLine("LIBRO INEXISTENTE");
        //    }
        //    else if (resultado == ResultadoPrestamo.TOPE_PRESTAMO_ALCANZADO)
        //    {
        //        Console.WriteLine("TOPE DE PRESTAMO ALCANZADO");
        //    }
        //    else if (resultado == ResultadoPrestamo.LECTOR_INEXISTENTE)
        //    {
        //        Console.WriteLine("LECTOR INEXISTENTE");
        //    }
        //}
    }
}
