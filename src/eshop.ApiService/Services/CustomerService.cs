using eshop.ApiService.Models;

namespace eshop.ApiService.Services;

public class CustomerService
{
    private readonly List<Customer> _items = [
        new Customer { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "555-0100" },
        new Customer { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "555-0101" }
    ];

    public IEnumerable<Customer> GetAll() => _items;

    public Customer? GetById(Guid id) => _items.FirstOrDefault(x => x.Id == id);

    public Customer Create(Customer item)
    {
        item.Id = Guid.NewGuid();
        _items.Add(item);
        return item;
    }

    public Customer? Update(Guid id, Customer item)
    {
        var existing = GetById(id);
        if (existing is null)
            return null;

        existing.FirstName = item.FirstName;
        existing.LastName = item.LastName;
        existing.Email = item.Email;
        existing.PhoneNumber = item.PhoneNumber;
        
        return existing;
    }

    public bool Delete(Guid id)
    {
        var existing = GetById(id);
        if (existing is null)
            return false;

        _items.Remove(existing);
        return true;
    }
}
