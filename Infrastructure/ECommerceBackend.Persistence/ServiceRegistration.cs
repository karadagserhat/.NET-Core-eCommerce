using ECommerceBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ECommerceBackend.Application.Repositories.Product;
using ECommerceBackend.Persistence.Repositories.Product;
using ECommerceBackend.Persistence.Config;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Persistence.Services;

namespace ECommerceBackend.Persistence;

public static class ServiceRegistration
{
  public static void AddPersistenceServices(this IServiceCollection services)
  {
    services.AddDbContext<ECommerceBackendDbContext>(opt => opt.UseSqlServer(Configuration.ConnectionString));

    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();

  }
}
