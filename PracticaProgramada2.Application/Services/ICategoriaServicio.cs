using PracticaProgramada2.Application.Dtos;

namespace PracticaProgramada2.Application.Services
{
    public interface ICategoriaServicio
    {
        Task<List<CategoriaDto>> ObtenerCategoriasAsync(CancellationToken cancellationToken = default);
        Task<CategoriaDto?> ObtenerCategoriaPorIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Respuesta<CategoriaDto>> CrearCategoriaAsync(CategoriaDto categoria, CancellationToken cancellationToken = default);
        Task<Respuesta<CategoriaDto>> ActualizarCategoriaAsync(CategoriaDto categoria, CancellationToken cancellationToken = default);
        Task<Respuesta<CategoriaDto>> EliminarCategoriaAsync(int id, CancellationToken cancellationToken = default);
    }
}
