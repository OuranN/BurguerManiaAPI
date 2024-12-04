
using BurguerManiaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BurguerManiaAPI.categories;

public static class CategoriesRotas{
  public static void AddRotasCategories(this WebApplication app){
    var rotasCategorie = app.MapGroup("category").WithTags("Categoria");
    var rotasCategories = app.MapGroup("categories").WithTags("Categoria");


    // post
    rotasCategorie.MapPost("", async (AddCategoriesRequest request, AppDbContext context) => {
      var newCategories = new Categories(request.Name, request.Description, request.Path_image);
      await context.Categories.AddAsync(newCategories);
      await context.SaveChangesAsync();
    });

    //update (put)
    rotasCategorie.MapPut("{id}", async (int id, UpdateCategoriesRequest request, AppDbContext context) => {
      var categories = await context.Categories.SingleOrDefaultAsync(categories=>categories.Id== id);

      if(categories==null){
        return Results.NotFound();
      }
      categories.SetName(request.Name);
      categories.SetDescription(request.Description);
      categories.SetPathImage(request.Path_image);

      await context.SaveChangesAsync();

      var categoriesReturn = new CategoriesDto(categories.Id, categories.Name, categories.Description, categories.PathImage);

      return Results.Ok(categoriesReturn);
    });

    //delete

    rotasCategorie.MapDelete("{id}", async (int id, AppDbContext context)=>{
      var categories = await context.Categories.SingleOrDefaultAsync(categories => categories.Id== id);

      if(categories==null){
        return Results.NotFound();
      }

      context.Categories.Remove(categories); 
      await context.SaveChangesAsync();
       return Results.NoContent();
    });

    //get

    rotasCategorie.MapGet("{id}", async (int id, AppDbContext context)=>{
      var categories = await context.Categories.SingleOrDefaultAsync(categories => categories.Id== id);

      if(categories==null){
        return Results.NotFound();
      }

      var categoriesReturn = new CategoriesDto(categories.Id, categories.Name, categories.Description, categories.PathImage);

      return Results.Ok(categoriesReturn);

    });

    // get all
    rotasCategories.MapGet("",async (AppDbContext context)=>{

      var categories = await context.Categories.Select(categories => new CategoriesDto(categories.Id, categories.Name, categories.Description, categories.PathImage)).ToListAsync();
      return categories;
    });

  }
}