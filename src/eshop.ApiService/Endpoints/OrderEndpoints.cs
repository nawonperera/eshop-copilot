using eshop.ApiService.Models;
using eshop.ApiService.Services;

namespace eshop.ApiService.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders").WithTags("Orders");

        group.MapGet("/", (OrderService service) =>
            Results.Ok(service.GetAll()));

        group.MapGet("/{id:int}", (int id, OrderService service) =>
            service.GetById(id) is Order order
                ? Results.Ok(order)
                : Results.NotFound());

        group.MapPost("/", (Order order, OrderService service) =>
        {
            var created = service.Create(order);
            return Results.Created($"/api/orders/{created.Id}", created);
        });

        group.MapPut("/{id:int}", (int id, Order order, OrderService service) =>
            service.Update(id, order) is Order updated
                ? Results.Ok(updated)
                : Results.NotFound());

        group.MapDelete("/{id:int}", (int id, OrderService service) =>
            service.Delete(id) ? Results.NoContent() : Results.NotFound());
    }
}