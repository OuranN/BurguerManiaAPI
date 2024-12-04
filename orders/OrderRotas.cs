using BurguerManiaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BurguerManiaAPI.Orders;

public static class OrderRotas
{
    public static void AddRotasOrder(this WebApplication app)
    {
        var rotasOrder = app.MapGroup("order").WithTags("Order");
        var rotasOrders = app.MapGroup("orders").WithTags("Order");

        // POST
        rotasOrder.MapPost("", async (AddOrderRequest request, AppDbContext context) =>
        {
            var newOrder = new Order(request.StatusId, request.Value);
            await context.Orders.AddAsync(newOrder);
            await context.SaveChangesAsync();
        });

        // PUT (Update)
        rotasOrder.MapPut("{id}", async (int id, UpdateOrderRequest request, AppDbContext context) =>
        {
            var order = await context.Orders.SingleOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return Results.NotFound();

            order.UpdateStatus(request.StatusId);
            order.UpdateValue(request.Value);

            await context.SaveChangesAsync();

            var orderReturn = new OrderDto(order.Id, order.StatusId, order.Value);
            return Results.Ok(orderReturn);
        });

        // DELETE
        rotasOrder.MapDelete("{id}", async (int id, AppDbContext context) =>
        {
            var order = await context.Orders.SingleOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return Results.NotFound();

            context.Orders.Remove(order);
            await context.SaveChangesAsync();
            return Results.NoContent();
        });

        // GET (By ID)
        rotasOrder.MapGet("{id}", async (int id, AppDbContext context) =>
        {
            var order = await context.Orders
                .Include(o => o.Status) 
                .SingleOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return Results.NotFound();

            var orderReturn = new OrderDto(order.Id, order.StatusId, order.Value);
            return Results.Ok(orderReturn);
        });

        // GET (All)
        rotasOrders.MapGet("", async (AppDbContext context) =>
        {
            var orders = await context.Orders
                .Select(o => new OrderDto(o.Id, o.StatusId, o.Value))
                .ToListAsync();

            return orders;
        });
    }
}