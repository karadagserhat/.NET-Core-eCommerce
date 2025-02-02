using ECommerceBackend.Application.Abstractions.Hubs;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Infrastructure.Helpers;
using ECommerceBackend.Infrastructure.Services;
using ECommerceBackend.Infrastructure.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ECommerceBackend.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionMultiplexer>(config =>
        {
            var connString = Configuration.ConnectionString
                ?? throw new Exception("Cannot get redis connection string");
            var configuration = ConfigurationOptions.Parse(connString, true);
            return ConnectionMultiplexer.Connect(configuration);
        });

        services.AddSingleton<ICartService, CartService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();

        services.AddTransient<IProductHubService, ProductHubService>();
        services.AddSignalR();

    }
}
