using Microsoft.AspNetCore.Identity;

namespace RestaurantTracker.Api.Entities;

public class ApplicationUser : IdentityUser
{
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}