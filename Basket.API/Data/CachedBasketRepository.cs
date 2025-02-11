﻿using System.Text.Json;
using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken)
    {
        var cachedBasket = await cache.GetStringAsync(username, cancellationToken);

        if (!string.IsNullOrEmpty(cachedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        }

        var basket = await repository.GetBasket(username, cancellationToken);
        await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(
        ShoppingCart basket,
        CancellationToken cancellationToken
    )
    {
        await repository.StoreBasket(basket, cancellationToken);
        await cache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket));
        return basket;
    }

    public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken)
    {
        await repository.DeleteBasket(username, cancellationToken);
        await cache.RemoveAsync(username, cancellationToken);
        return true;
    }
}
