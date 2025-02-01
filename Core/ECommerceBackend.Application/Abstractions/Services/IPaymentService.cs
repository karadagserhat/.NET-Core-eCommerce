using ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Abstractions.Services;

public interface IPaymentService
{
    Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId);

}