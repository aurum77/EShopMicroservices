﻿using Basket.API.Data;
using Basket.API.Models;

namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart ShoppingCart);

internal class GetBasketQueryHandler(IBasketRepository repository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(
        GetBasketQuery query,
        CancellationToken cancellationToken
    )
    {
        var basket = await repository.GetBasket(query.Username, cancellationToken);

        return new GetBasketResult(basket);
    }
}
