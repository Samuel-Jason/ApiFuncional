using ApiTesta.Models;

namespace ApiTesta.Repository
{
    public interface ICategoriaRepository
    {
        Task <IEnumerable<Categoria>>GetAll();
        Task <IEnumerable<Categoria>>GetCategoriesProducts();
        Task <Categoria>GetId(int id);
        Task <Categoria>Create(Categoria categoria);
        Task <Categoria>Update(Categoria categoria);
        Task <Categoria>Delete(int id);
    }
}
