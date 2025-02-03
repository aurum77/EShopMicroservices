using FastEndpoints;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductRequest(Guid Id);

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint(ISender sender)
    : Endpoint<DeleteProductRequest, DeleteProductResponse>
{
    public override void Configure()
    {
        Delete("/products/{id}");
        AllowAnonymous();
        Description(b =>
            b.WithName("DeleteProduct")
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product")
        );
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
    {
        var command = req.Adapt<DeleteProductCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<DeleteProductResponse>();
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
