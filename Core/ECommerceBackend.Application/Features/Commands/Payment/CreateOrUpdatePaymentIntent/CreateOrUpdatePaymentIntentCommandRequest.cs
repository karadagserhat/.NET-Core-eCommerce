using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Payment.CreateOrUpdatePaymentIntent;

public class CreateOrUpdatePaymentIntentCommandRequest : IRequest<CreateOrUpdatePaymentIntentCommandResponse>
{
    public string CartId { get; set; }
}