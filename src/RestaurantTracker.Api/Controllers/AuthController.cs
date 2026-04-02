using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Entities;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using RestaurantTracker.Api.Data;

namespace RestaurantTracker.Api.Controllers;

//TODO create auth service to go along with this controller
//TODO return custom error msgs
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        ApplicationDbContext dbContext
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _dbContext = dbContext;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            return BadRequest(new
            {
                message = "A user with that email already exists."
            });
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                errors = result.Errors.Select(e => e.Description)
            });
        }

        var authResponse = await CreateAuthResponseAsync(user);

        return Ok(authResponse);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            request.Password,
            lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        var authResponse = await CreateAuthResponseAsync(user);

        return Ok(authResponse);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        var refreshTokenHash = HashToken(request.RefreshToken);

        var existingRefreshToken = await _dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.TokenHash == refreshTokenHash);
    
        if (existingRefreshToken is null)
        {
            return Unauthorized(new
            {
                message = "Invalid refresh token."
            });
        }

        if (existingRefreshToken.RevokedAtUtc is not null)
        {
            return Unauthorized(new
            {
                message = "Refresh token has been revoked."
            });
        }

        if (existingRefreshToken.ExpiresAtUtc <= DateTime.UtcNow)
        {
            return Unauthorized(new
            {
                message = "Refresh token has expired."
            });
        }

        existingRefreshToken.RevokedAtUtc = DateTime.UtcNow;

        var (newRefreshToken, refreshTokenValue) = BuildRefreshToken(existingRefreshToken.User);

        existingRefreshToken.ReplacedByTokenHash = newRefreshToken.TokenHash;

        _dbContext.RefreshTokens.Add(newRefreshToken);

        var (accessToken, expiresAtUtc) = GenerateAccessToken(existingRefreshToken.User);

        try
        {
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            await transaction.RollbackAsync();
            return Unauthorized(new { message = "Refresh token has already been used." });
        }

        return Ok(new AuthResponse(
            accessToken,
            refreshTokenValue,
            expiresAtUtc
        ));
    }

    //TODO move to service?
    private (string AccessToken, DateTime ExpiresAtUtc) GenerateAccessToken(ApplicationUser user)
    {
        var secret = _configuration["Jwt:Secret"]
            ?? throw new InvalidOperationException("JWT secret is not configured.");

        var issuer = _configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException("JWT issuer is not configured.");

        var audience = _configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("JWT audience is not configured.");

        var expiryMinutes = int.Parse(_configuration["Jwt:ExpiryMinutes"] ?? "60");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresAtUtc = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: credentials);

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAtUtc);
    }

    private async Task<string> CreateAndSaveRefreshTokenAsync(ApplicationUser user)
    {
        var (refreshToken, refreshTokenValue) = BuildRefreshToken(user);

        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync();

        return refreshTokenValue;
    }

    private async Task<AuthResponse> CreateAuthResponseAsync(ApplicationUser user)
    {
        var (accessToken, expiresAtUtc) = GenerateAccessToken(user);
        var refreshTokenValue = await CreateAndSaveRefreshTokenAsync(user);

        return new AuthResponse(
            accessToken,
            refreshTokenValue,
            expiresAtUtc
        );
    }

    private (RefreshToken RefreshToken, string RefreshTokenValue) BuildRefreshToken(ApplicationUser user)
    {
        var refreshTokenDays = int.Parse(_configuration["Jwt:RefreshTokenDays"] ?? "30");
        var refreshTokenValue = GenerateRefreshTokenValue();

        return (
            new RefreshToken
            {
                Id = Guid.NewGuid(),
                TokenHash = HashToken(refreshTokenValue),
                UserId = user.Id,
                CreatedAtUtc = DateTime.UtcNow,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(refreshTokenDays)
            },
            refreshTokenValue);
    }

    private static string GenerateRefreshTokenValue()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    private static string HashToken(string token)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(bytes);
    }
}