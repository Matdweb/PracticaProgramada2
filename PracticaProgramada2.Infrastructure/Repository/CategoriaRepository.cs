using PracticaProgramada2.Domain;
using PracticaProgramada2.Infrastructure.Data;

namespace PracticaProgramada2.Infrastructure.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(ApplicationDbContext context) : base(context)
    {
    }
}
