using Basket.API.Dtos;
using FastEndpoints;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint(ISender sender) : Endpoint<CheckoutBasketRequest, CheckoutBasketResponse>
{
    public override void Configure()
    {
        Post("/basket/{username}");
        AllowAnonymous();
        Description(b =>
            b.WithName("CheckoutBasket")
                .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Checkout Basket")
                .WithDescription("Checkout Basket")
        );
    }

    public override async Task HandleAsync(CheckoutBasketRequest req, CancellationToken ct)
    {
        var query = req.Adapt<CheckoutBasketCommand>();
        var result = await sender.Send(query);
        var response = result.Adapt<CheckoutBasketResponse>();
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
