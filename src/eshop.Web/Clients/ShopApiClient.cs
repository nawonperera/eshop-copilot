using eshop.Web.Models;

namespace eshop.Web.Clients;

public class ShopApiClient(HttpClient httpClient)
{
    public async Task<List<Product>> GetProductsAsync()
    {
        return await httpClient.GetFromJsonAsync<List<Product>>("/api/products") ?? [];
    }

    public async Task<ShoppingCart> GetCartAsync(string cartId)
    {
        return await httpClient.GetFromJsonAsync<ShoppingCart>($"/api/cart/{cartId}") ?? new ShoppingCart { Id = cartId };
    }

    public async Task AddToCartAsync(string cartId, int productId, int quantity = 1)
    {
        var request = new CartItemRequest(productId, quantity);
        await httpClient.PostAsJsonAsync($"/api/cart/{cartId}/items", request);
    }

    public async Task RemoveFromCartAsync(string cartId, int productId)
    {
        await httpClient.DeleteAsync($"/api/cart/{cartId}/items/{productId}");
    }

    public async Task<Order?> CheckoutAsync(string cartId, string customerName, string customerEmail)
    {
        var request = new CheckoutRequest(customerName, customerEmail);
        var response = await httpClient.PostAsJsonAsync($"/api/cart/{cartId}/checkout", request);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Order>();
        }
        
        return null;
    }
}
