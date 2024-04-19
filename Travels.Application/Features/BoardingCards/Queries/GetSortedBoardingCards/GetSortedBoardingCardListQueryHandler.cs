using AutoMapper;
using MediatR;
using Travels.Application.Exceptions;
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
        List<BoardingCard> initialBoardingCardList = _mapper.Map<List<BoardingCard>>(request.boardingCardListVms);

        List<BoardingCard> newBoardingCardList = BoardingCardListConverter.SortingBoardingCard(initialBoardingCardList);

        List<string> sentences = BoardingCardListConverter.NaturalLangageConverter(newBoardingCardList);
        List<BoardingCardListVm> sortedBoardingCardList = _mapper.Map<List<BoardingCardListVm>>(newBoardingCardList);

        return new SortedBoardingCardListVm { sentences = sentences, sortedBoardingCards = sortedBoardingCardList };
    }
}