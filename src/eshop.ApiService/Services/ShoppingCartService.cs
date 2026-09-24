using eshop.ApiService.Models;

namespace eshop.ApiService.Services;

public class ShoppingCartService
{
    private readonly Dictionary<string, ShoppingCart> _carts = [];

    public ShoppingCart GetCart(string cartId)
    {
        if (!_carts.TryGetValue(cartId, out var cart))
        {
            cart = new ShoppingCart { Id = cartId };
            _carts[cartId] = cart;
        }
        return cart;
    }

    public ShoppingCart AddItem(string cartId, Product product, int quantity = 1)
    {
        var cart = GetCart(cartId);
        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == product.Id);

        if (existingItem is not null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = quantity
            });
        }
        return cart;
    }

    public ShoppingCart RemoveItem(string cartId, int productId)
    {
        var cart = GetCart(cartId);
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
        {
            cart.Items.Remove(item);
        }
        return cart;
    }

    public void ClearCart(string cartId)
    {
        if (_carts.ContainsKey(cartId))
        {
            _carts[cartId].Items.Clear();
        }
    }
}
