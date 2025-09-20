using System;

namespace Biblioteca
{
    internal class Libro
    {

        private string titulo;
        private string autor;
        private string editorial;

        public Libro(string titulo, string autor, string editorial)
        {
            // Validaciones básicas para saber si viene null o vacio
            if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("El título no puede estar vacío.", nameof(titulo));
            if (string.IsNullOrWhiteSpace(autor)) throw new ArgumentException("El autor no puede estar vacío.", nameof(autor));
            if (string.IsNullOrWhiteSpace(editorial)) throw new ArgumentException("La editorial no puede estar vacía.", nameof(editorial));

            this.titulo = titulo;
            this.autor = autor;
            this.editorial = editorial;
        }
        public string getTitulo() { return this.titulo; }

        // ToString para mostrar el libro desde listarLibros()
        public override string ToString() => $"Título: {titulo} | Autor: {autor} | Editorial: {editorial}";

    }
}
