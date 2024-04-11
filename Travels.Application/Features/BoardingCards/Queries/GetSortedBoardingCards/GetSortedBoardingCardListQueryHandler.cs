using AutoMapper;
using MediatR;
using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingTicket;
using Travels.Domain.Entities;

namespace Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingCards;

public class GetSortedBoardingCardListQueryHandler : IRequestHandler<GetSortedBoardingCardListQuery, SortedBoardingCardListVm>
{
    private readonly IMapper _mapper;

    public GetSortedBoardingCardListQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<SortedBoardingCardListVm> Handle(GetSortedBoardingCardListQuery request, CancellationToken cancellationToken)
    {
        var boardingCardList = _mapper.Map<List<BoardingCard>>(request);

        return new SortedBoardingCardListVm { SortedBoardingCards = boardingCardList };
    }
}