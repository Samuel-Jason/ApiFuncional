using ApiTesta.Data;
using ApiTesta.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTesta.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly BancoContext _context;

        public PessoaRepository(BancoContext context)
        {
            _context = context;
        }

        public async Task<Pessoa?> GetPessoaByIdAsync(int id)
        {
            return await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pessoa>> GetPessoaAsync()
        {
            return await _context.Pessoas.ToListAsync();
        }

        public async Task AddPessoaAsync(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePessoaAsync(Pessoa pessoa)
        {
            _context.Entry(pessoa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePessoaAsync(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa != null)
            {
                _context.Pessoas.Remove(pessoa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Pessoa> GetUserByEmailAsync(string email)
        {
            return await _context.Pessoas.FirstOrDefaultAsync(p => p.Email == email);

        }
    }
}
