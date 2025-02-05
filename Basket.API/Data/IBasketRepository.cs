using Basket.API.Models;

namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string username);
    Task<ShoppingCart> StoreBasket(ShoppingCart basket);
    Task<bool> DeleteBasket(string username);
}
