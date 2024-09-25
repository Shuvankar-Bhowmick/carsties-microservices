using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Auction, AuctionResponseDto>().IncludeMembers(x => x.Item);
        CreateMap<Item, AuctionResponseDto>();
        CreateMap<CreateAuctionRequestDto, Auction>()
            .ForMember(
                x => x.Item,
                opt => opt.MapFrom(x => x));
        CreateMap<CreateAuctionRequestDto, Item>();
    }
}