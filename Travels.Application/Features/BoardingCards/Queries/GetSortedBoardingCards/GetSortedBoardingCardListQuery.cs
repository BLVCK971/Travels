using MediatR;

namespace Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingCards;

public class GetSortedBoardingCardListQuery : IRequest<SortedBoardingCardListVm>
{
    public List<BoardingCardListVm>? boardingCardListVms;
}
