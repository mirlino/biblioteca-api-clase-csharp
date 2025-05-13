using AutoMapper;
using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LibroDTO>> Get()
        {
            var libros = await context.Libros.ToListAsync();
            var librosDTO = mapper.Map<IEnumerable<LibroDTO>>(libros);
            return librosDTO;

        }

        [HttpGet("{id:int}", Name = "ObtenerLibro")] // api/libros/id
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var libro = await context.Libros
                .Include(x => x.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(libro is null)
            {
                return NotFound();
            }
            var libroDTO = mapper.Map<LibroDTO>(libro);

            return libroDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            var libro = mapper.Map<Libro>(libroCreacionDTO);

            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if(!existeAutor)
            {
                ModelState.AddModelError(nameof(libro.AutorId), $"No existe un autor con el id {libro.AutorId}");
                return ValidationProblem();
            }

            context.Add(libro);
            await context.SaveChangesAsync();

            var libroDTO = mapper.Map<LibroDTO>(libro);

            return CreatedAtRoute("ObtenerLibro", new { id = libro.Id }, libroDTO);
        }

        [HttpPut("{id:int}")] // api/libros/id
        public async Task<ActionResult> Put(int id, LibroCreacionDTO libroCreacionDTO)
        {
            var libro = mapper.Map<Libro>(libroCreacionDTO);
            
            libro.Id = id;

            //if (id != libro.Id)
            //{
            //    return BadRequest("El id del libro no coincide");
            //}

            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            
            if (!existeAutor)
            {
                return BadRequest($"No existe un autor con el id {libro.AutorId}");
            }

            context.Update(libro);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")] // api/libros/id
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Libros
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
