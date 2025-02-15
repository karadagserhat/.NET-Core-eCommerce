using System;
using ECommerceBackend.Domain.Entities;
using ECommerceBackend.Domain.Entities.Common;
using ECommerceBackend.Persistence.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Persistence.Contexts;

public class ECommerceBackendDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
  public DbSet<Product> Products { get; set; }
  public DbSet<Address> Addresses { get; set; }
  public DbSet<Photo> Photos { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
  }

  //efcore interceptor
  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    var datas = ChangeTracker.Entries<BaseEntity>();
    foreach (var data in datas)
    {
      _ = data.State switch
      {
        EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
        EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
        _ => DateTime.UtcNow
      };
    }
    return await base.SaveChangesAsync(cancellationToken);
  }
}