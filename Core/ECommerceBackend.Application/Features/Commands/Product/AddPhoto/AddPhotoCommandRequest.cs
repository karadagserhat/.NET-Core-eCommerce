using MediatR;
using Microsoft.AspNetCore.Http;

namespace ECommerceBackend.Application.Features.Commands.Product.AddPhoto;

public class AddPhotoCommandRequest : IRequest<AddPhotoCommandResponse>
{
    public required IFormFile? File { get; set; }
    public required int ProductId { get; set; }
}