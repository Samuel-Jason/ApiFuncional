using ApiTesta.Infra;
using ApiTesta.Infra.Auth;
using ApiTesta.Models;
using ApiTesta.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiTesta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;
        private readonly IAuthService _authService;
        private readonly ILogger<PessoaController> _logger;


        public PessoaController(IPessoaService pessoaService, IAuthService authService, ILogger<PessoaController> logger)
        {
            _pessoaService = pessoaService;
            _authService = authService;
            _logger = logger;

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
        public async Task<IActionResult> Register([FromBody] RegisterModel login)
        {
            var result = await _pessoaService.RegisterAsync(login);

            if (result is string token)
                return Ok(new { Token = token });

            return BadRequest(new { message = "E-mail já cadastrado ou dados inválidos." });
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var token = await _pessoaService.LoginAsync(login);

            if (token == null)
            {
                return Unauthorized(new { message = "Email ou Senha incorretos." });
            }

            // Criar opções do cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,    // Protege contra ataques XSS
                Secure = true,      // Apenas HTTPS
                SameSite = SameSiteMode.Strict, // Evita CSRF
                Expires = DateTime.UtcNow.AddHours(1) // Token expira em 1 hora
            };

            // Adicionar o token no cookie
            Response.Cookies.Append("JwtToken", token, cookieOptions);

            _logger.LogInformation($"Token gerado: {token}");

            return Ok(new { message = "Login realizado com sucesso" });
        }


    }
}
