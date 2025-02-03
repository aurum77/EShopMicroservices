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
        Post("/basket/{username}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(StoreBasketRequest req, CancellationToken ct)
    {
        var command = req.Adapt<StoreBasketCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<StoreBasketResponse>();

        await SendAsync(response, StatusCodes.Status201Created, ct);
    }
}
