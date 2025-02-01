using FastEndpoints;

namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest()
{
    // these must have public access modifier in
    // order to use [QueryParam] binding.
    [QueryParam]
    public int PageNumber { get; set; }

    [QueryParam]
    public int PageSize { get; set; }
};

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint(ISender sender) : Endpoint<GetProductsRequest, GetProductsResponse>
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

    public override async Task HandleAsync(GetProductsRequest req, CancellationToken ct)
    {
        var query = req.Adapt<GetProductsQuery>();
        var result = sender.Send(query).Result;
        var response = result.Adapt<GetProductsResponse>();
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
