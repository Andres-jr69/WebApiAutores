using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class LIbro
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 250)]
        public string Titulo { get; set; }
       
        

    }
}
