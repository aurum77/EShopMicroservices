using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints;

public record GetOrdersByNameRequest(string Username);

public record GetOrdersByNameResponse(List<OrderDto> Orders);

public class GetOrdersByName(ISender sender) : Endpoint<GetOrdersByNameRequest, GetOrdersByNameResponse>
{
    public override void Configure()
    {
        Get("/orders/{username}");
        AllowAnonymous();
        Description(b =>
            b.WithName("GetOrder by Name")
                .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Order by Name")
                .WithDescription("Get Order by Name")
        );
    }

    public override async Task HandleAsync(GetOrdersByNameRequest req, CancellationToken ct)
    {
        var command = req.Adapt<GetOrdersByNameQuery>();
        var result = await sender.Send(command);
        var response = result.Adapt<GetOrdersByNameResponse>();
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
