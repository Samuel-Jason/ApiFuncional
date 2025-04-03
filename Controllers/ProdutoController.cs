using ApiTesta.DTOs;
using ApiTesta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTesta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAll()
        {
            var produtos = await _produtoService.GetAll();
            return produtos == null ? NotFound() : Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "GetProduto")]
        public async Task<ActionResult<ProdutoDTO>> GetById(int id)
        {
            var produto = await _produtoService.GetById(id);
            return produto == null ? NotFound() : Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDTO)
        {
            if (produtoDTO == null)
                return BadRequest("Dados inválidos");

            await _produtoService.Create(produtoDTO);
            return CreatedAtRoute("GetProduto", new { id = produtoDTO.Id }, produtoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProdutoDTO produtoDTO)
        {
            if (id != produtoDTO.Id)
                return BadRequest("ID do produto não corresponde");

            if (produtoDTO == null)
                return BadRequest("Dados inválidos");

            await _produtoService.Update(produtoDTO);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var produto = await _produtoService.GetById(id);
            if (produto == null)
                return NotFound("Produto não encontrado");

            await _produtoService.Delete(id);
            return Ok(produto);
        }
    }
}
