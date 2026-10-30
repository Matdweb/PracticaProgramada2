using PracticaProgramada2.Application.Dtos;

namespace PracticaProgramada2.Application.Services
{
    public interface IProductoServicio
    {
        Task<List<ProductoDto>> ObtenerProductosAsync(CancellationToken cancellationToken = default);
        Task<ProductoDto?> ObtenerProductoPorIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Respuesta<ProductoDto>> CrearProductoAsync(ProductoDto producto, CancellationToken cancellationToken = default);
        Task<Respuesta<ProductoDto>> ActualizarProductoAsync(ProductoDto producto, CancellationToken cancellationToken = default);
        Task<Respuesta<ProductoDto>> EliminarProductoAsync(int id, CancellationToken cancellationToken = default);
    }
}
