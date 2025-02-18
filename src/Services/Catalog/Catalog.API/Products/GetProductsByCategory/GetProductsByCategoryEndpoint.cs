using Carter;
using Catalog.API.Models;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProductsByCategory;

//no need to create request object since we will read category from url

public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", 
                async (string category, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductsByCategoryQuery(category));
            
                    var response = result.Adapt<GetProductsByCategoryResponse>();
            
                    return Results.Ok(response);
                })
            .WithName("GetProductByCategory")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Category")
            .WithDescription("Get Product By Category");
    }

}