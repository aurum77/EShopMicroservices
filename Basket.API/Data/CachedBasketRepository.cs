using System.Text.Json;
using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken)
    {
        var cachedBasket = await cache.GetStringAsync(username, cancellationToken);

        if (!string.IsNullOrEmpty(cachedBasket)) 
        { 
            JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
        }

        var basket = await repository.GetBasket(username, cancellationToken);
        await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);

        return cachedBasket ?? ;
    }

    public async Task<ShoppingCart> StoreBasket(
        ShoppingCart basket,
        CancellationToken cancellationToken
    )
    {
        return await repository.StoreBasket(basket, cancellationToken);
    }

    public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken)
    {
        return await repository.DeleteBasket(username, cancellationToken);
    }
}
