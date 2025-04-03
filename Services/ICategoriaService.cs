using ApiTesta.DTOs;
using ApiTesta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTesta.Services
{
    public interface ICategoriaService
    {
        Task<CategoriaDTO> GetById(int id);
        Task<IEnumerable<CategoriaDTO>> GetAll();
        Task<IEnumerable<CategoriaDTO>> GetCategoriesWithProducts();
        Task<CategoriaDTO> Create(CategoriaDTO categoriaDTO);
        Task<CategoriaDTO> Update(CategoriaDTO categoriaDTO);
        Task <CategoriaDTO> Delete(int id);
    }
}
