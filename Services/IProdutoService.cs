using ApiTesta.DTOs;
using ApiTesta.Models;

namespace ApiTesta.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDTO>> GetAll();
        Task<ProdutoDTO> GetById(int id);
        Task<ProdutoDTO> Create(ProdutoDTO produtoDTO);
        Task<ProdutoDTO> Update(ProdutoDTO produtoDTO);
        Task<ProdutoDTO> Delete(int id);
    }
}
