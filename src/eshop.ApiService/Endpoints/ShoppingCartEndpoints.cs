using eshop.ApiService.Models;
using eshop.ApiService.Services;

namespace eshop.ApiService.Endpoints;

public static class ShoppingCartEndpoints
{
    public static void MapShoppingCartEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cart").WithTags("ShoppingCart");

        group.MapGet("/{cartId}", (string cartId, ShoppingCartService service) =>
            Results.Ok(service.GetCart(cartId)));

        group.MapPost("/{cartId}/items", (string cartId, CartItemRequest request, ShoppingCartService cartService, ProductService productService) =>
        {
            var product = productService.GetById(request.ProductId);
            if (product is null) return Results.NotFound("Product not found");

            var updatedCart = cartService.AddItem(cartId, product, request.Quantity);
            return Results.Created($"/api/cart/{cartId}", updatedCart);
        });

        group.MapDelete("/{cartId}/items/{productId:int}", (string cartId, int productId, ShoppingCartService service) =>
        {
            service.RemoveItem(cartId, productId);
            return Results.NoContent();
        });

        group.MapPost("/{cartId}/checkout", (string cartId, CheckoutRequest request, ShoppingCartService cartService, OrderService orderService) =>
        {
            var cart = cartService.GetCart(cartId);
            if (cart.Items.Count == 0)
            {
                return Results.BadRequest("Cart is empty");
            }

            var order = new Order
            {
                CustomerName = request.CustomerName,
                CustomerEmail = request.CustomerEmail,
                Items = cart.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList(),
                TotalAmount = cart.Items.Sum(i => i.UnitPrice * i.Quantity),
                Status = "Pending"
            };

            var createdOrder = orderService.Create(order);
            cartService.ClearCart(cartId);

            return Results.Created($"/api/orders/{createdOrder.Id}", createdOrder);
        });
    }
}
