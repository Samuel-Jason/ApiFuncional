using System.ComponentModel.DataAnnotations;

namespace ApiTesta.Models
{
    public class RegisterModel
    {
        public RegisterModel(string nome, int idade, string email, string password)
        {
            Nome = nome;
            Idade = idade;
            Email = email;
            Password = password;
        }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(2, ErrorMessage = "O nome deve ter pelo menos 2 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(1, 120, ErrorMessage = "A idade deve estar entre 1 e 120 anos.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Password { get; set; }
    }
}
