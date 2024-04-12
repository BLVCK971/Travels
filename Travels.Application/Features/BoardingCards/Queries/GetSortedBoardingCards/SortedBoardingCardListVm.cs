using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingTicket;
using Travels.Domain.Entities;

namespace Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingCards;

public class SortedBoardingCardListVm
{
    public IList<string>? sentences { get; set; }
    public List<BoardingCardListVm>? sortedBoardingCards { get; set; }
}
