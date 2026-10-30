using AutoMapper;
using PracticaProgramada2.Application.Dtos;
using PracticaProgramada2.Domain;

namespace PracticaProgramada2.Application.Mapping
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();

            CreateMap<Producto, ProductoDto>();

            CreateMap<ProductoDto, Producto>()
                .ForMember(destino => destino.Categoria,
                    opciones => opciones.Ignore());
        }
    }
}
