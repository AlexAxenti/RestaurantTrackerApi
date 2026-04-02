using Microsoft.EntityFrameworkCore;
using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<RestaurantEntry> RestaurantEntries { get; set; }
}