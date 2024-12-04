using BurguerManiaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BurguerManiaAPI.products;

public static class ProductRotas
{
    public static void AddRotasProduct(this WebApplication app)
    {
        var rotasProduct = app.MapGroup("product").WithTags("Product");
        var rotasProducts = app.MapGroup("products").WithTags("Product");

        // POST
        rotasProduct.MapPost("", async (AddProductRequest request, AppDbContext context) =>
        {
            var newProduct = new Product(
                request.Name,
                request.PathImage,
                request.Price,
                request.BaseDescription,
                request.FullDescription,
                request.CategoryId
            );
            await context.Product.AddAsync(newProduct);
            await context.SaveChangesAsync();

            return Results.Created($"/product/{newProduct.Id}", new ProductDto(
                newProduct.Id,
                newProduct.Name,
                newProduct.PathImage,
                newProduct.Price,
                newProduct.BaseDescription,
                newProduct.FullDescription,
                newProduct.CategoryId
            ));
        });

        // PUT
        rotasProduct.MapPut("{id:int}", async (int id, UpdateProductRequest request, AppDbContext context) =>
        {
            var product = await context.Product.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return Results.NotFound();

            product.SetName(request.Name);
            product.SetPathImage(request.PathImage);
            product.SetPrice(request.Price);
            product.SetBaseDescription(request.BaseDescription);
            product.SetFullDescription(request.FullDescription);
            product.SetCategoryId(request.CategoryId);

            await context.SaveChangesAsync();

            return Results.Ok(new ProductDto(
                product.Id,
                product.Name,
                product.PathImage,
                product.Price,
                product.BaseDescription,
                product.FullDescription,
                product.CategoryId
            ));
        });

        // DELETE
        rotasProduct.MapDelete("{id:int}", async (int id, AppDbContext context) =>
        {
            var product = await context.Product.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return Results.NotFound();

            context.Product.Remove(product);
            await context.SaveChangesAsync();

            return Results.NoContent();
        });

        // GET por ID
        rotasProduct.MapGet("{id:int}", async (int id, AppDbContext context) =>
        {
            var product = await context.Product.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return Results.NotFound();

            return Results.Ok(new ProductDto(
                product.Id,
                product.Name,
                product.PathImage,
                product.Price,
                product.BaseDescription,
                product.FullDescription,
                product.CategoryId
            ));
        });

        // GET todos os produtos
        rotasProducts.MapGet("", async (AppDbContext context) =>
        {
            var products = await context.Product
                .Select(p => new ProductDto(
                    p.Id,
                    p.Name,
                    p.PathImage,
                    p.Price,
                    p.BaseDescription,
                    p.FullDescription,
                    p.CategoryId
                )).ToListAsync();

            return Results.Ok(products);
        });
    }
}
