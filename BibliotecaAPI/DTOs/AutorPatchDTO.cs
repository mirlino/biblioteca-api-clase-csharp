using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class AutorPatchDTO
    {
        [Required]
        [StringLength(150, ErrorMessage = "El {0} no puede exceder de {1} caracteres.")]
        public required string Nombres { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "El {0} no puede exceder de {1} caracteres.")]
        public required string Apellidos { get; set; }
        [StringLength(20, ErrorMessage = "El {0} no puede exceder de {1} caracteres.")]
        public string? Identificacion { get; set; }
    }
}
