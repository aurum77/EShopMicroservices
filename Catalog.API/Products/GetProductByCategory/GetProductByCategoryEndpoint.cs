using FastEndpoints;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryRequest(string Category);

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint(ISender sender)
    : Endpoint<GetProductByCategoryRequest, GetProductByCategoryResponse>
{
    public override void Configure()
    {
        Get("/products/by-category/{category}");
        AllowAnonymous();
        Description(b =>
            b.Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product by Category")
                .WithDescription("Get Product by Category")
        );
    }

    public override async Task HandleAsync(GetProductByCategoryRequest req, CancellationToken ct)
    {
        var query = new GetProductByCategoryQuery(req.Category);
        var result = await sender.Send(query);
        var response = result.Adapt<GetProductByCategoryResponse>();
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
