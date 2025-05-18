using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "El {0} no puede exceder de {1} caracteres.")]
        public required string Titulo { get; set; }
        public int AutorId { get; set; }
        public Autor? Autor { get; set; } // Relación muchos a uno
        public List<Comentario>? Comentarios { get; set; } = []; // Relación uno a muchos
    }
}
