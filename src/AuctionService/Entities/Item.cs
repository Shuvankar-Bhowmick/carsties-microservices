using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService.Entities;


[Table("Items")]
public class Item
{
    public Guid Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Color { get; set; }
    public int Mileage { get; set; }
    public string ImageUrl { get; set; }

    
    // nav properties for EF Core
    public Auction Auction { get; set; } // navigation property
    public Guid AuctionId { get; set; }  // foreign key property (optional when navigation property is already present)
}