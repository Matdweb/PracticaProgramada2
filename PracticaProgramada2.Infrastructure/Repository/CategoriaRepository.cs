using Microsoft.EntityFrameworkCore;
using PracticaProgramada2.Domain;
using PracticaProgramada2.Infrastructure.Data;

namespace PracticaProgramada2.Infrastructure.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Indica si ya existe una categoría con el mismo nombre.
    /// </summary>
    /// <param name="nombre">Nombre a buscar.</param>
    /// <param name="idExcluir">Id de la categoría que se ignora en la búsqueda (0 al crear).</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si existe otra categoría con ese nombre.</returns>
    public async Task<bool> ExisteNombreAsync(string nombre, int idExcluir, CancellationToken cancellationToken = default)
    {
        return await _context.Categorias
            .AsNoTracking()
            .AnyAsync(categoria => categoria.Nombre == nombre
                                   && categoria.CategoriaId != idExcluir, cancellationToken);
    }

    /// <summary>
    /// Indica si la categoría tiene productos asociados.
    /// </summary>
    /// <param name="id">Id de la categoría.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si tiene al menos un producto.</returns>
    public async Task<bool> TieneProductosAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Productos
            .AsNoTracking()
            .AnyAsync(producto => producto.CategoriaId == id, cancellationToken);
    }
}