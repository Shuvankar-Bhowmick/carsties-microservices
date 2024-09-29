using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/auctions")]
public class AuctionsController(
    AuctionDbContext context,
    IMapper mapper) : ControllerBase
{
    private readonly AuctionDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<List<AuctionResponseDto>>> GetAllAuctions()
    {
        var auctions = await _context.Auctions
            .Include(x => x.Item)
            .OrderBy(x => x.Item.Make)
            .ToListAsync();

        return _mapper.Map<List<AuctionResponseDto>>(auctions);
    }

    [HttpGet("{id:guid}", Name = "GetAuction")]
    public async Task<ActionResult<AuctionResponseDto>> GetAuctionById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var auction = await _context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (auction == null)
            return NotFound();

        return _mapper.Map<AuctionResponseDto>(auction);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionResponseDto>> CreateAuction(
        [FromBody] CreateAuctionRequestDto createAuctionRequestDto,
        CancellationToken cancellationToken)
    {
        var auction = _mapper.Map<Auction>(createAuctionRequestDto);

        // TODO: add current user as seller
        auction.Seller = "test";

        _context.Auctions.Add(auction);

        var result = await _context.SaveChangesAsync(cancellationToken) > 0;

        if (!result)
            return BadRequest("Could not save changes to the DB");

        // need to tell the user that we created resource and also where it was created at
        return CreatedAtAction(nameof(GetAuctionById), 
            new { id = auction.Id }, _mapper.Map<AuctionResponseDto>(auction));
    }
}