using System;
using System.Collections.Generic;

public static class MockRestaurants
{
    public static readonly List<Restaurant> Restaurants = new List<Restaurant>
    {
        new Restaurant
        {
            Id = 1,
            GooglePlaceId = "ChIJ1_J2K9K1K4gR1K8h7s8Fh3A",
            Name = "Jack Astor's Square One",
            FormattedAddress = "100 City Centre Dr, Mississauga, ON L5B 2C9, Canada",
            City = "Mississauga",
            Region = "ON",
            Country = "Canada",
            PostalCode = "L5B 2C9",
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-10),
            LastSynced = DateTimeOffset.UtcNow.AddDays(-1)
        },
        new Restaurant
        {
            Id = 2,
            GooglePlaceId = "ChIJ8d7z1yHL1IkR5K8h7s8Fh3B",
            Name = "Burger Priest Burlington",
            FormattedAddress = "2000 Appleby Line, Burlington, ON L7L 6M6, Canada",
            City = "Burlington",
            Region = "ON",
            Country = "Canada",
            PostalCode = "L7L 6M6",
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-8),
            LastSynced = DateTimeOffset.UtcNow.AddDays(-2)
        },
        new Restaurant
        {
            Id = 3,
            GooglePlaceId = "ChIJ9z7y2yHL1IkR9K8h7s8Fh3C",
            Name = "Sushi Masayuki",
            FormattedAddress = "60 Lakeshore Rd E, Mississauga, ON L5G 1E1, Canada",
            City = "Mississauga",
            Region = "ON",
            Country = "Canada",
            PostalCode = "L5G 1E1",
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-5),
            LastSynced = DateTimeOffset.UtcNow.AddDays(-1)
        }
    };
}

