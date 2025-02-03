using ECommerceBackend.Application.Abstractions.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace ECommerceBackend.Infrastructure.SignalR;

public class ProductHubService(IHubContext<ProductHub> hubContext) : IProductHubService
{
    private readonly IHubContext<ProductHub> _hubContext = hubContext;
    public async Task ProductAddedMessageAsync(string message)
    {
        await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
    }
}