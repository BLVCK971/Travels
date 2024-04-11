using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingTicket;

namespace Travels.API.IntegrationTests.Base;

public class Utilities
{
    public static List<BoardingCardListVm> RandomizeBoardingCardList(List<BoardingCardListVm> listToShuffle)
    {
        Random rand = new Random();

        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            var k = rand.Next(i + 1);
            var value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[i];
            listToShuffle[i] = value;
        }
        return listToShuffle;
    }
}
