using AuctionService.Data;
using AuctionService.DTOs;
using AutoMapper;
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
        var auctions = _context.Auctions
            .Include(x => x.Item)
            .OrderBy(x => x.Item.Make)
            .ToListAsync();
        
        return _mapper.Map<List<AuctionResponseDto>>(auctions);
    }

    [HttpGet("{id:guid}", Name = "GetAuction")]
    public async Task<ActionResult<AuctionResponseDto>> GetAuction(Guid id)
    {
        var auction = await _context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (auction == null)
            return NotFound();
        
        return _mapper.Map<AuctionResponseDto>(auction);
    }
}