namespace WebApiAutores.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Comtenido { get; set; }
        public int LibroId { get; set; }
        public LIbro Libro { get; set; }
    }
}
