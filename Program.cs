using BurguerManiaAPI;
using BurguerManiaAPI.categories;
using BurguerManiaAPI.Data;
using BurguerManiaAPI.Orders;
using BurguerManiaAPI.products;
using BurguerManiaAPI.status;
using BurguerManiaAPI.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddCors(options =>{
    options.AddPolicy(name: "myPolicy", policy=>{
        
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("myPolicy");

// config rotas
app.AddRotasUser();
app.AddRotasCategories();
app.AddRotasStatus();
app.AddRotasOrder();
app.AddRotasProduct();
app.Run();

