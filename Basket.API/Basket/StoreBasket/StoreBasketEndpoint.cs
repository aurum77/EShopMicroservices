using Basket.API.Models;
using FastEndpoints;
using Mapster;
using MediatR;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Basket);

public record StoreBasketResponse(string Username);

public class StoreBasketEndpoint(ISender sender) : Endpoint<StoreBasketRequest, StoreBasketResponse>
{
    public override void Configure()
    {
        Post("/basket");
        AllowAnonymous();
        Description(b =>
            b.WithName("StoreBasket")
                .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Store Basket")
                .WithDescription("Store Basket")
        );
    }

    public override async Task HandleAsync(StoreBasketRequest req, CancellationToken ct)
    {
        var command = req.Adapt<StoreBasketCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<StoreBasketResponse>();

        await SendAsync(response, StatusCodes.Status201Created, ct);
    }
}
