using AutoMapper;
using DeepEqual.Syntax;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using Travel.Application.Profiles;
using Travels.API.IntegrationTests.Base;
using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingCards;
using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingTicket;
using Travels.Domain.Entities;
using Xunit.Sdk;

namespace Travels.API.IntegrationTests.Controllers;

public class BoardingCardControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly IMapper _mapper;
    public BoardingCardControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task TechnicalTest()
    {
        var input = new List<BoardingCard>
        {
            new BoardingCard { Departure = "Madrid", Arrival = "Barcelona", Transport = "Train", TransportNumber = "78A", Seat = "45B" },
            new BoardingCard { Departure = "Barcelona", Arrival = "Gerona Airport", Transport = "Airport Bus" },
            new BoardingCard { Departure = "Gerona Airport", Arrival = "Stockholm", Transport = "Flight", TransportNumber = "SK455", Seat = "3A", Gate = "45B", Baggage = "344" },
            new BoardingCard { Departure = "Stockholm", Arrival = "New York", Transport = "Flight", TransportNumber = "SK22", Seat = "7B", Gate = "22" }
        };

        var vmList = _mapper.Map<List<BoardingCardListVm>>(input);

        var randomizedInput = Utilities.RandomizeBoardingCardList(vmList);

        var json = JsonSerializer.Serialize(randomizedInput);

        var client = _factory.GetAnonymousClient();

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/boardingcard/sortedboardingcards")
        {
            Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
        };

        var response = await client.SendAsync(request).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<SortedBoardingCardListVm>(responseString);

        Assert.IsType<SortedBoardingCardListVm>(result);
        Assert.NotNull(result.sortedBoardingCards);
        Assert.NotEmpty(result.sortedBoardingCards);

        Assert.Equal(input.Count, result.sortedBoardingCards.Count);
        var sortedBoardingCardsInput = _mapper.Map<List<BoardingCard>>(result.sortedBoardingCards);
        Assert.True(input.IsDeepEqual(sortedBoardingCardsInput));

        Assert.NotNull(result.sentences);
        Assert.NotEmpty(result.sentences);

        Assert.Equal(input.Count + 1, result.sentences.Count);

        Assert.Equal("Take train 78A from Madrid to Barcelona. Sit in seat 45B.", result.sentences[0]);
        Assert.Equal("Take the airport bus from Barcelona to Gerona Airport. No seat assignment.", result.sentences[1]);
        Assert.Equal("From Gerona Airport, take flight SK455 to Stockholm. Gate 45B, seat 3A. Baggage drop at ticket counter 344.", result.sentences[2]);
        Assert.Equal("From Stockholm, take flight SK22 to New York. Gate 22, seat 7B. Baggage will be automatically transferred from your last leg.", result.sentences[3]);
        Assert.Equal("You have arrived at your final destination.", result.sentences[4]);
        System.Diagnostics.Debug.WriteLine("Test Finished!");
    }
}
