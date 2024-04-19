using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Exceptions;
using Travels.Domain.Entities;

namespace Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingCards;

class BoardingCardListConverter
{
    public static List<BoardingCard> SortingBoardingCard(List<BoardingCard> initialBoardingCardList)
    {
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
        return newBoardingCardList;
    }


    public static List<string> NaturalLangageConverter(List<BoardingCard> boardingCardList)
    {
        List<string> sentences = new List<string>();

        for (int i = 0; i < boardingCardList.Count; i++)
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
