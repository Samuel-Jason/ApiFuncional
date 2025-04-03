using ApiTesta.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiTesta.DTOs
{
    public class CategoriaDTO
    {
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "Nome Obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Nome { get; set; }
        public ICollection<Produto>? Produtos { get; set; }

    }
}
