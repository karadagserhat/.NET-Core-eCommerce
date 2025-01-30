using ECommerceBackend.Application.Abstractions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Application.Features.Queries.Product.GetAllProducts;

public class GetAllProductQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
    readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Domain.Entities.Product>().GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => x.Name.Contains(request.Search));

        if (request.Brands.Count != 0)
            query = query.Where(x => request.Brands.Contains(x.Brand));

        if (request.Types.Count != 0)
            query = query.Where(x => request.Types.Contains(x.Type));

        var count = await query.CountAsync(cancellationToken);


        query = query.Skip(request.PageSize * (request.PageIndex - 1)).Take(request.PageSize);

        query = request.Sort switch
        {
            "priceAsc" => query.OrderBy(x => x.Price),
            "priceDesc" => query.OrderByDescending(x => x.Price),
            _ => query.OrderBy(x => x.Name)
        };

        var products = await query.ToListAsync(cancellationToken);

        return new()
        {
            Data = products,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Count = count

        };
    }
}
