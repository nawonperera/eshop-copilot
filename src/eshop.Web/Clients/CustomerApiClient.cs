using eshop.Web.Models;

namespace eshop.Web.Clients;

public class CustomerApiClient(HttpClient httpClient)
{
    public async Task<Customer[]> GetAllAsync() 
        => await httpClient.GetFromJsonAsync<Customer[]>("/api/customers") ?? [];
        
    public async Task<Customer?> GetByIdAsync(Guid id)
        => await httpClient.GetFromJsonAsync<Customer>($"/api/customers/{id}");
        
    public async Task<Customer?> CreateAsync(Customer customer)
    {
        var response = await httpClient.PostAsJsonAsync("/api/customers", customer);
        return await response.Content.ReadFromJsonAsync<Customer>();
    }
    
    public async Task<Customer?> UpdateAsync(Guid id, Customer customer)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/customers/{id}", customer);
        return await response.Content.ReadFromJsonAsync<Customer>();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await httpClient.DeleteAsync($"/api/customers/{id}");
    }
}
