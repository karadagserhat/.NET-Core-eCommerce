using Microsoft.AspNetCore.Builder;

namespace ECommerceBackend.Infrastructure.SignalR;

public static class HubRegistration
{
    public static void MapHubs(this WebApplication webApplication)
    {
        webApplication.MapHub<ProductHub>("/products-hub");
    }
}
