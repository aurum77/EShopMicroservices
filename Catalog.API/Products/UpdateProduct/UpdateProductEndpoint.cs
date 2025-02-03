using FastEndpoints;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
);

public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint(ISender sender)
    : Endpoint<UpdateProductRequest, UpdateProductResponse>
{
    public override void Configure()
    {
        Put("/products");
        AllowAnonymous();
        Description(b =>
            b.WithName("UpdateProduct")
                .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Product")
                .WithDescription("Update Product")
        );
    }

    public override async Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
    {
        var command = req.Adapt<UpdateProductCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<UpdateProductResponse>();
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
