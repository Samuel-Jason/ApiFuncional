using ApiTesta.Data;
using ApiTesta.Models;
using ApiTesta.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTesta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        // GET: api/Pessoa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoas()
        {
            var pessoas = await _pessoaRepository.GetPessoaAsync();
            return Ok(pessoas);
        }

        // GET: api/Pessoa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoa(int id)
        {
            var pessoa = await _pessoaRepository.GetPessoaByIdAsync(id);

            if (pessoa != null)
            {
                return NotFound();
            }
            return Ok(pessoa);
        }

        // POST: api/Pessoa
        [HttpPost]
        public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
        {
            var res = await _pessoaRepository.GetPessoaByIdAsync(pessoa.Id);
            if(res == null)
            {
                await _pessoaRepository.AddPessoaAsync(pessoa);
                return Ok(pessoa);
            }
            return NoContent();
        }

        // PUT: api/Pessoa/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            await _pessoaRepository.UpdatePessoaAsync(pessoa);
            return NoContent();
        }

        // DELETE: api/Pessoa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            var pessoa = await _pessoaRepository.GetPessoaByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            await _pessoaRepository.DeletePessoaAsync(id);
            return NoContent();
        }
    }
}
