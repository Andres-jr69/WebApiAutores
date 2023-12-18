using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class LIbro
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 250)]
        public string Titulo { get; set; }
        public DateTime? FechaDePublicacion { get; set; }
        public List<Comentario> Comentario { get; set; }
        public List<AutorLibro> AutoresLibros { get; set; }


    }
}
