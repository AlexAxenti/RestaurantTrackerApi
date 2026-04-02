using Microsoft.EntityFrameworkCore;
using RestaurantTracker.Api.Data;
using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Services;

public class RestaurantEntryService : IRestaurantEntryService
{
    private readonly ApplicationDbContext _context;

    public RestaurantEntryService(ApplicationDbContext context)
    {
        _context = context;
    }

    private static RestaurantEntryResponse ToResponse(RestaurantEntry entry) =>
        new RestaurantEntryResponse(
            entry.Id,
            entry.UserId,
            entry.Status,
            entry.Rating,
            entry.Notes,
            entry.VisitedAt,
            entry.CreatedAt,
            entry.UpdatedAt,
            entry.RestaurantName,
            entry.RestaurantAddress
        );

    //TODO return proper error codes
    public async Task<RestaurantEntryResponse?> GetRestaurantEntryByIdAsync(int id, string userId)
    {
        var entry = await _context.RestaurantEntries.FindAsync(id);
        if (entry == null || entry.UserId != userId) return null;
        return ToResponse(entry);
    }

    public async Task<IEnumerable<RestaurantEntryResponse>> GetRestaurantEntriesByUserIdAsync(string userId, EntryStatus? status = null)
    {
        var entries = await _context.RestaurantEntries
            .Where(e => e.UserId == userId && (!status.HasValue || e.Status == status.Value))
            .ToListAsync();

        return entries.Select(ToResponse);
    }

    public async Task<RestaurantEntryResponse> CreateRestaurantEntryAsync(string userId, CreateRestaurantEntryRequest request)
    {
        var newEntry = new RestaurantEntry
        {
            UserId = userId,
            GooglePlaceId = request.GooglePlaceId,
            RestaurantName = request.RestaurantName,
            RestaurantAddress = request.RestaurantAddress,
            Status = request.Status,
            Rating = request.Rating,
            Notes = request.Notes,
            VisitedAt = request.VisitedAt,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _context.RestaurantEntries.Add(newEntry);
        await _context.SaveChangesAsync();
        return ToResponse(newEntry);
    }

    public async Task<RestaurantEntryResponse?> UpdateRestaurantEntryAsync(int id, string userId, UpdateRestaurantEntryRequest request)
    {
        var entry = await _context.RestaurantEntries.FindAsync(id);
        if (entry == null || entry.UserId != userId) return null;

        if (request.Status.HasValue) entry.Status = request.Status.Value;
        if (request.Rating.HasValue) entry.Rating = request.Rating.Value;
        if (request.Notes != null) entry.Notes = request.Notes;
        if (request.VisitedAt.HasValue) entry.VisitedAt = request.VisitedAt.Value;
        if (request.RestaurantName != null) entry.RestaurantName = request.RestaurantName;
        if (request.RestaurantAddress != null) entry.RestaurantAddress = request.RestaurantAddress;

        entry.UpdatedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();
        return ToResponse(entry);
    }

    public async Task<bool> DeleteRestaurantEntryAsync(int id, string userId)
    {
        var entry = await _context.RestaurantEntries.FindAsync(id);
        if (entry == null || entry.UserId != userId) return false;

        _context.RestaurantEntries.Remove(entry);
        await _context.SaveChangesAsync();
        return true;
    }
}