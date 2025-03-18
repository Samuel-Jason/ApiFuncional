using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiTesta.Models
{
    public class Pessoa
    {
        public Pessoa(string nome, int idade, string email, string passwordHash)
        {
            Nome = nome;
            Idade = idade;
            Email = email;
            PasswordHash = passwordHash;
        }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
