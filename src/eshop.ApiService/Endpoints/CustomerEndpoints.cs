using eshop.ApiService.Models;
using eshop.ApiService.Services;

namespace eshop.ApiService.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customers").WithTags("Customers");

        group.MapGet("/", (CustomerService service) =>
            Results.Ok(service.GetAll()));

        group.MapGet("/{id:guid}", (Guid id, CustomerService service) =>
            service.GetById(id) is Customer customer
                ? Results.Ok(customer)
                : Results.NotFound());

        group.MapPost("/", (Customer customer, CustomerService service) =>
        {
            var created = service.Create(customer);
            return Results.Created($"/api/customers/{created.Id}", created);
        });

        group.MapPut("/{id:guid}", (Guid id, Customer customer, CustomerService service) =>
            service.Update(id, customer) is Customer updated
                ? Results.Ok(updated)
                : Results.NotFound());

        group.MapDelete("/{id:guid}", (Guid id, CustomerService service) =>
            service.Delete(id) ? Results.NoContent() : Results.NotFound());
    }
}
