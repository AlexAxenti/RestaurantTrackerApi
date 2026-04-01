using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Data;

public static class MockUsers
{
    public static readonly List<User> Users = new List<User>
    {
        new User
        {
            Id = 1,
            Name = "Alice Johnson",
            Email = "alice@example.com",
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-30),
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-30)
        },
        new User
        {
            Id = 2,
            Name = "Bob Smith",
            Email = "bob@example.com",
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-20),
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-20)
        }
    };
}
