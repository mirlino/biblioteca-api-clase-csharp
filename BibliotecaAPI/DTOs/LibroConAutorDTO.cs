namespace BibliotecaAPI.DTOs
{
    public class LibroConAutorDTO: LibroDTO
    {
        public int AutorId { get; set; }
        public string? NombreAutor { get; set; } // Se puede usar el ? para indicar que es nullable, pero no es necesario en este caso.
    }
    
}
