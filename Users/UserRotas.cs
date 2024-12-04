
using BurguerManiaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BurguerManiaAPI.Users;

public static class UserRotas{
  public static void AddRotasUser(this WebApplication app){
    var rotasUser = app.MapGroup("user").WithTags("User");
    var rotasUsers = app.MapGroup("users").WithTags("User");


    // post
    rotasUser.MapPost("", async (AddUserRequest request, AppDbContext context) => {
      var newUser = new User(request.Name, request.Email, request.Password);
      await context.User.AddAsync(newUser);
      await context.SaveChangesAsync();
    });

    //update (put)
    rotasUser.MapPut("{id}", async (int id, UpdateUserRequest request, AppDbContext context) => {
      var user = await context.User.SingleOrDefaultAsync(user=>user.Id== id);

      if(user==null){
        return Results.NotFound();
      }
      user.setName(request.Name);
      user.setEmail(request.Email);
      user.setPassword(request.Password);

      await context.SaveChangesAsync();

      var userReturn = new UserDto(user.Id, user.Name, user.Email);

      return Results.Ok(userReturn);
    });

    //delete
    rotasUser.MapDelete("{id}", async (int id, AppDbContext context)=>{
      var user = await context.User.SingleOrDefaultAsync(user => user.Id== id);

      if(user==null){
        return Results.NotFound();
      }

      context.User.Remove(user); 
      await context.SaveChangesAsync();
       return Results.NoContent();
    });

    //get
    rotasUser.MapGet("{id}", async (int id, AppDbContext context)=>{
      var user = await context.User.SingleOrDefaultAsync(user => user.Id== id);

      if(user==null){
        return Results.NotFound();
      }

      var userReturn = new UserDto(user.Id, user.Name, user.Email);

      return Results.Ok(userReturn);

    });

    // get all
    rotasUsers.MapGet("",async (AppDbContext context)=>{

      var users = await context.User.Select(user => new UserDto(user.Id, user.Name, user.Email)).ToListAsync();
      return users;
    });

  }
}