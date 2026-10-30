using Microsoft.EntityFrameworkCore;
using PracticaProgramada2.Domain;
using PracticaProgramada2.Infrastructure.Data;

namespace PracticaProgramada2.Infrastructure.Repository;

public class ProductoRepository : Repository<Producto>, IProductoRepository
{
    public ProductoRepository(ApplicationDbContext context) : base(context)
    {
    }

    protected override IQueryable<Producto> Consulta()
    {
        return base.Consulta().Include(producto => producto.Categoria);
    }
}
