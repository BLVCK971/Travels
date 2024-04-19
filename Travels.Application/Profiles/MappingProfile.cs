using AutoMapper;
using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingCards;
using Travels.Domain.Entities;

namespace Travels.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BoardingCard, BoardingCardListVm>().ReverseMap();
    }
}
