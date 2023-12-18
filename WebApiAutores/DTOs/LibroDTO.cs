﻿using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        
        public string Titulo { get; set; }
        public DateTime FechaDePublicacion { get; set; }
        //public List<ComentarioDTO> Comentario { get; set; }
    }
}
