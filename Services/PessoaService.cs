using ApiTesta.Infra;
using ApiTesta.Models;
using ApiTesta.Repository;
using ApiTesta.Services.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace ApiTesta.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IAuthService _authService;
        public PessoaService(IPessoaRepository pessoaRepository, IAuthService authService)
        {
            _pessoaRepository = pessoaRepository;
            _authService = authService;
        }
        public async Task<Pessoa> GetPessoaByIdAsync(int id)
        {
            return await _pessoaRepository.GetPessoaByIdAsync(id);
        }

        public async Task<IEnumerable<Pessoa>> GetPessoasAsync()
        {
            return await _pessoaRepository.GetPessoaAsync();
        }
        public async Task<Pessoa> AddPessoaAsync(Pessoa pessoa)
        {
            if (pessoa == null)
            {
                return null;
            }

            await _pessoaRepository.AddPessoaAsync(pessoa);
            return pessoa;
        }

        public async Task UpdatePessoaAsync(Pessoa pessoa)
        {
            await _pessoaRepository.UpdatePessoaAsync(pessoa);

        }
        public async Task DeletePessoaAsync(int id)
        {
            await _pessoaRepository.DeletePessoaAsync(id);
        }

        public async Task<Pessoa> GetUserByEmailAsync(string email)
        {
            return await _pessoaRepository.GetUserByEmailAsync(email);
        }

        public async Task<string> RegisterAsync(RegisterModel login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return null;
            }

            var usuarioExistente = await _pessoaRepository.GetUserByEmailAsync(login.Email);
            if (usuarioExistente != null)
            {
                return null;
            }

            var senhaHash = _authService.computedSha256Hash(login.Password);
            var novaPessoa = new Pessoa(login.Nome, login.Idade, login.Email, senhaHash);

            await _pessoaRepository.AddPessoaAsync(novaPessoa);

            return _authService.GenerateJwtToken(login.Email);
        }

        public async Task<string> LoginAsync(LoginModel login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return null;
            }

            var usuario = await _pessoaRepository.GetUserByEmailAsync(login.Email);
            if (usuario == null)
            {
                return null;
            }

            var senhaHash = _authService.computedSha256Hash(login.Password);
            if (usuario.PasswordHash != senhaHash)
            {
                return null;
            }

            return _authService.GenerateJwtToken(usuario.Email);
        }
    }
}
