using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

public record DeleteOrderRequest(Guid OrderId);

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrderEndpoint(ISender sender) : Endpoint<DeleteOrderRequest, DeleteOrderResponse>
{
    public override void Configure()
    {
        Delete("/orders/{orderId}");
        AllowAnonymous();
        Description(b =>
            b.WithName("DeleteOrder")
                .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Order")
                .WithDescription("Delete Order")
        );
    }

    public override async Task HandleAsync(DeleteOrderRequest req, CancellationToken ct)
    {
        var command = req.Adapt<DeleteOrderCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<DeleteOrderResponse>();

        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
