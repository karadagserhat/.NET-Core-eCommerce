using ECommerceBackend.Application.Repositories.Product;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories.Product;

public class ProductRepository : GenericRepository<Domain.Entities.Product>, IProductRepository
{
    public ProductRepository(ECommerceBackendDbContext context) : base(context)
    {
    }
}