using Microsoft.EntityFrameworkCore;
using PracticaProgramada2.Infrastructure.Data;

namespace PracticaProgramada2.Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    private readonly string _nombreLlave;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
        _nombreLlave = context.Model.FindEntityType(typeof(T))!.FindPrimaryKey()!.Properties[0].Name;
    }

    protected virtual IQueryable<T> Consulta()
    {
        return _dbSet.AsNoTracking();
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Consulta().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            return null;
        }

        return await Consulta()
            .FirstOrDefaultAsync(e => EF.Property<int>(e, _nombreLlave) == id, cancellationToken);
    }

    public async Task<bool> CreateAsync(T entidad, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entidad, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateAsync(T entidad, CancellationToken cancellationToken = default)
    {
        var id = _context.Entry(entidad)
            .Property(_nombreLlave)
            .CurrentValue;

        var existente = await _dbSet.FindAsync([id], cancellationToken);
        if (existente is null)
        {
            return false;
        }

        var entrada = _context.Entry(existente);
        entrada.CurrentValues.SetValues(entidad);

        foreach (var propiedad in entrada.Properties
                     .Where(propiedad => propiedad.Metadata.GetDefaultValueSql() != null))
        {
            propiedad.CurrentValue = propiedad.OriginalValue;
            propiedad.IsModified = false;
        }

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existente = await _dbSet.FindAsync([id], cancellationToken);
        if (existente is null)
        {
            return false;
        }

        _dbSet.Remove(existente);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
