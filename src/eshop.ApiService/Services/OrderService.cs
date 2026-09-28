using eshop.ApiService.Models;

namespace eshop.ApiService.Services;

public class OrderService
{
    private readonly List<Order> _orders =
    [
        new()
        {
            Id = 1,
            CustomerName = "Alice Johnson",
            CustomerEmail = "alice@example.com",
            Items =
            [
                new() { ProductId = 1, ProductName = "Wireless Noise-Cancelling Headphones", Quantity = 1, UnitPrice = 249.99m },
                new() { ProductId = 5, ProductName = "Portable SSD 1TB", Quantity = 2, UnitPrice = 99.99m },
            ],
            TotalAmount = 449.97m,
            Status = "Processing",
            CreatedAt = new DateTime(2026, 8, 1, 9, 0, 0, DateTimeKind.Utc),
        },
        new()
        {
            Id = 2,
            CustomerName = "Bob Smith",
            CustomerEmail = "bob@example.com",
            Items =
            [
                new() { ProductId = 3, ProductName = "4K Ultra HD Monitor", Quantity = 1, UnitPrice = 549.99m },
            ],
            TotalAmount = 549.99m,
            Status = "Shipped",
            CreatedAt = new DateTime(2026, 8, 3, 14, 30, 0, DateTimeKind.Utc),
        },
    ];

    private int _nextId = 3;

    public IEnumerable<Order> GetAll() => _orders;

    public Order? GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);

    public Order Create(Order order)
    {
        order.Id = _nextId++;
        order.CreatedAt = DateTime.UtcNow;
        _orders.Add(order);
        return order;
    }

    public Order? Update(int id, Order updated)
    {
        var existing = GetById(id);
        if (existing is null) return null;

        existing.CustomerName = updated.CustomerName;
        existing.CustomerEmail = updated.CustomerEmail;
        existing.Items = updated.Items;
        existing.TotalAmount = updated.TotalAmount;
        existing.Status = updated.Status;
        return existing;
    }

    public bool Delete(int id)
    {
        var order = GetById(id);
        if (order is null) return false;

        _orders.Remove(order);
        return true;
    }
}