using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;


namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        /*Yo voy a obtener un http Get, por que con este get va a ser de un libro en especifico*/
        [HttpGet("{id:int}", Name = "obtenerLibro")]
        public async Task<ActionResult<LibrosDTOConAutores >> Get(int id)
        {
            /*Puede acceder a Libros por que lo agrego en ApplicationDbContext*/
            /*Incluide significa que incluira Autores de los libros */
            /*Incluide significa que incluira el listado de comentarios del libro*/
            var libro = await context.Libros.
                Include(libroDb => libroDb.AutoresLibros)
                .ThenInclude(autorLibroDB => autorLibroDB.Autor).FirstOrDefaultAsync(x => x.Id == id);

            if (libro is null)
            {
                return NotFound();
            }

            libro.AutoresLibros = libro.AutoresLibros.OrderBy(x => x.Orden).ToList();

            return mapper.Map<LibrosDTOConAutores>(libro);
        }
        //Metodo para crear un libro
        [HttpPost(Name = "crearLibro")]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            if (libroCreacionDTO.AutoresIds == null)
            {
                return BadRequest("No se puede crear un libro sin autores");
            }

            /*Primero se verifica que Exista el autor*/
            /*AnyAsync: Significa si existe alguno*/
            /*obtengo la intersección entre ids recibidos e ids de la base de datos*/
            var autoresIds = await context.Autores
                .Where(autorBD => libroCreacionDTO.AutoresIds.Contains(autorBD.Id)).Select(x => x.Id).ToListAsync();

            if (libroCreacionDTO.AutoresIds.Count != autoresIds.Count)
            {
                return BadRequest("No existe uno de los autores enviados");
            }

            var libro = mapper.Map<LIbro>(libroCreacionDTO);
            AsignarOrdenAutores(libro);


            context.Add(libro);
            await context.SaveChangesAsync();

            var libroDTO = mapper.Map<LibroDTO>(libro);

            return CreatedAtRoute("obtenerLibro", new {id = libro.Id}, libroDTO);


        }

        [HttpPut("{id:int}", Name = "actualizarLibros")]
        public async Task<ActionResult> Put(int id, LibroCreacionDTO libroCreacionDTO)
        {
            var libroDB = await context.Libros
                .Include(libroDB => libroDB.AutoresLibros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (libroDB is null)
            {
                return NotFound();
            }

            libroDB = mapper.Map(libroCreacionDTO, libroDB);
            AsignarOrdenAutores(libroDB);
            await context.SaveChangesAsync();
            return NoContent();
        }


        private void AsignarOrdenAutores(LIbro libro)
        {
            

            if (libro.AutoresLibros != null)
            {
                for (int i = 0; i < libro.AutoresLibros.Count; i++)
                {
                    libro.AutoresLibros[i].Orden = i;
                }
            }
        }

        [HttpPatch("{id:int}", Name = "patchLibro")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<LibroPatchDTO> patchDocument)
        {
            if (patchDocument is null)
            {
                return BadRequest();
            }

            var libroDB = await context.Libros.FirstOrDefaultAsync(x => x.Id == id);

            if (libroDB is null)
            {
                return NotFound();
            }

            var libroTDO = mapper.Map<LibroPatchDTO>(libroDB);

            patchDocument.ApplyTo(libroTDO, ModelState);

            var esvalido = TryValidateModel(libroDB);

            if (!esvalido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(libroTDO, libroDB);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "borrarLibro")] //api/autores/1
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Libros.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new LIbro() { Id = id });
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}
