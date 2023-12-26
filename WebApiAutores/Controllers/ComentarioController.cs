
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    /*Cada Comentario depende enteramente de un libro 
     * asi se expresa */
    [Route("\"api/libros/{libroId:int}/comentarios\"")]
    public class ComentarioController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public ComentarioController(ApplicationDbContext context, 
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        /*Listado de Comentarios de un libro*/
        [HttpGet("obtenerComentariosLibro")]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int libroId)
        {
            var existeLibro = await context.Libros.AnyAsync(x => x.Id == libroId);
            if (!existeLibro)
            {
                return NotFound();
            }
            /*Cargar el listado de comentarios que tenga su libroId igual al libroId que tenemos aqui*/
            var comentarios = await context.Comentarios.
                Where(x => x.LibroId == libroId).ToListAsync();
            return mapper.Map<List<ComentarioDTO>>(comentarios);

        }

        [HttpGet("{id:int}", Name = "obtenerComentario")]
        public async Task<ActionResult<ComentarioDTO>> GetPorId(int id, int libroId)
        {
            var comentario = await context.Comentarios.Where(x => x.LibroId == libroId).FirstOrDefaultAsync(x => x.Id == id);


            if (comentario == null)
            {
                return NotFound();
            }

            return mapper.Map<ComentarioDTO>(comentario);

        }


        [HttpPost(Name = "crearComentario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDTO comentarioCreacionDTO)
        {
            /*Obtiene los datos del usuario emailClaim*/
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;
            /*primero tengo que verificar la existencia del libro al cual le van a corresponder el comentario */
            var existeLibro = await context.Libros.AnyAsync(x => x.Id == libroId);
            if (!existeLibro) 
            {
                return NotFound();
            }
            /*Map<Comentario>: Tipo de dato destino*/
            var comentario = mapper.Map<Comentario>(comentarioCreacionDTO);
            /*Debemos asignarle el libro*/
            comentario.LibroId = libroId;
            comentario.UsuarioId = usuarioId;
            context.Add(comentario);
            await context.SaveChangesAsync();

            var comentarioDTO = mapper.Map<ComentarioDTO>(comentario);

            return CreatedAtRoute("obtenerComentario", new {id = comentario.Id, libroId = libroId}, comentarioDTO);


        }

        [HttpPut("{id:int}", Name = "ActualizarComentario")]
        public async Task<ActionResult> Put(int libroId, int id, ComentarioCreacionDTO comentarioCreacionDTO)
        {
            var existeLibro = await context.Libros.AnyAsync(x => x.Id == libroId);
            if (!existeLibro)
            {
                return NotFound();
            }

            var existeComentario = await context.Comentarios.AnyAsync(comentarioDB => comentarioDB.Id == id);
            if (!existeComentario)
            {
                return NotFound();
            }

            var comentario = mapper.Map<Comentario>(comentarioCreacionDTO);
            comentario.Id = id;
            comentario.LibroId = libroId;

            context.Update(comentario);
            await context.SaveChangesAsync();
            return NoContent();


        }
    }
}
