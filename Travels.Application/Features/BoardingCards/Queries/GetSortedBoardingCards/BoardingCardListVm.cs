namespace Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingTicket;

public class BoardingCardListVm
{
    public string departure { get; set; } = string.Empty;
    public string arrival { get; set; } = string.Empty;
    public string transport { get; set; } = string.Empty;
    public string? transportNumber { get; set; } = string.Empty;
    public string? seat { get; set; } = string.Empty;
    public string? gate { get; set; } = string.Empty;
    public string? baggage { get; set; } = string.Empty;
}
