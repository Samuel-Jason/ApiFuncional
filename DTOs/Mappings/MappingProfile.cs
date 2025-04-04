using ApiTesta.Models;
using AutoMapper;
using System.ComponentModel;

namespace ApiTesta.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>();
            CreateMap<Produto, ProdutoDTO>()
                .ForMember(x => x.CategoriaNome, opt => opt.MapFrom(src => src.Categoria.Nome));
        }
    }
}
