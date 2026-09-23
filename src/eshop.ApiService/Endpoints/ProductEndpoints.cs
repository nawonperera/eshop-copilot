using eshop.ApiService.Models;
using eshop.ApiService.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eshop.ApiService.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products");

        group.MapGet("/", GetAllProducts);
        group.MapGet("/{id:int}", GetProductById);
        group.MapPost("/", CreateProduct);
        group.MapPut("/{id:int}", UpdateProduct);
        group.MapDelete("/{id:int}", DeleteProduct);

        return app;
    }

    private static Ok<IEnumerable<Product>> GetAllProducts(ProductService service)
    {
        return TypedResults.Ok(service.GetAll());
    }

    private static Results<Ok<Product>, NotFound> GetProductById(int id, ProductService service)
    {
        var product = service.GetById(id);
        return product is not null ? TypedResults.Ok(product) : TypedResults.NotFound();
    }

    private static Created<Product> CreateProduct(Product product, ProductService service)
    {
        var created = service.Create(product);
        return TypedResults.Created($"/api/products/{created.Id}", created);
    }

    private static Results<Ok<Product>, NotFound> UpdateProduct(int id, Product product, ProductService service)
    {
        var updated = service.Update(id, product);
        return updated is not null ? TypedResults.Ok(updated) : TypedResults.NotFound();
    }

    private static Results<NoContent, NotFound> DeleteProduct(int id, ProductService service)
    {
        var deleted = service.Delete(id);
        return deleted ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}
