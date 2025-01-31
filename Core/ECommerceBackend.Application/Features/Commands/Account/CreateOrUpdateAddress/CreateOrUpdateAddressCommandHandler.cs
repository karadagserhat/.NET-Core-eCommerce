using ECommerceBackend.Application.Abstractions.Services;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Account.CreateOrUpdateAddress;

public class CreateOrUpdateAddressCommandHandler(IAccountService accountService) : IRequestHandler<CreateOrUpdateAddressCommandRequest, CreateOrUpdateAddressCommandResponse>
{
    readonly IAccountService _accountService = accountService;

    public async Task<CreateOrUpdateAddressCommandResponse> Handle(CreateOrUpdateAddressCommandRequest request, CancellationToken cancellationToken)
    {
        var response = await _accountService.CreateOrUpdateAddress(request.AddressDto);

        return new()
        {
            Line1 = response.Line1,
            Line2 = response.Line2,
            City = response.City,
            State = response.State,
            Country = response.Country,
            PostalCode = response.PostalCode,
        };
    }
}