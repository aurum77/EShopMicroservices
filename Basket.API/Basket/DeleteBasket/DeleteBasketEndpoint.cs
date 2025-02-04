using FastEndpoints;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketRequest(string Username);

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint(ISender sender) : Endpoint<DeleteBasketRequest, DeleteBasketResponse>
{
    public override void Configure()
    {
        Delete("/basket/{username}");
        AllowAnonymous();
        Description(b =>
            b.WithName("DeleteProduct")
                .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Basket")
                .WithDescription("Delete Basket")
        );
    }

    public override async Task HandleAsync(DeleteBasketRequest req, CancellationToken ct)
    {
        var command = req.Adapt<DeleteBasketCommand>();
        var result = sender.Send(command);
        var response = result.Adapt<DeleteBasketResponse>();

        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
