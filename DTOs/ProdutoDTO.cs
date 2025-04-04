using ApiTesta.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiTesta.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome Obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Preço Obrigatório")]
        public decimal Preco { get; set; }
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "Estoque é Obrigatório")]
        [Range(1, 9999)]
        public long Estoque { get; set; }
        public string? ImageURL { get; set; }
        [JsonIgnore]
        public Categoria? Categoria { get; set; }
        public string? CategoriaNome { get; set; }
        public int CategoriaId { get; set; }
    }
}
