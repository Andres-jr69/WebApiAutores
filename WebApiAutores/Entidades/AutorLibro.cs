﻿namespace WebApiAutores.Entidades
{
    public class AutorLibro
    {
        public int LibroId { get; set; }
        public int AutorId { get; set; }
        public int Orden { get; set; }
        public LIbro Libro { get; set; }
        public Autor Autor { get; set; }

    }
}