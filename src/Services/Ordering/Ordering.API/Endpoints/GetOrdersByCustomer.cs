using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerRequest(Guid CustomerId);

public record GetOrdersByCustomerResponse(List<OrderDto> Orders);

public class GetOrdersByCustomer(ISender sender) : Endpoint<GetOrdersByCustomerRequest, GetOrdersByCustomerResponse>
{
    public override void Configure()
    {
        Get("/orders/customer/{customerId}");
        AllowAnonymous();
        Description(b =>
            b.WithName("Get Orders by Customer")
                .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Orders by Customer")
                .WithDescription("Get Orders by Customer")
        );
    }

    public override async Task HandleAsync(GetOrdersByCustomerRequest req, CancellationToken ct)
    {
        var command = req.Adapt<GetOrdersByCustomerQuery>();
        var result = await sender.Send(command);
        var response = result.Adapt<GetOrdersByCustomerResponse>();
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
