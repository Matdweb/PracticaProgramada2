namespace PracticaProgramada2.Infrastructure.Repository;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CreateAsync(T entidad, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(T entidad, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
