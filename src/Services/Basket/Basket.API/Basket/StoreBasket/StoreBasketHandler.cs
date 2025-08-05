using Basket.API.Data;
using Basket.API.Models;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Basket) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string Username);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Basket).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Basket.Username).NotEmpty().WithMessage("Username is require.");
    }
}

internal class StoreBasketCommandHandler(
    IBasketRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountProto
) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(
        StoreBasketCommand command,
        CancellationToken cancellationToken
    )
    {
        await DeductDiscount(command.Basket, cancellationToken);

        await repository.StoreBasket(command.Basket, cancellationToken);

        return new StoreBasketResult(command.Basket.Username);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(
                new GetDiscountRequest { ProductName = item.ProductName },
                cancellationToken: cancellationToken
            );

            item.Price -= coupon.Amount;
        }
    }
}
