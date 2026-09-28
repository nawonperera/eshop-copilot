using eshop.Web.Models;

namespace eshop.Web.Clients;

public class OrderApiClient(HttpClient httpClient)
{
    public async Task<Order[]> GetAllAsync()
        => await httpClient.GetFromJsonAsync<Order[]>("/api/orders") ?? [];

    public async Task<Order?> GetByIdAsync(int id)
        => await httpClient.GetFromJsonAsync<Order>($"/api/orders/{id}");
}
