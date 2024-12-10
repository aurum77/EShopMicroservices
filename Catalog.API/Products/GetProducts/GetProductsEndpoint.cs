using FastEndpoints;

namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint(ISender sender) : EndpointWithoutRequest<GetProductsResponse>
{
    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
        Description(b =>
            b.WithName("GetProducts")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Get Products")
        );
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        var query = new GetProductsQuery();
        var result = sender.Send(query).Result;
        var response = result.Adapt<GetProductsResponse>();
        return SendAsync(response, StatusCodes.Status200OK);
    }
}
