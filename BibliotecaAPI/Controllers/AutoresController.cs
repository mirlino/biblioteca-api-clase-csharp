﻿using AutoMapper;
using Azure;
using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<AutorDTO>> Get()
        {
            var autores = await context.Autores.ToListAsync();
            var autoresDTO = mapper.Map<IEnumerable<AutorDTO>>(autores);
            return autoresDTO;

        }

        [HttpGet("Primero")] // api/autores/Primero
        public async Task<ActionResult<Autor>> GetPrimeroAutor()
        {
            return await context.Autores.FirstAsync();
        }

        [HttpGet("{id:int}", Name = "ObtenerAutor")] // api/autores/id
        public async Task<ActionResult<AutorConLibrosDTO>> Get(int id)
        {
            var autor = await context.Autores
                .Include(x => x.Libros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (autor is null)
            {
                return NotFound();
            }

            var autorDTO = mapper.Map<AutorConLibrosDTO>(autor);

            return autorDTO;
        }

        [HttpGet("{Parameter1}/{parameter2?}")] // api/autores/{Parameter1}/{parameter2?}
        public ActionResult Get(string parameter1, string parameter2 = "Por defecto")
        {
            return Ok(new { parameter1,parameter2 });
        }

        [HttpPost]
        public async Task<ActionResult> Post(AutorCreacionDTO autorCreacionDTO)
        {
            var autor = mapper.Map<Autor>(autorCreacionDTO);

            context.Add(autor);
            await context.SaveChangesAsync();

            var autorDTO = mapper.Map<AutorDTO>(autor);

            return CreatedAtRoute("ObtenerAutor", new { id = autor.Id }, autorDTO);
        }

        [HttpPut("{id:int}")] // api/autores/id
        public async Task<ActionResult> Put(int id, AutorCreacionDTO autorCreacionDTO)
        {
            var autor = mapper.Map<Autor>(autorCreacionDTO);

            //if (id != autor.Id)
            //{
            //    return BadRequest("El id del autor no coincide");
            //}

            autor.Id = id;
            context.Update(autor);
            await context.SaveChangesAsync();
            return NoContent();
        }

        // USO DEL PATCH
        [HttpPatch("{id:int}")] // api/autores/id
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<AutorPatchDTO> patchDoc)
        {
           if (patchDoc is null)
            {
                return BadRequest();
            }
            
            var autorDB = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if ( autorDB is null )
            {
                return NotFound();
            }

            var autorPatchDTO = mapper.Map<AutorPatchDTO>(autorDB);

            patchDoc.ApplyTo(autorPatchDTO, ModelState);

            var esValido = TryValidateModel(autorPatchDTO);

            if ( !esValido ) 
            {
                return ValidationProblem();
            }

            mapper.Map(autorPatchDTO, autorDB); 

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")] // api/autores/id
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Autores
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
            if (registrosBorrados == 0){ return NotFound();}
            return NoContent();
        }

    }
}
