using AutoMapper;
using MediatR;
using Travels.Application.Exceptions;
using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingTicket;
using Travels.Domain.Entities;

namespace Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingCards;

public class GetSortedBoardingCardListQueryHandler : IRequestHandler<GetSortedBoardingCardListQuery, SortedBoardingCardListVm>
{
    private readonly IMapper _mapper;

    public GetSortedBoardingCardListQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<SortedBoardingCardListVm> Handle(GetSortedBoardingCardListQuery request, CancellationToken cancellationToken)
    {
        List<BoardingCard> initialBoardingCardList = _mapper.Map<List<BoardingCard>>(request.boardingCardListVms);

        BoardingCard? startingPoint = initialBoardingCardList.Find(x => initialBoardingCardList.TrueForAll(y => y.Arrival != x.Departure));
        BoardingCard? endingPoint = initialBoardingCardList.Find(x => initialBoardingCardList.TrueForAll(y => y.Departure != x.Arrival));

        if (startingPoint == null || endingPoint == null)
        {
            throw new NotFoundException(nameof(BoardingCard), "Starting or ending point is not found");
        }

        string endingDestination = endingPoint.Arrival;
        List<BoardingCard> newBoardingCardList = new List<BoardingCard> { startingPoint };

        while (newBoardingCardList[newBoardingCardList.Count - 1].Arrival != endingDestination)
        {
            BoardingCard? nextBoardingCard = initialBoardingCardList.Find(x => x.Departure == newBoardingCardList[newBoardingCardList.Count - 1].Arrival);
            if (nextBoardingCard == null)
            {
                throw new NotFoundException(nameof(BoardingCard), "Next boarding card is not found");
            }

            newBoardingCardList.Add(nextBoardingCard);
        }

        List<string> sentences = NLPBoardingCardListConverter(newBoardingCardList);
        List<BoardingCardListVm> sortedBoardingCardList = _mapper.Map<List<BoardingCardListVm>>(newBoardingCardList);

        return new SortedBoardingCardListVm { sentences = sentences, sortedBoardingCards = sortedBoardingCardList };
    }

    public List<string> NLPBoardingCardListConverter(List<BoardingCard> boardingCardList)
    {
        List<string> sentences = new List<string>();

        for(int i = 0; i < boardingCardList.Count; i++)
        {
            sentences.Add("");
            if (boardingCardList[i].Transport == "Train")
            {
                sentences[i] += "Take train " + boardingCardList[i].TransportNumber;
                sentences[i] += " from " + boardingCardList[i].Departure + " to " + boardingCardList[i].Arrival + ". ";
                sentences[i] += "Sit in seat " + boardingCardList[i].Seat + ".";
            }

            if (boardingCardList[i].Transport == "Airport Bus")
            {
                sentences[i] += "Take the airport bus from " + boardingCardList[i].Departure + " to " + boardingCardList[i].Arrival + ". ";
                sentences[i] += boardingCardList[i].Seat == string.Empty ? "No seat assignment." : "Sit in seat " + boardingCardList[i].Seat + ".";
            }

            if (boardingCardList[i].Transport == "Flight")
            {
                sentences[i] += "From " + boardingCardList[i].Departure + ", take flight " + boardingCardList[i].TransportNumber + " to " + boardingCardList[i].Arrival + ". ";
                sentences[i] += "Gate " + boardingCardList[i].Gate + ", seat " + boardingCardList[i].Seat + ". ";
                sentences[i] += boardingCardList[i].Baggage == string.Empty ? "Baggage will be automatically transferred from your last leg." : "Baggage drop at ticket counter " + boardingCardList[i].Baggage + ".";
            }
        }
        sentences.Add("You have arrived at your final destination.");
        return sentences;
    }
}