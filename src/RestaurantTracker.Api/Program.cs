using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantTracker.Api.Data;
using RestaurantTracker.Api.Endpoints;
using RestaurantTracker.Api.Entities;
using RestaurantTracker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services
    .AddIdentityCore<ApplicationUser>(options => 
    {
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddSignInManager()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
builder.Services.AddScoped<IRestaurantEntryService, RestaurantEntryService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "RestaurantTracker API v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

SearchEndpoint.Map(app);

app.Run();