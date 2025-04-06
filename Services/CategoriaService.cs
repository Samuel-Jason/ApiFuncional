using ApiTesta.DTOs;
using ApiTesta.Models;
using ApiTesta.Repository;
using ApiTesta.Services.Contracts;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTesta.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAll()
        {
            var categorias = await _categoriaRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
        }

        public async Task<CategoriaDTO> GetById(int id)
        {
            var categoria = await _categoriaRepository.GetId(id);
            return _mapper.Map<CategoriaDTO>(categoria);
        }

        public async Task<IEnumerable<CategoriaDTO>> GetCategoriesWithProducts()
        {
            var categorias = await _categoriaRepository.GetCategoriesProducts();
            return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
        }

        public async Task <CategoriaDTO>Create(CategoriaDTO categoriaDTO)
        {
            var categoriaEntity = _mapper.Map<Categoria>(categoriaDTO);
            await _categoriaRepository.Create(categoriaEntity);
            categoriaDTO.CategoriaId = categoriaEntity.CategoriaId;
            return categoriaDTO;
        }

        public async Task<CategoriaDTO> Update(CategoriaDTO categoriaDTO)
        {
            var categoriaEntity = _mapper.Map<Categoria>(categoriaDTO);
            await _categoriaRepository.Update(categoriaEntity);
            return _mapper.Map<CategoriaDTO>(categoriaEntity);
        }

        public async Task<CategoriaDTO> Delete(int id)
        {
            var categoriaEntity = await _categoriaRepository.GetId(id);
            if (categoriaEntity == null) return null;

            await _categoriaRepository.Delete(categoriaEntity.CategoriaId);
            return _mapper.Map<CategoriaDTO>(categoriaEntity);
        }

        public Task<CategoriaDTO> Create(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public Task<CategoriaDTO> Update(Categoria categoria)
        {
            throw new NotImplementedException();
        }
    }
}
