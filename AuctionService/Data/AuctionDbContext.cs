using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class AuctionDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Auction> Auctions { get; set; }
    // Don't need to specify the Item db-set as it is already related to Auctions hence EF Core will
    // automatically create db-set for it as well
}

