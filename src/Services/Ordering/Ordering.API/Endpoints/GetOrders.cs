using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;

public record GetOrdersRequest
{
    [QueryParam]
    public int PageIndex { get; set; }

    [QueryParam]
    public int PageSize { get; set; }
}

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders(ISender sender) : Endpoint<GetOrdersRequest, GetOrdersResponse>
{
    public override void Configure()
    {
        Get("/orders");
        AllowAnonymous();
        Description(b =>
            b.WithName("GetOrders")
                .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Orders")
                .WithDescription("Get Orders")
        );
    }

    public override async Task HandleAsync(GetOrdersRequest req, CancellationToken ct)
    {
        var result = await sender.Send(new GetOrdersQuery(new PaginationRequest(PageIndex: req.PageIndex, PageSize: req.PageSize)), ct);

        var response = result.Adapt<GetOrdersResponse>();

        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
