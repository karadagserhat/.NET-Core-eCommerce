﻿using ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Features.Commands.Cart.UpdateCart;

public class UpdateCartCommandResponse
{
    public required string Id { get; set; }
    public List<CartItem> Items { get; set; } = [];
}
