using Basket.API.Exceptions;
using Basket.API.Models;

namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username)
    {
        var basket = await session.LoadAsync<ShoppingCart>(username);

        return basket is not null ? basket : throw new BasketNotFoundException(username);
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket)
    {
        session.Store(basket);
        await session.SaveChangesAsync();

        return basket;
    }

    public async Task<bool> DeleteBasket(string username)
    {
        session.Delete<ShoppingCart>(username);
        await session.SaveChangesAsync();

        return true;
    }
}
