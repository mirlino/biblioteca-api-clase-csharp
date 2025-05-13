using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "El {0} no puede exceder de {1} caracteres.")]
        public required string Nombres { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "El {0} no puede exceder de {1} caracteres.")]
        public required string Apellidos { get; set; }
        [StringLength(20, ErrorMessage = "El {0} no puede exceder de {1} caracteres.")]
        public string? Identificacion { get; set; }
        public List<Libro> Libros { get; set; } = new List<Libro>(); // Relación uno a muchos
    }
}
