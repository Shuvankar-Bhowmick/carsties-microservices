﻿namespace AuctionService.Entities;

public class Auction
{
    public Guid Id { get; set; } // used as PK of Auction table by EF Core implicitly
    public int ReservePrice { get; set; } = 0;
    public string Seller { get; set; }
    public string Winner { get; set; }
    public int SoldAmount { get; set; }
    public int CurrentHighBid { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime AuctionEnd { get; set; }
    public Status Status { get; set; }
    
    public Item Item { get; set; }  // navigation property
}

/*
    Navigation properties are used to create mappings/relationships between two tables by EF Core. These properties have to be 
    mentioned in both the classes so that EF Core can create the relationship properly. EF Core takes care of these mappings by 
    itself.   
*/