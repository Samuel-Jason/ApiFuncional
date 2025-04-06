using ApiTesta.DTOs;
using ApiTesta.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTesta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAll()
        {
            var categorias = await _categoriaService.GetAll();
            return categorias == null ? NotFound() : Ok(categorias);
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriaProdutos()
        {
            var categorias = await _categoriaService.GetCategoriesWithProducts();
            return categorias == null ? NotFound() : Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoriaDTO>> GetById(int id)
        {
            var categoria = await _categoriaService.GetById(id);
            return categoria == null ? NotFound() : Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO == null)
                return BadRequest("Dados inválidos");

            await _categoriaService.Create(categoriaDTO);
            return CreatedAtRoute("GetCategory", new { id = categoriaDTO.CategoriaId }, categoriaDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoriaDTO categoriaDTO)
        {
            if (id != categoriaDTO.CategoriaId)
                return BadRequest("ID da categoria não corresponde");

            if (categoriaDTO == null)
                return BadRequest("Dados inválidos");

            await _categoriaService.Update(categoriaDTO);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = await _categoriaService.GetById(id);
            if (categoria == null)
                return NotFound("Categoria não encontrada");

            await _categoriaService.Delete(id);
            return Ok(categoria);
        }
    }
}
