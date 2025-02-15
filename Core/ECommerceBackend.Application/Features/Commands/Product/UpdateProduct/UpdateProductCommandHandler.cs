﻿using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceBackend.Application.Features.Commands.Product.UpdateProduct;

public class UpdateProductCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateProductCommandHandler> logger) : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    readonly IUnitOfWork _unitOfWork = unitOfWork;
    readonly ILogger<UpdateProductCommandHandler> _logger = logger;

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product? product = await _unitOfWork.Repository<Domain.Entities.Product>().GetByIdAsync(request.Id) ?? throw new ProductUpdateFailedException();

        product.Name = request.Name;
        product.Price = request.Price;
        product.Type = request.Type;
        product.PictureUrl = request.PictureUrl;
        product.Brand = request.Brand;
        product.QuantityInStock = request.QuantityInStock;
        product.Description = request.Description;

        await _unitOfWork.Complete();

        _logger.LogInformation("Product UPDATED!!!!");

        return new();
    }
}
