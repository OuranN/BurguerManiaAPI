
using BurguerManiaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BurguerManiaAPI.status;

public static class StatusRotas{
  public static void AddRotasStatus(this WebApplication app){
    var rotasStatus = app.MapGroup("status").WithTags("Status");
    var rotasStatuss = app.MapGroup("allStatus").WithTags("Status");


    // post
    rotasStatus.MapPost("", async (AddStatusRequest request, AppDbContext context) => {
      var newStatus = new Status(request.Name);
      await context.Status.AddAsync(newStatus);
      await context.SaveChangesAsync();
    });

    //update (put)
    rotasStatus.MapPut("{id}", async (int id, UpdateStatusRequest request, AppDbContext context) => {
      var status = await context.Status.SingleOrDefaultAsync(status=>status.Id== id);

      if(status==null){
        return Results.NotFound();
      }
      status.setName(request.Name);

      await context.SaveChangesAsync();

      var statusReturn = new StatusDto(status.Id, status.Name);

      return Results.Ok(statusReturn);
    });

    //delete

    rotasStatus.MapDelete("{id}", async (int id, AppDbContext context)=>{
      var status = await context.Status.SingleOrDefaultAsync(status => status.Id== id);

      if(status==null){
        return Results.NotFound();
      }

      context.Status.Remove(status); 
      await context.SaveChangesAsync();
       return Results.NoContent();
    });

    //get

    rotasStatus.MapGet("{id}", async (int id, AppDbContext context)=>{
      var status = await context.Status.SingleOrDefaultAsync(status => status.Id== id);

      if(status==null){
        return Results.NotFound();
      }

      var statusReturn = new StatusDto(status.Id, status.Name);

      return Results.Ok(statusReturn);

    });



    // get all
    rotasStatuss.MapGet("",async (AppDbContext context)=>{

      var statuss = await context.Status.Select(status => new StatusDto(status.Id, status.Name)).ToListAsync();
      return statuss;
    });

  }
}