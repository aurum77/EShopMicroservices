using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);

public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrderEndpoint(ISender sender) : Endpoint<UpdateOrderRequest, UpdateOrderResponse>
{
    public override void Configure()
    {
        Put("/orders");
        AllowAnonymous();
        Description(b =>
            b.WithName("UpdateOrder")
                .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Order")
                .WithDescription("Update Order")
        );
    }

    public override async Task HandleAsync(UpdateOrderRequest req, CancellationToken ct)
    {
        var command = req.Adapt<UpdateOrderCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<UpdateOrderResponse>();
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
