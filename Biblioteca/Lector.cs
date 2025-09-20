using Biblioteca;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaApp
{
    internal class Lector
    {
        private string nombre;
        private string dni;
        private List<Libro> prestamos;

        private const int MAX_PRESTAMOS = 3;

        public Lector(string nombre, string dni)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("nombre vacío", nameof(nombre));
            if (string.IsNullOrWhiteSpace(dni)) throw new ArgumentException("dni vacío", nameof(dni));

            this.nombre = nombre;
            this.dni = dni;
            this.prestamos = new List<Libro>();
        }

        public string getDni()
        {
            return this.dni;
        }

        // devuelve true si se agregó, false si el tope fue alcanzado
        public bool agregarPrestamo(Libro libro)
        {
            if (libro == null) throw new ArgumentNullException(nameof(libro));
            if (prestamos.Count >= MAX_PRESTAMOS) return false;

            prestamos.Add(libro);
            return true;
        }

        public void listarPrestamos()
        {
            Console.WriteLine($"Prestamos de {nombre} (DNI: {dni})");
            if (prestamos.Count == 0)
            {
                Console.WriteLine("(Sin préstamos)");
            }
            else
            {
                for (int i = 0; i < prestamos.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {prestamos[i].getTitulo()}");
                }
            }
            Console.WriteLine("\n"); // Salto de linea
        }

        public override string ToString()
        {
            return $"Lector: {nombre} | DNI: {dni} | Prestamos: {prestamos.Count}";
        }
    }
}
