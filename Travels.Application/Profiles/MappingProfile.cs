using AutoMapper;
using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingTicket;
using Travels.Domain.Entities;

namespace Travel.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BoardingCard, BoardingCardListVm>().ReverseMap();
    }
}
