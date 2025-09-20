using BibliotecaApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca
{
    internal class Biblioteca
    {
        private List<Libro> libros;
        private List<Lector> lectores;

        public Biblioteca()
        {
            this.libros = new List<Libro>();
            this.lectores = new List<Lector>();
        }

        // Búsqueda por título, devuelve la referencia al Libro o null
        private Libro buscarLibro(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                return null;

            Libro libroBuscado = null;
            int i = 0;
            // Recorremos usando Count y acceso por índice
            while (i < libros.Count &&
                   !string.Equals(libros[i].getTitulo(), titulo, StringComparison.OrdinalIgnoreCase))
            {
                i++;
            }

            if (i != libros.Count)
                libroBuscado = libros[i];

            return libroBuscado;
        }
        private Lector buscarLector(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni)) return null;

            Lector lectorBuscado = null;
            int i = 0;
            while (i < lectores.Count && !string.Equals(lectores[i].getDni(), dni, StringComparison.OrdinalIgnoreCase))
            {
                i++;
            }

            if (i != lectores.Count) lectorBuscado = lectores[i];
            return lectorBuscado;
        }

        // Si no existe por título, lo crea y agrega, devuelve true si lo agregó
        public bool agregarLibro(string titulo, string autor, string editorial)
        {
            if (string.IsNullOrWhiteSpace(titulo)) return false;
            if (buscarLibro(titulo) != null) return false;

            libros.Add(new Libro(titulo, autor, editorial));
            return true;
        }

        // Eliminar por título devuelve true si lo encontró y lo eliminó
        public bool eliminarLibro(string titulo)
        {
            Libro libro = buscarLibro(titulo);
            if (libro == null) return false;
            return libros.Remove(libro);
        }

        // Listar todos los libros por consola
        public void listarLibros()
        {
            Console.WriteLine("-Lista de libros en la biblioteca-");
            if (libros.Count == 0)
            {
                Console.WriteLine("(La biblioteca está vacía)");
            }
            else
            {
                foreach (var libro in libros)
                {
                    Console.WriteLine(libro);
                }
            }
            Console.WriteLine("\n"); // Salto de línea
        }

        public bool altaLector(string nombre, string dni)
        {
            if (string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(nombre)) return false;
            if (buscarLector(dni) != null) return false;

            lectores.Add(new Lector(nombre, dni));
            return true;
        }

        public ResultadoPrestamo prestarLibro(string titulo, string dni)
        {
            Libro libro = buscarLibro(titulo);
            if (libro == null)
            {
                return ResultadoPrestamo.LIBRO_INEXISTENTE;
            }

            Lector lector = buscarLector(dni);
            if (lector == null)
            {
                return ResultadoPrestamo.LECTOR_INEXISTENTE;
            }

            if (!lector.agregarPrestamo(libro))
            {
                return ResultadoPrestamo.TOPE_PRESTAMO_ALCANZADO;
            }

            // préstamo exitoso = remover libro de la biblioteca
            libros.Remove(libro);
            return ResultadoPrestamo.PRESTAMO_EXITOSO;
        }
    }
}
