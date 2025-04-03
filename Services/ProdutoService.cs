using ApiTesta.DTOs;
using ApiTesta.Models;
using ApiTesta.Repository;
using AutoMapper;

namespace ApiTesta.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetAll()
        {
            var produtos = await _produtoRepository.GetAll();
            return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        }

        public async Task<ProdutoDTO> GetById(int id)
        {
            var produto = await _produtoRepository.GetById(id);
            return _mapper.Map<ProdutoDTO>(produto);
        }

        public async Task<ProdutoDTO> Create(ProdutoDTO produtoDTO)
        {
            var produtoEntity = _mapper.Map<Produto>(produtoDTO);
            await _produtoRepository.Create(produtoEntity);
            produtoDTO.Id = produtoEntity.Id;
            return _mapper.Map<ProdutoDTO>(produtoEntity);
        }

        public async Task<ProdutoDTO> Update(ProdutoDTO produtoDTO)
        {
            var produtoEntity = _mapper.Map<Produto>(produtoDTO);
            await _produtoRepository.Update(produtoEntity);
            return _mapper.Map<ProdutoDTO>(produtoEntity);
        }

        public async Task<ProdutoDTO> Delete(int id)
        {
            var produtoEntity = await _produtoRepository.Delete(id);
            return _mapper.Map<ProdutoDTO>(produtoEntity);
        }
    }
}
