namespace eshop.ApiService.Models;

public class ShoppingCart
{
    public string Id { get; set; } = string.Empty;
    public List<CartItem> Items { get; set; } = [];
}

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}

public record CartItemRequest(int ProductId, int Quantity);
public record CheckoutRequest(string CustomerName, string CustomerEmail);
