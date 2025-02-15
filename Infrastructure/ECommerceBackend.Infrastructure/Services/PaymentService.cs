using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace ECommerceBackend.Infrastructure.Services;

public class PaymentService(IConfiguration config, ICartService cartService, IUnitOfWork unitOfWork) : IPaymentService
{
    readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId)
    {
        StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];

        var cart = await cartService.GetCartAsync(cartId);

        if (cart == null) return null;

        var shippingPrice = 0m;

        foreach (var item in cart.Items)
        {
            var productItem = await _unitOfWork.Repository<Domain.Entities.Product>().GetByIdAsync(item.ProductId);
            if (productItem == null) return null;
            if (item.Price != productItem.Price)
            {
                item.Price = productItem.Price;
            }
        }

        var service = new PaymentIntentService();
        PaymentIntent? intent = null;

        if (string.IsNullOrEmpty(cart.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100))
                    + (long)shippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = ["card"]
            };
            intent = await service.CreateAsync(options);
            cart.PaymentIntentId = intent.Id;
            cart.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100))
                    + (long)shippingPrice * 100
            };
            intent = await service.UpdateAsync(cart.PaymentIntentId, options);
        }

        await cartService.SetCartAsync(cart);
        return cart;
    }
}