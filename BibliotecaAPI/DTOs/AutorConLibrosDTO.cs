namespace BibliotecaAPI.DTOs
{
    public class AutorConLibrosDTO: AutorDTO
    {
        public List<LibroDTO> Libros { get; set; } = new List<LibroDTO>(); // Se puede usar corchetes vacios o el new List<LibroDTO>();
    }
    
}
