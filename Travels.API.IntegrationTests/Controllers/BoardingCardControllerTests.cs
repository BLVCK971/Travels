using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json;
using Travels.API.IntegrationTests.Base;
using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingCards;
using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingTicket;

namespace Travels.API.IntegrationTests.Controllers;

public class BoardingCardControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public BoardingCardControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task TechnicalTest()
    {
        var input = new List<BoardingCardListVm>
        {
            new BoardingCardListVm { Departure = "Madrid", Arrival = "Barcelona", Transport = "Train", TransportNumber = "78A", Seat = "45B" },
            new BoardingCardListVm { Departure = "Barcelona", Arrival = "Gerona Airport", Transport = "Airport Bus" },
            new BoardingCardListVm { Departure = "Gerona Airport", Arrival = "Stockholm", Transport = "Flight", TransportNumber = "SK455", Seat = "3A", Gate = "45B", Baggage = "344" },
            new BoardingCardListVm { Departure = "Stockholm", Arrival = "New York", Transport = "Flight", TransportNumber = "SK22", Seat = "7B", Gate = "22" }
        };

        var randomizedInput = Utilities.RandomizeBoardingCardList(input);

        var json = JsonConvert.SerializeObject(input);

        var client = _factory.GetAnonymousClient();

        var request = new HttpRequestMessage(HttpMethod.Get, "/api/boardingcard/sortedboardingcards")
        {
            Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
        };

        var response = await client.SendAsync(request).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = System.Text.Json.JsonSerializer.Deserialize<SortedBoardingCardListVm>(responseString);

        Assert.IsType<SortedBoardingCardListVm>(result);
        Assert.NotNull(result.SortedBoardingCards);
        Assert.NotEmpty(result.SortedBoardingCards);

        Assert.Equal(input.Count, result.SortedBoardingCards.Count);

        Assert.NotNull(result.Sentences);
        Assert.NotEmpty(result.Sentences);

        Assert.Equal(input.Count + 1, result.Sentences.Count);

        Assert.Equal("Take train 78A from Madrid to Barcelona. Sit in seat 45B.", result.Sentences[0]);
        Assert.Equal("Take the airport bus from Barcelona to Gerona Airport. No seat assignment.", result.Sentences[1]);
        Assert.Equal("From Gerona Airport, take flight SK455 to Stockholm. Gate 45B, seat 3A. Baggage drop at ticket counter 344.", result.Sentences[2]);
        Assert.Equal("From Stockholm, take flight SK22 to New York. Gate 22, seat 7B. Baggage will be automatically transferred from your last leg.", result.Sentences[3]);
        Assert.Equal("You have arrived at your final destination.", result.Sentences[4]);

    }
}
