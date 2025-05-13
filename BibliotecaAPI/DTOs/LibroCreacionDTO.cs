using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class LibroCreacionDTO
    {
        [Required]
        [StringLength(250, ErrorMessage = "El {0} no puede exceder de {1} caracteres.")]
        public required string Titulo { get; set; }
        public int AutorId { get; set; }
    }
}
