using Travels.Domain.Entities;

namespace Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingCards;

public class SortedBoardingCardListVm
{
    public ICollection<string>? Sentences { get; set; }
    public ICollection<BoardingCard>? SortedBoardingCards { get; set; }
}
