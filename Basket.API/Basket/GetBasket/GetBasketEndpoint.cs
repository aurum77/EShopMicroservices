using Basket.API.Models;
using FastEndpoints;
using Mapster;
using MediatR;

namespace Basket.API.Basket.GetBasket;

public record GetBasketRequest(string Username);

public record GetBasketResponse(ShoppingCart ShoppingCart);

public class GetBasketEndpoint(ISender sender) : Endpoint<GetBasketRequest, GetBasketResponse>
{
    public override void Configure()
    {
        Get("/basket/{username}");
        AllowAnonymous();
        Description(b =>
            b.WithName("GetBasket")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Basket")
                .WithDescription("Get Basket")
        );
    }

    public override async Task HandleAsync(GetBasketRequest req, CancellationToken ct)
    {
        var query = req.Adapt<GetBasketQuery>();
        var result = await sender.Send(query);
        var response = result.Adapt<GetBasketResponse>();
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
