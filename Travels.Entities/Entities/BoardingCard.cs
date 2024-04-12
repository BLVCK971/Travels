namespace Travels.Domain.Entities;

public class BoardingCard
{

    public string Departure { get; set; } = string.Empty;
    public string Arrival { get; set; } = string.Empty;
    public string Transport { get; set; } = string.Empty;
    public string? TransportNumber { get; set; } = string.Empty;
    public string? Seat { get; set; } = string.Empty;
    public string? Gate { get; set; } = string.Empty;
    public string? Baggage { get; set; } = string.Empty;
}
