using AutoMapper;
using PracticaProgramada2.Application.Dtos;
using PracticaProgramada2.Domain;
using PracticaProgramada2.Infrastructure.Repository;

namespace PracticaProgramada2.Application.Services;

public class ProductoServicio : IProductoServicio
{
    private readonly IProductoRepository _productoRepository;
    private readonly IMapper _mapper;

    public ProductoServicio(IProductoRepository productoRepository, IMapper mapper)
    {
        _productoRepository = productoRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductoDto>> ObtenerProductosAsync(CancellationToken cancellationToken = default)
    {
        var productos = await _productoRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<ProductoDto>>(productos);
    }

    public async Task<ProductoDto?> ObtenerProductoPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var producto = await _productoRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<ProductoDto>(producto);
    }

    public async Task<Respuesta<ProductoDto>> CrearProductoAsync(ProductoDto producto, CancellationToken cancellationToken = default)
    {
        var respuesta = new Respuesta<ProductoDto>();
        var productoEntity = _mapper.Map<Producto>(producto);

        if (!productoEntity.ValidarReglasDeNegocio())
        {
            return Error(respuesta, "El producto contiene datos inválidos.", 400);
        }

        if (!await _productoRepository.CreateAsync(productoEntity, cancellationToken))
        {
            return Error(respuesta, "No se pudo crear el producto.", 500);
        }

        respuesta.Dato = _mapper.Map<ProductoDto>(productoEntity);
        respuesta.Codigo = 201;
        respuesta.Mensaje = "Producto creado correctamente.";
        return respuesta;
    }

    public async Task<Respuesta<ProductoDto>> ActualizarProductoAsync(ProductoDto producto, CancellationToken cancellationToken = default)
    {
        var respuesta = new Respuesta<ProductoDto>();
        var productoEntity = _mapper.Map<Producto>(producto);

        if (!productoEntity.ValidarReglasDeNegocio())
        {
            return Error(respuesta, "El producto contiene datos inválidos.", 400);
        }

        if (!await _productoRepository.UpdateAsync(productoEntity, cancellationToken))
        {
            return Error(respuesta, "No se encontró el producto.", 404);
        }

        respuesta.Dato = _mapper.Map<ProductoDto>(productoEntity);
        respuesta.Mensaje = "Producto actualizado correctamente.";
        return respuesta;
    }

    public async Task<Respuesta<ProductoDto>> EliminarProductoAsync(int id, CancellationToken cancellationToken = default)
    {
        var respuesta = new Respuesta<ProductoDto>();

        if (!await _productoRepository.DeleteAsync(id, cancellationToken))
        {
            return Error(respuesta, "No se encontró el producto.", 404);
        }

        respuesta.Mensaje = "Producto eliminado correctamente.";
        return respuesta;
    }

    private static Respuesta<ProductoDto> Error(
        Respuesta<ProductoDto> respuesta,
        string mensaje,
        int codigo)
    {
        respuesta.EsCorrecto = false;
        respuesta.Mensaje = mensaje;
        respuesta.Codigo = codigo;
        return respuesta;
    }
}
