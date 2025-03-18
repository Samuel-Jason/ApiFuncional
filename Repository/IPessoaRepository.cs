using ApiTesta.Models;

namespace ApiTesta.Repository
{
    public interface IPessoaRepository
    {
       Task<IEnumerable<Pessoa>>GetPessoaAsync();
       Task<Pessoa>GetPessoaByIdAsync(int id);
       Task AddPessoaAsync(Pessoa pessoa);
       Task UpdatePessoaAsync(Pessoa pessoa);
       Task DeletePessoaAsync(int id);
       Task<Pessoa> GetUserByEmailAsync(string email);
    }
}
