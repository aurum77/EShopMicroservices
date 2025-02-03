using FastEndpoints;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdRequest(Guid Id);

public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint(ISender sender)
    : Endpoint<GetProductByIdRequest, GetProductByIdResponse>
{
    public override void Configure()
    {
        Get("/products/{id}");
        AllowAnonymous();
        Description(b =>
            b.WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product by Id")
                .WithDescription("Get Product by Id")
        );
    }

    public override async Task HandleAsync(GetProductByIdRequest req, CancellationToken ct)
    {
        var query = new GetProductByIdQuery(req.Id);
        var result = sender.Send(query).Result;
        var response = result.Adapt<GetProductByIdResponse>();
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
