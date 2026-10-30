using AutoMapper;
using PracticaProgramada2.Application.Dtos;
using PracticaProgramada2.Domain;
using PracticaProgramada2.Infrastructure.Repository;

namespace PracticaProgramada2.Application.Services;

public class CategoriaServicio : ICategoriaServicio
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CategoriaServicio(ICategoriaRepository categoriaRepository, IMapper mapper)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoriaDto>> ObtenerCategoriasAsync(CancellationToken cancellationToken = default)
    {
        var categorias = await _categoriaRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<CategoriaDto>>(categorias);
    }

    public async Task<CategoriaDto?> ObtenerCategoriaPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<CategoriaDto>(categoria);
    }

    public async Task<Respuesta<CategoriaDto>> CrearCategoriaAsync(CategoriaDto categoria, CancellationToken cancellationToken = default)
    {
        var respuesta = new Respuesta<CategoriaDto>();
        var categoriaEntity = _mapper.Map<Categoria>(categoria);

        if (!categoriaEntity.ValidarReglasDeNegocio())
        {
            return Error(respuesta, "La categoría debe tener nombre y descripción.", 400);
        }

        if (!await _categoriaRepository.CreateAsync(categoriaEntity, cancellationToken))
        {
            return Error(respuesta, "No se pudo crear la categoría.", 500);
        }

        respuesta.Dato = _mapper.Map<CategoriaDto>(categoriaEntity);
        respuesta.Codigo = 201;
        respuesta.Mensaje = "Categoría creada correctamente.";
        return respuesta;
    }

    public async Task<Respuesta<CategoriaDto>> ActualizarCategoriaAsync(CategoriaDto categoria, CancellationToken cancellationToken = default)
    {
        var respuesta = new Respuesta<CategoriaDto>();
        var categoriaEntity = _mapper.Map<Categoria>(categoria);

        if (!categoriaEntity.ValidarReglasDeNegocio())
        {
            return Error(respuesta, "La categoría debe tener nombre y descripción.", 400);
        }

        if (!await _categoriaRepository.UpdateAsync(categoriaEntity, cancellationToken))
        {
            return Error(respuesta, "No se pudo actualizar la categoría.", 404);
        }

        respuesta.Dato = _mapper.Map<CategoriaDto>(categoriaEntity);
        respuesta.Mensaje = "Categoría actualizada correctamente.";
        return respuesta;
    }

    public async Task<Respuesta<CategoriaDto>> EliminarCategoriaAsync(int id, CancellationToken cancellationToken = default)
    {
        var respuesta = new Respuesta<CategoriaDto>();

        if (!await _categoriaRepository.DeleteAsync(id, cancellationToken))
        {
            return Error(respuesta, "No se encontró la categoría.", 404);
        }

        respuesta.Mensaje = "Categoría eliminada correctamente.";
        return respuesta;
    }

    private static Respuesta<CategoriaDto> Error(
        Respuesta<CategoriaDto> respuesta,
        string mensaje,
        int codigo)
    {
        respuesta.EsCorrecto = false;
        respuesta.Mensaje = mensaje;
        respuesta.Codigo = codigo;
        return respuesta;
    }
}
