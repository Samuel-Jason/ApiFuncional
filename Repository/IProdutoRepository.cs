using ApiTesta.Models;

namespace ApiTesta.Repository
{
    public interface IProdutoRepository
    {
        Task<Produto> Create(Produto produto);
        Task<Produto> Delete(int id);
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(int id);
        Task<Produto> Update(Produto produto);
    }
}
