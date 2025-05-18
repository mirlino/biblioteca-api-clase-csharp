using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;

namespace BibliotecaAPI.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Autor, AutorDTO>()
                .ForMember(dto => dto.NombreCompleto, 
                    config => config.MapFrom(autor => MapearNombreCompletoAutor(autor)));

            CreateMap<Autor, AutorConLibrosDTO>()
                .ForMember(dto => dto.NombreCompleto,
                    config => config.MapFrom(autor => MapearNombreCompletoAutor(autor)));

            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorPatchDTO>().ReverseMap(); // Se realiza este proceso por el patch

            CreateMap<Libro, LibroDTO>();
            CreateMap<LibroCreacionDTO, Libro>();
            
            CreateMap<Libro, LibroConAutorDTO>()
                .ForMember(dto => dto.NombreAutor, 
                    config => config.MapFrom(ent => MapearNombreCompletoAutor(ent.Autor!)));

        }

        private string MapearNombreCompletoAutor(Autor autor) => $"{autor.Nombres} {autor.Apellidos}";
        
    }
}
