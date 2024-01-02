using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.DTOs
{
    public class LibroCreacionDTO
    {
        [StringLength(maximumLength: 250)]
        [Required]
        [PrimeraLetraMayuscula]
        public string Titulo { get; set; }
        public DateTime FechaDePublicacion { get; set; }
        public List<int> AutoresIds { get; set; }
    }
}
