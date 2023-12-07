using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;


namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }
        //Yo voy a obtener un http Get, por que con este get va a ser de un libro en especifico
        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<LIbro>> Get(int id)
        //{
        //    /*Puede acceder a Libros por que lo agrego en ApplicationDbContext*/
        //    /*Incluide significa que incluira Uutores de los libros */
        //    return await context.Lbros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);

        //}
        ////Metodo para crear un libro
        //[HttpPost]
        //public async Task<ActionResult> Post(LIbro libro)
        //{
        //    /*Primero se verifica que Exista el autor*/
        //    /*AnyAsync: Significa si existe alguno*/
        //    var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
        //    if (!existeAutor)
        //    {
        //        return BadRequest($"No existe el autor de Id: {libro.AutorId}");
        //    }
        //    context.Add(libro);
        //    await context.SaveChangesAsync();
        //    return Ok();


        //}

    }
}
