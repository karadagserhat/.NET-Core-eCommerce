using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities.Common;

namespace ECommerceBackend.Application.Abstractions.Services;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<bool> Complete();
}