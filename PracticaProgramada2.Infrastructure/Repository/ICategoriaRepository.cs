using PracticaProgramada2.Domain;

namespace PracticaProgramada2.Infrastructure.Repository;

public interface ICategoriaRepository : IRepository<Categoria>
{
    /// <summary>
    /// Indica si ya existe una categoría con el mismo nombre.
    /// </summary>
    /// <param name="nombre">Nombre a buscar.</param>
    /// <param name="idExcluir">Id de la categoría que se ignora en la búsqueda (0 al crear).</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si existe otra categoría con ese nombre.</returns>
    Task<bool> ExisteNombreAsync(string nombre, int idExcluir, CancellationToken cancellationToken = default);

    /// <summary>
    /// Indica si la categoría tiene productos asociados.
    /// </summary>
    /// <param name="id">Id de la categoría.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si tiene al menos un producto.</returns>
    Task<bool> TieneProductosAsync(int id, CancellationToken cancellationToken = default);
}