using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResults>;

public record GetProductsByCategoryResults(IEnumerable<Product> Products);

internal class GetProductsByCategoryQueryHandler(IDocumentSession session) :
    IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResults>
{
    public async Task<GetProductsByCategoryResults> Handle(GetProductsByCategoryQuery query,
        CancellationToken cancellationToken)
    {
        var results = await session.Query<Product>().Where(p => p.Category.Contains(query.Category)).ToListAsync(cancellationToken);

        return new GetProductsByCategoryResults(results);
    }
}