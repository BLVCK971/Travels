namespace Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingTicket;

public class BoardingCardListVm
{
    public string Reference { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string TransportType { get; set; } = string.Empty;
    public string? Seat { get; set; } = string.Empty;
    public string? Gate { get; set; } = string.Empty;
    public string? Baggage { get; set; } = string.Empty;
}
