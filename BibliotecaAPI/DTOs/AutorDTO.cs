
namespace BibliotecaAPI.DTOs
{
    public class AutorDTO
    {
        public int Id { get; set; }
        public required string NombreCompleto { get; set; }
        public List<LibroDTO> Libros { get; set; } = []; //new List<LibroDTO>(); // Se puede usar corchetes vacios o el new List<LibroDTO>();

    }
}
