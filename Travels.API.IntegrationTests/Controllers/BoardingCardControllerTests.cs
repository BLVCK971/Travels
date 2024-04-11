using Travels.API.IntegrationTests.Base;
using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingTicket;

namespace Travels.API.IntegrationTests.Controllers;

public class BoardingCardControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public BoardingCardControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    //[Fact]
    //public async Task TechnicalTest()
    //{
    //    var input = new List<BoardingCardListVm>
    //    {
    //        new BoardingCardListVm { Departure = "Madrid", Arrival = "Barcelona", Transport = "Train", TransportNumber = "78A", Seat = "45B" },
    //        new BoardingCardListVm { Departure = "Barcelona", Arrival = "Gerona Airport", Transport = "Airport Bus" },
    //        new BoardingCardListVm { Departure = "Gerona Airport", Arrival = "Stockholm", Transport = "Flight", TransportNumber = "SK455", Seat = "3A", Gate = "45B", Baggage = "344" },
    //        new BoardingCardListVm { Departure = "Stockholm", Arrival = "New York", Transport = "Flight", TransportNumber = "SK22", Seat = "7B", Gate = "22" }
    //    };

    //    var client = _factory.GetAnonymousClient();

    //    var response = await client.GetAsync("/api/category/all");

    //    response.EnsureSuccessStatusCode();

    //    var responseString = await response.Content.ReadAsStringAsync();

    //    var result = JsonSerializer.Deserialize<List<CategoryListVm>>(responseString);

    //    Assert.IsType<List<CategoryListVm>>(result);
    //    Assert.NotEmpty(result);
    //}
}
