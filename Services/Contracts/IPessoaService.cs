using ApiTesta.Models;

namespace ApiTesta.Services.Contracts
{
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> GetPessoasAsync();
        Task<Pessoa> GetPessoaByIdAsync(int id);
        Task<Pessoa> AddPessoaAsync(Pessoa pessoa);
        Task UpdatePessoaAsync(Pessoa pessoa);
        Task DeletePessoaAsync(int id);
        Task<Pessoa> GetUserByEmailAsync(string email);
        Task<string?> RegisterAsync(RegisterModel login);
        Task<string?> LoginAsync(LoginModel login);
    }
}
