using ApiTesta.Infra;
using ApiTesta.Infra.Auth;
using ApiTesta.Models;
using ApiTesta.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiTesta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;
        private readonly IAuthService _authService;

        public PessoaController(IPessoaService pessoaService, IAuthService authService)
        {
            _pessoaService = pessoaService;
            _authService = authService;
        }

        // GET: api/Pessoa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoas()
        {
            var pessoas = await _pessoaService.GetPessoasAsync();
            return Ok(pessoas);
        }

        // GET: api/Pessoa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoaById(int id)
        {
            var pessoa = await _pessoaService.GetPessoaByIdAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return Ok(pessoa);
        }

        // POST: api/Pessoa
        [HttpPost]
        public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
        {
            await _pessoaService.AddPessoaAsync(pessoa);
            return Ok(pessoa);
        }

        // PUT: api/Pessoa/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
        {
            await _pessoaService.UpdatePessoaAsync(pessoa);
            return NoContent();
        }

        // DELETE: api/Pessoa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            var pessoa = await _pessoaService.GetPessoaByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            await _pessoaService.DeletePessoaAsync(id);
            return NoContent();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel login) =>
           (await _pessoaService.RegisterAsync(login)) is string token
        ? Ok(new { Token = token })
        : BadRequest(new { message = "E-mail já cadastrado ou dados inválidos." });

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login) =>
            (await _pessoaService.LoginAsync(login)) is string token
                ? Ok(new { Token = token })
                : Unauthorized(new { message = "Credenciais inválidas." });

    }
}
