using eshop.ApiService.Models;

namespace eshop.ApiService.Services;

public class ProductService
{
    private readonly List<Product> _products;
    private int _nextId;

    public ProductService()
    {
        _products = new List<Product>
        {
            new() { Id = 1, Name = "Wireless Noise-Canceling Headphones", Description = "Premium over-ear headphones with active noise cancellation and 30-hour battery life.", Price = 299.99m, ImageUrl = "https://example.com/images/headphones.jpg" },
            new() { Id = 2, Name = "Mechanical Gaming Keyboard", Description = "RGB backlit mechanical keyboard with tactile switches and programmable macro keys.", Price = 129.50m, ImageUrl = "https://example.com/images/keyboard.jpg" },
            new() { Id = 3, Name = "4K Ultra HD Smart TV", Description = "55-inch 4K OLED Smart TV with HDR10+ and built-in streaming apps.", Price = 899.00m, ImageUrl = "https://example.com/images/tv.jpg" },
            new() { Id = 4, Name = "Ergonomic Office Chair", Description = "High-back mesh office chair with adjustable lumbar support and headrest.", Price = 199.99m, ImageUrl = "https://example.com/images/chair.jpg" },
            new() { Id = 5, Name = "Smartphone Gimbal Stabilizer", Description = "3-axis handheld gimbal stabilizer for smartphones with object tracking.", Price = 89.00m, ImageUrl = "https://example.com/images/gimbal.jpg" }
        };
        _nextId = 6;
    }

    public IEnumerable<Product> GetAll() => _products;

    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public Product Create(Product product)
    {
        product.Id = _nextId++;
        _products.Add(product);
        return product;
    }

    public Product? Update(int id, Product updatedProduct)
    {
        var existing = _products.FirstOrDefault(p => p.Id == id);
        if (existing == null) return null;

        existing.Name = updatedProduct.Name;
        existing.Description = updatedProduct.Description;
        existing.Price = updatedProduct.Price;
        existing.ImageUrl = updatedProduct.ImageUrl;
        
        return existing;
    }

    public bool Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return false;
        
        _products.Remove(product);
        return true;
    }
}
